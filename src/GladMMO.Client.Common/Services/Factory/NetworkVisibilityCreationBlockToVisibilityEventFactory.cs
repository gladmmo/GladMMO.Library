using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	public sealed class NetworkVisibilityCreationBlockToVisibilityEventFactory : IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block>
	{
		private IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		private IEntityGuidMappable<MovementBlockData> MovementBlockMappable { get; }

		/// <inheritdoc />
		public NetworkVisibilityCreationBlockToVisibilityEventFactory([NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection,
			[NotNull] IEntityGuidMappable<MovementBlockData> movementBlockMappable)
		{
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
			MovementBlockMappable = movementBlockMappable ?? throw new ArgumentNullException(nameof(movementBlockMappable));
		}

		/// <inheritdoc />
		public NetworkEntityNowVisibleEventArgs Create(ObjectUpdateCreateObject1Block context)
		{
			ObjectGuid guid = context.CreationData.CreationGuid.RawGuidValue;

			IEntityDataFieldContainer container = CreateInitialEntityFieldContainer(context.CreationData.ObjectValuesCollection);
			ChangeTrackingEntityFieldDataCollectionDecorator trackingEntityFieldDataCollectionDecorator = new ChangeTrackingEntityFieldDataCollectionDecorator(container, context.CreationData.ObjectValuesCollection.UpdateMask);

			ChangeTrackableCollection.AddObject(guid, trackingEntityFieldDataCollectionDecorator);
			MovementBlockMappable.AddObject(guid, context.CreationData.MovementData);

			return new NetworkEntityNowVisibleEventArgs(guid);
		}

		//Public and static because it's referenced in a unit test. That design oddity is
		//worth the absolute critical design fault that was uncovered and now protected by a test.
		public static unsafe IEntityDataFieldContainer CreateInitialEntityFieldContainer(UpdateFieldValueCollection fieldUpdateCollection)
		{
			//TODO: We could pool this.
			//we actually CAN'T use the field enum length or count. Since TrinityCore may send additional bytes at the end so that
			//it's evently divisible by 32.
			byte[] internalEntityDataBytes = new byte[fieldUpdateCollection.UpdateMask.Length * sizeof(int)];

			//It's absolutely CRITICAL that we don't use the sent fieldvalue internal bits for the set indication data
			//BECAUSE it also will be used as the initial changed values/change array. So we do the one-time copy here to avoid this critical
			//fault which is now covered by tests.
			byte[] copiedFieldUpdateMask = new byte[fieldUpdateCollection.UpdateMask.InternalIntegerArray.Count];
			Buffer.BlockCopy(fieldUpdateCollection.UpdateMask.InternalIntegerArray.ToArrayTryAvoidCopy(), 0, copiedFieldUpdateMask, 0, fieldUpdateCollection.UpdateMask.InternalIntegerArray.Count);

			IEntityDataFieldContainer t = new EntityFieldDataCollection(new WireReadyBitArray(copiedFieldUpdateMask), internalEntityDataBytes);

			//Taken from old TrinityCore branch.
			int updateDiffIndex = 0;
			foreach(int setIndex in t.DataSetIndicationArray.EnumerateSetBitsByIndex())
			{
				//TODO: Would it be faster to buffer copy?
				//The way wow works is these are 4 byte chunks
				for(int i = 0; i < 4; i++)
					internalEntityDataBytes[setIndex * sizeof(int) + i] = fieldUpdateCollection.UpdateDiffValues[updateDiffIndex * sizeof(int) + i];

				updateDiffIndex++;
			}

			return t;
		}
	}
}
