using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.ServiceBus;
using Refit;

namespace GladMMO
{

	//TODO: Consolidate with client version.
	public sealed class ZoneAzureServiceQueueDependencyAutofacModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			//TODO: Renable one day when Unity3D can support netstandard2.0 c
			builder.RegisterType<ProxiedAzureServiceBusHttpClientHandler>()
				.AsSelf()
				.SingleInstance();

			//This is not dangerous to distribute the SAS.
			//The reasoning for this is that zones need to be able to push into the queue anyway.
			//Authorization occurs at the other end so it doesn't matter if people try to do sneaky stuff.
			string zoneManagerQueueClientAccessKey = @"PABbPWzpF+ZSb8YOyGOUUN9RYgnO56mkAOgasXXqE/I="; //this SAS is only for send.

			ServiceBusConnectionStringBuilder serviceHubConnectionBuilder = new ServiceBusConnectionStringBuilder($@"Endpoint=sb://projectvindictive.servicebus.windows.net/;SharedAccessKeyName=UserZoneServer;SharedAccessKey={zoneManagerQueueClientAccessKey};EntityPath=zoneservermanagement");
			//https://projectvindictive.servicebus.windows.net/zoneservermanagement
			//Azure Service Bus register
			IQueueClient zoneManagerServiceQueue = new QueueClient(serviceHubConnectionBuilder);

			builder.RegisterInstance(zoneManagerServiceQueue)
				.As<IQueueClient>()
				.SingleInstance();

			//throw new InvalidOperationException($"TODO: Go disable normal REST for {nameof(IZoneRegistryServiceQueueable)}.");

			builder.Register<IZoneRegistryServiceQueueable>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				ProxiedAzureServiceBusHttpClientHandler handler = context.Resolve<ProxiedAzureServiceBusHttpClientHandler>();

				return new AsyncEndpointZoneRegistryService(QueryForRemoteServiceEndpoint(serviceDiscovery, "ZoneManager"), new RefitSettings(){ HttpMessageHandlerFactory = () => handler });
			});

			builder.Register<IZonePersistenceServiceQueueable>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();
				ProxiedAzureServiceBusHttpClientHandler handler = context.Resolve<ProxiedAzureServiceBusHttpClientHandler>();

				return new AsyncEndpointZonePersistenceService(QueryForRemoteServiceEndpoint(serviceDiscovery, "ZoneManager"), new RefitSettings() { HttpMessageHandlerFactory = () => handler });
			});
		}
	}
}
