using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Conctract for types that are linked to <see cref="GameObjectEntryModel"/> instances.
	/// </summary>
	public interface IGameObjectEntryLinkable
	{
		/// <summary>
		/// Defines the <see cref="GameObjectEntryModel"/> instance that
		/// this linkable is attached to. It is the primary
		/// and foreign key to the instance it's attached to.
		/// </summary>
		int TargetGameObjectId { get; set; }

		//Navigation property
		/// <summary>
		/// The GameObject instance the linkable is attached to.
		/// </summary>
		GameObjectEntryModel TargetGameObject { get; }
	}
}
