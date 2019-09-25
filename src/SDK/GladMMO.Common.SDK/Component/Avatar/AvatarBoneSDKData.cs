using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GladMMO.FinalIK;

namespace GladMMO.SDK
{
	/// <summary>
	/// Component that is similar to VR-IK component
	/// that contains references to bones that can be automatically initialized.
	/// </summary>
	[RequireComponent(typeof(AvatarDefinitionData))]
	public sealed class AvatarBoneSDKData : MonoBehaviour, IAvatarIKReferenceContainer<CustomVRIKReferences>
	{
		/// <summary>
		/// Bone mapping. Right-click on the component header and select 'Auto-detect References' of fill in manually if not a Humanoid character. Chest, neck, shoulder and toe bones are optional. VRIK also supports legless characters. If you do not wish to use legs, leave all leg references empty.
		/// </summary>
		[ContextMenuItem("Auto-detect References", "AutoDetectReferences")]
		[Tooltip("Bone mapping. Right-click on the component header and select 'Auto-detect References' of fill in manually if not a Humanoid character. Chest, neck, shoulder and toe bones are optional. Also supports legless characters. If you do not wish to use legs, leave all leg references empty.")]
		public CustomVRIKReferences _references = new CustomVRIKReferences();

		/// <summary>
		/// Auto-detects bone references for this Avatar. Works with a Humanoid Animator on the gameobject only.
		/// </summary>
		[ContextMenu("Auto-detect Bones")]
		public void AutoDetectBones()
		{
			CustomVRIKReferences.AutoDetectReferences(transform, out _references);

			//TODO: This only works if the avatar is in TPOSE and is FACING FORWARD.
			//Now with the references, we can compute some the stored pre-computed rotations
			_references.LocalHeadRotation = _references.head.eulerAngles;
			_references.LocalLeftHandRotation = _references.leftHand.eulerAngles;
			_references.LocalRightHandRotation = _references.rightHand.eulerAngles;
		}

		/// <inheritdoc />
		public CustomVRIKReferences references => _references;
	}
}