using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeThirdPartyAvatarInitializablesEventListener : EntityAvatarChangedEventListener
	{
		private IEntityNameQueryable NameQueryable { get; }

		public InitializeThirdPartyAvatarInitializablesEventListener(IEntityAvatarChangedEventSubscribable subscriptionService,
			[NotNull] IEntityNameQueryable nameQueryable) 
			: base(subscriptionService)
		{
			NameQueryable = nameQueryable ?? throw new ArgumentNullException(nameof(nameQueryable));
		}

		protected override void OnEventFired(object source, EntityAvatarChangedEventArgs args)
		{
			//This is for custom complex avatars that may need initialization after downloading.
			IAvatarInitializable avatarInitializable = args.AvatarWorldRepresentation.GetComponentInChildren<IAvatarInitializable>();

			//3rd party avatar integrations require both GUID and NAME because we shouldn't assume they have the ability
			//to work with 1 or the other.
			avatarInitializable?.InitializeAvatar(args.EntityGuid, NameQueryable.RetrieveAsync(args.EntityGuid));
		}
	}
}
