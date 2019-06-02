using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;
using JetBrains.Annotations;
using Nito.AsyncEx;

namespace GladMMO
{
	/// <summary>
	/// Simplfied version of <see cref="ControlledEntityRequestHandler{TSpecificPayloadType, TLockingPolicyType, TLockingContextType}"/> that
	/// works on locking over an entity globally (read) and grabs the context
	/// </summary>
	/// <typeparam name="TSpecificPayloadType"></typeparam>
	public abstract class ControlledEntityRequestHandler<TSpecificPayloadType> : ControlledEntityRequestHandler<TSpecificPayloadType, IContextualResourceLockingPolicy<NetworkEntityGuid>, NetworkEntityGuid> where TSpecificPayloadType : GameClientPacketPayload
	{
		/// <inheritdoc />
		protected ControlledEntityRequestHandler(ILog logger, IReadonlyConnectionEntityCollection connectionIdToEntityMap, IContextualResourceLockingPolicy<NetworkEntityGuid> lockingPolicy) 
			: base(logger, connectionIdToEntityMap, lockingPolicy)
		{

		}

		/// <inheritdoc />
		protected sealed override NetworkEntityGuid GenerateLockContext(IPeerSessionMessageContext<GameServerPacketPayload> context, TSpecificPayloadType payload)
		{
			//We just grab the entity, and assume that they want this.
			return ExtractEntityGuidFromContext(context);
		}
	}

	/// <summary>
	/// Base <see cref="IPeerPayloadSpecificMessageHandler{TPayloadType,TOutgoingPayloadType}"/> handler for
	/// messages that require a controlled/associated <see cref="NetworkEntityGuid"/> with the session to be handled.
	/// For example, movement. Can't handle movement packets if the session doesn't even have an associated entity.
	/// </summary>
	/// <typeparam name="TSpecificPayloadType"></typeparam>
	/// <typeparam name="TLockingPolicyType"></typeparam>
	public abstract class ControlledEntityRequestHandler<TSpecificPayloadType, TLockingPolicyType, TLockingContextType> : BaseServerRequestHandler<TSpecificPayloadType>
		where TSpecificPayloadType : GameClientPacketPayload
		where TLockingPolicyType : class, IContextualResourceLockingPolicy<TLockingContextType>
	{
		private IReadonlyConnectionEntityCollection ConnectionIdToEntityMap { get; }

		private TLockingPolicyType LockingPolicy { get; }

		//TODO: Don't use dictionary, creatre interface.
		protected ControlledEntityRequestHandler(
			[NotNull] ILog logger, 
			[NotNull] IReadonlyConnectionEntityCollection connectionIdToEntityMap,
			[NotNull] TLockingPolicyType lockingPolicy)
			: base(logger)
		{
			ConnectionIdToEntityMap = connectionIdToEntityMap ?? throw new ArgumentNullException(nameof(connectionIdToEntityMap));
			LockingPolicy = lockingPolicy ?? throw new ArgumentNullException(nameof(lockingPolicy));
		}

		public override async Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, TSpecificPayloadType payload)
		{
			if(!ValidateConnectionOwnsEntity(context.Details.ConnectionId))
				return;

			//TODO: What do we do in cases where the entity 
			ProjectVersionStage.AssertBeta();
			//TODO: We may want a timeout to prevent production deadlocks
			//Just need reader locking to lock on the policy specified by the implementer.
			//So, as anote it's possible to have aquired this lock while waiting on a Write lock to end. That would
			//mean, since we use write locks only for removal (usually), that the entity might not exist.
			//We actually would normally need to do double-check locking BUT the check below of the connection entity map
			//will detect if it's been removed, thus everything will be ok.
			using(var lockObj = await LockingPolicy.ReaderLockAsync(GenerateLockContext(context, payload), CancellationToken.None)
				.ConfigureAwait(false))
			{
				//We have to double check lock, could have been removed since the last check.
				if(!ValidateConnectionOwnsEntity(context.Details.ConnectionId))
					return;

				//We just dispatch to child handler, who will use the payload, context and guid.
				await HandleMessage(context, payload, ExtractEntityGuidFromContext(context))
					.ConfigureAwait(false);
			}
		}

		private bool ValidateConnectionOwnsEntity(int connectionId)
		{
			if(!ConnectionIdToEntityMap.ContainsKey(connectionId))
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Recieved: {typeof(TSpecificPayloadType).Name} from Connection: {connectionId} but no entity guid associated.");

				return false;
			}

			return true;
		}

		/// <summary>
		/// Generates the context required by the specified <typeparamref name="TLockingContextType"/>.
		/// </summary>
		/// <returns>The context to use for locking.</returns>
		protected abstract TLockingContextType GenerateLockContext(IPeerSessionMessageContext<GameServerPacketPayload> context, TSpecificPayloadType payload);

		protected NetworkEntityGuid ExtractEntityGuidFromContext(IPeerSessionMessageContext<GameServerPacketPayload> context)
		{
			return ConnectionIdToEntityMap[context.Details.ConnectionId];
		}

		//TODO: Should we create a new context instead?
		protected abstract Task HandleMessage(IPeerSessionMessageContext<GameServerPacketPayload> context, TSpecificPayloadType payload, NetworkEntityGuid guid);

		/// <summary>
		/// Retrieves the mapped object of type TObjectType from the provided <see cref="map"/>.
		/// </summary>
		/// <typeparam name="TObjectType">The object map type.</typeparam>
		/// <param name="map">The map to retrieve from.</param>
		/// <param name="context">The context used to get the entity.</param>
		/// <returns></returns>
		protected TObjectType GetEntityMappedObject<TObjectType>(IReadonlyEntityGuidMappable<TObjectType> map, IPeerSessionMessageContext<GameServerPacketPayload> context)
		{
			return GetEntityMappedObject(map, context.Details.ConnectionId);
		}

		/// <summary>
		/// Retrieves the mapped object of type TObjectType from the provided <see cref="map"/>.
		/// </summary>
		/// <typeparam name="TObjectType">The object map type.</typeparam>
		/// <param name="map">The map to retrieve from.</param>
		/// <param name="connectionId">The context used to get the entity.</param>
		/// <returns></returns>
		protected TObjectType GetEntityMappedObject<TObjectType>(IReadonlyEntityGuidMappable<TObjectType> map, int connectionId)
		{
			NetworkEntityGuid entityGuid = ConnectionIdToEntityMap[connectionId];
			if(!map.ContainsKey(entityGuid))
			{
				throw new InvalidOperationException($"Entity: {entityGuid} did not have a registered service for Type: {typeof(TObjectType).Name}");
			}

			//TODO: This is a race condition here
			ProjectVersionStage.AssertAlpha();
			return map[entityGuid];
		}
	}
}
