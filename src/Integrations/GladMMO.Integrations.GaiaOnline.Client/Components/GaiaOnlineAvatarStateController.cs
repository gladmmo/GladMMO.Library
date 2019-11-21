using System;
using System.Collections;
using System.Collections.Generic;
using GaiaOnline;
using GladMMO;
using GladMMO.GaiaOnline;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class GaiaOnlineAvatarStateController : MonoBehaviour, IMovementDirectionChangedListener
	{
		public enum FacingState
		{
			Default = 0,
			Forward = 1,
			Backward = 2,
		}

		private FacingState Facing = FacingState.Default;

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

		[SerializeField]
		private GaiaOnlineLegsMotionAnimator LegAnimator;

		private int CurrentFrameOffset => Facing == FacingState.Forward ? 4 : 5;

		private Camera cachedCameraReference;

		private Vector2 CurrentMovementDirection = Vector2.zero;

		private bool isInMovementState = false;

		private Vector3 lastPosition;

		private void Start()
		{
			//We should default to facing forward
			SetFacingForwards();
			cachedCameraReference = Camera.main;
			lastPosition = transform.position;
		}

		private void SetMaterialFacing(bool isMoving)
		{
			//Apply the current frame offset calculated
			GaiaAvatarRenderer.material.mainTextureOffset = new Vector2((isMoving ? ApplyMovementOffset(CurrentFrameOffset) : CurrentFrameOffset) / 10.0f, GaiaAvatarRenderer.material.mainTextureOffset.y);
		}

		private int ApplyMovementOffset(int frameOffset)
		{
			return frameOffset + MovementFrameOffset;
		}

		[Button]
		private void SetFacingForwards()
		{
			Facing = FacingState.Forward;
			//CurrentFrameOffset = 4;
		}

		[Button]
		private void SetFacingBackwards()
		{
			Facing = FacingState.Backward;
			//CurrentFrameOffset = 5;
		}

		void LateUpdate()
		{
			transform.LookAt(transform.position + cachedCameraReference.transform.rotation * Vector3.forward,
				cachedCameraReference.transform.rotation * Vector3.up);
			transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y + 180, Vector3.up);

			Vector3 cameraForwardVector = new Vector3(cachedCameraReference.transform.root.forward.x, 0.0f, cachedCameraReference.transform.root.forward.z).normalized;
			float delta = Vector3.Angle(cameraForwardVector, transform.root.forward);

			int currentOffset = CurrentFrameOffset;
			if(delta > 90.0f)
				SetFacingForwards();
			else
				SetFacingBackwards();

			var positionDelta = lastPosition - transform.position;
			bool isMoving = CurrentMovementDirection.sqrMagnitude > 0.0f;

			if(currentOffset != CurrentFrameOffset || isInMovementState != isMoving)
				SetMaterialFacing(isMoving);

			if(isMoving)
			{
				LegAnimator.gameObject.SetActive(true);

				//positionDelta = Quaternion.AngleAxis(delta, Vector3.up) * positionDelta;
				var direction = transform.InverseTransformDirection(positionDelta);

				//assume normalization, won't matter
				if(direction.x > Vector3.kEpsilon) //TODO: Is this the best way to determine facing?
					gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x) * -1.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				else if(Math.Abs(direction.x) > Vector3.kEpsilon) //ignore 0 to make sure it doesn't change from other inputs
					gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			}
			else
			{
				//TODO: Is this more efficient?
				if(LegAnimator.gameObject.activeSelf)
					LegAnimator.gameObject.SetActive(false);
			}

			lastPosition = transform.position;
			isInMovementState = isMoving;
		}

		public void SetMovementDirection(Vector2 direction)
		{
			CurrentMovementDirection = direction;
		}
	}
}