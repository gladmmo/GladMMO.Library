using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GladMMO.Database;

namespace GladMMO
{
	public interface IInstanceableWorldObjectModel
	{
		/// <summary>
		/// The unique identifier for the instance.
		/// </summary>
		int ObjectInstanceId { get; }

		/// <summary>
		/// The spawn position of the GameObject.
		/// </summary>
		GladMMO.Database.Vector3<float> SpawnPosition { get; }

		/// <summary>
		/// The initial Y-axis orientation/rotation of the GameObject when spawned.
		/// Especially important for stationary GameObjects.
		/// </summary>
		float InitialOrientation { get; }
	}
}
