using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultCreatureEntityActor))]
	public sealed class CreatureSuccessfulInitializationHandler : BaseEntityActorMessageHandler<DefaultCreatureActorState, EntityActorInitializationSuccessMessage>
	{
		private ILog Logger { get; }

		//Random is not thread safe so it's critical we make it thread static.
		[ThreadStatic]
		private static readonly Random CreatureInitializationRandomGenerator = new Random();

		public CreatureSuccessfulInitializationHandler([NotNull] ILog logger)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, DefaultCreatureActorState state, EntityActorInitializationSuccessMessage message)
		{
			//We need to initialize our replicateable state here.
			int creatureLevel = CreatureInitializationRandomGenerator.Next(state.TemplateModel.MinimumLevel, state.TemplateModel.MaximumLevel);

			//We should send ourselves the level initialization message because it takes care of stats
			messageContext.Entity.Tell(new SetEntityActorLevelMessage(creatureLevel));
		}
	}
}
