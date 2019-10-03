using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Contract for world objects that are interactable.
	/// </summary>
	public interface IWorldInteractable
	{
		/// <summary>
		/// Interacts with the <see cref="IWorldInteractable"/>.
		/// </summary>
		/// <param name="entityInteracting">The guid of the entity attempting to interact with the interactable.</param>
		void Interact(NetworkEntityGuid entityInteracting);
	}
}
