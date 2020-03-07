using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using Autofac.Core;
using Fasterflect;
using GladNet;
using Moq;
using NUnit.Framework;
using SceneJect;
using SceneJect.Common;

namespace GladMMO
{
	[TestFixture]
	public class GeneralHandlerTests
	{
		[Test]
		public void Test_Client_Can_Create_MessageHandlerService_From_DependencyModules()
		{
			//TODO: This won't work if we have multiple configurable modules.
			//arrange
			ContainerBuilder builder = TestIoC.CreateDefaultContainer();

			IContainer resolver = builder.Build();

			MessageHandlerService<GameServerPacketPayload, GameClientPacketPayload> handler = null;

			//act
			try
			{
				handler = resolver.Resolve<MessageHandlerService<GameServerPacketPayload, GameClientPacketPayload>>();
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
			Assert.NotNull(handler);
		}
	}
}
