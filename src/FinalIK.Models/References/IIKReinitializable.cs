using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.FinalIK
{
	/// <summary>
	/// Contract for IK components that expose the ability
	/// for re-initialization.
	/// </summary>
	public interface IIKReinitializable
	{
		/// <summary>
		/// Reinitializes the IK component.
		/// </summary>
		void ReInitialize();
	}
}
