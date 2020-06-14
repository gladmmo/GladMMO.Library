using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CompressedObjectUpdatePayloadHandler : BaseGameClientGameMessageHandler<SMSG_COMPRESSED_UPDATE_OBJECT_Payload>
	{
		private IObjectUpdateBlockDispatcher UpdateBlockDispatcher { get; }

		/// <inheritdoc />
		public CompressedObjectUpdatePayloadHandler(ILog logger, [NotNull] IObjectUpdateBlockDispatcher updateBlockDispatcher)
			: base(logger)
		{
			UpdateBlockDispatcher = updateBlockDispatcher ?? throw new ArgumentNullException(nameof(updateBlockDispatcher));
		}

		/// <inheritdoc />
		public override async Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_COMPRESSED_UPDATE_OBJECT_Payload payload)
		{
			/*
			[WireDataContractBaseType(3, typeof(ObjectUpdateCreateObject2Block))]
			[WireDataContractBaseType(1, typeof(ObjectUpdateMovementBlock))]
			[WireDataContractBaseType(2, typeof(ObjectUpdateCreateObject1Block))]
			[WireDataContractBaseType(0, typeof(ObjectUpdateValuesObjectBlock))]
			[WireDataContractBaseType(5, typeof(ObjectUpdateNearObjectsBlock))]
			[WireDataContractBaseType(4, typeof(ObjectUpdateDestroyObjectBlock))]
			*/

			//This reason we must lock here is to prevent overriding INITIAL movement data and generation
			//it's SO dumb but there is a race condition on Entity spawning at the same time we recieve a movement update.
			foreach (var updateBlock in payload.UpdateBlocks)
			{
				switch (updateBlock.UpdateType)
				{
					case ObjectUpdateType.UPDATETYPE_VALUES:
					case ObjectUpdateType.UPDATETYPE_MOVEMENT:
					case ObjectUpdateType.UPDATETYPE_CREATE_OBJECT:
					case ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2:
					case ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS:
					case ObjectUpdateType.UPDATETYPE_NEAR_OBJECTS:
						UpdateBlockDispatcher.Dispatch(updateBlock);
						break;
					default:
						throw new ArgumentOutOfRangeException($"Unable to handle UpdateType: {updateBlock.UpdateType}");
				}
			}
		}
	}

	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdatePayloadHandler : BaseGameClientGameMessageHandler<SMSG_UPDATE_OBJECT_Payload>
	{
		private IObjectUpdateBlockDispatcher UpdateBlockDispatcher { get; }

		/// <inheritdoc />
		public ObjectUpdatePayloadHandler(ILog logger, [NotNull] IObjectUpdateBlockDispatcher updateBlockDispatcher)
			: base(logger)
		{
			UpdateBlockDispatcher = updateBlockDispatcher ?? throw new ArgumentNullException(nameof(updateBlockDispatcher));
		}

		/// <inheritdoc />
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_UPDATE_OBJECT_Payload payload)
		{
			/*
			[WireDataContractBaseType(3, typeof(ObjectUpdateCreateObject2Block))]
			[WireDataContractBaseType(1, typeof(ObjectUpdateMovementBlock))]
			[WireDataContractBaseType(2, typeof(ObjectUpdateCreateObject1Block))]
			[WireDataContractBaseType(0, typeof(ObjectUpdateValuesObjectBlock))]
			[WireDataContractBaseType(5, typeof(ObjectUpdateNearObjectsBlock))]
			[WireDataContractBaseType(4, typeof(ObjectUpdateDestroyObjectBlock))]
			*/

			foreach(var updateBlock in payload.UpdateBlocks)
			{
				switch(updateBlock.UpdateType)
				{
					case ObjectUpdateType.UPDATETYPE_VALUES:
					case ObjectUpdateType.UPDATETYPE_MOVEMENT:
					case ObjectUpdateType.UPDATETYPE_CREATE_OBJECT:
					case ObjectUpdateType.UPDATETYPE_CREATE_OBJECT2:
					case ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS:
					case ObjectUpdateType.UPDATETYPE_NEAR_OBJECTS:
						UpdateBlockDispatcher.Dispatch(updateBlock);
						break;
					default:
						throw new ArgumentOutOfRangeException($"Unable to handle UpdateType: {updateBlock.UpdateType}");
				}
			}

			return Task.CompletedTask;
		}
	}
}