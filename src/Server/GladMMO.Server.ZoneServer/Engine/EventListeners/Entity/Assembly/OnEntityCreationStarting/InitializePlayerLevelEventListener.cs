﻿using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	//Basically a stub for where/how to implement player level initialization.
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializePlayerLevelEventListener : PlayerCreationStartingEventListener
	{
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IEntityExperienceLevelStrategy LevelStrategy { get; }

		private IReadonlyEntityGuidMappable<CharacterDataInstance> InitialCharacterDataMappable { get; }

		public InitializePlayerLevelEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IEntityExperienceLevelStrategy levelStrategy,
			[NotNull] IReadonlyEntityGuidMappable<CharacterDataInstance> initialCharacterDataMappable) 
			: base(subscriptionService)
		{
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			LevelStrategy = levelStrategy ?? throw new ArgumentNullException(nameof(levelStrategy));
			InitialCharacterDataMappable = initialCharacterDataMappable ?? throw new ArgumentNullException(nameof(initialCharacterDataMappable));
		}

		protected override void OnPlayerEntityCreationStarting(EntityCreationStartingEventArgs args)
		{
			CharacterDataInstance entity = InitialCharacterDataMappable.RetrieveEntity(args.EntityGuid);

			IEntityDataFieldContainer entityDataFieldContainer = EntityDataMappable.RetrieveEntity(args.EntityGuid);

			//Set the level from the initial experience data.
			entityDataFieldContainer.SetFieldValue((int)BaseObjectField.UNIT_FIELD_LEVEL, LevelStrategy.ComputeLevelFromExperience(entity.Experience));
			entityDataFieldContainer.SetFieldValue((int)PlayerObjectField.PLAYER_TOTAL_EXPERIENCE, entity.Experience);
		}
	}
}