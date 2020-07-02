using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public class EntityAnimationController
	{
		private Animator InternalAnimator { get; set; }

		/// <summary>
		/// Indicates if the Entity animation controller can be animated.
		/// </summary>
		public bool IsAnimatable => InternalAnimator != null;

		public EntityAnimationController(Animator internalAnimator)
		{
			InternalAnimator = internalAnimator;
		}

		/// <summary>
		/// Replaces the internal <see cref="Animator"/> component
		/// with the provided <see cref="animator"/>
		/// </summary>
		/// <param name="animator">The animator. (MUST BE NON-NULL)</param>
		public void ReplaceAnimator([NotNull] Animator animator)
		{
			if (animator == null) throw new ArgumentNullException(nameof(animator));

			//Null is a valid animator state BUT not for replacing.
			InternalAnimator = animator;
		}

		/// <summary>
		/// Clears the internal <see cref="Animator"/> component
		/// </summary>
		public void ClearAnimator()
		{
			InternalAnimator = null;
		}
	}
}
