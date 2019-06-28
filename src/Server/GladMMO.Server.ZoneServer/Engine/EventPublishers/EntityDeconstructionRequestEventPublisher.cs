using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IEventPublisher<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs>))]
	[AdditionalRegisterationAs(typeof(IEntityDeconstructionRequestedEventSubscribable))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class EntityDeconstructionRequestEventPublisher : IEventPublisher<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs>, IEntityDeconstructionRequestedEventSubscribable, IGameInitializable
	{
		public event EventHandler<EntityDeconstructionRequestedEventArgs> OnEntityDeconstructionRequested;

		public void PublishEvent(object sender, EntityDeconstructionRequestedEventArgs eventArgs)
		{
			if(sender == null) throw new ArgumentNullException(nameof(sender));
			if(eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnEntityDeconstructionRequested?.Invoke(sender, eventArgs);
		}

		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}
