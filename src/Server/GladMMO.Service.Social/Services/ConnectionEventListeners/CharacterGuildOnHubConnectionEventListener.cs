using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class CharacterGuildOnHubConnectionEventListener : IOnHubConnectionEventListener
	{
		private ILogger<CharacterGuildOnHubConnectionEventListener> Logger { get; }

		//TODO: We don't want to directly expose the response
		private IEntityGuidMappable<CharacterGuildMembershipStatusResponse> GuildStatusMappable { get; }

		private ISocialService SocialService { get; }

		/// <inheritdoc />
		public CharacterGuildOnHubConnectionEventListener([JetBrains.Annotations.NotNull] ILogger<CharacterGuildOnHubConnectionEventListener> logger,
			IEntityGuidMappable<CharacterGuildMembershipStatusResponse> guildStatusMappable,
			[JetBrains.Annotations.NotNull] ISocialService socialService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			GuildStatusMappable = guildStatusMappable;
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
		}

		/// <inheritdoc />
		public async Task<HubOnConnectionState> OnConnected(Hub hubConnectedTo)
		{
			//TODO: Verify that the character they requested is owned by them.
			ProjectVersionStage.AssertAlpha();

			ObjectGuid guid = new ObjectGuidBuilder()
				.WithId(int.Parse(hubConnectedTo.Context.UserIdentifier))
				.WithType(EntityTypeId.TYPEID_PLAYER)
				.Build();

			HubOnConnectionState state = await TryRequestCharacterGuildStatus(guid, hubConnectedTo.Context.UserIdentifier)
				.ConfigureAwaitFalse();

			if (state == HubOnConnectionState.Success)
			{
				await RegisterGuildOnExistingResponse(guid, hubConnectedTo.Groups, hubConnectedTo.Context.ConnectionId)
					.ConfigureAwaitFalseVoid();

				return HubOnConnectionState.Success;
			}

			//Just error, we don't need to abort. Something didn't work right though.
			return HubOnConnectionState.Error;
		}

		private async Task<HubOnConnectionState> TryRequestCharacterGuildStatus(ObjectGuid guid, string userIdentifier)
		{
			ResponseModel<CharacterGuildMembershipStatusResponse, CharacterGuildMembershipStatusResponseCode> response = null;

			try
			{
				response = await SocialService.GetCharacterMembershipGuildStatus(int.Parse(userIdentifier))
					.ConfigureAwaitFalse();
			}
			catch(Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to get guild status of Connection: {userIdentifier}. Exception: {e.Message}\n\nStack:{e.StackTrace}");

				return HubOnConnectionState.Abort;
			}

			//Don't ADD, we have to assume that we might have an entity, maybe web or mobile, already connected
			//and merely just update the guild status
			if(response.isSuccessful)
				GuildStatusMappable.Add(guid, response.Result);

			return HubOnConnectionState.Success;
		}

		private async Task RegisterGuildOnExistingResponse(ObjectGuid guid, IGroupManager groupManager, string connectionId)
		{
			//If no guild status exists we cannot register a guild channel.
			if (!GuildStatusMappable.ContainsKey(guid))
				return;

			//TODO: don't hardcode
			await groupManager.AddToGroupAsync(connectionId, $"guild:{GuildStatusMappable.RetrieveEntity(guid).GuildId}")
				.ConfigureAwaitFalseVoid();
		}
	}
}
