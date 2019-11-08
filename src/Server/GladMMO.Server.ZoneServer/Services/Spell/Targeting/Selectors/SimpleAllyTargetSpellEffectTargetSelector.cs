using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	//TODO: Handle actual hostility calculations.
	[SpellEffectTargetingStrategy(SpellEffectTargetType.TARGET_UNIT_TARGET_ALLY, SpellEffectTargetType.NO_TARGET)]
	public sealed class SimpleAllyTargetSpellEffectTargetSelector : BaseTargetSpellEffectSelector
	{
		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		public SimpleAllyTargetSpellEffectTargetSelector([NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		public override SpellEffectTargetContext CalculateTargets([NotNull] SpellDefinitionDataModel spellDefinition, [NotNull] SpellEffectDefinitionDataModel spellEffect, [NotNull] DefaultEntityActorStateContainer actorState, [NotNull] IPendingSpellCastData pendingSpellCast)
		{
			if (spellDefinition == null) throw new ArgumentNullException(nameof(spellDefinition));
			if (spellEffect == null) throw new ArgumentNullException(nameof(spellEffect));
			if (actorState == null) throw new ArgumentNullException(nameof(actorState));
			if (pendingSpellCast == null) throw new ArgumentNullException(nameof(pendingSpellCast));

			return new SpellEffectTargetContext(ComputeAllyTargets(pendingSpellCast, actorState));
		}

		public IEnumerable<IActorRef> ComputeAllyTargets([NotNull] IPendingSpellCastData pendingSpellData, [NotNull] DefaultEntityActorStateContainer actorState)
		{
			if (pendingSpellData == null) throw new ArgumentNullException(nameof(pendingSpellData));
			if (actorState == null) throw new ArgumentNullException(nameof(actorState));

			//TODO: Handle hostility calculation. Right now it just only supports player targeting, and opts to self target if targeting a creature.
			yield return pendingSpellData.SnapshotEntityTarget.EntityType == EntityType.Player ? ActorReferenceMappable.RetrieveEntity(pendingSpellData.SnapshotEntityTarget) : ActorReferenceMappable.RetrieveEntity(actorState.EntityGuid);
		}
	}
}
