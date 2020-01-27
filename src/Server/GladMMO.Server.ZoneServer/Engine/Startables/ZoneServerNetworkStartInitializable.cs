using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	//Initializable that just starts the zoneserver network listener.
	[AdditionalRegisterationAs(typeof(IServerStartedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IServerStartingEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class ZoneServerNetworkStartInitializable : IGameStartable, IServerStartingEventSubscribable, IServerStartedEventSubscribable
	{
		private ZoneServerApplicationBase ApplicationBase { get; }

		public event EventHandler OnServerStarting;

		public event EventHandler OnServerStarted;

		/// <inheritdoc />
		public ZoneServerNetworkStartInitializable([NotNull] ZoneServerApplicationBase applicationBase)
		{
			ApplicationBase = applicationBase ?? throw new ArgumentNullException(nameof(applicationBase));
		}

		/// <inheritdoc />
		public async Task OnGameStart()
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

#pragma warning disable 4014
			Task.Factory.StartNew(async () =>
#pragma warning restore 4014
			{
				completionSource.SetResult(null);

				await ApplicationBase.BeginListening()
					.ConfigureAwaitFalseVoid();

				if(ApplicationBase.Logger.IsWarnEnabled)
					ApplicationBase.Logger.Warn($"Server is shutting down.");

			}, TaskCreationOptions.LongRunning);

			await completionSource.Task;

			//This is the closest we can get to "started" without GladNet exposing event API
			OnServerStarted?.Invoke(this, EventArgs.Empty);
		}
	}
}
