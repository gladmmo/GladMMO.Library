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
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ExperienceBar)] IUIFillable experienceBar) 
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			LevelStrategy = levelStrategy ?? throw new ArgumentNullException(nameof(levelStrategy));
			ExperienceBar = experienceBar ?? throw new ArgumentNullException(nameof(experienceBar));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			//Player spawns, we just want to rig up the experience bar callbacks
			//so it can be filled as we get more. To know how much we need for our level
			//though we actually have to compute it locally.
			int currentLevel = PlayerDetails.EntityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL);
			int currentExperience = PlayerDetails.EntityData.GetFieldValue<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE);

			int experienceToLevel = LevelStrategy.TotalExperienceRequiredForLevel(currentLevel + 1) - LevelStrategy.TotalExperienceRequiredForLevel(currentLevel);
			int experienceIntoCurrentLevel = currentExperience - LevelStrategy.TotalExperienceRequiredForLevel(currentLevel);

			//Set initial
			ExperienceBar.FillAmount = (float)experienceIntoCurrentLevel / experienceToLevel;

			RegisterPlayerDataChangeCallback<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE, OnPlayerExperienceChanged);
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
