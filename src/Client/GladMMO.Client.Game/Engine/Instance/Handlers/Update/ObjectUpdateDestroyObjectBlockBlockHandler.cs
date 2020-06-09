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

		public ObjectUpdateDestroyObjectBlockBlockHandler(ILog logger, 
			[NotNull] INetworkEntityVisibilityLostEventPublisher visibilityLostPublisher) 
			: base(ObjectUpdateType.UPDATETYPE_OUT_OF_RANGE_OBJECTS, logger)
		{
			VisibilityLostPublisher = visibilityLostPublisher ?? throw new ArgumentNullException(nameof(visibilityLostPublisher));
		}

		/// <inheritdoc />
		public override void HandleUpdateBlock([NotNull] ObjectUpdateDestroyObjectBlock updateBlock)
		{
			if (updateBlock == null) throw new ArgumentNullException(nameof(updateBlock));

			foreach (PackedGuid destroyData in updateBlock.DestroyedGuids.Items)
			{
				Logger.Info($"Attempting to Despawn: {destroyData}");
				VisibilityLostPublisher.PublishEvent(this, new NetworkEntityVisibilityLostEventArgs(destroyData));
			}
		}
	}
}