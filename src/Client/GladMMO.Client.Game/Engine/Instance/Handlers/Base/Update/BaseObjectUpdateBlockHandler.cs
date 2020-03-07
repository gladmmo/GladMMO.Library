using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Common.Logging;
using FreecraftCore;

namespace GladMMO
{
	public abstract class BaseObjectUpdateBlockHandler<TSpecificUpdateBlockType> : IObjectUpdateBlockHandler<TSpecificUpdateBlockType>
		where TSpecificUpdateBlockType : ObjectUpdateBlock
	{
		/// <inheritdoc />
		public ObjectUpdateType UpdateType { get; }

		protected ILog Logger { get; }

		/// <inheritdoc />
		protected BaseObjectUpdateBlockHandler(ObjectUpdateType updateType, [NotNull] ILog logger)
		{
			if(!Enum.IsDefined(typeof(ObjectUpdateType), updateType)) throw new InvalidEnumArgumentException(nameof(updateType), (int)updateType, typeof(ObjectUpdateType));
			UpdateType = updateType;
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <inheritdoc />
		public abstract void HandleUpdateBlock(TSpecificUpdateBlockType updateBlock);

		/// <inheritdoc />
		public void HandleUpdateBlock([NotNull] ObjectUpdateBlock updateBlock)
		{
			if(updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			if(updateBlock.UpdateType == UpdateType)
				HandleUpdateBlock((TSpecificUpdateBlockType)updateBlock);
			else
				throw new InvalidOperationException($"Failed to handle UpdateBlock: {updateBlock.GetType().Name} in {GetType().Name} because does not match Type: {typeof(TSpecificUpdateBlockType).Name}.");
		}
	}
}