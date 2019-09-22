using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class NetworkVisibilityCreationBlockToVisibilityEventFactory : IFactoryCreatable<NetworkEntityNowVisibleEventArgs, EntityCreationData>
	{
		private IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		private IEntityGuidMappable<IEntityDataFieldContainer> DataMappable { get; }

		private IEntityGuidMappable<IMovementData> MovementBlockMappable { get; }

		/// <inheritdoc />
		public NetworkVisibilityCreationBlockToVisibilityEventFactory([NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection,
			[NotNull] IEntityGuidMappable<IEntityDataFieldContainer> dataMappable,
			[NotNull] IEntityGuidMappable<IMovementData> movementBlockMappable)
		{
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
			DataMappable = dataMappable ?? throw new ArgumentNullException(nameof(dataMappable));
			MovementBlockMappable = movementBlockMappable ?? throw new ArgumentNullException(nameof(movementBlockMappable));
		}

		/// <inheritdoc />
		public NetworkEntityNowVisibleEventArgs Create(EntityCreationData context)
		{
			NetworkEntityGuid guid = context.EntityGuid;

			//Build the update values stuff and initialize the initial movement data.
			DataMappable.AddObject(guid, CreateInitialEntityFieldContainer(context.InitialFieldValues));
			ChangeTrackableCollection.AddObject(guid, new ChangeTrackingEntityFieldDataCollectionDecorator(DataMappable.RetrieveEntity(guid), context.InitialFieldValues.FieldValueUpdateMask));
			MovementBlockMappable.AddObject(guid, context.InitialMovementData);

			return new NetworkEntityNowVisibleEventArgs(guid);
		}

		public unsafe IEntityDataFieldContainer CreateInitialEntityFieldContainer(FieldValueUpdate fieldValueData)
		{
			//TODO: We could pool this.
			//we actually CAN'T use the field enum length or count. Since TrinityCore may send additional bytes at the end so that
			//it's evently divisible by 32.
			byte[] internalEntityDataBytes = new byte[fieldValueData.FieldValueUpdateMask.Length * sizeof(int)];
			IEntityDataFieldContainer t = new EntityFieldDataCollection<EUnitFields>(fieldValueData.FieldValueUpdateMask, internalEntityDataBytes);

			int updateDiffIndex = 0;
			foreach(int setIndex in t.DataSetIndicationArray.EnumerateSetBitsByIndex())
			{
				int value = fieldValueData.FieldValueUpdates.ElementAt(updateDiffIndex);
				byte* bytes = (byte*)&value;

				//TODO: Would it be faster to buffer copy?
				//The way wow works is these are 4 byte chunks
				for (int i = 0; i < 4; i++)
					internalEntityDataBytes[setIndex * sizeof(int) + i] = *(bytes + i);

				updateDiffIndex++;
			}

			return t;
		}
	}
}
