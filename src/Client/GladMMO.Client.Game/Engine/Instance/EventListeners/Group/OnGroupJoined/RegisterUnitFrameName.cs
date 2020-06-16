using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using FreecraftCore;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class RegisterUnitFrameName : OnGroupJoinedEventListener
	{
		private IEntityNameQueryable NameQueryable { get; }

		public RegisterUnitFrameName(IPlayerGroupJoinedEventSubscribable subscriptionService, 
			ILog logger, 
			IGroupUnitFrameManager groupUnitFrameManager,
			[NotNull] IEntityNameQueryable nameQueryable) 
			: base(subscriptionService, logger, groupUnitFrameManager)
		{
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));
		}

		protected override void OnGroupJoined(PlayerJoinedGroupEventArgs joinArgs)
		{
			//Initialize name
			if(NameQueryable.Exists(joinArgs.PlayerGuid))
				GroupUnitFrameManager[joinArgs.PlayerGuid].UnitName.Text = NameQueryable.Retrieve(joinArgs.PlayerGuid);
			else
			{
				UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
				{
					string name = await NameQueryable.RetrieveAsync(joinArgs.PlayerGuid);

					//Check that they are still in the group
					//Possible they left before the request finished.
					if(GroupUnitFrameManager.Contains(joinArgs.PlayerGuid))
						GroupUnitFrameManager[joinArgs.PlayerGuid].UnitName.Text = name;
				});
			}
		}
	}
}
