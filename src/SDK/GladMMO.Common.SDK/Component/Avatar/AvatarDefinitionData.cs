using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.SDK
{
	[RequireComponent(typeof(AvatarBoneSDKData))]
	public sealed class AvatarDefinitionData : GladMMOSDKMonoBehaviour, IUploadedContentDataDefinition
	{
		[Tooltip("The unique avatar id.")]
		[SerializeField]
		private long _AvatarId;

		[Tooltip("The unique content identifier for the content.")]
		[SerializeField]
		private string _AvatarGuid;

		/// <summary>
		/// The GUID for the avatar content.
		/// </summary>
		public Guid ContentGuid
		{
			get => Guid.Parse(_AvatarGuid);
			internal set => _AvatarGuid = value.ToString();
		}

		/// <summary>
		/// The identifier for the avatar.
		/// </summary>
		public long ContentId
		{
			get => _AvatarId;
			internal set => _AvatarId = value;
		}
	}
}
