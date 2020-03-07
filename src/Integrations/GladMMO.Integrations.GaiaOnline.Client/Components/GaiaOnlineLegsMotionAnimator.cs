using System; using FreecraftCore;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GladMMO.GaiaOnline
{
	public sealed class GaiaOnlineLegsMotionAnimator : MonoBehaviour
	{
		/// <summary>
		/// The <see cref="Renderer"/> component for the legs.
		/// </summary>
		[SerializeField]
		[Tooltip("The renderer reference for the leg animation.", order = 1)]
		private Renderer LegRenderer;

		/// <summary>
		/// The time inbetween frames. Consider this like the inverse of animation speed.
		/// </summary>
		[SerializeField]
		[Tooltip("The time between the frames.", order = 2)]
		private float TimeBetweenFrames = 0.03f; //verified tested default value.

		/// <summary>
		/// The starting offset of the leg animation in the strip.
		/// </summary>
		[SerializeField]
		[Tooltip("The starting offset for the leg animation.", order = 3)]
		private int LegMaterialOffsetAnimationStartFrame = 6; //verified tested default value.

		/// <summary>
		/// The ending offset of the leg animation in the strip.
		/// </summary>
		[SerializeField]
		[Tooltip("The ending offset for the leg animation.", order = 4)]
		private int LegMaterialOffsetAnimationStopFrame = 9; //verified tested default value.

		private void Start()
		{
			if(LegMaterialOffsetAnimationStartFrame > LegMaterialOffsetAnimationStopFrame)
				throw new InvalidOperationException("The provided leg animation range is invalid.");

			if(LegMaterialOffsetAnimationStopFrame == LegMaterialOffsetAnimationStartFrame)
				throw new InvalidOperationException("The provided leg animation range is invalid.");
		}

		//When enabled we'll start moving the legs.
		private void OnEnable()
		{
			StartCoroutine(LegAnimationRoutine());
		}

		//TODO: Move this into an animation clip, if that's better. Research.
		private IEnumerator LegAnimationRoutine()
		{
			int currentOffset = LegMaterialOffsetAnimationStartFrame;

			while(true)
			{
				LegRenderer.material.mainTextureOffset = new Vector2(currentOffset / 10.0f, LegRenderer.material.mainTextureOffset.y);

				currentOffset++;

				//wrap around: https://stackoverflow.com/questions/14415753/wrap-value-into-range-min-max-without-division
				currentOffset = ((currentOffset - LegMaterialOffsetAnimationStartFrame) % (LegMaterialOffsetAnimationStopFrame - LegMaterialOffsetAnimationStartFrame)) + LegMaterialOffsetAnimationStartFrame;

				//TODO: Cache WaitForSeconds due to still terrible GC in Unity 2017.1
				yield return new WaitForSeconds(TimeBetweenFrames);
			}
		}
	}
}
