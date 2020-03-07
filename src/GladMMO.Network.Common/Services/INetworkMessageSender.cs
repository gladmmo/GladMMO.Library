using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that can send network messages based
	/// on a provided context.
	/// </summary>
	/// <typeparam name="TContextType"></typeparam>
	public interface INetworkMessageSender<in TContextType>
		where TContextType : IEntityGuidContainer
	{
		/// <summary>
		/// Sends a message based on the provid context.
		/// </summary>
		/// <param name="context">The context.</param>
		void Send(TContextType context);

		/// <summary>
		/// Sends a message based on the provid context asyncronously.
		/// </summary>
		/// <param name="context">The context.</param>
		Task SendAsync(TContextType context);
	}
}
