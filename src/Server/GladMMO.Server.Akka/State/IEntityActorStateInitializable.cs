using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IEntityActorStateInitializable<in TActorStateType>
	{
		/// <summary>
		/// Indicates if the state has been initialized.
		/// </summary>
		bool isInitialized { get; }

		/// <summary>
		/// Initializes the actor's state.
		/// </summary>
		/// <exception cref="InvalidOperationException">Throws if <see cref="isInitialized"/> is true.</exception>
		/// <param name="state">The state.</param>
		void InitializeState(TActorStateType state);
	}
}
