using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FreecraftCore;
using FreecraftCore.Serializer;

namespace GladMMO
{
	public sealed class ClientStaticDataDependencyAutofacModule : Module
	{
		private static IClientDataCollectionContainer CachedClientDataCollection { get; set; }

		protected override void Load([NotNull] ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			builder.Register<IClientDataCollectionContainer>(context =>
			{
				if (CachedClientDataCollection != null)
					return CachedClientDataCollection;
				else
				{
					CachedClientDataCollection = BuildClientDataCollection(context.Resolve<ISerializerService>());
				}

				return CachedClientDataCollection;
			});

			builder.RegisterType<DefaultAddressableContentLoader>()
				.As<IAddressableContentLoader>()
				.SingleInstance();

			//Don't need serializer for reading/writing if it's not null
			//Otherwise the issue
			if (CachedClientDataCollection == null)
			{
				builder.RegisterType<SerializerService>()
					.AsSelf()
					.As<ISerializerService>()
					.OnActivated(args =>
					{
						foreach (Type dbcType in DefaultClientDataCollectionContainer.DBCTypes)
							args.Instance.RegisterType(dbcType);

						foreach (Type collectionType in DefaultClientDataCollectionContainer.DBCCollectionTypes)
							args.Instance.RegisterType(collectionType);

						args.Instance.Compile();
					});
			}
		}

		private IClientDataCollectionContainer BuildClientDataCollection([NotNull] ISerializerService serializer)
		{
			if (serializer == null) throw new ArgumentNullException(nameof(serializer));

			//TODO: Multithreaded loading
			var collectionContainer = new DefaultClientDataCollectionContainer(serializer);
			collectionContainer.StartLoadingAsync();

			return collectionContainer;
		}
	}
}
