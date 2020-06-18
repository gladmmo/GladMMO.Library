using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Castle.Core.Internal;
using Fasterflect;
using Glader.Essentials;
using NUnit.Framework;
using NUnit.Framework.Internal;
using GladMMO;

namespace GladMMO
{
	[TestFixture]
	public sealed class GameInitializableTests
	{
		public static IReadOnlyCollection<Type> GameInitializables
			=> typeof(IGameInitializable).Assembly.GetExportedTypes()
				.Where(t => t.Implements(typeof(IGameInitializable)))
				.ToArrayTryAvoidCopy();

		[Test]
		[TestCaseSource(nameof(GameInitializables))]
		public void Test_All_GameInitablizes_Have_GameSceneAttribute(Type t)
		{
			//arrange
			if (t.IsAbstract)
			{
				Assert.Pass();
				return;
			}

			//act
			bool hasAttribute = t.HasAttribute<SceneTypeCreateGladMMO>();

			//assert
			Assert.True(hasAttribute, $"Type: {t.Name} does not have {nameof(SceneTypeCreateGladMMO)}. All {nameof(IGameInitializable)} must have this attribute.");
		}

		[Test]
		[TestCase(GameSceneType.TitleScreen)]
		public void Test_Only_Finds_Initializables_Specified(GameSceneType sceneType)
		{
			//arrange
			ContainerBuilder builder = new ContainerBuilder();
			EngineInterfaceRegisterationModule module = new EngineInterfaceRegisterationModule((int)sceneType, typeof(IMovementInputChangedEventSubscribable).Assembly);
			builder.RegisterModule(module);

			IReadOnlyCollection<IGameInitializable> initiablizes = builder.Build().Resolve<IReadOnlyCollection<IGameInitializable>>();

			//act
			foreach(var init in initiablizes)
			{
				//If we have an intiiablizable that doesn't have the scene then this will throw.
				Assert.True(init.GetType().GetAttributes<SceneTypeCreateGladMMO>().Any(a => a.SceneType == (int)sceneType));
			}
		}

		[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
		public class TestGameSceneInit : IGameInitializable
		{
			/// <inheritdoc />
			public Task OnGameInitialized()
			{
				throw new NotImplementedException();
			}
		}

		[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
		public class TestTitleSceneInit : IGameInitializable
		{
			/// <inheritdoc />
			public Task OnGameInitialized()
			{
				throw new NotImplementedException();
			}
		}

		[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
		[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
		public class TestBothSceneInit : IGameInitializable
		{
			/// <inheritdoc />
			public Task OnGameInitialized()
			{
				throw new NotImplementedException();
			}
		}
	}
}
