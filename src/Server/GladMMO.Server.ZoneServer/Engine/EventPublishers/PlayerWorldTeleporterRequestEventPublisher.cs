using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEventPublisher<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerWorldTeleporterRequestEventArgs>))]
	[AdditionalRegisterationAs(typeof(IPlayerWorldTeleporterRequestedEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerWorldTeleporterRequestEventPublisher : IEventPublisher<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerWorldTeleporterRequestEventArgs>, IPlayerWorldTeleporterRequestedEventSubscribable, IGameInitializable
	{
		public event EventHandler<PlayerWorldTeleporterRequestEventArgs> OnWorldTeleporterRequested;

		public void PublishEvent(object sender, [NotNull] PlayerWorldTeleporterRequestEventArgs eventArgs)
		{
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnWorldTeleporterRequested?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
