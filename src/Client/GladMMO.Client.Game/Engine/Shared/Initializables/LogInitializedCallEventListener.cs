using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.TitleScreen)]
	public sealed class LogInitializedCallEventListener : IGameStartable
	{
		private ILog Logger { get; }

		public LogInitializedCallEventListener([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public Task OnGameStart()
		{
			if(Logger.IsDebugEnabled)
				Logger.Debug($"Scene Initialized called.");

			return Task.CompletedTask;
		}
	}
}
