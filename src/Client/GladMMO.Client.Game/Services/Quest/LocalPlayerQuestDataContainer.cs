using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(ILocalPlayerQuestAddedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class LocalPlayerQuestDataContainer : IGameStartable, ILocalPlayerQuestAddedEventSubscribable
	{
		private ILocalPlayerDetails PlayerDetails { get; }

		private IEntityDataChangeCallbackRegisterable DataChangeCallbackRegister { get; }

		private ILog Logger { get; }

		public event EventHandler<LocalPlayerQuestAddedEventArgs> OnLocalPlayerQuestAdded;

		public LocalPlayerQuestDataContainer([NotNull] ILocalPlayerDetails playerDetails, 
			[NotNull] IEntityDataChangeCallbackRegisterable dataChangeCallbackRegister,
			[NotNull] ILog logger)
		{
			PlayerDetails = playerDetails ?? throw new ArgumentNullException(nameof(playerDetails));
			DataChangeCallbackRegister = dataChangeCallbackRegister ?? throw new ArgumentNullException(nameof(dataChangeCallbackRegister));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public Task OnGameStart()
		{
			for (int i = (int) EPlayerFields.PLAYER_QUEST_LOG_1_1; i < (int) EPlayerFields.PLAYER_QUEST_LOG_25_1; i += 5)
			{
				//Required for lambda capture.
				var i1 = i;
				DataChangeCallbackRegister.RegisterCallback<int>(PlayerDetails.LocalPlayerGuid, i, (e, args) => OnQuestLogQuestIdChanged(e, (EPlayerFields)i1, args));
			}

			return Task.CompletedTask;
		}

		private void OnQuestLogQuestIdChanged(ObjectGuid entity, EPlayerFields playerFieldChanged, EntityDataChangedArgs<int> questIdSlotChanged)
		{
			//Quest slot starts at 0.
			int questSlot = ((int)playerFieldChanged - (int) EPlayerFields.PLAYER_QUEST_LOG_1_1) / 5; //5 field counts for quests.

			if (Logger.IsInfoEnabled)
				Logger.Info($"Quest Slot Changed: {questIdSlotChanged.OriginalValue} now {questIdSlotChanged.NewValue}");

			//0 would probably be remove/abandon
			//Therefore only broadcast Quest Add if it has a valid id.
			if (questIdSlotChanged.NewValue != 0)
			{
				OnLocalPlayerQuestAdded?.Invoke(this, new LocalPlayerQuestAddedEventArgs(questSlot, questIdSlotChanged.NewValue));
			}
		}

		public bool HasStartedQuest(int questId)
		{
			return CurrentQuests().Contains(questId);
		}

		private IEnumerable<int> CurrentQuests()
		{
			for (int i = (int) EPlayerFields.PLAYER_QUEST_LOG_1_1; i < (int) EPlayerFields.PLAYER_QUEST_LOG_25_1; i += 5)
			{
				if (PlayerDetails.EntityData.DataSetIndicationArray.Get(i))
				{
					int id = PlayerDetails.EntityData.GetFieldValue<int>(i);
					if (id != 0)
						yield return id;
				}
			}
		}
	}
}
