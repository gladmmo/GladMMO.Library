using System;
using Akka.Actor;

namespace GladMMO
{
	public class DefaultPendingSpellCastFactory : IPendingSpellCastFactory
	{
		private IReadonlyNetworkTimeService TimeService { get; }

		private IScheduler ActorScheduler { get; }

		private IReadonlySpellDataCollection SpellDataCollection { get; }

		public DefaultPendingSpellCastFactory([NotNull] IReadonlyNetworkTimeService timeService, 
			[NotNull] IScheduler actorScheduler, 
			[NotNull] IReadonlySpellDataCollection spellDataCollection)
		{
			TimeService = timeService ?? throw new ArgumentNullException(nameof(timeService));
			ActorScheduler = actorScheduler ?? throw new ArgumentNullException(nameof(actorScheduler));
			SpellDataCollection = spellDataCollection ?? throw new ArgumentNullException(nameof(spellDataCollection));
		}

		public PendingSpellCastData Create([NotNull] PendingSpellCastCreationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			ICancelable pendingSpellCastCancelable = new Cancelable(ActorScheduler);
			SpellDefinitionDataModel definition = SpellDataCollection.GetSpellDefinition(context.SpellId);

			//We need to compute a timespan for the pending cast from the definition casting time.
			TimeSpan castTimeSpan = definition.CastTime == 0 ? TimeSpan.Zero : TimeSpan.FromMilliseconds(definition.CastTime);
			long startCastTime = TimeService.CurrentLocalTime;
			long expectedFinishTime = startCastTime + castTimeSpan.Ticks;

			return new PendingSpellCastData(startCastTime, expectedFinishTime, context.SpellId, pendingSpellCastCancelable, castTimeSpan, context.CurrentTarget);
		}
	}
}