using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladMMO;
using GladNet;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdateDestroyObjectBlockBlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateDestroyObjectBlock>
	{
		public INetworkEntityVisibilityLostEventPublisher VisibilityLostPublisher { get; }

		public ObjectUpdateDestroyObjectBlockBlockHandler(ObjectUpdateType updateType, ILog logger, [NotNull] INetworkEntityVisibilityLostEventPublisher visibilityLostPublisher) 
			: base(updateType, logger)
		{
			VisibilityLostPublisher = visibilityLostPublisher ?? throw new ArgumentNullException(nameof(visibilityLostPublisher));
		}

		/// <inheritdoc />
		public ObjectUpdateDestroyObjectBlockBlockHandler(ILog logger)
			: base(ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS, logger)
		{

		}

		/// <inheritdoc />
		public override void HandleUpdateBlock([NotNull] ObjectUpdateDestroyObjectBlock updateBlock)
		{
			if (updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			foreach (PackedGuid destroyData in updateBlock.DestroyedGuids.Items)
			{
				ObjectGuid guid = new ObjectGuid(destroyData);
				Logger.Info($"Attempting to Despawn: {guid}");

				VisibilityLostPublisher.PublishEvent(this, new NetworkEntityVisibilityLostEventArgs(guid));
			}
		}
	}
}