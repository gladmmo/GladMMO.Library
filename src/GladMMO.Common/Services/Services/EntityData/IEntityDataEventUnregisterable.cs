using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that can handle unregisters events.
	/// </summary>
	public interface IEntityDataEventUnregisterable
	{
		/// <summary>
		/// Unregisters a stored event registeration.
		/// </summary>
		void Unregister();
	}
}
