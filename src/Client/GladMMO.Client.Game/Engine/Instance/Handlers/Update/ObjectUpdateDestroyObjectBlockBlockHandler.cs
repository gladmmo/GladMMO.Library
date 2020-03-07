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
	[AdditionalRegisterationAs(typeof(INetworkEntityVisibilityLostEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ObjectUpdateDestroyObjectBlockBlockHandler : BaseObjectUpdateBlockHandler<ObjectUpdateDestroyObjectBlock>,
		INetworkEntityVisibilityLostEventSubscribable
	{
		/// <inheritdoc />
		public event EventHandler<NetworkEntityVisibilityLostEventArgs> OnNetworkEntityVisibilityLost;

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

				OnNetworkEntityVisibilityLost?.Invoke(this, new NetworkEntityVisibilityLostEventArgs(guid));
			}
		}
	}
}