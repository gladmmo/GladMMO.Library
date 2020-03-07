using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public interface IMovementGenerator<in TEntityType> //we make entity type generic so it will be easy to swap between guid/gameobject if needed.
	{
		/// <summary>
		/// Updates the movement for the <see cref="entity"/>
		/// based on the time <see cref="currentTime"/>.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="currentTime"></param>
		void Update(TEntityType entity, long currentTime);

		/// <summary>
		/// Indicates the current position of the Entity.
		/// </summary>
		Vector3 CurrentPosition { get; }

		//TODO: Expose a state instead.
		/// <summary>
		/// Indicates if the movement generator is running.
		/// </summary>
		bool isStarted { get; }

		/// <summary>
		/// Indicates if the generator is finished.
		/// </summary>
		bool isFinished { get; }
	}
}
