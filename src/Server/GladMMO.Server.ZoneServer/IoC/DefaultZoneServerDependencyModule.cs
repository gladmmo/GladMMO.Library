using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using ProtoBuf;
using Refit;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultZoneServerDependencyModule : Module
	{
		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.CheckCertificateRevocationList = false;

			//TODO: Extract to seperate module like the client.
			//Register the serialization models.
			Unity3DProtobufPayloadRegister unityProtobufRegisteration = new Unity3DProtobufPayloadRegister();
			unityProtobufRegisteration.RegisterDefaults();
			unityProtobufRegisteration.Register();

			//Set the sync context
			UnityAsyncHelper.InitializeSyncContext();

			//TODO: We need to not have such a high rate of Update and need to add prediction.
			Application.targetFrameRate = 60;

			builder.RegisterType<ProtobufNetGladNetSerializerAdapter>()
				.As<INetworkSerializationService>()
				.SingleInstance();

			builder.RegisterType<ZoneServerDefaultRequestHandler>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterInstance(new UnityLogger(LogLevel.All))
				.As<ILog>();

			builder.RegisterType<MessageHandlerService<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>>>()
				.As<MessageHandlerService<GameClientPacketPayload, GameServerPacketPayload, IPeerSessionMessageContext<GameServerPacketPayload>>>()
				.SingleInstance();

			builder.RegisterInstance(new DefaultSessionCollection())
				.As<IRegisterable<int, ZoneClientSession>>()
				.As<ISessionCollection>()
				.SingleInstance();

			//TODO: Make this configurable
			builder.RegisterInstance(new NetworkAddressInfo(IPAddress.Parse("192.168.0.12"), 5006))
				.As<NetworkAddressInfo>()
				.SingleInstance()
				.ExternallyOwned();

			builder.RegisterType<ZoneServerApplicationBase>()
				.SingleInstance()
				.AsSelf();

			RegisterEntityMappableCollections(builder);

			//TODO: We should automate the registeration of message senders
			builder.RegisterType<VisibilityChangeMessageSender>()
				.AsImplementedInterfaces()
				.AsSelf();

			builder.RegisterType<DefaultGameObjectToEntityMappable>()
				.As<IReadonlyGameObjectToEntityMappable>()
				.As<IGameObjectToEntityMappable>()
				.SingleInstance();

			RegisterPlayerFactoryServices(builder);

			builder.RegisterType<GenericMessageSender<PlayerSelfSpawnEventPayload>>()
				.AsSelf()
				.AsImplementedInterfaces();

			builder.RegisterType<DefaultManagedSessionFactory>()
				.AsImplementedInterfaces();

			//TODO: Extract this into a handlers registrar

			//This is for mapping connection IDs to the main controlled EntityGuid.
			builder.RegisterInstance(new ConnectionEntityMap())
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<PlayerEntityGuidEnumerable>()
				.As<IPlayerEntityGuidEnumerable>()
				.AsSelf();

			builder.RegisterType<MovementUpdateMessageSender>()
				.As<INetworkMessageSender<EntityMovementMessageContext>>()
				.AsSelf();

			//Keep this one here, zoneserver needs it.
			builder.Register<IServiceDiscoveryService>(context => RestService.For<IServiceDiscoveryService>(@"http://72.190.177.214:5000"));
			builder.Register(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				return new AsyncEndpointZoneServerToGameServerService(QueryForRemoteServiceEndpoint(serviceDiscovery, "GameServer"));
			})
				.As<IZoneServerToGameServerClient>()
				.SingleInstance();

			/*builder.RegisterType<DefaultMovementHandlerService>()
				.As<IMovementDataHandlerService>()
				.AsSelf();

			builder.RegisterType<PathMovementBlockHandler>()
				.AsImplementedInterfaces()
				.AsSelf();

			builder.RegisterType<PositionChangeMovementBlockHandler>()
				.AsImplementedInterfaces()
				.AsSelf();*/

			builder.RegisterType<UtcNowNetworkTimeService>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<FieldValueUpdateFactory>()
				.AsSelf()
				.AsImplementedInterfaces();

			RegisterEntityDestructionServices(builder);

			//Honestly, before running this I think it'll be a MIRACLE if this actually works
			//Registering the generic networkmessage sender
			builder.RegisterGeneric(typeof(GenericMessageSender<>))
				.As(typeof(INetworkMessageSender<>).MakeGenericType(typeof(GenericSingleTargetMessageContext<>)))
				.SingleInstance();

			RegisterLockingPolicies(builder);

			//IPhysicsTriggerEventSubscribable TriggerEventSubscribable
			builder.RegisterInstance(GlobalPhysicsEventSystem.Instance)
				.AsImplementedInterfaces()
				.SingleInstance();

			//DefaultEntitySessionMessageSender : IEntitySessionMessageSender
			builder.RegisterType<DefaultEntitySessionMessageSender>()
				.As<IEntitySessionMessageSender>();

			//EntityBaseStatsDataFactory : IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable>
			builder.RegisterType<EntityBaseStatsDataFactory>()
				.As<IFactoryCreatable<EntityBaseStatsModel, EntityDataStatsDerivable>>();

			//RoundRobinSpawnStrategy : ISpawnPointStrategy
			builder.RegisterType<RoundRobinSpawnStrategy>()
				.As<ISpawnPointStrategy>();

			builder.RegisterType<PlayerSpawnStrategyQueue>()
				.AsSelf()
				.SingleInstance();

			builder.RegisterType<DefaultThreadUnSafeKnownEntitySet>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();
		}

		private static void RegisterEntityMappableCollections(ContainerBuilder builder)
		{
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

			builder.RegisterType<MovementDataCollection>()
				.AsSelf()
				.As<IReadonlyEntityGuidMappable<IMovementData>>()
				.As<IEntityGuidMappable<IMovementData>>()
				.As<IDirtyableMovementDataCollection>()
				.OnActivated(args =>
				{
					removableComponentsList.Add((IEntityCollectionRemovable)args.Instance);
				})
				.SingleInstance();

			//This will allow everyone to register the removable collection collection.
			builder.RegisterInstance(removableComponentsList)
				.As<IReadOnlyCollection<IEntityCollectionRemovable>>()
				.AsSelf()
				.SingleInstance();

		}

		private static void RegisterLockingPolicies(ContainerBuilder builder)
		{
			//Add a single entity-based locking policy
			builder.RegisterType<GlobalEntityResourceLockingPolicy>()
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<GlobalEntityCollectionsLockingPolicy>()
				.AsSelf();
		}

		private static void RegisterEntityDestructionServices(ContainerBuilder builder)
		{
			builder.RegisterType<PlayerSessionDestructor>()
				.As<IObjectDestructorable<PlayerSessionDeconstructionContext>>();

			builder.RegisterType<ServerPlayerEntityDestructor>()
				.As<IObjectDestructorable<PlayerEntityDestructionContext>>();

			builder.RegisterType<DefaultEntityDestructor>()
				.As<IObjectDestructorable<NetworkEntityGuid>>();

			builder.RegisterType<NetworkedEntityDataSaveable>()
				.As<IEntityDataSaveable>();
		}

		private static void RegisterPlayerFactoryServices(ContainerBuilder builder)
		{
			builder.RegisterType<EntityPrefabFactory>()
				.As<IFactoryCreatable<GameObject, EntityPrefab>>()
				.SingleInstance();
		}

		//TODO: Put this in a base class or something
		private async Task<string> QueryForRemoteServiceEndpoint(IServiceDiscoveryService serviceDiscovery, string serviceType)
		{
			ResolveServiceEndpointResponse endpointResponse = await serviceDiscovery.DiscoverService(new ResolveServiceEndpointRequest(ClientRegionLocale.US, serviceType));

			if(!endpointResponse.isSuccessful)
				throw new InvalidOperationException($"Failed to query for Service: {serviceType} Result: {endpointResponse.ResultCode}");

			Debug.Log($"Recieved service discovery response: {endpointResponse.Endpoint.EndpointAddress}:{endpointResponse.Endpoint.EndpointPort} for Type: {serviceType}");

			//TODO: Do we need extra slash?
			return $"{endpointResponse.Endpoint.EndpointAddress}:{endpointResponse.Endpoint.EndpointPort}/";
		}

		//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
		private bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
		{
			return true;
		}
	}
}
