using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeEntityAsyncLockEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityGuidMappable<AsyncReaderWriterLock> AsyncLockMappable { get; }

		public InitializeEntityAsyncLockEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<AsyncReaderWriterLock> asyncLockMappable) 
			: base(subscriptionService)
		{
			AsyncLockMappable = asyncLockMappable ?? throw new ArgumentNullException(nameof(asyncLockMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			AsyncLockMappable.AddObject(args.EntityGuid, new AsyncReaderWriterLock());
		}
	}
}
