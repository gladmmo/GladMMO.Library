using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;
using UnityEngine;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdateMovementBlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateMovementBlock>
	{
		private IMovementDataUpdater<MovementBlockData> MovementBlockUpdater { get; }

		public ObjectUpdateMovementBlockHandler(ILog logger,
			[NotNull] IMovementDataUpdater<MovementBlockData> movementBlockUpdater) 
			: base(ObjectUpdateType.UPDATETYPE_MOVEMENT, logger)
		{
			MovementBlockUpdater = movementBlockUpdater ?? throw new ArgumentNullException(nameof(movementBlockUpdater));
		}

		public override void HandleUpdateBlock(ObjectUpdateMovementBlock updateBlock)
		{
			//It's possible to get a movement update before client main thread actually spawned the entity.
			//This is actually kind of a problem.
			//TODO: Fix problem outlined above.
			try
			{
				MovementBlockUpdater.Update(updateBlock.MovementGuid, updateBlock.MovementData, true);
			}
			catch (Exception e)
			{
				if(Logger.IsErrorEnabled)
					Logger.Error($"Likely Movement Update from: {updateBlock.MovementGuid} before spawned. THIS IS A BIG BUG, MUST FIX! Error: {e.Message}");
			}
		}
	}
}
