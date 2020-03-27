﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;

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
			//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.CheckCertificateRevocationList = false;

			services.AddMvc(options =>
				{
					//This prevents ASP Core from trying to validate Vector3's children, which contain Vector3 (because Unity3D thanks)
					//so it will cause stack overflows. This will avoid it.
					//options.ModelMetadataDetailsProviders.Add(new SuppressChildValidationMetadataProvider(typeof(Vector3)));
				})
				.RegisterHealthCheckController();

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

			//This provides JwtBearer support for Authorize attribute/header
			services.AddJwtAuthorization(cert);
			services.AddResponseCaching();

			ISignalRServerBuilder signalRBuilder = services.AddSignalR(options => { }).AddJsonProtocol();

			//TODO: Handle failure.
			//This adds the SignalR rerouting to the specified SignalR backplane.
#if AZURE_RELEASE || AZURE_DEBUG
			signalRBuilder.AddAzureSignalR(Environment.GetEnvironmentVariable(GladMMOServiceConstants.AZURE_SIGNALR_CONNECTION_STRING_ENV_VAR_PATH));
#endif

			services.AddSingleton<IUserIdProvider, SignalRPlayerCharacterUserIdProvider>();

			//TODO: Support release/prod service query.
#if AZURE_RELEASE || AZURE_DEBUG
			services.AddSingleton<IServiceDiscoveryService>(provider => RestService.For<IServiceDiscoveryService>("https://test-guardians-servicediscovery.azurewebsites.net"));
#else
			services.AddSingleton<IServiceDiscoveryService>(provider => RestService.For<IServiceDiscoveryService>("http://72.190.177.214:5000"));
