using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class SavePlayerDataOnDeconstructionEventListener : BaseSingleEventListenerInitializable<IEntityDeconstructionStartingEventSubscribable, EntityDeconstructionStartingEventArgs>
	{
		private IEntityDataSaveable EntityDataSaveable { get; }

		public SavePlayerDataOnDeconstructionEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService, [NotNull] IEntityDataSaveable entityDataSaveable) 
			: base(subscriptionService)
		{
			EntityDataSaveable = entityDataSaveable ?? throw new ArgumentNullException(nameof(entityDataSaveable));
		}

		protected override void OnEventFired(object source, EntityDeconstructionStartingEventArgs args)
		{
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				await EntityDataSaveable.SaveAsync(args.EntityGuid)
					.ConfigureAwait(false);
			});
		}
	}
}
