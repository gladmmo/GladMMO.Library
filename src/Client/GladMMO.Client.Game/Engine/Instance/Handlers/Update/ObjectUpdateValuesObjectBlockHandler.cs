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
	public sealed class ObjectUpdateValuesObjectBlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateValuesObjectBlock>
	{
		public IEntityGuidMappable<IChangeTrackableEntityDataCollection> ChangeTrackableCollection { get; }

		/// <inheritdoc />
		public ObjectUpdateValuesObjectBlockHandler(ILog logger,
			[NotNull] IEntityGuidMappable<IChangeTrackableEntityDataCollection> changeTrackableCollection)
			: base(ObjectUpdateType.UPDATETYPE_VALUES, logger)
		{
			ChangeTrackableCollection = changeTrackableCollection ?? throw new ArgumentNullException(nameof(changeTrackableCollection));
		}

		public void GenerateFieldUpdateDiff(UpdateFieldValueCollection fieldsCollection, [NotNull] IChangeTrackableEntityDataCollection changeTrackable)
		{
			if(changeTrackable == null) throw new ArgumentNullException(nameof(changeTrackable));

			lock(changeTrackable.SyncObj)
			{
				int updateDiffIndex = 0;
				foreach(int setIndex in fieldsCollection.UpdateMask.EnumerateSetBitsByIndex())
				{
					changeTrackable.SetFieldValue(setIndex, fieldsCollection.UpdateDiffValues.ElementAt(updateDiffIndex));

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

		/// <inheritdoc />
		public override void HandleUpdateBlock(ObjectUpdateValuesObjectBlock updateBlock)
		{
			//TODO: We should assume we know this
			if(ChangeTrackableCollection.ContainsKey(new ObjectGuid(updateBlock.ObjectToUpdate.RawGuidValue)))
				GenerateFieldUpdateDiff(updateBlock.UpdateValuesCollection, ChangeTrackableCollection[new ObjectGuid(updateBlock.ObjectToUpdate.RawGuidValue)]);
		}
	}
}