using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public static class GameObjectExtensions
	{
		/// <summary>
		/// Indicates if the provided <see cref="GameObject"/> is
		/// still valid. Null <see cref="GameObject"/>s can be null
		/// because they have been destroyed OR if the reference was never
		/// initialized in the first place. This method confirms that it hasn't been destroyed
		/// but will THROW if it hasn't been initialized.
		/// </summary>
		/// <param name="gameobject">The <see cref="GameObject"/> to validate.</param>
		/// <returns>True: If the <see cref="GameObject"/> reference has been initialized but is now destroyed.
		/// False: If the <see cref="GameObject"/> has not been destroyed.
		/// Throws if <paramref name="gameobject"/> was never initialized.</returns>
		public static bool IsGameObjectValid([NotNull] this GameObject gameobject)
		{
			if(gameobject == null) throw new ArgumentNullException(nameof(gameobject), $"{nameof(IsGameObjectValid)} checks if a {nameof(GameObject)} is valid but throws if {nameof(GameObject)} reference was never initialized.");

			return !gameobject.Equals(null);
		}
	}
}
