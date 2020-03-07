using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class QueryEntityNameOnDiscoverEventListener : BaseSingleEventListenerInitializable<INetworkEntityVisibleEventSubscribable, NetworkEntityNowVisibleEventArgs>
	{
		private IEntityNameQueryable NameQueryService { get; }

		public QueryEntityNameOnDiscoverEventListener(INetworkEntityVisibleEventSubscribable subscriptionService,
			[NotNull] IEntityNameQueryable nameQueryService) 
			: base(subscriptionService)
		{
			NameQueryService = nameQueryService ?? throw new ArgumentNullException(nameof(nameQueryService));
		}

		protected override void OnEventFired(object source, NetworkEntityNowVisibleEventArgs args)
		{
			//Just fire and forget a retrieve so that hopefully when the player eventually interacts with the entity
			//the name will be available already.
			NameQueryService.RetrieveAsync(args.EntityGuid);
		}
	}
}
