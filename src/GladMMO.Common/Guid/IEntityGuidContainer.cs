using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that contain a <see cref="ObjectGuid"/>
	/// </summary>
	public interface IEntityGuidContainer
	{
		/// <summary>
		/// The Network GUID contained inside the container.
		/// </summary>
		ObjectGuid EntityGuid { get; }
	}
}
