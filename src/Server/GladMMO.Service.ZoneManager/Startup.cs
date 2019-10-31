using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Amazon.CloudWatchLogs.Model;
using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Azure.ServiceBus;
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

			RegisterRefitInterfaces(services);

			RegisterAzureServiceQueue(services);

			services.AddCommonLoggingAdapter();

			services.AddHostedService<ExpiredZoneServerCleanupJob>();
			

			services.AddSingleton<TimedJobConfig<ExpiredZoneServerCleanupJob>>(new TimedJobConfig<ExpiredZoneServerCleanupJob>(TimeSpan.FromMinutes(11)));
		}

		private void RegisterAzureServiceQueue(IServiceCollection services)
		{
			string zoneManagerQueueClientAccessKey = Environment.GetEnvironmentVariable(SecurityEnvironmentVariables.AZURE_SERVICE_BUS_API_KEY);

			if (String.IsNullOrWhiteSpace(zoneManagerQueueClientAccessKey))
			{
				throw new InvalidOperationException($"Failed to load Azure Service Bus key from Enviroment Variable: {nameof(SecurityEnvironmentVariables.AZURE_SERVICE_BUS_API_KEY)} VariableName: {SecurityEnvironmentVariables.AZURE_SERVICE_BUS_API_KEY}");
			}

			ServiceBusConnectionStringBuilder serviceHubConnectionBuilder = new ServiceBusConnectionStringBuilder($@"Endpoint=sb://projectvindictive.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey={zoneManagerQueueClientAccessKey};EntityPath=zoneservermanagement");
			//https://projectvindictive.servicebus.windows.net/zoneservermanagement
			//Azure Service Bus register
			IQueueClient zoneManagerServiceQueue = new QueueClient(serviceHubConnectionBuilder);
			services.AddSingleton(zoneManagerServiceQueue);

			services.AddHostedService<StartAzureHttpProxyServiceQueueJob>();

			services.AddTransient<ProxiedHttpRequestAzureServiceQueueManager>();

			//TODO: Find a way to avoid this loopback
			//Now we need to the proxy client stuff.
			services.AddSingleton<IAzureServiceQueueProxiedHttpClient>(provider =>
			{
				var serviceDiscClient = provider.GetService<IServiceDiscoveryService>();

				return new AsyncAzureServiceQueueProxiedClient(QueryForRemoteServiceEndpoint(serviceDiscClient, "ZoneManager"));
			});
		}

		private void RegisterDatabaseServices([NotNull] IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<CharacterDatabaseContext>(o =>
			{
				o.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");
			});

			services.AddTransient<IZoneServerRepository, DatabaseBackedZoneServerRepository>();
			services.AddTransient<ICharacterLocationRepository, DatabaseBackedCharacterLocationRepository>();
			services.AddTransient<ICharacterDataRepository, DatabaseBackedCharacterDataRepository>();
			services.AddTransient<ICharacterSessionRepository, DatabaseBackedCharacterSessionRepository>();
		}

		private void RegisterRefitInterfaces([NotNull] IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddSingleton<IServiceDiscoveryService>(provider => RestService.For<IServiceDiscoveryService>("http://72.190.177.214:5000"));

			services.AddSingleton<IWorldDataServiceClient>(provider =>
			{
				var serviceDiscClient = provider.GetService<IServiceDiscoveryService>();

				return new AsyncWorldDataServiceClient(QueryForRemoteServiceEndpoint(serviceDiscClient, "ContentServer"));
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

		//AutoFac DI/IoC registeration method.
		//See: https://autofaccn.readthedocs.io/en/latest/integration/aspnetcore.html
		public void ConfigureContainer(ContainerBuilder builder)
		{
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
			loggerFactory.AddDebug();

			app.UseMvcWithDefaultRoute();
		}
	}
}
