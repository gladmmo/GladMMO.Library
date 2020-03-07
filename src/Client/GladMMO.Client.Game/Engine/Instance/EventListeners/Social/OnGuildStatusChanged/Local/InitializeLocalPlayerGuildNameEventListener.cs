using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;
using Glader.Essentials;
using Nito.AsyncEx;

namespace GladMMO
{
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class InitializeLocalPlayerGuildNameEventListener : OnLocalPlayerGuildStatusChangedEventListener
	{
		private IUIText GuildNameText { get; }

		private INameQueryService NameQueryService { get; }

		public InitializeLocalPlayerGuildNameEventListener(IReadonlyLocalPlayerDetails playerDetails, 
			IGuildStatusChangedEventSubscribable subscriptionService,
			[NotNull] [KeyFilter(UnityUIRegisterationKey.GuildList)] IUIText guildNameText,
			[NotNull] INameQueryService nameQueryService) 
			: base(subscriptionService, playerDetails)
		{
			GuildNameText = guildNameText ?? throw new ArgumentNullException(nameof(guildNameText));
			NameQueryService = nameQueryService ?? throw new ArgumentNullException(nameof(nameQueryService));
		}

		protected override void OnGuildStatusChanged(GuildStatusChangedEventModel changeArgs)
		{
			UnityAsyncHelper.UnityMainThreadContext.PostAsync(async () =>
			{
				ResponseModel<NameQueryResponse, NameQueryResponseCode> nameQueryResponse = await NameQueryService.RetrieveGuildNameAsync(changeArgs.GuildId)
					.ConfigureAwait(true);

				if (nameQueryResponse.isSuccessful)
					GuildNameText.Text = $"<{nameQueryResponse.Result.EntityName}>";
				else
					GuildNameText.Text = $"<UNKNOWN-GUILDNAME>";
			});
		}
	}
}
