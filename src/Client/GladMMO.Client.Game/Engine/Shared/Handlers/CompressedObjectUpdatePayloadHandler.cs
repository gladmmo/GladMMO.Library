using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using GladNet;

namespace GladMMO
{
	/*[SceneTypeCreateGladMMO(GameSceneType.DefaultLobby)]
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
		public override Task HandleMessage(IPeerMessageContext<GamePacketPayload> context, SMSG_COMPRESSED_UPDATE_OBJECT_Payload payload)
		{
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
	}*/
}
