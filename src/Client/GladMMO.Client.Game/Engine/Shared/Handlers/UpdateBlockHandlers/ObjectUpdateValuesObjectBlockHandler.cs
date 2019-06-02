using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using Reinterpret.Net;

namespace GladMMO
{
	/*public sealed class ObjectUpdateValuesObjectBlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateValuesObjectBlock>
	{
		public IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		/// <inheritdoc />
		public ObjectUpdateValuesObjectBlockHandler(ILog logger,
			[NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection)
			: base(ObjectUpdateType.UPDATETYPE_VALUES, logger)
		{
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
		}


		public void Test(UpdateFieldValueCollection fieldsCollection, [NotNull] IChangeTrackableEntityDataCollection changeTrackable)
		{
			if(changeTrackable == null) throw new ArgumentNullException(nameof(changeTrackable));

			lock(changeTrackable.SyncObj)
			{
				int updateDiffIndex = 0;
				foreach(int setIndex in fieldsCollection.UpdateMask.EnumerateSetBitsByIndex())
				{
					changeTrackable.SetFieldValue(setIndex, fieldsCollection.UpdateDiffValues.Reinterpret<int>(updateDiffIndex * sizeof(int)));
					updateDiffIndex++;
				}
			}
		}

		/// <inheritdoc />
		public override void HandleUpdateBlock(ObjectUpdateValuesObjectBlock updateBlock)
		{
			//TODO: We should assume we know this
			if(ChangeTrackableCollection.ContainsKey(new NetworkEntityGuid(updateBlock.ObjectToUpdate.RawGuidValue)))
				Test(updateBlock.UpdateValuesCollection, ChangeTrackableCollection[new NetworkEntityGuid(updateBlock.ObjectToUpdate.RawGuidValue)]);
		}
	}*/
}
