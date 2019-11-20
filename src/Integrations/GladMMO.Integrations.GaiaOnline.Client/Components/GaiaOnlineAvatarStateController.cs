using System;
using System.Collections;
using System.Collections.Generic;
using GaiaOnline;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class GaiaOnlineAvatarStateController : MonoBehaviour
	{
		/// <summary>
		/// Material ref for the Gaia avatar.
		/// </summary>
		[SerializeField]
		[Tooltip("This is the reference to the renderer that will be used to render the Gaia avatar.")]
		private Renderer GaiaAvatarRenderer;

		[SerializeField]
		[Range(-9, 9)] //clamp to -9 and 9. Shouldn't need to go move than the full 10 ranges would make no sense
		[Tooltip("The fame offset that should be relatively applied for movement animation.")]
		private int MovementFrameOffset = -4; //default working value.

		//TODO: Come out with a solution to result in correct facing and movement animation frame without tracking the last frame offset
		private int CurrentFrameOffset;

		/// <summary>
		/// Indicates if the avatar is in motion.
		/// (This controls which section of the animation strip is used)
		/// </summary>
		public bool isMoving { get; set; } = false; //don't make readonly, Unity delegates need setter

		private Camera cachedCameraReference;

		private void Start()
		{
			//We should default to facing forward
			SetFacingForwards();
			cachedCameraReference = Camera.main;
		}

		//Called when the direction of the avatar has changed. Assuming it is properly subscribed in the editor.
		public void OnDirectionChanged(Vector2 direction)
		{
			//assume normalization, won't matter
			if(direction.x > 0) //TODO: Is this the best way to determine facing?
				gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x) * -1.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			else if(Math.Abs(direction.x) > float.Epsilon) //ignore 0 to make sure it doesn't change from other inputs
				gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);

			//If they're moving Y they are moving "backwards"
			if(direction.y > 0)
				SetFacingBackwards();
			else if(Math.Abs(direction.y) > float.Epsilon) //ignore 0 to make sure it doesn't change from other inputs
				SetFacingForwards();

			//Apply the current frame offset calculated
			GaiaAvatarRenderer.material.mainTextureOffset = new Vector2((isMoving ? ApplyMovementOffset(CurrentFrameOffset) : CurrentFrameOffset) / 10.0f, GaiaAvatarRenderer.material.mainTextureOffset.y);
		}

		private int ApplyMovementOffset(int frameOffset)
		{
			return frameOffset + MovementFrameOffset;
		}

		private void SetFacingForwards()
		{
			//TODO: Should we let this be exposed and set in the editor?
			CurrentFrameOffset = 4;
		}

		private void SetFacingBackwards()
		{
			//TODO: Should we let this be exposed and set in the editor?
			CurrentFrameOffset = 5;
		}

		void Update()
		{
			//Sets the facing of the avatar towards the camera.
			transform.LookAt(cachedCameraReference.transform.position, transform.up);
		}
	}
}