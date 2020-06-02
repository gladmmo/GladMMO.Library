using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IObjectUpdateBlockHandler))]
	public abstract class BaseObjectUpdateBlockHandler<TSpecificUpdateBlockType> : IObjectUpdateBlockHandler<TSpecificUpdateBlockType>, IGameInitializable
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

		//Hack to get it into the scene.
		public Task OnGameInitialized()
		{
			return Task.CompletedTask;
		}
	}
}