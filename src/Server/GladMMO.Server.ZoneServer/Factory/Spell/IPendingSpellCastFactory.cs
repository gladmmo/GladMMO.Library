using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;

namespace GladMMO
{
	public sealed class PendingSpellCastCreationContext
	{
		public int SpellId { get; }

		public PendingSpellCastCreationContext(int spellId)
		{
			if (spellId < 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
		}
	}

	public interface IPendingSpellCastFactory : IFactoryCreatable<PendingSpellCastData, PendingSpellCastCreationContext>
	{

	}

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
			TimeSpan castTimeSpan = TimeSpan.FromMilliseconds(definition.CastTime);

			return new PendingSpellCastData(TimeService.CurrentLocalTime, context.SpellId, pendingSpellCastCancelable, castTimeSpan);
		}
	}
}
