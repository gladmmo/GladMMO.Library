using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using UnityEngine;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class SpellCastRequestPayloadHandler : ControlledEntityRequestHandler<SpellCastRequestPayload>
	{
		private IReadonlyEntityGuidMappable<IActorRef> ActorReferenceMappable { get; }

		public SpellCastRequestPayloadHandler(ILog logger, 
			IReadonlyConnectionEntityCollection connectionIdToEntityMap, 
			IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy,
			[NotNull] IReadonlyEntityGuidMappable<IActorRef> actorReferenceMappable) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{
			ActorReferenceMappable = actorReferenceMappable ?? throw new ArgumentNullException(nameof(actorReferenceMappable));
		}

		protected override Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, SpellCastRequestPayload payload, NetworkEntityGuid guid)
		{
			IActorRef entityActor = ActorReferenceMappable.RetrieveEntity(guid);
			entityActor.TellSelf(new TryCastSpellMessage(payload));

			return Task.CompletedTask;
		}
	}
}
