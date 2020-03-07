using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using UnityEngine;

namespace GladMMO
{
	public sealed class DefaultEntityCreationContext : IEntityCreationContext
	{
		/// <inheritdoc />
		public ObjectGuid EntityGuid { get; }

		/// <inheritdoc />
		public Vector3 InitialPosition { get; }

		/// <inheritdoc />
		public float Orientation { get; }

		/// <inheritdoc />
		public EntityPrefab PrefabType { get; }

		/// <inheritdoc />
		public DefaultEntityCreationContext([NotNull] ObjectGuid entityGuid, Vector3 initialPosition, float orientation, EntityPrefab prefabType)
		{
			if(!Enum.IsDefined(typeof(EntityPrefab), prefabType)) throw new InvalidEnumArgumentException(nameof(prefabType), (int)prefabType, typeof(EntityPrefab));

			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			InitialPosition = initialPosition;
			Orientation = orientation;
			PrefabType = prefabType;
		}
	}
}
