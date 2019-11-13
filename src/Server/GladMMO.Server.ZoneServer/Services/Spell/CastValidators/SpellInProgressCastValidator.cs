using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SpellCastValidator]
	public sealed class SpellInProgressCastValidator : ISpellCastValidator
	{
		private IReadonlyEntityGuidMappable<PendingSpellCastData> PendingSpellCastMappable { get; }

		private IReadonlyNetworkTimeService TimeService { get; }

		public SpellInProgressCastValidator([NotNull] IReadonlyEntityGuidMappable<PendingSpellCastData> pendingSpellCastMappable, [NotNull] IReadonlyNetworkTimeService timeService)
		{
			PendingSpellCastMappable = pendingSpellCastMappable ?? throw new ArgumentNullException(nameof(pendingSpellCastMappable));
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
		}

		public SpellCastResult ValidateSpellCast(DefaultEntityActorStateContainer state, SpellDefinitionDataModel spellDefinition)
		{
			if (PendingSpellCastMappable.ContainsKey(state.EntityGuid))
			{
				PendingSpellCastData pendingCast = PendingSpellCastMappable.RetrieveEntity(state.EntityGuid);

				if(!pendingCast.IsSpellcastFinished(TimeService.CurrentLocalTime))
					return SpellCastResult.SPELL_FAILED_SPELL_IN_PROGRESS;
			}

			return SpellCastResult.SPELL_FAILED_SUCCESS;
		}
	}
}
