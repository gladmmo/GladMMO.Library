using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Common.Logging;
using Fasterflect;
using Glader.Essentials;
using Moq;
using GladMMO.Client;
using SceneJect;
using SceneJect.Common;

namespace GladMMO
{
	internal static class TestIoC
	{
		//TODO: Refactor and extract this
		public class MockedUIDependenciesAutofacModule : Module
		{
			public MockedUIDependenciesAutofacModule()
			{

			}

			/// <inheritdoc />
			protected override void Load(ContainerBuilder builder)
			{
				//TODO: Automate discovery of adapter types
				IUIText uiText = Mock.Of<IUIText>();
				IUIImage uiImage = Mock.Of<IUIImage>();
				IUIButton uiButton = Mock.Of<IUIButton>();

				IUIFillableImage fillableImage = Mock.Of<IUIFillableImage>();


				foreach(UnityUIRegisterationKey key in Enum.GetValues(typeof(UnityUIRegisterationKey)))
				{
					builder.RegisterInstance(uiText)
						.Keyed<IUIText>(key);

					builder.RegisterInstance(uiImage)
						.Keyed<IUIImage>(key);

					builder.RegisterInstance(uiButton)
						.Keyed<IUIButton>(key);

					builder.RegisterInstance(fillableImage)
						.Keyed<IUIFillableImage>(key);
				}

			}
		}

		public static ContainerBuilder CreateDefaultContainer()
		{
			ContainerBuilder builder = new ContainerBuilder();

			//We can't let autofac do this, since we have multiple versions of some modules.
			foreach(var module in typeof(AuthenticationRegisterModule).Assembly
				.GetExportedTypes()
				.Where(t => t.Inherits(typeof(Autofac.Module)))
				.Where(t => !t.IsAbstract) //we can't deal with base abstract autofac modules
				.Where(t => t != typeof(EngineInterfaceRegisterationModule))) //TODO: Make this so we can override the modules better
			{
				builder.RegisterModule(Activator.CreateInstance(module) as Module);
			}
			builder.RegisterModule(new MockedUIDependenciesAutofacModule());

			//Register every GameInit module
			foreach(GameSceneType sceneType in Enum.GetValues(typeof(GameSceneType)))
			{
				builder.RegisterModule(new EngineInterfaceRegisterationModule((int)sceneType, typeof(IMovementInputChangedEventSubscribable).Assembly));
			}

			//Manually register SceneJect services
			builder.Register(context => new DefaultGameObjectFactory(context.Resolve<ILifetimeScope>(), new DefaultInjectionStrategy()))
				.As<IGameObjectFactory>()
				.SingleInstance();

			builder.Register(context => new DefaultGameObjectComponentAttachmentFactory(context.Resolve<ILifetimeScope>(), new DefaultInjectionStrategy()))
				.As<IGameObjectComponentAttachmentFactory>()
				.SingleInstance();

			builder.Register(context => new DefaultManualInjectionStrategy(context.Resolve<IComponentContext>()))
				.As<IManualInjectionStrategy>()
				.SingleInstance();

			RegisterExpectedInSceneDependecyMocks(builder);

			return builder;
		}

		public static void RegisterExpectedInSceneDependecyMocks(ContainerBuilder builder)
		{
			//Common thing used in temp registeration
			builder.RegisterInstance(ObjectGuid.Empty)
				.AsSelf();

			//We do this to override the UnityLogger which calls ECall Sec errors.
			builder.RegisterType<ConsoleLogger>()
				.AsSelf()
				.As<ILog>()
				.SingleInstance();

			builder.RegisterInstance(Mock.Of<IChatMessageBoxReciever>())
				.AsSelf()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
