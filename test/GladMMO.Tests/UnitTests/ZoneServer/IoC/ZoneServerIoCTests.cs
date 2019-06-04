using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Autofac;
using Autofac.Core;
using Common.Logging.Simple;
using Glader.Essentials;
using GladNet;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public sealed class ZoneServerIoCTests
	{
		[Test]
		public void Test_Can_Create_IGameTickables()
		{
			//arrange
			IContainer container = BuildTestContainer();

			//assert
			try
			{
				container.Resolve<IEnumerable<IGameTickable>>();
			}
			catch(DependencyResolutionException e)
			{
				//This makes it so the error is more readable. So we can see the exact dependency that is missing.
				DependencyResolutionException dependencyResolveException = e;

				while(dependencyResolveException.InnerException is DependencyResolutionException)
					dependencyResolveException = (DependencyResolutionException)dependencyResolveException.InnerException;

				Assert.Fail($"Failed: {dependencyResolveException.Message}\n\n{dependencyResolveException.StackTrace}");
			}
		}

		private static IContainer BuildTestContainer()
		{
			ContainerBuilder builder = new ContainerBuilder();
			builder.RegisterModule<DefaultZoneServerDependencyModule>();
			IContainer build = builder.Build();
			return build;
		}

		[Test]
		public void Test_Can_Get_MovementCollection_When_OpenGeneric_Registered()
		{
			//arrange
			IContainer container = BuildTestContainer();

			//act
			IReadonlyEntityGuidMappable<IMovementData> movementDataCollection = container.Resolve<IReadonlyEntityGuidMappable<IMovementData>>();
			IReadonlyEntityGuidMappable<int> intTestCollection = container.Resolve<IReadonlyEntityGuidMappable<int>>();

			//assert
			Assert.True(movementDataCollection is MovementDataCollection, $"Could not create {typeof(MovementDataCollection).Name} while open generic entity mappables were registered.");
			Assert.True(intTestCollection is EntityGuidDictionary<int>);
		}
	}
}
