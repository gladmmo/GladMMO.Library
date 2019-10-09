using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IGameObjectBehaviourCreatedEventSubscribable
	{
		event EventHandler<GameObjectBehaviourCreatedEventArgs> OnBehaviourCreated;
	}

	public sealed class GameObjectBehaviourCreatedEventArgs : EventArgs
	{
		public BaseGameObjectEntityBehaviourComponent Component { get; }

		public GameObjectBehaviourCreatedEventArgs([NotNull] BaseGameObjectEntityBehaviourComponent component)
		{
			Component = component ?? throw new ArgumentNullException(nameof(component));
		}
	}
}
