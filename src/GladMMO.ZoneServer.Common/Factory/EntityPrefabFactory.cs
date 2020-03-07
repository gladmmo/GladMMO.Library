using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	/// <summary>
	/// Simplified interface for <see cref="IFactoryCreatable{TCreateType,TContextType}"/>
	/// </summary>
	public interface IEntityPrefabFactory : IFactoryCreatable<GameObject, EntityPrefab>
	{

	}

	//TODO: This might not be something that should be common between client/server
	public sealed class EntityPrefabFactory : IEntityPrefabFactory, IFactoryCreatable<GameObject, EntityPrefab>
	{
		/// <inheritdoc />
		public GameObject Create(EntityPrefab context)
		{
			switch(context)
			{
				case EntityPrefab.Unknown:
					break;
				case EntityPrefab.LocalPlayer:
					//TODO: Hack to support VR builds with in-editor non-VR.
					if(Application.isEditor)
					{
						//TODO: We should handle prefabs better
						return Resources.Load<GameObject>("Prefabs/LocalPlayer");
						//return Resources.Load<GameObject>("Prefabs/LocalPlayer_vr");
					}
					else
					{
						//TODO: Renable VR builds someday
						//return Resources.Load<GameObject>("Prefabs/LocalPlayer_vr");
						return Resources.Load<GameObject>("Prefabs/LocalPlayer");
					}
				case EntityPrefab.RemotePlayer:
					//TODO: We should handle prefabs better
					return Resources.Load<GameObject>("Prefabs/RemotePlayer");
				case EntityPrefab.NetworkNpc:
					return Resources.Load<GameObject>("Prefabs/NetworkNpc");
				case EntityPrefab.NetworkGameObject:
					return Resources.Load<GameObject>("Prefabs/NetworkGameObject");
				case EntityPrefab.MessageBoxText:
					return Resources.Load<GameObject>("Prefabs/ChatWindowText");
				case EntityPrefab.CharacterFriendSlot:
					return Resources.Load<GameObject>("Prefabs/Character_Friend_Slot");
				case EntityPrefab.CharacterGuildSlot:
					return Resources.Load<GameObject>("Prefabs/Character_Guild_Slot");
			}

			throw new NotImplementedException($"Failed to load prefab for {nameof(EntityPrefab)}: {context}");
		}
	}
}
