using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using UnityEngine;

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
			Debug.LogError($"EXPERIENCE BAR IS NOT REIMPLEMENTED YET DUE TO PLAYER_TOTAL_EXPERIENCE MISSING");

			//Even if we don't know the experience yet, we should initialize. It could end up as 0 but that's ok.
			/*int currentExperience = PlayerDetails.EntityData.GetFieldValue<int>(EPlayerFields.PLAYER_TOTAL_EXPERIENCE);
			OnPlayerExperienceChanged(args.EntityGuid, new EntityDataChangedArgs<int>(currentExperience, currentExperience));

			RegisterPlayerDataChangeCallback<int>(EPlayerFields.PLAYER_TOTAL_EXPERIENCE, OnPlayerExperienceChanged);*/
		}

		private void OnPlayerExperienceChanged(ObjectGuid entity, EntityDataChangedArgs<int> changeArgs)
		{
			//Just ignore the change args.
			int currentLevel = LevelStrategy.ComputeLevelFromExperience(changeArgs.NewValue);
			int currentExperience = changeArgs.NewValue;

			int experienceToLevel = LevelStrategy.TotalExperienceRequiredForLevel(currentLevel + 1) - LevelStrategy.TotalExperienceRequiredForLevel(currentLevel);
			int experienceIntoCurrentLevel = currentExperience - LevelStrategy.TotalExperienceRequiredForLevel(currentLevel);

			ExperienceBar.FillAmount = (float)experienceIntoCurrentLevel / experienceToLevel;
		}
	}
}
