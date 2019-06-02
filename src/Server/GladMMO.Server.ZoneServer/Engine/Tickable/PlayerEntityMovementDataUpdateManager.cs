using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Glader.Essentials;
using JetBrains.Annotations;

namespace GladMMO
{
	[GameInitializableOrdering(1)]
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class PlayerEntityMovementDataUpdateManager : IGameTickable
	{
		private IPlayerEntityGuidEnumerable PlayerGuids { get; }

		private INetworkMessageSender<EntityMovementMessageContext> MovementUpdateMessageSender { get; }

		private IDirtyableMovementDataCollection MovementCollection { get; }

		/// <summary>
		/// The collections locking policy.
		/// </summary>
		private GlobalEntityCollectionsLockingPolicy LockingPolicy { get; }

		/// <inheritdoc />
		public PlayerEntityMovementDataUpdateManager(
			[NotNull] IPlayerEntityGuidEnumerable playerGuids, 
			[NotNull] INetworkMessageSender<EntityMovementMessageContext> movementUpdateMessageSender,
			[NotNull] IDirtyableMovementDataCollection movementCollection,
			[NotNull] GlobalEntityCollectionsLockingPolicy lockingPolicy)
		{
			PlayerGuids = playerGuids ?? throw new ArgumentNullException(nameof(playerGuids));
			MovementUpdateMessageSender = movementUpdateMessageSender ?? throw new ArgumentNullException(nameof(movementUpdateMessageSender));
			MovementCollection = movementCollection ?? throw new ArgumentNullException(nameof(movementCollection));
			LockingPolicy = lockingPolicy ?? throw new ArgumentNullException(nameof(lockingPolicy));
		}

		/// <inheritdoc />
		public void Tick()
		{
			using(LockingPolicy.ReaderLock(null, CancellationToken.None))
			{
				//For every player we need to do some processing so that we can send a movement update
				//packet for them.
				foreach(var guid in PlayerGuids)
				{
					//We just dispatch a movement update to be send
					//to the connection associated with the provided guid.
					MovementUpdateMessageSender.Send(new EntityMovementMessageContext(guid));
				}

				//After all movement is done we need to clear all tracked/dirty changes in the movement collection
				MovementCollection.ClearDirty();
			}
		}
	}
}
