using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GladMMO
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc(options =>
				{
					//This prevents ASP Core from trying to validate Vector3's children, which contain Vector3 (because Unity3D thanks)
					//so it will cause stack overflows. This will avoid it.
					options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Vector3)));
					options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(NetworkEntityGuid)));
					options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(CreatureInstanceModel)));
				})
				.RegisterHealthCheckController();

			RegisterDatabaseServices(services);

			X509Certificate2 cert = null;
			string certPath = "Certs/TestCert.pfx";

			try
			{
				cert = X509Certificate2Loader.Create(certPath).Load();
			}
			catch(Exception e)
			{
				throw new System.InvalidOperationException($"Failed to load {nameof(X509Certificate2)} from Path: {certPath} \n\n StackTrace: {e.StackTrace}", e);
			}

			services.AddResponseCaching();
			services.AddLogging();

			//This provides JwtBearer support for Authorize attribute/header
			services.AddJwtAuthorization(cert);

			//TODO: Simplify avatar/world content repos with base repo.
			//Adds and registers S3 service for URLBuilding and communication/credentials and etc
			//services.AddS3Service(Configuration);
			
			//World
			services.AddTransient<IWorldEntryRepository, DatabaseBackedWorldEntryRepository>();
			services.AddTransient<ICustomContentRepository<WorldEntryModel>>(provider => provider.GetRequiredService<IWorldEntryRepository>());

			//Avatar
			services.AddTransient<IAvatarEntryRepository, DatabaseBackedAvatarEntryRepository>();
			services.AddTransient<ICustomContentRepository<AvatarEntryModel>>(provider => provider.GetRequiredService<IAvatarEntryRepository>());

			//Creature
			services.AddTransient<ICustomContentRepository<CreatureModelEntryModel>, DatabaseBackedCreatureModelEntryRepository>();
			services.AddTransient<ICreatureTemplateRepository, DatabaseBackedCreatureTemplateEntryRepository>();
			services.AddTransient<ICreatureEntryRepository, DatabaseBackedCreatureEntryRepository>();

			//GameObjects
			services.AddTransient<ICustomContentRepository<GameObjectModelEntryModel>, DatabaseBackedGameObjectModelEntryRepository>();
			services.AddTransient<IGameObjectTemplateRepository, DatabaseBackedGameObjectTemplateEntryRepository>();

			services.AddTransient<IContentDownloadAuthroizationValidator, UnimplementedContentDownloadAuthorizationValidator>();

			//AZURE_STORAGE_CONNECTIONSTRING
			string ConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTIONSTRING");
			if(String.IsNullOrWhiteSpace(ConnectionString))
				throw new InvalidOperationException($"Failed to load AZURE_STORAGE_CONNECTIONSTRING.");

			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

			services.AddScoped(p => storageAccount.CreateCloudBlobClient());
			services.AddTransient<IStorageUrlBuilder, AzureBlobStorageURLBuilder>();

			//TODO: Add auto-registeration for type converters
			//CreatureTemplateTableToNetworkTypeConverter : ITypeConverterProvider<CreatureTemplateEntryModel, CreatureTemplateModel>
			//CreatureInstanceTableToNetworkTypeConverter : ITypeConverterProvider<CreatureEntryModel, CreatureInstanceModel>
			services.AddSingleton<ITypeConverterProvider<CreatureTemplateEntryModel, CreatureTemplateModel>, CreatureTemplateTableToNetworkTypeConverter>();
			services.AddSingleton<ITypeConverterProvider<CreatureEntryModel, CreatureInstanceModel>, CreatureInstanceTableToNetworkTypeConverter>();
			//GameObjectTemplateTableToNetworkTypeConverter : ITypeConverterProvider<GameObjectTemplateEntryModel, GameObjectTemplateModel>
			services.AddSingleton<ITypeConverterProvider<GameObjectTemplateEntryModel, GameObjectTemplateModel>, GameObjectTemplateTableToNetworkTypeConverter>();

			//TODO: This doesn't fully convert it
			services.AddSingleton<ITypeConverterProvider<CreatureInstanceModel, CreatureEntryModel>, CreatureInstanceNetworkToTableTypeConverter>();

			//DefaultCreatureEntryModelFactory : IFactoryCreatable<CreatureEntryModel, WorldInstanceableEntryModelCreationContext>
			services.AddTransient<IFactoryCreatable<CreatureEntryModel, WorldInstanceableEntryModelCreationContext>, DefaultCreatureEntryModelFactory>();
		}

		private static void RegisterDatabaseServices(IServiceCollection services)
		{
			services.AddDbContext<ContentDatabaseContext>(o =>
			{
				//On local builds we don't want to use config. We want to default to local
#if !DEBUG_LOCAL && !RELEASE_LOCAL
				throw new NotSupportedException("AWS/Remote database not supported yet.");
				//o.UseMySql(authOptions.Value.AuthenticationDatabaseString);
#else
				o.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");
#endif
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();

			app.UseResponseCaching();
			app.UseAuthentication();

			loggerFactory.RegisterGuardiansLogging(Configuration);
			loggerFactory.AddDebug();

			app.UseMvcWithDefaultRoute();
		}
	}
}
