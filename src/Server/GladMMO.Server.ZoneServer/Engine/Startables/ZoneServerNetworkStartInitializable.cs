using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	//Initializable that just starts the zoneserver network listener.
	[AdditionalRegisterationAs(typeof(IServerStartingEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ZoneServerNetworkStartInitializable : IGameStartable, IServerStartingEventSubscribable
	{
		private ZoneServerApplicationBase ApplicationBase { get; }

		public event EventHandler OnServerStarting;

		/// <inheritdoc />
		public ZoneServerNetworkStartInitializable([NotNull] ZoneServerApplicationBase applicationBase)
		{
			ApplicationBase = applicationBase ?? throw new ArgumentNullException(nameof(applicationBase));
		}

		/// <inheritdoc />
		public Task OnGameStart()
		{
			OnServerStarting?.Invoke(this, EventArgs.Empty);

			if(!ApplicationBase.StartServer())
			{
				string error = $"Failed to start server on Details: {ApplicationBase.ServerAddress}";

				if(ApplicationBase.Logger.IsErrorEnabled)
					ApplicationBase.Logger.Error(error);

				throw new InvalidOperationException(error);
			}

			TaskCompletionSource<object> completionSource = new TaskCompletionSource<object>();

			Task.Factory.StartNew(async () =>
			{
				completionSource.SetResult(null);

				await ApplicationBase.BeginListening()
					.ConfigureAwait(false);

				if(ApplicationBase.Logger.IsWarnEnabled)
					ApplicationBase.Logger.Warn($"Server is shutting down.");

			}, TaskCreationOptions.LongRunning);

			return completionSource.Task;
		}
	}
}
