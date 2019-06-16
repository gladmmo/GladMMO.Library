using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using UnityEngine;

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

			services.AddSingleton<IZoneInstanceWorkQueue, LocalInMemoryZoneInstanceWorkQueue>(provider =>
			{
				//TODO: This is for testing purposes, we want and have the request to boot up at map 1 as an instance.
				return new LocalInMemoryZoneInstanceWorkQueue(new ZoneInstanceWorkEntry(1), new ZoneInstanceWorkEntry(1));
			});

			RegisterRefitInterfaces(services);
		}

		private static void RegisterDatabaseServices(IServiceCollection services)
		{
			services.AddDbContext<CharacterDatabaseContext>(o =>
			{
				o.UseMySql("Server=192.168.0.3;Database=guardians.gameserver;Uid=root;Pwd=test;");
			});

			services.AddTransient<ICharacterRepository, DatabaseBackedCharacterRepository>();
			services.AddTransient<ICharacterLocationRepository, DatabaseBackedCharacterLocationRepository>();
			services.AddTransient<ICharacterSessionRepository, DatabaseBackedCharacterSessionRepository>();
			services.AddTransient<IZoneServerRepository, DatabaseBackedZoneServerRepository>();
			services.AddTransient<IGuildCharacterMembershipRepository, DatabaseBackedGuildCharacterMembershipRepository>();

			services.AddDbContext<NpcDatabaseContext>(o =>
			{
				//On local builds we don't want to use config. We want to default to local
#if !DEBUG_LOCAL && !RELEASE_LOCAL
				throw new NotSupportedException("AWS/Remote database not supported yet.");
				//o.UseMySql(authOptions.Value.AuthenticationDatabaseString);
#else
				o.UseMySql("Server=72.190.177.214;Database=guardians.gameserver;Uid=root;Pwd=test;");
#endif
			});

			services.AddDbContext<CommonGameDatabaseContext>(o =>
			{
				//On local builds we don't want to use config. We want to default to local
#if !DEBUG_LOCAL && !RELEASE_LOCAL
				throw new NotSupportedException("AWS/Remote database not supported yet.");
				//o.UseMySql(authOptions.Value.AuthenticationDatabaseString);
#else
				o.UseMySql("Server=72.190.177.214;Database=guardians.gameserver;Uid=root;Pwd=test;");
#endif
			});

			services.AddTransient<IWaypointsRepository, DatabaseBackedWaypointsRepository>();
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

		private void RegisterRefitInterfaces([NotNull] IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			//PlayFabId is null for servers.
			services.AddSingleton(new GladMMOPlayFabClientConfiguration(Environment.GetEnvironmentVariable(GladMMOPlayfabConstants.PLAYFAB_SECRET_ENVIROMENT_PATH), null));
			services.AddSingleton<GladMMOPlayFabHttpHandler>();

			services.AddSingleton<IPlayfabCharacterClient>(provider =>
			{
				GladMMOPlayFabHttpHandler handler = provider.GetService<GladMMOPlayFabHttpHandler>();

				//TODO: Let's not hardcode the title id.
				return RestService.For<IPlayfabCharacterClient>($@"https://{63815}.playfabapi.com", new RefitSettings(){ HttpMessageHandlerFactory = () => handler });
			});
		}
	}
}
