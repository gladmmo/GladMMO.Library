using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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
					options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(ObjectGuid)));
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

			services.AddTransient<IContentDownloadAuthroizationValidator, UnimplementedContentDownloadAuthorizationValidator>();

			//AZURE_STORAGE_CONNECTIONSTRING
			string ConnectionString = Environment.GetEnvironmentVariable("AZURE_STORAGE_CONNECTIONSTRING");
			if(String.IsNullOrWhiteSpace(ConnectionString))
				throw new InvalidOperationException($"Failed to load AZURE_STORAGE_CONNECTIONSTRING.");

			CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConnectionString);

			services.AddScoped(p => storageAccount.CreateCloudBlobClient());
			services.AddTransient<IStorageUrlBuilder, AzureBlobStorageURLBuilder>();

			//Register all the type converters in the assembly
			services.AddTypeConverters(GetType().Assembly);
		}

		private static void RegisterDatabaseServices(IServiceCollection services)
		{
			services.AddDbContext<ContentDatabaseContext>(o =>
			{
				//Fuck configuration, I'm sick of it and we can't check it into source control
				//so we're using enviroment variables for sensitive deployment specific values.
#if AZURE_RELEASE || AZURE_DEBUG
				try
				{
					o.UseMySql(Environment.GetEnvironmentVariable(GladMMOServiceConstants.CONTENT_DATABASE_CONNECTION_STRING_ENV_VAR_PATH));
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to register Authentication Database. Make sure Env Variable path: {GladMMOServiceConstants.AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH} is correctly configured.", e);
				}
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

			app.UseMvcWithDefaultRoute();
		}
	}
}
