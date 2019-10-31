using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GladMMO.SDK;
using UnityEditor;
using UnityEngine;

namespace GladMMO
{
	[CustomEditor(typeof(AvatarBoneSDKData))]
	public class AvatarBoneSDKDataTypeDrawer : ExtendedMonoBehaviourTypeDrawer
	{
		internal static string sBoneName = "m_BoneName";
		internal static string sHumanName = "m_HumanName";

		private class BonePoseData
		{
			public Vector3 direction = Vector3.zero;
			public bool compareInGlobalSpace = false;
			public float maxAngle;
			public int[] childIndices = null;
			public Vector3 planeNormal = Vector3.zero;
			public BonePoseData(Vector3 dir, bool globalSpace, float maxAngleDiff)
			{
				direction = (dir == Vector3.zero ? dir : dir.normalized);
				compareInGlobalSpace = globalSpace;
				maxAngle = maxAngleDiff;
			}

			public BonePoseData(Vector3 dir, bool globalSpace, float maxAngleDiff, int[] children) : this(dir, globalSpace, maxAngleDiff)
			{
				childIndices = children;
			}

			public BonePoseData(Vector3 dir, bool globalSpace, float maxAngleDiff, Vector3 planeNormal, int[] children) : this(dir, globalSpace, maxAngleDiff, children)
			{
				this.planeNormal = planeNormal;
			}
		}

