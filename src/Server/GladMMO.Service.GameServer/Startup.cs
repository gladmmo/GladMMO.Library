using System; 
using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
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
using InvalidOperationException = System.InvalidOperationException;

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
					options.EnableEndpointRouting = false;
				})
				.AddNewtonsoftJson()
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

			RegisterRefitInterfaces(services);
			services.AddTypeConverters(GetType().Assembly);
		}

		private static void RegisterDatabaseServices(IServiceCollection services)
		{
			services.AddDbContext<CharacterDatabaseContext>(o =>
			{
				//Fuck configuration, I'm sick of it and we can't check it into source control
				//so we're using enviroment variables for sensitive deployment specific values.
#if AZURE_RELEASE || AZURE_DEBUG
				try
				{
					o.UseMySql(Environment.GetEnvironmentVariable(GladMMOServiceConstants.CHARACTER_DATABASE_CONNECTION_STRING_ENV_VAR_PATH));
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to register Authentication Database. Make sure Env Variable path: {GladMMOServiceConstants.AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH} is correctly configured.", e);
				}
#else
				o.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");
#endif
			});

			RegisterTrinityCoreDatabase(services);

			services.AddTransient<ICharacterRepository, DatabaseBackedCharacterRepository>();
			services.AddTransient<ICharacterLocationRepository, DatabaseBackedCharacterLocationRepository>();
			services.AddTransient<ICharacterSessionRepository, DatabaseBackedCharacterSessionRepository>();
			services.AddTransient<IZoneServerRepository, DatabaseBackedZoneServerRepository>();
			services.AddTransient<IGuildCharacterMembershipRepository, DatabaseBackedGuildCharacterMembershipRepository>();
			services.AddTransient<ICharacterAppearanceRepository, DatabaseBackedCharacterAppearanceRepository>();
			services.AddTransient<ICharacterDataRepository, DatabaseBackedCharacterDataRepository>();
			//DatabaseBackedCharacterActionBarRepository
			services.AddTransient<ICharacterActionBarRepository, DatabaseBackedCharacterActionBarRepository>();
			services.AddTransient<ICharacterDefaultActionBarRepository, DatabaseBackedDefaultActionBarRepository>();
		}

		private static void RegisterTrinityCoreDatabase(IServiceCollection services)
		{
			//"server=127.0.0.1;port=3307;user=root;password=test;database=proudmoore_world Timeout=9000"
			services.AddDbContext<wotlk_charactersContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3307;user=root;password=test;database=wotlk_characters"); })
				.AddEntityFrameworkMySql();

			services.AddTransient<ITrinityCharacterRepository, TrinityCoreCharacterRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();

			app.UseResponseCaching();
			app.UseAuthentication();

			//loggerFactory.RegisterGuardiansLogging(Configuration);

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

			//TODO: Support release/prod service query.
#if AZURE_RELEASE || AZURE_DEBUG
			services.AddSingleton<IServiceDiscoveryService>(provider => RestService.For<IServiceDiscoveryService>("https://test-guardians-servicediscovery.azurewebsites.net"));
#else
			services.AddSingleton<IServiceDiscoveryService>(provider => RestService.For<IServiceDiscoveryService>("http://72.190.177.214:5000"));
#endif

			services.AddSingleton<IPlayerSpawnPointDataServiceClient>(provider =>
			{
				var serviceDiscClient = provider.GetService<IServiceDiscoveryService>();

				return new AsyncPlayerSpawnPointDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscClient, "ContentServer"));
			});

			services.AddSingleton<IGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel>>(provider =>
			{
				var serviceDiscClient = provider.GetService<IServiceDiscoveryService>();

				return new AsyncGameObjectBehaviourDataServiceClient<WorldTeleporterInstanceModel>(CreateBehaviourDataEndpointFromServiceEndpoint(QueryForRemoteServiceEndpoint(serviceDiscClient, "ContentServer"), "WorldTeleporterData"));
			});

			services.AddSingleton<IGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel>>(provider =>
			{
				var serviceDiscClient = provider.GetService<IServiceDiscoveryService>();

				return new AsyncGameObjectBehaviourDataServiceClient<AvatarPedestalInstanceModel>(CreateBehaviourDataEndpointFromServiceEndpoint(QueryForRemoteServiceEndpoint(serviceDiscClient, "ContentServer"), "AvatarPedestalData"));
			});
		}

		//TODO: Put this in a base class or something
		protected async Task<string> QueryForRemoteServiceEndpoint(IServiceDiscoveryService serviceDiscovery, string serviceType)
		{
			ResolveServiceEndpointResponse endpointResponse = await serviceDiscovery.DiscoverService(new ResolveServiceEndpointRequest(ClientRegionLocale.US, serviceType));

			if(!endpointResponse.isSuccessful)
				throw new System.InvalidOperationException($"Failed to query for Service: {serviceType} Result: {endpointResponse.ResultCode}");

			//TODO: Do we need extra slash?
			return $"{endpointResponse.Endpoint.EndpointAddress}:{endpointResponse.Endpoint.EndpointPort}/";
		}

		private async Task<string> CreateBehaviourDataEndpointFromServiceEndpoint(Task<string> endpoint, string behaviourNameType)
		{
			string endpointString = await endpoint;

			return $"{endpointString}api/{behaviourNameType}/";
		}
	}
}
