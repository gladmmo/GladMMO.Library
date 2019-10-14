using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public interface IEntityDestructionRequestedEventPublisher : IEventPublisher<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs>
	{

	}

	[AdditionalRegisterationAs(typeof(IEventPublisher<IEntityDeconstructionRequestedEventSubscribable, EntityDeconstructionRequestedEventArgs>))]
	[AdditionalRegisterationAs(typeof(IEntityDeconstructionRequestedEventSubscribable))]
	[AdditionalRegisterationAs(typeof(IEntityDestructionRequestedEventPublisher))]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class EntityDeconstructionRequestEventPublisher : IEntityDestructionRequestedEventPublisher, IEntityDeconstructionRequestedEventSubscribable, IGameInitializable
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
