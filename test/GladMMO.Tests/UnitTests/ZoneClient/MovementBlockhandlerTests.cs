using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using NUnit.Framework;
using SceneJect;
using SceneJect.Common;

namespace GladMMO
{
	[TestFixture]
	public sealed class MovementBlockhandlerTests
	{
		/*[Test]
		public void Test_Can_Resolve_RegisteredMovementBlockHandlers()
		{
			//TODO: If we change the ctor signature for the handler service we need to change this test
			//arrange
			//TODO: This won't work if we have multiple configurable modules.
			//arrange
			ContainerBuilder builder = TestIoC.CreateDefaultContainer();

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

			IContainer resolver = builder.Build();

			//act
			IReadOnlyCollection<IMovementBlockHandler> handlers = resolver.Resolve<IReadOnlyCollection<IMovementBlockHandler>>();

			//assert
			Assert.NotNull(handlers);
			Console.WriteLine($"Found Handler Count: {handlers.Count}");
			Assert.IsNotEmpty(handlers);
		}*/
	}
}
