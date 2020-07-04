using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;

namespace GladMMO
{
	public sealed class DynamicContentDownloadingAutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//CustomAvatarLoaderCancelableFactory : IFactoryCreatable<CustomAvatarLoaderCancelable, CustomAvatarLoaderCreationContext>, IAvatarPrefabCompletedDownloadEventSubscribable
			builder.RegisterType<CustomModelLoaderCancelableFactory>()
				.AsImplementedInterfaces()
				.SingleInstance();

			//DefaultLoadableContentResourceManager : ILoadableContentResourceManager, IDisposable
			RegisterPlayerAvatarResourceContentManager(builder);

			builder.RegisterType<NetworkCreatureContentResourceManager>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<NetworkGameObjectContentResourceManager>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}

		private static void RegisterPlayerAvatarResourceContentManager([NotNull] ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			switch (GladMMOClientConstants.CLIENT_MODE)
			{
				case ClientGameMode.Default:
					builder.RegisterType<NetworkAvatarContentResourceManager>()
						.AsImplementedInterfaces()
						.SingleInstance();
					break;
				case ClientGameMode.GaiaOnline:
					builder.RegisterType<GaiaPlayerAvatarContentResourceManager>()
						.AsImplementedInterfaces()
						.SingleInstance();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
