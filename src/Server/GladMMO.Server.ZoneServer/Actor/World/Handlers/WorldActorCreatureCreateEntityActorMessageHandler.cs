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

		private IReadonlyEntityGuidMappable<IEntityDataFieldContainer> EntityDataMappable { get; }

		private IReadonlyEntityGuidMappable<CreatureInstanceModel> CreatureInstanceMappable { get; }

		private IReadonlyEntityGuidMappable<CreatureTemplateModel> CreatureTemplateMappable { get; }

		public WorldActorCreatureCreateEntityActorMessageHandler([NotNull] ILog logger,
			[NotNull] IDependencyResolver resolver,
			[NotNull] IEntityGuidMappable<IActorRef> actorRefMappable,
			[NotNull] IReadonlyEntityGuidMappable<IEntityDataFieldContainer> entityDataMappable,
			[NotNull] IReadonlyEntityGuidMappable<CreatureInstanceModel> creatureInstanceMappable,
			[NotNull] IReadonlyEntityGuidMappable<CreatureTemplateModel> creatureTemplateMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			Resolver = resolver ?? throw new ArgumentNullException(nameof(resolver));
			ActorRefMappable = actorRefMappable ?? throw new ArgumentNullException(nameof(actorRefMappable));
			EntityDataMappable = entityDataMappable ?? throw new ArgumentNullException(nameof(entityDataMappable));
			CreatureInstanceMappable = creatureInstanceMappable ?? throw new ArgumentNullException(nameof(creatureInstanceMappable));
			CreatureTemplateMappable = creatureTemplateMappable ?? throw new ArgumentNullException(nameof(creatureTemplateMappable));
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
			CreatureInstanceModel instanceModel = CreatureInstanceMappable.RetrieveEntity(message.EntityGuid);
			CreatureTemplateModel templateModel = CreatureTemplateMappable.RetrieveEntity(message.EntityGuid);
			DefaultCreatureActorState actorState = new DefaultCreatureActorState(EntityDataMappable.RetrieveEntity(message.EntityGuid), message.EntityGuid, instanceModel, templateModel);

			actorRef.Tell(new EntityActorStateInitializeMessage<DefaultCreatureActorState>(actorState));

			ActorRefMappable.AddObject(message.EntityGuid, actorRef);

			if (Logger.IsInfoEnabled)
				Logger.Info($"Created Creature Actor: {typeof(DefaultCreatureEntityActor)} for Entity: {message.EntityGuid}");
		}
	}
}
