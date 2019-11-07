using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	[SpellEffectTargetingStrategy(SpellEffectTargetType.TARGET_UNIT_TARGET_ENEMY, SpellEffectTargetType.NO_TARGET)]
	public sealed class SimpleEnemyTargetSpellEffectTargetSelector : BaseTargetSpellEffectSelector
	{
		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		public SimpleEnemyTargetSpellEffectTargetSelector([NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		public override SpellEffectTargetContext CalculateTargets([NotNull] SpellDefinitionDataModel spellDefinition, [NotNull] SpellEffectDefinitionDataModel spellEffect, [NotNull] DefaultEntityActorStateContainer actorState, [NotNull] IPendingSpellCastData pendingSpellCast)
		{
			if (spellDefinition == null) throw new ArgumentNullException(nameof(spellDefinition));
			if (spellEffect == null) throw new ArgumentNullException(nameof(spellEffect));
			if (actorState == null) throw new ArgumentNullException(nameof(actorState));
			if (pendingSpellCast == null) throw new ArgumentNullException(nameof(pendingSpellCast));

			return new SpellEffectTargetContext(SingleTargetEnemySnapshotTarget(pendingSpellCast));
		}

		public IEnumerable<IActorRef> SingleTargetEnemySnapshotTarget([NotNull] IPendingSpellCastData pendingSpellData)
		{
			if (pendingSpellData == null) throw new ArgumentNullException(nameof(pendingSpellData));

			yield return ActorReferenceMappable.RetrieveEntity(pendingSpellData.SnapshotEntityTarget);
		}
	}
}
