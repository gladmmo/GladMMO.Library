using System;
using System.Collections.Generic;
using System.Text;
using Akka.Actor;
using Akka.DI.Core;
using Common.Logging;

namespace GladMMO
{
	[EntityActorMessageHandler(typeof(DefaultWorldActor))]
	public sealed class WorldActorCreatureCreateEntityActorMessageHandler : BaseEntityActorMessageHandler<WorldActorState, CreateCreatureEntityActorMessage>
	{
		private ILog Logger { get; }

		private IDependencyResolver Resolver { get; }

		private IEntityGuidMappable<IActorRef> ActorRefMappable { get; }

		//TODO: Move to factory.
		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IReadonlyEntityGuidMappable<CreatureInstanceModel> CreatureInstanceMappable { get; }

		private IReadonlyEntityGuidMappable<CreatureTemplateModel> CreatureTemplateMappable { get; }

		private IReadonlyEntityGuidMappable<InterestCollection> InterestMappable { get; }

		public WorldActorCreatureCreateEntityActorMessageHandler([NotNull] ILog logger,
			[NotNull] IDependencyResolver resolver,
			[NotNull] IEntityGuidMappable<IActorRef> actorRefMappable,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<CreatureInstanceModel> creatureInstanceMappable,
			[NotNull] IReadonlyEntityGuidMappable<CreatureTemplateModel> creatureTemplateMappable,
			[NotNull] IReadonlyEntityGuidMappable<InterestCollection> interestMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
			ActorRefMappable = actorRefMappable ?? throw new ArgumentNullException(nameof(actorRefMappable));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			CreatureInstanceMappable = creatureInstanceMappable ?? throw new ArgumentNullException(nameof(creatureInstanceMappable));
			CreatureTemplateMappable = creatureTemplateMappable ?? throw new ArgumentNullException(nameof(creatureTemplateMappable));
			InterestMappable = interestMappable ?? throw new ArgumentNullException(nameof(interestMappable));
		}

		protected override void HandleMessage(EntityActorMessageContext messageContext, WorldActorState state, CreateCreatureEntityActorMessage message)
		{
			if(message.EntityGuid.EntityType != EntityType.Creature)
				throw new InvalidOperationException($"Tried to create Creature Actor for non-Creature Entity: {message.EntityGuid}");

			try
			{
				CreateActor(state, message);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Failed to create Actor: {e.Message}\n\nStack: {e.StackTrace}");
				throw;
			}
		}

		private void CreateActor(WorldActorState state, CreateCreatureEntityActorMessage message)
		{
			//Create the actor and tell it to initialize.
			IActorRef actorRef = state.WorldActorFactory.ActorOf(Resolver.Create<DefaultCreatureEntityActor>(), message.EntityGuid.RawGuidValue.ToString());

			//TODO: Move to factory.
			CreatureInstanceModel instanceModel = CreatureInstanceMappable.RetrieveEntity(message.EntityGuid);
			CreatureTemplateModel templateModel = CreatureTemplateMappable.RetrieveEntity(message.EntityGuid);

			DefaultCreatureEntityActor.InitializeActor(actorRef, new DefaultCreatureActorState(EntityDataMappable.RetrieveEntity(message.EntityGuid), message.EntityGuid, instanceModel, templateModel, InterestMappable.RetrieveEntity(message.EntityGuid)));

			ActorRefMappable.AddObject(message.EntityGuid, actorRef);

			if (Logger.IsInfoEnabled)
				Logger.Info($"Created Creature Actor: {typeof(DefaultCreatureEntityActor)} for Entity: {message.EntityGuid}");
		}
	}
}
