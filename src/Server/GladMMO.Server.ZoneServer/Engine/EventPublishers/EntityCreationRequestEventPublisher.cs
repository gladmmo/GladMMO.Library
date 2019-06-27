using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs>))]
	[AdditionalRegisterationAs(typeof(IEntityCreationRequestedEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class EntityCreationRequestEventPublisher : IEventPublisher<IEntityCreationRequestedEventSubscribable, EntityCreationRequestedEventArgs>, IEntityCreationRequestedEventSubscribable, IGameInitializable
	{
		public event EventHandler<EntityCreationRequestedEventArgs> OnEntityCreationRequested;

		public void PublishEvent([NotNull] object sender, [NotNull] EntityCreationRequestedEventArgs eventArgs)
		{
			if (sender == null) throw new ArgumentNullException(nameof(sender));
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnEntityCreationRequested?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