		private static BonePoseData[] sBonePoses = new BonePoseData[]
		{
			new BonePoseData(Vector3.up, true, 15),  // Hips,
			new BonePoseData(new Vector3(-0.05f, -1, 0),      true, 15),   // LeftUpperLeg,
			new BonePoseData(new Vector3(0.05f, -1, 0),      true, 15),    // RightUpperLeg,
			new BonePoseData(new Vector3(-0.05f, -1, -0.15f), true, 20),   // LeftLowerLeg,
			new BonePoseData(new Vector3(0.05f, -1, -0.15f), true, 20),    // RightLowerLeg,
			new BonePoseData(new Vector3(-0.05f, 0, 1),       true, 20, Vector3.up, null),   // LeftFoot,
			new BonePoseData(new Vector3(0.05f, 0, 1),       true, 20, Vector3.up, null),    // RightFoot,
			new BonePoseData(Vector3.up, true, 30, new int[] {(int)HumanBodyBones.Chest, (int)HumanBodyBones.UpperChest, (int)HumanBodyBones.Neck, (int)HumanBodyBones.Head}),  // Spine,
			new BonePoseData(Vector3.up, true, 30, new int[] {(int)HumanBodyBones.UpperChest, (int)HumanBodyBones.Neck, (int)HumanBodyBones.Head}),  // Chest,
			new BonePoseData(Vector3.up, true, 30),  // Neck,
			null, // Head,
			new BonePoseData(-Vector3.right, true, 20),  // LeftShoulder,
			new BonePoseData(Vector3.right, true, 20),   // RightShoulder,
			new BonePoseData(-Vector3.right, true, 05),  // LeftArm,
			new BonePoseData(Vector3.right, true, 05),   // RightArm,
			new BonePoseData(-Vector3.right, true, 05),  // LeftForeArm,
			new BonePoseData(Vector3.right, true, 05),   // RightForeArm,
			new BonePoseData(-Vector3.right, false, 10, Vector3.forward, new int[] {(int)HumanBodyBones.LeftMiddleProximal}),  // LeftHand,
			new BonePoseData(Vector3.right, false, 10, Vector3.forward, new int[] {(int)HumanBodyBones.RightMiddleProximal}),   // RightHand,
			null, // LeftToes,
			null, // RightToes,
			null, // LeftEye,
			null, // RightEye,
			null, // Jaw,
			new BonePoseData(new Vector3(-1, 0, 1), false, 10), // Left Thumb
			new BonePoseData(new Vector3(-1, 0, 1), false, 05),
			new BonePoseData(new Vector3(-1, 0, 1), false, 05),
			new BonePoseData(-Vector3.right, false, 10),  // Left Index
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 10),  // Left Middle
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 10),  // Left Ring
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 10),  // Left Little
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(-Vector3.right, false, 05),
			new BonePoseData(new Vector3(1, 0, 1), false, 10),  // Right Thumb
			new BonePoseData(new Vector3(1, 0, 1), false, 05),
			new BonePoseData(new Vector3(1, 0, 1), false, 05),
			new BonePoseData(Vector3.right, false, 10),   // Right Index
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 10),   // Right Middle
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 10),   // Right Ring
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 10),   // Right Little
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.right, false, 05),
			new BonePoseData(Vector3.up, true, 30, new int[] {(int)HumanBodyBones.Neck, (int)HumanBodyBones.Head}),  // UpperChest,
		};

		public override void OnInspectorGUI()
		{
			base.OnInspectorGUI();

			if (GUILayout.Button($"Auto-TPose"))
			{
				//Dictionary<Transform, bool> modelBones = GetModelBones(((MonoBehaviour)target).gameObject.transform, false, null);
				//BoneWrapper[] humanBones = GetHumanBones(modelBones);
				//MakePoseValid(humanBones);
				MakeHumanAvatar(((MonoBehaviour) target).gameObject.GetComponent<Animator>().avatar);
			}
		}

		//Based on: https://forum.unity.com/threads/modelimporter-how-to-import-as-humanoid.487727/
		/*public static void MakeHumanAvatar(Avatar avatar, bool reimportModel = true)
		{
			Type aEditorType = typeof(Editor).Assembly.GetType("UnityEditor.AvatarEditor");
			Editor editor = Editor.CreateEditor(avatar, aEditorType);

			var nonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

			aEditorType.GetMethod("SwitchToEditMode", nonPublicInstance).Invoke(editor, null);

			var mapperField = aEditorType.GetField("m_MappingEditor", nonPublicInstance);
			var mapperType = mapperField.FieldType;
			var mapper = mapperField.GetValue(editor);

			mapperType.GetMethod("PerformAutoMapping", nonPublicInstance).Invoke(mapper, null);
			mapperType.GetMethod("MakePoseValid", nonPublicInstance).Invoke(mapper, null);
			mapperType.GetMethod("Apply", nonPublicInstance).Invoke(mapper, null);

			aEditorType.GetMethod("SwitchToAssetMode", nonPublicInstance).Invoke(editor, null);

			if(!reimportModel) return;

			var modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(avatar)) as ModelImporter;
			if(modelImporter != null) modelImporter.SaveAndReimport();
		}*/

		public static void MakeHumanAvatar(Avatar avatar, bool reimportModel = true)
		{
			Type aEditorType = typeof(Editor).Assembly.GetType("UnityEditor.AvatarEditor");
			Editor editor = Editor.CreateEditor(avatar, aEditorType);

			var nonPublicInstance = BindingFlags.Instance | BindingFlags.NonPublic;

			aEditorType.GetMethod("SwitchToEditMode", nonPublicInstance).Invoke(editor, null);

			var mapperField = aEditorType.GetField("m_MappingEditor", nonPublicInstance);
			var mapperType = mapperField.FieldType;
			var mapper = mapperField.GetValue(editor);

			mapperType.GetMethod("PerformAutoMapping", nonPublicInstance).Invoke(mapper, null);
			mapperType.GetMethod("MakePoseValid", nonPublicInstance).Invoke(mapper, null);
			mapperType.GetMethod("Apply", nonPublicInstance).Invoke(mapper, null);

			aEditorType.GetMethod("SwitchToAssetMode", nonPublicInstance).Invoke(editor, null);

			if(!reimportModel) return;

			var modelImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(avatar)) as ModelImporter;
			if(modelImporter != null) modelImporter.SaveAndReimport();
		}

		public static BoneWrapper[] GetHumanBones(Dictionary<Transform, bool> actualBones)
		{
			string[] humanBoneNames = HumanTrait.BoneName;
			BoneWrapper[] bones = new BoneWrapper[humanBoneNames.Length];
			for(int i = 0; i < humanBoneNames.Length; i++)
			{
				Transform bone = null;

				var humanBoneName = humanBoneNames[i];
				bones[i] = new BoneWrapper(humanBoneName, bone);
			}
			return bones;
		}

		public static Dictionary<Transform, bool> GetModelBones(Transform root, bool includeAll, BoneWrapper[] humanBones)
		{
			if(root == null)
				return null;

			// Find out which transforms are actual bones and which are parents of actual bones
			Dictionary<Transform, bool> bones = new Dictionary<Transform, bool>();
			List<Transform> skinnedBones = new List<Transform>();

			if(!includeAll)
			{
				// Find out in advance which bones are used by SkinnedMeshRenderers
				SkinnedMeshRenderer[] skinnedMeshRenderers = root.GetComponentsInChildren<SkinnedMeshRenderer>();

				foreach(SkinnedMeshRenderer rend in skinnedMeshRenderers)
				{
					Transform[] meshBones = rend.bones;
					bool[] meshBonesUsed = new bool[meshBones.Length];
					BoneWeight[] weights = rend.sharedMesh.boneWeights;
					foreach(BoneWeight w in weights)
					{
						if(w.weight0 != 0)
							meshBonesUsed[w.boneIndex0] = true;
						if(w.weight1 != 0)
							meshBonesUsed[w.boneIndex1] = true;
						if(w.weight2 != 0)
							meshBonesUsed[w.boneIndex2] = true;
						if(w.weight3 != 0)
							meshBonesUsed[w.boneIndex3] = true;
					}
					for(int i = 0; i < meshBones.Length; i++)
					{
						if(meshBonesUsed[i])
							if(!skinnedBones.Contains(meshBones[i]))
								skinnedBones.Add(meshBones[i]);
					}
				}

				// Recursive call
				DetermineIsActualBone(root, bones, skinnedBones, false, humanBones);
			}

			// If not enough bones were found, fallback to treating all transforms as bones
			if(bones.Count < HumanTrait.RequiredBoneCount)
			{
				bones.Clear();
				skinnedBones.Clear();
				DetermineIsActualBone(root, bones, skinnedBones, true, humanBones);
			}

			return bones;
		}

		private static bool DetermineIsActualBone(Transform tr, Dictionary<Transform, bool> bones, List<Transform> skinnedBones, bool includeAll, BoneWrapper[] humanBones)
		{
			bool actualBone = includeAll;
			bool boneParent = false;
			bool boneChild = false;

			// Actual bone parent if any of children are bones
			int childBones = 0;
			foreach(Transform child in tr)
				if(DetermineIsActualBone(child, bones, skinnedBones, includeAll, humanBones))
					childBones++;

			if(childBones > 0)
				boneParent = true;
			if(childBones > 1)
				actualBone = true;

			// Actual bone if used by skinned mesh
			if(!actualBone)
				if(skinnedBones.Contains(tr))
					actualBone = true;

			// Actual bone if contains component other than transform
			if(!actualBone)
			{
				Component[] components = tr.GetComponents<Component>();
				if(components.Length > 1)
				{
					foreach(Component comp in components)
					{
						if((comp is Renderer) && !(comp is SkinnedMeshRenderer))
						{
							Bounds bounds = (comp as Renderer).bounds;

							// Double size of bounds in order to still make bone valid
							// if its pivot is just slightly outside of renderer bounds.
							bounds.extents = bounds.size;

							// If the parent is inside the bounds, this transform is probably
							// just a geometry dummy for the parent bone
							if(tr.childCount == 0 && tr.parent && bounds.Contains(tr.parent.position))
							{
								if(tr.parent.GetComponent<Renderer>() != null)
									actualBone = true;
								else
									boneChild = true;
							}
							// if not, give transform itself a chance.
							// If pivot is way outside of bounds, it's not an actual bone.
							else if(bounds.Contains(tr.position))
							{
								actualBone = true;
							}
						}
					}
				}
			}

			// Actual bone if the bone is define in human definition.
			if(!actualBone && humanBones != null)
			{
				foreach(var boneWrapper in humanBones)
				{
					if(tr == boneWrapper.bone)
					{
						actualBone = true;
						break;
					}
				}
			}

			if(actualBone)
				bones[tr] = true;
			else if(boneParent)
			{
				if(!bones.ContainsKey(tr))
					bones[tr] = false;
			}
			else if(boneChild)
				bones[tr.parent] = true;

			return bones.ContainsKey(tr);
		}

		public static void MakePoseValid(BoneWrapper[] bones)
		{
			Quaternion orientation = AvatarComputeOrientation(bones);
			for(int i = 0; i < sBonePoses.Length; i++)
			{
				MakeBoneAlignmentValid(bones, orientation, i);
				// Recalculate orientation after handling hips since they may have changed it
				if(i == (int)HumanBodyBones.Hips)
					orientation = AvatarComputeOrientation(bones);
			}

			// Move feet to ground plane
			MakeCharacterPositionValid(bones);
		}

		public static Quaternion AvatarComputeOrientation(BoneWrapper[] bones)
		{
			Transform leftUpLeg = bones[(int)HumanBodyBones.LeftUpperLeg].bone;
			Transform rightUpLeg = bones[(int)HumanBodyBones.RightUpperLeg].bone;
			Transform leftArm = bones[(int)HumanBodyBones.LeftUpperArm].bone;
			Transform rightArm = bones[(int)HumanBodyBones.RightUpperArm].bone;
			if(leftUpLeg != null && rightUpLeg != null && leftArm != null && rightArm != null)
				return AvatarComputeOrientation(leftUpLeg.position, rightUpLeg.position, leftArm.position, rightArm.position);
			else
				return Quaternion.identity;
		}

		public static Quaternion AvatarComputeOrientation(Vector3 leftUpLeg, Vector3 rightUpLeg, Vector3 leftArm, Vector3 rightArm)
		{
			Vector3 legsRightDir = Vector3.Normalize(rightUpLeg - leftUpLeg);
			Vector3 armsRightDir = Vector3.Normalize(rightArm - leftArm);
			Vector3 torsoRightDir = Vector3.Normalize(legsRightDir + armsRightDir);

			// Find out if torso right dir seems sensible or completely arbitrary.
			// It's sensible if it's aligned along some axis.
			bool sensibleOrientation =
				Mathf.Abs(torsoRightDir.x * torsoRightDir.y) < 0.05f &&
				Mathf.Abs(torsoRightDir.y * torsoRightDir.z) < 0.05f &&
				Mathf.Abs(torsoRightDir.z * torsoRightDir.x) < 0.05f;

			Vector3 legsAvgPos = (leftUpLeg + rightUpLeg) * 0.5f;
			Vector3 armsAvgPos = (leftArm + rightArm) * 0.5f;
			Vector3 torsoUpDir = Vector3.Normalize(armsAvgPos - legsAvgPos);

			// If the orientation is sensible, assume character up vector is aligned along x, y, or z axis, so fix it to closest axis
			if(sensibleOrientation)
			{
				int axisIndex = 0;
				for(int i = 1; i < 3; i++)
					if(Mathf.Abs(torsoUpDir[i]) > Mathf.Abs(torsoUpDir[axisIndex]))
						axisIndex = i;
				float sign = Mathf.Sign(torsoUpDir[axisIndex]);
				torsoUpDir = Vector3.zero;
				torsoUpDir[axisIndex] = sign;
			}

			Vector3 torsoForwardDir = Vector3.Cross(torsoRightDir, torsoUpDir);

			if(torsoForwardDir == Vector3.zero || torsoUpDir == Vector3.zero)
				return Quaternion.identity;

			return Quaternion.LookRotation(torsoForwardDir, torsoUpDir);
		}

		public static void MakeBoneAlignmentValid(BoneWrapper[] bones, Quaternion avatarOrientation, int boneIndex)
		{
			if(boneIndex < 0 || boneIndex >= sBonePoses.Length || boneIndex >= bones.Length)
				return;

			BoneWrapper bone = bones[boneIndex];
			BonePoseData pose = sBonePoses[boneIndex];
			if(bone.bone == null || pose == null)
				return;

			if(boneIndex == (int)HumanBodyBones.Hips)
			{
				float angleX = Vector3.Angle(avatarOrientation * Vector3.right, Vector3.right);
				float angleY = Vector3.Angle(avatarOrientation * Vector3.up, Vector3.up);
				float angleZ = Vector3.Angle(avatarOrientation * Vector3.forward, Vector3.forward);
				if(angleX > pose.maxAngle || angleY > pose.maxAngle || angleZ > pose.maxAngle)
					bone.bone.rotation = Quaternion.Inverse(avatarOrientation) * bone.bone.rotation;
				return;
			}

			Vector3 dir = GetBoneAlignmentDirection(bones, avatarOrientation, boneIndex);
			if(dir == Vector3.zero)
				return;
			Quaternion space = GetRotationSpace(bones, avatarOrientation, boneIndex);
			Vector3 goalDir = space * pose.direction;
			if(pose.planeNormal != Vector3.zero)
				dir = Vector3.ProjectOnPlane(dir, space * pose.planeNormal);

			// If the bone direction is not close enough to the target direction,
			// rotate it so it matches the target direction.
			float deltaAngle = Vector3.Angle(dir, goalDir);
			if(deltaAngle > pose.maxAngle * 0.99f)
			{
				Quaternion adjust = Quaternion.FromToRotation(dir, goalDir);

				// If this bone is hip or knee, remember global foor rotation and apply it after this adjustment
				Transform footBone = null;
				Quaternion footRot = Quaternion.identity;
				if(boneIndex == (int)HumanBodyBones.LeftUpperLeg || boneIndex == (int)HumanBodyBones.LeftLowerLeg)
					footBone = bones[(int)HumanBodyBones.LeftFoot].bone;
				if(boneIndex == (int)HumanBodyBones.RightUpperLeg || boneIndex == (int)HumanBodyBones.RightLowerLeg)
					footBone = bones[(int)HumanBodyBones.RightFoot].bone;
				if(footBone != null)
					footRot = footBone.rotation;

				// Adjust only enough to fall within maxAngle
				float adjustAmount = Mathf.Clamp01(1.05f - (pose.maxAngle / deltaAngle));
				adjust = Quaternion.Slerp(Quaternion.identity, adjust, adjustAmount);

				bone.bone.rotation = adjust * bone.bone.rotation;

				// Revert foot rotation to what it was
				if(footBone != null)
					footBone.rotation = footRot;
			}
		}

		private static Vector3 GetBoneAlignmentDirection(BoneWrapper[] bones, Quaternion avatarOrientation, int boneIndex)
		{
			if(sBonePoses[boneIndex] == null)
				return Vector3.zero;

			BoneWrapper bone = bones[boneIndex];
			Vector3 dir;

			// Get the child bone
			BonePoseData pose = sBonePoses[boneIndex];
			int childBoneIndex = -1;
			if(pose.childIndices != null)
			{
				foreach(int i in pose.childIndices)
				{
					if(bones[i].bone != null)
					{
						childBoneIndex = i;
						break;
					}
				}
			}
			else
			{
				childBoneIndex = GetHumanBoneChild(bones, boneIndex);
			}

			// TODO@MECANIM Something si wrong with the indexes
			//if (boneIndex == (int)HumanBodyBones.LeftHand)
			//  Debug.Log ("Child bone for left hand: "+childBoneIndex);

			if(childBoneIndex >= 0 && bones[childBoneIndex] != null && bones[childBoneIndex].bone != null)
			{
				// Get direction from bone to child
				BoneWrapper childBone = bones[childBoneIndex];
				dir = childBone.bone.position - bone.bone.position;

				// TODO@MECANIM Something si wrong with the indexes
				//if (boneIndex == (int)HumanBodyBones.LeftHand)
				//  Debug.Log (" - "+childBone.humanBoneName + " - " +childBone.bone.name);
			}
			else
			{
				if(bone.bone.childCount != 1)
					return Vector3.zero;

				dir = Vector3.zero;
				// Get direction from bone to child
				foreach(Transform child in bone.bone)
				{
					dir = child.position - bone.bone.position;
					break;
				}
			}

			return dir.normalized;
		}

		internal static void MakeCharacterPositionValid(BoneWrapper[] bones)
		{
			float error;
			Vector3 adjustVector = GetCharacterPositionAdjustVector(bones, out error);
			if(adjustVector != Vector3.zero)
				bones[(int)HumanBodyBones.Hips].bone.position += adjustVector;
		}

		private static Vector3 GetCharacterPositionAdjustVector(BoneWrapper[] bones, out float error)
		{
			error = 0;

			// Get hip bones
			Transform leftUpLeg = bones[(int)HumanBodyBones.LeftUpperLeg].bone;
			Transform rightUpLeg = bones[(int)HumanBodyBones.RightUpperLeg].bone;
			if(leftUpLeg == null || rightUpLeg == null)
				return Vector3.zero;
			Vector3 avgHipPos = (leftUpLeg.position + rightUpLeg.position) * 0.5f;

			// Get foot bones
			// Prefer toe bones but use foot bones if toes are not mapped
			bool usingToes = true;
			Transform leftFoot = bones[(int)HumanBodyBones.LeftToes].bone;
			Transform rightFoot = bones[(int)HumanBodyBones.RightToes].bone;
			if(leftFoot == null || rightFoot == null)
			{
				usingToes = false;
				leftFoot = bones[(int)HumanBodyBones.LeftFoot].bone;
				rightFoot = bones[(int)HumanBodyBones.RightFoot].bone;
			}
			if(leftFoot == null || rightFoot == null)
				return Vector3.zero;
			Vector3 avgFootPos = (leftFoot.position + rightFoot.position) * 0.5f;

			// Get approximate length of legs
			float hipsHeight = avgHipPos.y - avgFootPos.y;
			if(hipsHeight <= 0)
				return Vector3.zero;

			Vector3 adjustVector = Vector3.zero;

			// We can force the feet to be at an approximate good height.
			// But the feet might be at a perfect height from the start if the bind pose is good.
			// So only do it if the feet look like they're not at a good position from the beginning.
			// Check if feet are already at height that looks about right.
			if(avgFootPos.y < 0 || avgFootPos.y > hipsHeight * (usingToes ? 0.1f : 0.3f))
			{
				// Current height is not good, so adjust it using best guess based on human anatomy.
				float estimatedFootBottomHeight = avgHipPos.y - hipsHeight * (usingToes ? 1.03f : 1.13f);
				adjustVector.y = -estimatedFootBottomHeight;
			}

			// Move the avg hip pos to the center on the left-right axis if it's not already there.
			if(Mathf.Abs(avgHipPos.x) > 0.01f * hipsHeight)
				adjustVector.x = -avgHipPos.x;

			// Move the avg hip pos to the center on the front-back axis if it's not already approximately there.
			if(Mathf.Abs(avgHipPos.z) > 0.2f * hipsHeight)
				adjustVector.z = -avgHipPos.z;

			error = adjustVector.magnitude * 100 / hipsHeight;
			return adjustVector;
		}

		private static Quaternion GetRotationSpace(BoneWrapper[] bones, Quaternion avatarOrientation, int boneIndex)
		{
			Quaternion parentDelta = Quaternion.identity;
			BonePoseData pose = sBonePoses[boneIndex];
			if(!pose.compareInGlobalSpace)
			{
				int parentIndex = HumanTrait.GetParentBone(boneIndex);

				if(parentIndex > 0)
				{
					BonePoseData parentPose = sBonePoses[parentIndex];
					if(bones[parentIndex].bone != null && parentPose != null)
					{
						Vector3 parentDir = GetBoneAlignmentDirection(bones, avatarOrientation, parentIndex);
						if(parentDir != Vector3.zero)
						{
							Vector3 parentPoseDir = avatarOrientation * parentPose.direction;
							parentDelta = Quaternion.FromToRotation(parentPoseDir, parentDir);
						}
					}
				}
			}

			return parentDelta * avatarOrientation;
		}

		public static int GetHumanBoneChild(BoneWrapper[] bones, int boneIndex)
		{
			for(int i = 0; i < HumanTrait.BoneCount; i++)
				if(HumanTrait.GetParentBone(i) == boneIndex)
					return i;
			return -1;
		}

		[System.Serializable]
		public class BoneWrapper
		{
			private string m_HumanBoneName;
			public string humanBoneName { get { return m_HumanBoneName; } }
			public string error = string.Empty;
			public Transform bone;
			public BoneState state;

			public string messageName
			{
				get
				{
					return ObjectNames.NicifyVariableName(m_HumanBoneName) + " Transform '" + (bone ? bone.name : "None") + "'";
				}
			}

			public BoneWrapper(string humanBoneName, Transform bone)
			{
				this.m_HumanBoneName = humanBoneName;
				this.bone = bone;
				this.state = BoneState.Valid;
			}

			public void Reset(SerializedProperty humanBoneArray, Dictionary<Transform, bool> bones)
			{
				bone = null;
				SerializedProperty property = GetSerializedProperty(humanBoneArray, false);
				if(property != null)
				{
					string boneName = property.FindPropertyRelative(sBoneName).stringValue;
					bone = bones.Keys.FirstOrDefault(b => (b != null && b.name == boneName));
				}
				state = BoneState.Valid;
			}

			public void Serialize(SerializedProperty humanBoneArray)
			{
				if(bone == null)
				{
					DeleteSerializedProperty(humanBoneArray);
				}
				else
				{
					SerializedProperty property = GetSerializedProperty(humanBoneArray, true);
					if(property != null)
						property.FindPropertyRelative(sBoneName).stringValue = bone.name;
				}
			}

			protected void DeleteSerializedProperty(SerializedProperty humanBoneArray)
			{
				if(humanBoneArray == null || !humanBoneArray.isArray)
					return;

				for(int i = 0; i < humanBoneArray.arraySize; i++)
				{
					SerializedProperty humanNameP = humanBoneArray.GetArrayElementAtIndex(i).FindPropertyRelative(sHumanName);
					if(humanNameP.stringValue == humanBoneName)
					{
						humanBoneArray.DeleteArrayElementAtIndex(i);
						break;
					}
				}
			}

			public SerializedProperty GetSerializedProperty(SerializedProperty humanBoneArray, bool createIfMissing)
			{
				if(humanBoneArray == null || !humanBoneArray.isArray)
					return null;

				for(int i = 0; i < humanBoneArray.arraySize; i++)
				{
					SerializedProperty humanNameP = humanBoneArray.GetArrayElementAtIndex(i).FindPropertyRelative(sHumanName);
					if(humanNameP.stringValue == humanBoneName)
						return humanBoneArray.GetArrayElementAtIndex(i);
				}

				if(createIfMissing)
				{
					humanBoneArray.arraySize++;
					SerializedProperty bone = humanBoneArray.GetArrayElementAtIndex(humanBoneArray.arraySize - 1);
					if(bone != null)
					{
						bone.FindPropertyRelative(sHumanName).stringValue = humanBoneName;
						return bone;
					}
				}

				return null;
			}

			public const int kIconSize = 19;

			static Color kBoneValid = new Color(0, 0.75f, 0, 1.0f);
			static Color kBoneInvalid = new Color(1.0f, 0.3f, 0.25f, 1.0f);
			static Color kBoneInactive = Color.gray;
			static Color kBoneSelected = new Color(0.4f, 0.7f, 1.0f, 1.0f);
			static Color kBoneDrop = new Color(0.1f, 0.7f, 1.0f, 1.0f);
		}
	}
}
