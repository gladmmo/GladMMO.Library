using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeExperienceBarEventListener : DataChangedLocalPlayerSpawnedEventListener
	{
		private IEntityExperienceLevelStrategy LevelStrategy { get; }

		private IUIFillable ExperienceBar { get; }

		public InitializeExperienceBarEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] IEntityExperienceLevelStrategy levelStrategy,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ExperienceBar)] IUIFillableImage experienceBar) 
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			LevelStrategy = levelStrategy ?? throw new ArgumentNullException(nameof(levelStrategy));
			ExperienceBar = experienceBar ?? throw new ArgumentNullException(nameof(experienceBar));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			RegisterPlayerDataChangeCallback<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE, OnPlayerExperienceChanged);

			if (PlayerDetails.EntityData.DataSetIndicationArray.Get((int) PlayerObjectField.PLAYER_TOTAL_EXPERIENCE))
			{
				int currentExperience = PlayerDetails.EntityData.GetFieldValue<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE);
				OnPlayerExperienceChanged(args.EntityGuid, new EntityDataChangedArgs<int>(currentExperience, currentExperience));
			}
		}

		private void OnPlayerExperienceChanged(NetworkEntityGuid entity, EntityDataChangedArgs<int> changeArgs)
		{
			//Just ignore the change args.
			int currentLevel = PlayerDetails.EntityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL);
			int currentExperience = changeArgs.NewValue;

			int experienceToLevel = LevelStrategy.TotalExperienceRequiredForLevel(currentLevel + 1) - LevelStrategy.TotalExperienceRequiredForLevel(currentLevel);
			int experienceIntoCurrentLevel = currentExperience - LevelStrategy.TotalExperienceRequiredForLevel(currentLevel);

			ExperienceBar.FillAmount = (float)experienceIntoCurrentLevel / experienceToLevel;
		}
	}
}
