using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultPlayerEntityActor))]
	public sealed class AddPlayerExperienceActorMessageHandler : BaseEntityActorMessageHandler<DefaultEntityActorStateContainer, AddPlayerExperienceActorMessage>
	{
		private IEntityExperienceLevelStrategy LevelStrategy { get; }

		public AddPlayerExperienceActorMessageHandler([NotNull] IEntityExperienceLevelStrategy levelStrategy)
		{
			LevelStrategy = levelStrategy ?? throw new ArgumentNullException(nameof(levelStrategy));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultEntityActorStateContainer state, AddPlayerExperienceActorMessage message)
		{
			int newExperienceAmount = state.EntityData.GetFieldValue<int>(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE) + message.ExperienceAmount;

			//Just set the experience.
			state.EntityData.SetFieldValue(PlayerObjectField.PLAYER_TOTAL_EXPERIENCE, newExperienceAmount);

			int currentLevel = state.EntityData.GetFieldValue<int>(BaseObjectField.UNIT_FIELD_LEVEL);

			//We dinged! But don't assume it was only 1 level. Could be multiple
			if (currentLevel < LevelStrategy.ComputeLevelFromExperience(newExperienceAmount))
			{
				int newLevel = LevelStrategy.ComputeLevelFromExperience(newExperienceAmount);

				messageContext.Entity.Tell(new SetEntityActorLevelMessage(newLevel));
			}
		}
	}
}
