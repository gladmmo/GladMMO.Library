using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Autofac;
using Common.Logging;
using Glader.Essentials;
using Refit;

namespace GladMMO
{
	public sealed class CommonGameDependencyModule : NetworkServiceDiscoveryableAutofaceModule
	{
		/// <summary>
		/// The scene to load initializables for.
		/// </summary>
		private GameSceneType Scene { get; }

		private string ServiceDiscoveryUrl { get; }

		/// <summary>
		/// Default autofac ctor.
		/// </summary>
		public CommonGameDependencyModule()
		{
			//We shouldn't call this, I don't think.
		}

		//TODO: Shoudl we expose the ServiceDiscovery URL to the editor? Is there value in that?
		/// <inheritdoc />
		public CommonGameDependencyModule(GameSceneType scene, [NotNull] string serviceDiscoveryUrl = "http://72.190.177.214:5000")
		{
			if(!Enum.IsDefined(typeof(GameSceneType), scene)) throw new InvalidEnumArgumentException(nameof(scene), (int)scene, typeof(GameSceneType));

			Scene = scene;
			ServiceDiscoveryUrl = serviceDiscoveryUrl ?? throw new ArgumentNullException(nameof(serviceDiscoveryUrl));
		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.Register(context => LogLevel.All)
				.As<LogLevel>()
				.SingleInstance();

			builder.RegisterType<LocalCharacterDataRepository>()
				.As<ICharacterDataRepository>()
				.SingleInstance();

			builder.RegisterType<LocalZoneDataRepository>()
				.As<IZoneDataRepository>()
				.As<IReadonlyZoneDataRepository>()
				.SingleInstance();

			builder.RegisterType<AuthenticationTokenRepository>()
				.As<IAuthTokenRepository>()
				.As<IReadonlyAuthTokenRepository>()
				.SingleInstance();

			//Handlers aren't needed for all scenes, but for most.
			//TODO: We should expose SceneTypeCreatable or whatever on handlers
			builder.RegisterModule(new GameClientMessageHandlerAutofacModule(Scene));

			builder.RegisterModule(new EngineInterfaceRegisterationModule((int)Scene, GetType().Assembly));

			//builder.RegisterModule<EntityMappableRegisterationModule<NetworkEntityGuid>>();
			RegisterEntityContainers(builder);

			builder.Register<IServiceDiscoveryService>(context => RestService.For<IServiceDiscoveryService>(ServiceDiscoveryUrl))
				.As<IServiceDiscoveryService>()
				.SingleInstance();


			builder.Register<INameQueryService>(context =>
			{
				IServiceDiscoveryService serviceDiscovery = context.Resolve<IServiceDiscoveryService>();

				//TODO: Eventually gameserver won't be endpoint for namequeries.
				return new AsyncEndpointNameQueryService(QueryForRemoteServiceEndpoint(serviceDiscovery, "GameServer"), new RefitSettings() { HttpMessageHandlerFactory = () => new FiddlerEnabledWebProxyHandler() });
			});

			builder.RegisterType<CacheableEntityNameQueryable>()
				.As<IEntityNameQueryable>()
				.SingleInstance();
		}

		private static void RegisterEntityContainers(ContainerBuilder builder)
		{
			//HelloKitty: We actually have to do this manually, and not use GladerEssentials because we use simplified interfaces.
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
				.AsSelf()
				.SingleInstance();
		}
	}
}
