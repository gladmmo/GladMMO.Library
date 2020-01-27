using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class SavePlayerDataOnDeconstructionEventListener : BaseSingleEventListenerInitializable<IEntityDeconstructionStartingEventSubscribable, EntityDeconstructionStartingEventArgs>
	{
		private IEntityDataSaveable EntityDataSaveable { get; }

		private ILog Logger { get; }

		public SavePlayerDataOnDeconstructionEventListener(IEntityDeconstructionStartingEventSubscribable subscriptionService, 
			[NotNull] IEntityDataSaveable entityDataSaveable,
			[NotNull] ILog logger) 
			: base(subscriptionService)
		{
			EntityDataSaveable = entityDataSaveable ?? throw new ArgumentNullException(nameof(entityDataSaveable));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		protected override void OnEventFired(object source, EntityDeconstructionStartingEventArgs args)
		{
			if (args.EntityGuid.EntityType != EntityType.Player)
				return;

			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				try
				{
					await EntityDataSaveable.SaveAsync(args.EntityGuid)
						.ConfigureAwaitFalseVoid();
				}
				catch (Exception e)
				{
					if(Logger.IsErrorEnabled)
						Logger.Error($"CRITICAL ERROR. Failed to save player entity data and FAILED to release session. Reason: {e.ToString()}");

					throw;
				}
			});
		}
	}
}