#endif

			services.AddSingleton<IReadonlyAuthTokenRepository, SocialServiceAuthTokenRepository>();

			services.AddSingleton<IAuthenticationService, AsyncEndpointAuthenticationService>(provider =>
			{
				return new AsyncEndpointAuthenticationService(QueryForRemoteServiceEndpoint(provider.GetService<IServiceDiscoveryService>(), "Authentication"),
					new RefitSettings() { HttpMessageHandlerFactory = () => new BypassHttpsValidationHandler() });
			});

			services.AddSingleton<ISocialServiceToGameServiceClient, AsyncEndpointISocialServiceToGameServiceClient>(provider =>
			{
				IReadonlyAuthTokenRepository repository = provider.GetService<IReadonlyAuthTokenRepository>();

				return new AsyncEndpointISocialServiceToGameServiceClient(QueryForRemoteServiceEndpoint(provider.GetService<IServiceDiscoveryService>(), "GameServer"),
					new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(repository) });
			});

			services.AddSingleton<INameQueryService>(provider =>
			{
				IReadonlyAuthTokenRepository repository = provider.GetService<IReadonlyAuthTokenRepository>();

				return new AsyncEndpointNameQueryService(QueryForRemoteServiceEndpoint(provider.GetService<IServiceDiscoveryService>(), "NameQuery"),
					new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(repository) });
			});

			services.AddSingleton<ISocialService>(provider =>
			{
				IReadonlyAuthTokenRepository repository = provider.GetService<IReadonlyAuthTokenRepository>();

				return new AsyncSocialServiceClient(QueryForRemoteServiceEndpoint(provider.GetService<IServiceDiscoveryService>(), GladMMONetworkConstants.SOCIAL_SERVICE_NAME),
					new RefitSettings() { HttpMessageHandlerFactory = () => new AuthenticatedHttpClientHandler(repository) });
			});

			//This is for Hub connection event listeners
			services.AddSingleton<IOnHubConnectionEventListener, CharacterZoneOnHubConnectionEventListener>();
			services.AddSingleton<IOnHubConnectionEventListener, CharacterGuildOnHubConnectionEventListener>();

			//SocialSignalRMessageRouter<TRemoteClientHubInterfaceType> : ISocialModelMessageRouter<TRemoteClientHubInterfaceType>
			services.AddSingleton<ISocialModelMessageRouter<IRemoteSocialHubClient>, SocialSignalRMessageRouter<IRemoteSocialHubClient>>();
			services.AddSingleton<ISocialModelPayloadHandler<IRemoteSocialHubClient>, TestSocialModelHandler>();
			services.AddSingleton<ISocialModelPayloadHandler<IRemoteSocialHubClient>, GuildMemberInviteRequestModelHandler>();
			services.AddSingleton<ISocialModelPayloadHandler<IRemoteSocialHubClient>, PendingGuildInviteResultHandler>();

			RegisterDatabaseServices(services);
		}

		//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
		private bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
		{
			return true;
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

			services.AddTransient<IGuildCharacterMembershipRepository, DatabaseBackedGuildCharacterMembershipRepository>();
			services.AddTransient<ICharacterFriendRepository, DatabaseBackedCharacterFriendRepository>();
		}

		private async Task<string> GetSocialServiceAuthorizationToken([JetBrains.Annotations.NotNull] IAuthenticationService authService)
		{
			if(authService == null) throw new ArgumentNullException(nameof(authService));

			//TODO: Don't hardcode the authentication details
			ProjectVersionStage.AssertBeta();

			//TODO: Handle errors
			return (await authService.TryAuthenticate(new AuthenticationRequestModel(GladMMONetworkConstants.SOCIAL_SERVICE_NAME, "Test69!"))).AccessToken;
		}

		private async Task<string> QueryForRemoteServiceEndpoint(IServiceDiscoveryService serviceDiscovery, string serviceType)
		{
			ResolveServiceEndpointResponse endpointResponse = await serviceDiscovery.DiscoverService(new ResolveServiceEndpointRequest(ClientRegionLocale.US, serviceType));

			if(!endpointResponse.isSuccessful)
				throw new InvalidOperationException($"Failed to query for Service: {serviceType} Result: {endpointResponse.ResultCode}");

			//TODO: Logging
			//Debug.Log($"Recieved service discovery response: {endpointResponse.Endpoint.EndpointAddress}:{endpointResponse.Endpoint.EndpointPort} for Type: {serviceType}");

			//TODO: Do we need extra slash?
			return $"{endpointResponse.Endpoint.EndpointAddress}:{endpointResponse.Endpoint.EndpointPort}/";
		}

		//AutoFac DI/IoC registeration method.
		//See: https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html
		public void ConfigureContainer(ContainerBuilder builder)
		{
			//This enables registeration of IEntityGuidMappables.

			//TODO: Not really thread safe.
			//TODO: Refactor this in shared module, done on client and server for Game library.
			//The below is kinda a hack to register the non-generic types in the
			//removabale collection
			List<IEntityCollectionRemovable> removableComponentsList = new List<IEntityCollectionRemovable>(10);

			builder.RegisterGeneric(typeof(EntityGuidDictionary<>))
				.AsSelf()
				.As(typeof(IReadonlyEntityGuidMappable<>))
				.As(typeof(IEntityGuidMappable<>))
				.OnActivated(args =>
				{
					if(args.Instance is IEntityCollectionRemovable removable)
						removableComponentsList.Add(removable);
				})
				.SingleInstance();

			//This will allow everyone to register the removable collection collection.
			builder.RegisterInstance(removableComponentsList)
				.As<IReadOnlyCollection<IEntityCollectionRemovable>>()
				.As<IEnumerable<IEntityCollectionRemovable>>()
				.AsSelf()
				.SingleInstance();

			//Now we register repository factories
			builder.RegisterGeneric(typeof(RepositoryFactory<>))
				.AsSelf()
				.As(typeof(IRepositoryFactory<>))
				.SingleInstance();
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

			

#if AZURE_RELEASE || AZURE_DEBUG
			app.UseAzureSignalR(builder =>
			{
				builder.MapHub<GeneralSocialSignalRHub>("/realtime/social");
			});
#else
			app.UseSignalR(routes =>
			{
				routes.MapHub<GeneralSocialSignalRHub>("/realtime/social");
			});
#endif
		}
	}
}
