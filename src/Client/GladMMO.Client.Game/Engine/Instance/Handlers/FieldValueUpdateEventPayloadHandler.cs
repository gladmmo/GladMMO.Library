using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;
using Reinterpret.Net;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class FieldValueUpdateEventPayloadHandler : BaseGameClientGameMessageHandler<FieldValueUpdateEvent>
	{
		public IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		/// <inheritdoc />
		public FieldValueUpdateEventPayloadHandler(ILog logger,
			[NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection)
			: base(logger)
		{
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
		}

		public void GenerateFieldUpdateDiff(FieldValueUpdate fieldsCollection, [NotNull] IChangeTrackableEntityDataCollection changeTrackable)
		{
			if(changeTrackable == null) throw new ArgumentNullException(nameof(changeTrackable));

			lock(changeTrackable.SyncObj)
			{
				int updateDiffIndex = 0;
				foreach(int setIndex in fieldsCollection.FieldValueUpdateMask.EnumerateSetBitsByIndex())
				{
					changeTrackable.SetFieldValue(setIndex, fieldsCollection.FieldValueUpdates.ElementAt(updateDiffIndex));

					//Hey, so there was a bug for 8byte values that caused this to break.
					//I know it's bad design but we all have deadlines here. We NEED this to FORCEIBLY
					//make it appear as if it's changed even if it hasn't. Otherwise the change may not get dispatched.
					//Trust me, these hacks will be hidden deep in networking engine code like this that runs on another thread
					//that nobody understands. If you're here, then you understand. Tell them only that the Lich King is dead
					//and that Bolvar Fordragon died with him.
					changeTrackable.ChangeTrackingArray.Set(setIndex, true);
					updateDiffIndex++;
				}
			}
		}

		public override Task HandleMessage(IPeerMessageContext<GameClientPacketPayload> context, FieldValueUpdateEvent payload)
		{
			foreach (var entry in payload.FieldValueUpdates)
			{
				GenerateFieldUpdateDiff(entry.Data, ChangeTrackableCollection.RetrieveEntity(entry.EntityGuid));
			}

			return Task.CompletedTask;
		}
	}
}