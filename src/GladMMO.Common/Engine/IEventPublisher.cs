using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for a type that publishes a particular event interface type.
	/// </summary>
	/// <typeparam name="TEventArgsType"></typeparam>
	public interface IEventPublisher<TEventPublisherInterface, in TEventArgsType> //One day I dream of a generic type arg that can be constrained to a type in the implementing types hierarchy.
		where TEventArgsType : EventArgs
	{
		/// <summary>
		/// Publishes the event with the args <see cref="eventArgs"/>
		/// with sender <see cref="sender"/>.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		void PublishEvent(object sender, TEventArgsType eventArgs);
	}
}
