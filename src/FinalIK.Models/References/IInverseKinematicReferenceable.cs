using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace FinalIK
{
	public interface IInverseKinematicReferenceable
	{
		/// <summary>
		/// Gets the set transforms for IK operations.
		/// </summary>
		/// <returns>Array of all the IK transforms.</returns>
		Transform[] GetTransforms();

		/// <summary>
		/// Returns true if all required Transforms have been assigned (shoulder, toe and neck bones are optional).
		/// </summary>
		bool isFilled { get; }

		//These are exposed because the VRIK references them directly.
		Transform LeftHand { get; }

		Transform LeftForearm { get; }

		Transform RightHand { get; }

		Transform RightForearm { get; }

		void SetRoot(Transform root);
	}
}
