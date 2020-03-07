using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO.SDK
{
	public sealed class CreatureDefinitionData : GladMMOSDKMonoBehaviour, IUploadedContentDataDefinition
	{
		[Tooltip("The unique creature model id.")]
		[SerializeField]
		private long _CreatureId;

		[Tooltip("The unique content identifier for the content.")]
		[SerializeField]
		private string _CreatureGuid;

		/// <summary>
		/// The GUID for the creature content.
		/// </summary>
		public Guid ContentGuid
		{
			get
			{
				if(Guid.TryParse(_CreatureGuid, out Guid guidResult))
					return guidResult;
				else
					return Guid.Empty;
			}
			set => _CreatureGuid = value.ToString();
		}

		/// <summary>
		/// The identifier for the creature model.
		/// </summary>
		public long ContentId
		{
			get => _CreatureId;
			set => _CreatureId = value;
		}
	}
}
