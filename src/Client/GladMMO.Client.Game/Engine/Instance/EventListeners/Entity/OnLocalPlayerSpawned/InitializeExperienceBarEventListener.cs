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
		private IUIFillable ExperienceBar { get; }

		public InitializeExperienceBarEventListener(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			IEntityDataChangeCallbackRegisterable entityDataCallbackRegister, 
			IReadonlyLocalPlayerDetails playerDetails,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.ExperienceBar)] IUIFillableImage experienceBar) 
			: base(subscriptionService, entityDataCallbackRegister, playerDetails)
		{
			ExperienceBar = experienceBar ?? throw new ArgumentNullException(nameof(experienceBar));
		}

		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			//Even if we don't know the experience yet, we should initialize. It could end up as 0 but that's ok.
			OnPlayerExperienceChanged(args.EntityGuid, new EntityDataChangedArgs<int>(0, 0));

			RegisterPlayerDataChangeCallback<int>(EPlayerFields.PLAYER_XP, OnPlayerExperienceChanged);
			RegisterPlayerDataChangeCallback<int>(EPlayerFields.PLAYER_NEXT_LEVEL_XP, OnPlayerExperienceChanged);
		}

		private void OnPlayerExperienceChanged(ObjectGuid entity, EntityDataChangedArgs<int> changeArgs)
		{
			//Just ignore the change args.
			int currentExperience = this.PlayerDetails.EntityData.GetFieldValue<int>(EPlayerFields.PLAYER_XP);
			int experienceToLevel = this.PlayerDetails.EntityData.GetFieldValue<int>(EPlayerFields.PLAYER_NEXT_LEVEL_XP);

			ExperienceBar.FillAmount = (float)currentExperience / experienceToLevel;
		}
	}
}
