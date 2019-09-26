using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.SDK
{
	public sealed class GameObjectDefinitionData : GladMMOSDKMonoBehaviour, IUploadedContentDataDefinition
	{
		[Tooltip("The unique gameobject model id.")]
		[SerializeField]
		private long _GameObjectModelId;

		[Tooltip("The unique content identifier for the content.")]
		[SerializeField]
		private string _GameObjectModelGuid;

		/// <summary>
		/// The GUID for the creature content.
		/// </summary>
		public Guid ContentGuid
		{
			get
			{
				if(Guid.TryParse(_GameObjectModelGuid, out Guid guidResult))
					return guidResult;
				else
					return Guid.Empty;
			}
			internal set => _GameObjectModelGuid = value.ToString();
		}

		/// <summary>
		/// The identifier for the creature model.
		/// </summary>
		public long ContentId
		{
			get => _GameObjectModelId;
			internal set => _GameObjectModelId = value;
		}
	}
}
