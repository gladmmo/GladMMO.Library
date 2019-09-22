using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Data component for the World/Scene data link.
	/// </summary>
	public sealed class WorldDefinitionData : GladMMOSDKMonoBehaviour
	{
		[Tooltip("The unique world id.")]
		[SerializeField]
		private long _WorldId;

		[Tooltip("The unique content identifier for the content.")]
		[SerializeField]
		private string _WorldGuid;

		/// <summary>
		/// The GUID for the world content.
		/// </summary>
		public string WorldGuid
		{
			get => _WorldGuid;
			internal set => _WorldGuid = value;
		}

		/// <summary>
		/// The identifier for the world.
		/// </summary>
		public long WorldId
		{
			get => _WorldId;
			internal set => _WorldId = value;
		}

		[Button]
		public void UnlinkContent()
		{
			_WorldId = 0;
			_WorldGuid = null;
			GameObject.DestroyImmediate(this.gameObject);
		}
	}
}
