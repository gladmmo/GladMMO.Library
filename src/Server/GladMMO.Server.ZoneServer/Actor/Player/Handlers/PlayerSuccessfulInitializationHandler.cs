using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class PlayerSuccessfulInitializationHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, EntityActorInitializationSuccessMessage>
	{
		private ILog Logger { get; }

		private IReadonlyEntityGuidMappable<CharacterDataInstance> InitialCharacterDataMappable { get; }

		private IEntityExperienceLevelStrategy LevelStrategy { get; }

		public PlayerSuccessfulInitializationHandler([NotNull] ILog logger,
			[NotNull] IReadonlyEntityGuidMappable<CharacterDataInstance> initialCharacterDataMappable,
			[NotNull] IEntityExperienceLevelStrategy levelStrategy)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			InitialCharacterDataMappable = initialCharacterDataMappable ?? throw new ArgumentNullException(nameof(initialCharacterDataMappable));
			LevelStrategy = levelStrategy ?? throw new ArgumentNullException(nameof(levelStrategy));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, EntityActorInitializationSuccessMessage message)
		{
			//We should send ourselves the level initialization message because it takes care of stats
			CharacterDataInstance entity = InitialCharacterDataMappable.RetrieveEntity(state.EntityGuid);
			IEntityDataFieldContainer entityDataFieldContainer = state.EntityData;

			entityDataFieldContainer.SetFieldValue((int)PlayerObjectField.PLAYER_TOTAL_EXPERIENCE, entity.Experience);

			//Set the level from the initial experience data.
			messageContext.Entity.Tell(new SetEntityActorLevelMessage(LevelStrategy.ComputeLevelFromExperience(entity.Experience)));
		}
	}
}
