using System;
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

		private ISocialServiceToGameServiceClient SocialToGameClient { get; }

		private IClaimsPrincipalReader ClaimsReader { get; }

		//TODO: We don't want to directly expose the response
		private IEntityGuidMappable<CharacterGuildMembershipStatusResponse> GuildStatusMappable { get; }

		/// <inheritdoc />
		public CharacterGuildOnHubConnectionEventListener([JetBrains.Annotations.NotNull] ILogger<CharacterGuildOnHubConnectionEventListener> logger, [JetBrains.Annotations.NotNull] ISocialServiceToGameServiceClient socialToGameClient, [JetBrains.Annotations.NotNull] IClaimsPrincipalReader claimsReader, IEntityGuidMappable<CharacterGuildMembershipStatusResponse> guildStatusMappable)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			SocialToGameClient = socialToGameClient ?? throw new ArgumentNullException(nameof(socialToGameClient));
			ClaimsReader = claimsReader ?? throw new ArgumentNullException(nameof(claimsReader));
			GuildStatusMappable = guildStatusMappable;
		}

		/// <inheritdoc />
		public async Task<HubOnConnectionState> OnConnected(Hub hubConnectedTo)
		{
			//TODO: Verify that the character they requested is owned by them.
			ProjectVersionStage.AssertAlpha();

			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(int.Parse(hubConnectedTo.Context.UserIdentifier))
				.WithType(EntityType.Player)
				.Build();

			//We may already be able to register.
			if(await TryRegisterGuildStatus(guid, hubConnectedTo.Groups, hubConnectedTo.Context.ConnectionId).ConfigureAwait(false) == HubOnConnectionState.Success)
			{
				return HubOnConnectionState.Success;
			}

			HubOnConnectionState state = await TryRequestCharacterGuildStatus(guid, hubConnectedTo.Context.UserIdentifier)
				.ConfigureAwait(false);

			if(state == HubOnConnectionState.Success)
				return await TryRegisterGuildStatus(guid, hubConnectedTo.Groups, hubConnectedTo.Context.ConnectionId)
					.ConfigureAwait(false);

			//Just error, we don't need to abort. Something didn't work right though.
			return HubOnConnectionState.Error;
		}

		private async Task<HubOnConnectionState> TryRegisterGuildStatus(NetworkEntityGuid guid, IGroupManager groups, string connectionId)
		{
			//It's possible we already maintain guild information for this entity
			//This can happen if multiple connections share an entity.
			if(GuildStatusMappable.ContainsKey(guid))
			{
				await RegisterGuildOnExistingResponse(guid, groups, connectionId)
					.ConfigureAwait(false);

				return HubOnConnectionState.Success;
			}

			return HubOnConnectionState.Error;
		}

		private async Task<HubOnConnectionState> TryRequestCharacterGuildStatus(NetworkEntityGuid guid, string userIdentifier)
		{
			CharacterGuildMembershipStatusResponse response = null;

			try
			{
				response = await SocialToGameClient.GetCharacterMembershipGuildStatus(int.Parse(userIdentifier))
					.ConfigureAwait(false);
			}
			catch(Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to get guild status of Connection: {userIdentifier}. Exception: {e.Message}\n\nStack:{e.StackTrace}");

				return HubOnConnectionState.Abort;
			}

			//Don't ADD, we have to assume that we might have an entity, maybe web or mobile, already connected
			//and merely just update the guild status
			GuildStatusMappable.Add(guid, response);
			return HubOnConnectionState.Success;
		}

		private async Task RegisterGuildOnExistingResponse(NetworkEntityGuid guid, IGroupManager groupManager, string connectionId)
		{
			if(GuildStatusMappable[guid].isSuccessful)
			{
				//TODO: don't hardcode
				await groupManager.AddToGroupAsync(connectionId, $"guild:{GuildStatusMappable.RetrieveEntity(guid).GuildId}")
					.ConfigureAwait(false);
			}
		}
	}
}
