using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class AttachAuraApplicationCollectionEventListener : BaseSingleEventListenerInitializable<IEntityCreationStartingEventSubscribable, EntityCreationStartingEventArgs>
	{
		private IEntityGuidMappable<IAuraApplicationCollection> AuraApplicationMappable { get; }

		public AttachAuraApplicationCollectionEventListener(IEntityCreationStartingEventSubscribable subscriptionService,
			[NotNull] IEntityGuidMappable<IAuraApplicationCollection> auraApplicationMappable) 
			: base(subscriptionService)
		{
			AuraApplicationMappable = auraApplicationMappable ?? throw new ArgumentNullException(nameof(auraApplicationMappable));
		}

		protected override void OnEventFired(object source, EntityCreationStartingEventArgs args)
		{
			AuraApplicationMappable.Add(args.EntityGuid, new DefaultAuraApplicationCollection());
		}
	}
}
