using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IObjectUpdateBlockDispatcher))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdateDispatcher : IObjectUpdateBlockDispatcher, IGameInitializable
	{
		//We don't use a dictionary for throughput
		//this WILL be a hot path in the network handling code
		private IObjectUpdateBlockHandler[] Handlers { get; }

		public ObjectUpdateDispatcher(IEnumerable<IObjectUpdateBlockHandler> handlers)
		{
			Handlers = new IObjectUpdateBlockHandler[(int)Enum.GetValues(typeof(ObjectUpdateType)).Cast<ObjectUpdateType>().Max()];

			foreach (IObjectUpdateBlockHandler handler in handlers)
				Handlers[(int) handler.UpdateType] = handler;
		}

		public void Dispatch([NotNull] ObjectUpdateBlock updateBlock)
		{
			if (updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			Handlers[(int)updateBlock.UpdateType]?.HandleUpdateBlock(updateBlock);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
