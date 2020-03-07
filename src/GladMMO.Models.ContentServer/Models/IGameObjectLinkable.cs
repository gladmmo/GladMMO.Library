using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for models that reference or are
	/// linked to a <see cref="GameObjectInstanceModel"/>.
	/// </summary>
	public interface IGameObjectLinkable
	{
		/// <summary>
		/// ID of the linked <see cref="GameObjectInstanceModel"/>.
		/// </summary>
		int LinkedGameObjectId { get; }
	}
}
