﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class ToggleGuildSocialTabeEventListener : OnLocalPlayerGuildStatusChangedEventListener
	{
		private IUIToggle GuildListToggle { get; }

		public ToggleGuildSocialTabeEventListener(IGuildStatusChangedEventSubscribable subscriptionService, IReadonlyLocalPlayerDetails localPlayerDetails,
			[KeyFilter(UnityUIRegisterationKey.GuildList)] [NotNull] IUIToggle guildListToggle) 
			: base(subscriptionService, localPlayerDetails)
		{
			GuildListToggle = guildListToggle ?? throw new ArgumentNullException(nameof(guildListToggle));
		}

		protected override void OnGuildStatusChanged(GuildStatusChangedEventArgs changeArgs)
		{
			//The guild tab should be accessible if we're not guildless.
			GuildListToggle.IsInteractable = !changeArgs.IsGuildless;
		}
	}
}
