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
			builder.RegisterType<NetworkAvatarContentResourceManager>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<NetworkCreatureContentResourceManager>()
				.AsImplementedInterfaces()
				.SingleInstance();

			builder.RegisterType<NetworkGameObjectContentResourceManager>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}
	}
}
