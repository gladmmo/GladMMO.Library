using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
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
		/// The assemblies being used to gather engine interface implementations from.
		/// </summary>
		public IEnumerable<Assembly> EngineInterfaceAssemblies { get; }

		/// <summary>
		/// Default autofac ctor.
		/// </summary>
		public CommonGameDependencyModule()
		{
			//We shouldn't call this, I don't think.
		}

		//TODO: Shoudl we expose the ServiceDiscovery URL to the editor? Is there value in that?
		/// <inheritdoc />
		public CommonGameDependencyModule(GameSceneType scene, [NotNull] string serviceDiscoveryUrl, params Assembly[] engineInterfaceAssemblies)
		{
			if(!Enum.IsDefined(typeof(GameSceneType), scene)) throw new InvalidEnumArgumentException(nameof(scene), (int)scene, typeof(GameSceneType));

			Scene = scene;
			ServiceDiscoveryUrl = serviceDiscoveryUrl ?? throw new ArgumentNullException(nameof(serviceDiscoveryUrl));
			EngineInterfaceAssemblies = engineInterfaceAssemblies;
		}

		/// <inheritdoc />
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.Register(context => LogLevel.All)
				.As<LogLevel>()
				.SingleInstance();

			builder.RegisterType<LocalCharacterDataRepository>()
				.As<ILocalCharacterDataRepository>()
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
			foreach(var assembly in EngineInterfaceAssemblies)
				builder.RegisterModule(new GameClientMessageHandlerAutofacModule(Scene, assembly));

			foreach(var assembly in EngineInterfaceAssemblies)
				builder.RegisterModule(new EngineInterfaceRegisterationModule((int)Scene, assembly));

			//builder.RegisterModule<EntityMappableRegisterationModule<NetworkEntityGuid>>();
			RegisterEntityContainers(builder);

			builder.RegisterModule(new ServiceDiscoveryDependencyAutofacModule(ServiceDiscoveryUrl));

			builder.RegisterModule<NameQueryServiceDependencyAutofacModule>();

			builder.RegisterType<CacheableEntityNameQueryable>()
				.As<IEntityNameQueryable>()
				.SingleInstance();

			//Used on character selection too.
			builder.RegisterType<DefaultEntityExperienceLevelStrategy>()
				.As<IEntityExperienceLevelStrategy>()
				.SingleInstance();

			RegisterEntityContainers(builder);
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

			//EntityFieldDataCollectionEntityGuidMappable
			builder.RegisterType<EntityFieldDataCollectionEntityGuidMappable>()
				.AsImplementedInterfaces()
				.OnActivated(args =>
				{
					removableComponentsList.Add((IEntityCollectionRemovable)args.Instance);
				})
				.SingleInstance();
		}
	}
}
