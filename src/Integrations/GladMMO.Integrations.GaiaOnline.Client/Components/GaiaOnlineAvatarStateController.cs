using System;
using System.Collections;
using System.Collections.Generic;
using GaiaOnline;
using GladMMO;
using UnityEngine;

namespace GaiaOnline
{
	public sealed class GaiaOnlineAvatarStateController : MonoBehaviour
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
		public bool SetFacingDependingOnDirection = false;

		private int CurrentFrameOffset => Facing == FacingState.Forward ? 4 : 5; 

		/// <summary>
		/// Indicates if the avatar is in motion.
		/// (This controls which section of the animation strip is used)
		/// </summary>
		public bool isMoving { get; set; } = false; //don't make readonly, Unity delegates need setter

		private Camera cachedCameraReference;

		private Vector3 lastPosition;

		private void Start()
		{
			//We should default to facing forward
			SetFacingForwards();
			cachedCameraReference = Camera.main;
			lastPosition = transform.position;
		}

		//Called when the direction of the avatar has changed. Assuming it is properly subscribed in the editor.
		public void OnDirectionChanged(Vector2 direction)
		{
			//TODO: Refactor when it's not 3:00am
			if (Facing == FacingState.Backward)
			{
				//assume normalization, won't matter
				if(direction.x > 0) //TODO: Is this the best way to determine facing?
					gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x) * -1.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				else if(Math.Abs(direction.x) > float.Epsilon) //ignore 0 to make sure it doesn't change from other inputs
					gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			} else if (Facing == FacingState.Forward)
			{
				//assume normalization, won't matter
				if(direction.x > 0) //TODO: Is this the best way to determine facing?
					gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x), gameObject.transform.localScale.y, gameObject.transform.localScale.z);
				else if(Math.Abs(direction.x) > float.Epsilon) //ignore 0 to make sure it doesn't change from other inputs
					gameObject.transform.localScale = new Vector3(Mathf.Abs(gameObject.transform.localScale.x) * -1.0f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
			}

			if(SetFacingDependingOnDirection)
				SetFacingFromMovementDirection(direction);

			SetMaterialFacing();
		}

		private void SetMaterialFacing()
		{
			//Apply the current frame offset calculated
			GaiaAvatarRenderer.material.mainTextureOffset = new Vector2((isMoving ? ApplyMovementOffset(CurrentFrameOffset) : CurrentFrameOffset) / 10.0f, GaiaAvatarRenderer.material.mainTextureOffset.y);
		}

		private void SetFacingFromMovementDirection(Vector2 direction)
		{
			//If they're moving Y they are moving "backwards"
			if (direction.y > 0)
				SetFacingBackwards();
			else if (Math.Abs(direction.y) > float.Epsilon) //ignore 0 to make sure it doesn't change from other inputs
				SetFacingForwards();
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

		void Update()
		{
			//Sets the facing of the avatar towards the camera.
			//transform.LookAt(cachedCameraReference.transform.position);
			//transform.rotation = Quaternion.AngleAxis(-transform.rotation.eulerAngles.y, Vector3.up);
			transform.LookAt(transform.position + cachedCameraReference.transform.rotation * Vector3.forward,
				cachedCameraReference.transform.rotation * Vector3.up);
			transform.rotation = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up);

			//Now we check rotation of the root object
			//we can use this to determine which direction we should be facing.
			//float yAxisRotation = transform.root.rotation.eulerAngles.y;
			//float cameraYAxisRotation = cachedCameraReference.transform.root.rotation.eulerAngles.y;
			//float delta = Math.Abs(yAxisRotation - cameraYAxisRotation);
			float delta = Vector3.Angle(new Vector3(cachedCameraReference.transform.root.forward.x, 0.0f, cachedCameraReference.transform.root.forward.z), transform.root.forward);
			//Debug.Log($"yAxisRotation: {yAxisRotation} cameraYAxisRotation: {cameraYAxisRotation} delta: {delta}");

			//TODO: Cleanup
			int currentOffset = CurrentFrameOffset;
			//If the rotation is greater than 180 then the user is actually looking
			//towards us
			if(delta > 90.0f)
				SetFacingForwards();
			else
				SetFacingBackwards();

			if(currentOffset != CurrentFrameOffset)
				SetMaterialFacing();

			//target.position - player.position
			Vector3 positionDelta = (transform.position - lastPosition);
			OnDirectionChanged(new Vector2(positionDelta.x, positionDelta.z));
			lastPosition = transform.position;
		}
	}
}