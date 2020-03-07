using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Autofac.Core;
using Glader.Essentials;
using NUnit.Framework;
using SceneJect;
using SceneJect.Common;

namespace GladMMO
{
	[TestFixture]
	public static class GameTickableTests
	{
		[Test]
		public static void Test_Can_Resolve_GameTickables()
		{
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
			IReadOnlyCollection<IGameTickable> gameTickables = null;

			try
			{
				gameTickables = resolver.Resolve<IReadOnlyCollection<IGameTickable>>();
			}
			catch(DependencyResolutionException e)
			{
				//This makes it so the error is more readable. So we can see the exact dependency that is missing.
				DependencyResolutionException dependencyResolveException = e;

				while(dependencyResolveException.InnerException is DependencyResolutionException)
					dependencyResolveException = (DependencyResolutionException)dependencyResolveException.InnerException;

				Assert.Fail($"Failed: {dependencyResolveException.Message}\n\n{dependencyResolveException.StackTrace}");
			}


			//assert
			Assert.NotNull(gameTickables);
			Console.WriteLine($"Found game tickable Count: {gameTickables.Count}");
			Assert.IsNotEmpty(gameTickables);
		}
	}
}
