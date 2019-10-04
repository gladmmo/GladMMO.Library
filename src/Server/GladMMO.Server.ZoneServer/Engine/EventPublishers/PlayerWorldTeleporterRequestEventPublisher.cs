using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEventPublisher<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerSessionClaimedEventArgs>))]
	[AdditionalRegisterationAs(typeof(IPlayerWorldTeleporterRequestedEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerWorldTeleporterRequestEventPublisher : IEventPublisher<IPlayerWorldTeleporterRequestedEventSubscribable, PlayerSessionClaimedEventArgs>, IPlayerWorldTeleporterRequestedEventSubscribable, IGameInitializable
	{
		public event EventHandler<PlayerSessionClaimedEventArgs> OnWorldTeleporterRequested;

		public void PublishEvent(object sender, [NotNull] PlayerSessionClaimedEventArgs eventArgs)
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
