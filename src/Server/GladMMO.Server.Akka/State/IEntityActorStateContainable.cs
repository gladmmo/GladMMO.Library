using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Base contract for actors' state.
	/// </summary>
	public interface IEntityActorStateContainable
	{
		/// <summary>
		/// Reference to Entity/Actor raw replicatable entity data.
		/// </summary>
		IEntityDataFieldContainer EntityData { get; }
	}
}
