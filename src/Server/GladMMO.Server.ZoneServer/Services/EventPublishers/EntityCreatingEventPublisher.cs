using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Handles the subscrption of event <see cref="IEntityCreatingEventSubscribable"/>
	/// and the publishing through <see cref="IEventPublisher{TEventPublisherInterface,TEventArgsType}"/>.
	/// </summary>
	public sealed class EntityCreatingEventPublisher : IEventPublisher<IEntityCreatingEventSubscribable, EntityCreatingEventArgs>, IEntityCreatingEventSubscribable
	{
		public event EventHandler<EntityCreatingEventArgs> OnEntityCreating;

		public void PublishEvent([NotNull] object sender, [NotNull] EntityCreatingEventArgs eventArgs)
		{
			if (sender == null) throw new ArgumentNullException(nameof(sender));
			if (eventArgs == null) throw new ArgumentNullException(nameof(eventArgs));

			OnEntityCreating?.Invoke(sender, eventArgs);
		}
	}
}
