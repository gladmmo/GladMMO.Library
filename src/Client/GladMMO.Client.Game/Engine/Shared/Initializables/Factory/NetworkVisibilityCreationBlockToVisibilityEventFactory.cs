using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	/*[AdditionalRegisterationAs(typeof(IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block>))]
	[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
	public sealed class NetworkVisibilityCreationBlockToVisibilityEventFactory : IGameInitializable, IFactoryCreatable<NetworkEntityNowVisibleEventArgs, ObjectUpdateCreateObject1Block>
	{
		private IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		private IEntityGuidMappable<IEntityDataFieldContainer> DataMappable { get; }

		private IEntityGuidMappable<MovementBlockData> MovementBlockMappable { get; }

		/// <inheritdoc />
		public NetworkVisibilityCreationBlockToVisibilityEventFactory([NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection,
			[NotNull] IEntityGuidMappable<IEntityDataFieldContainer> dataMappable,
			[NotNull] IEntityGuidMappable<MovementBlockData> movementBlockMappable)
		{
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
			DataMappable = dataMappable ?? throw new ArgumentNullException(nameof(dataMappable));
			MovementBlockMappable = movementBlockMappable ?? throw new ArgumentNullException(nameof(movementBlockMappable));
		}

		/// <inheritdoc />
		public NetworkEntityNowVisibleEventArgs Create(ObjectUpdateCreateObject1Block context)
		{
			NetworkEntityGuid guid = new NetworkEntityGuid(context.CreationData.CreationGuid);

			var initialContainer = DataMappable[guid] = CreateInitialEntityFieldContainer(context.CreationData);
			ChangeTrackableCollection[guid] = new ChangeTrackingEntityFieldDataCollectionDecorator(initialContainer, context.CreationData.ObjectValuesCollection.UpdateMask);
			MovementBlockMappable[guid] = context.CreationData.MovementData;

			return new NetworkEntityNowVisibleEventArgs(guid);
		}

		public IEntityDataFieldContainer CreateInitialEntityFieldContainer(ObjectCreationData creationData)
		{
			//TODO: We could pool this.
			//we actually CAN'T use the field enum length or count. Since TrinityCore may send additional bytes at the end so that
			//it's evently divisible by 32.
			byte[] internalEntityDataBytes = new byte[creationData.ObjectValuesCollection.UpdateMask.Length * sizeof(int)];
			IEntityDataFieldContainer t = new EntityFieldDataCollection<EUnitFields>(creationData.ObjectValuesCollection.UpdateMask, internalEntityDataBytes);

			int updateDiffIndex = 0;
			foreach(int setIndex in t.DataSetIndicationArray.EnumerateSetBitsByIndex())
			{
				//TODO: Would it be faster to buffer copy?
				//The way wow works is these are 4 byte chunks
				for(int i = 0; i < 4; i++)
					internalEntityDataBytes[setIndex * sizeof(int) + i] = creationData.ObjectValuesCollection.UpdateDiffValues[updateDiffIndex * sizeof(int) + i];

				updateDiffIndex++;
			}

			return t;
		}

		/// <inheritdoc />
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}*/
}
