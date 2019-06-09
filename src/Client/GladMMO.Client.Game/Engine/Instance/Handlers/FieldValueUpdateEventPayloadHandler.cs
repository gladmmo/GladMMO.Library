using System;
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