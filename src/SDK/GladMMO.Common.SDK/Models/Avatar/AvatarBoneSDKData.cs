using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GladMMO.FinalIK;

namespace GladMMO.SDK
{
	/// <summary>
	/// Component that is similar to VR-IK component
	/// that contains references to bones that can be automatically initialized.
	/// </summary>
	public sealed class AvatarBoneSDKData : GladMMOSDKMonoBehaviour, IAvatarIKReferenceContainer<CustomVRIKReferences>
	{
		/// <summary>
		/// Bone mapping. Right-click on the component header and select 'Auto-detect References' of fill in manually if not a Humanoid character. Chest, neck, shoulder and toe bones are optional. VRIK also supports legless characters. If you do not wish to use legs, leave all leg references empty.
		/// </summary>
		[ContextMenuItem("Auto-detect References", "AutoDetectReferences")]
		[Tooltip("Bone mapping. Right-click on the component header and select 'Auto-detect References' of fill in manually if not a Humanoid character. Chest, neck, shoulder and toe bones are optional. Also supports legless characters. If you do not wish to use legs, leave all leg references empty.")]
		public CustomVRIKReferences _references = new CustomVRIKReferences();

		[Header("Height settings")]
		[SerializeField]
		public float FloatingNameHeight = 1.0f;

		[Range(0.0f, 5.0f)]
		[SerializeField]
		public float HeadHeight = 1.6f;

		/// <summary>
		/// Auto-detects bone references for this Avatar. Works with a Humanoid Animator on the gameobject only.
		/// </summary>
		[Button]
		[ContextMenu("Auto-detect Bones")]
		public void AutoDetectBones()
		{
			CustomVRIKReferences.AutoDetectReferences(transform, out _references);

			//TODO: This only works if the avatar is in TPOSE and is FACING FORWARD.
			//Now with the references, we can compute some the stored pre-computed rotations
			_references.LocalHeadRotation = _references.head.eulerAngles;
			_references.LocalLeftHandRotation = _references.leftHand.eulerAngles;
			_references.LocalRightHandRotation = _references.rightHand.eulerAngles;

			//Approximate the height of the head by the bone, but leave it as configurable for the user.
			//We also adjust for worldspace height of the root.
			if (_references.head != null)
				HeadHeight = _references.head.position.y - gameObject.transform.position.y;
		}

		[Button]
		public void ComputeNameHeight()
		{
			//This should be the maximum height bounds
			//of all renderers included in the avatar.
			FloatingNameHeight = gameObject.GetComponentsInChildren<Renderer>()
				.Concat(gameObject.GetComponents<Renderer>())
				.Select(r => r.bounds.max)
				.Max(vector3 => vector3.y);

			//We should consider the current worldspace height of the root object though.
			//as this can skew the results if it's not at 0,0,0 etc.
			FloatingNameHeight -= gameObject.transform.position.y;

			if(FloatingNameHeight > HeadHeight * 3.0f)
				Debug.LogWarning($"Floating name height is significantly higher than head height. Client may ignore this data.");
		}

		/// <inheritdoc />
		public CustomVRIKReferences references => _references;
	}
}