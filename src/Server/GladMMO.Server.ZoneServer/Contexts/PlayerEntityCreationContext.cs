using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	public sealed class PlayerEntityCreationContext : IEntityGuidContainer, IEntityCreationContext
	{
		/// <inheritdoc />
		public NetworkEntityGuid EntityGuid { get; }

		public PlayerEntitySessionContext SessionContext { get; }

		public Vector3 InitialPosition { get; }

		public float Orientation { get; }

		/// <inheritdoc />
		public EntityPrefab PrefabType { get; }

		/// <inheritdoc />
		public PlayerEntityCreationContext([NotNull] NetworkEntityGuid entityGuid, 
			[NotNull] PlayerEntitySessionContext sessionContext, 
			EntityPrefab prefabType, 
			Vector3 initialPosition, 
			float orientation)
		{
			if(!Enum.IsDefined(typeof(EntityPrefab), prefabType)) throw new InvalidEnumArgumentException(nameof(prefabType), (int)prefabType, typeof(EntityPrefab));
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			SessionContext = sessionContext ?? throw new ArgumentNullException(nameof(sessionContext));
			PrefabType = prefabType;
			InitialPosition = initialPosition;
			Orientation = orientation;
		}
	}
}
