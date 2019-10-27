using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class GuildMemberInviteRequestModelHandler : BaseSignalRMessageHandler<GuildMemberInviteRequestModel, IRemoteSocialHubClient>
	{
		private ISocialService SocialService { get; }

		private ILogger<GuildMemberInviteRequestModelHandler> Logger { get; }

		private INameQueryService NameQueryService { get; }

		private IEntityGuidMappable<PendingGuildInviteData> PendingInviteData { get; }

		public GuildMemberInviteRequestModelHandler([JetBrains.Annotations.NotNull] ISocialService socialService,
			[JetBrains.Annotations.NotNull] ILogger<GuildMemberInviteRequestModelHandler> logger,
			[JetBrains.Annotations.NotNull] INameQueryService nameQueryService,
			[JetBrains.Annotations.NotNull] IEntityGuidMappable<PendingGuildInviteData> pendingInviteData)
		{
			SocialService = socialService ?? throw new ArgumentNullException(nameof(socialService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			NameQueryService = nameQueryService ?? throw new ArgumentNullException(nameof(nameQueryService));
			PendingInviteData = pendingInviteData ?? throw new ArgumentNullException(nameof(pendingInviteData));
		}

		protected override async Task OnMessageRecieved(IHubConnectionMessageContext<IRemoteSocialHubClient> context, GuildMemberInviteRequestModel payload)
		{
			var nameQueryResponseTask = NameQueryService.RetrievePlayerGuidAsync(payload.MemberToInvite);

			//First we need to check if they're in a guild.
			//We don't really need to handle the response for this, since it should never really happen.
			var guildStatus = await SocialService.GetCharacterMembershipGuildStatus(context.CallerGuid.EntityId);

			if (!guildStatus.isSuccessful)
			{
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"User: {context.CallerGuid} attempted to Invite: {payload.MemberToInvite} to a guild but was not apart of a guild.");

				return;
			}

			//Now we should know what guild the caller is in due to the query.
			//Now we need to do a reverse namequery to get the guid of who they're attempting to invite.
			var nameQueryResponse = await nameQueryResponseTask;

			//If it's not successful, assume the user doesn't exist.
			if (!nameQueryResponse.isSuccessful)
			{
				await SendGuildInviteResponse(context, GuildMemberInviteResponseCode.PlayerNotFound, NetworkEntityGuid.Empty);
				return;
			}

			//Now check if the user is already guilded
			//If they are we should indicate that to the client.
			if (await CheckIfGuilded(nameQueryResponse.Result))
			{
				await SendGuildInviteResponse(context, GuildMemberInviteResponseCode.PlayerAlreadyInGuild, nameQueryResponse.Result);
				return;
			}

			//Ok, the reverse name query was successful. Check if there is a pending invite.
			//TODO: Right now we rely on local state to indicate if there is a pending invite. We need to NOT do that because it won't work when we scale out.
			ProjectVersionStage.AssertBeta();

			//TODO: There is a race condition if multiple invites are sent at the same time, should we care??
			//If they have a pending invite.
			if (PendingInviteData.ContainsKey(nameQueryResponse.Result))
			{
				//If NOT expired then we need to say they're currently pending an invite
				if (PendingInviteData[nameQueryResponse.Result].isInviteExpired())
				{
					await SendGuildInviteResponse(context, GuildMemberInviteResponseCode.PlayerAlreadyHasPendingInvite, nameQueryResponse.Result);
					return;
				}
				else
				{
					//The invite is EXPIRED so let's added a new one.
					PendingInviteData.ReplaceObject(nameQueryResponse.Result, GeneratePendingInviteData(context.CallerGuid, guildStatus.Result.GuildId));
				}
			}

			//TODO: There is currently no handling to indicate that they are online.

			//Now they have a valid pending invite, so let's address the client
			//that needs to recieve the guild invite.
			IRemoteSocialHubClient playerClient = context.Clients.RetrievePlayerClient(nameQueryResponse.Result);

			//Indicate the player has been invited.
			await SendGuildInviteResponse(context, GuildMemberInviteResponseCode.Success, nameQueryResponse.Result);

			//Now tell the remote/target player they're being invited to a guild.
			await playerClient.ReceiveGuildInviteEventAsync(new GuildMemberInviteEventModel(guildStatus.Result.GuildId, context.CallerGuid));
		}

		private async Task<bool> CheckIfGuilded(NetworkEntityGuid guid)
		{
			var guildStatus = await SocialService.GetCharacterMembershipGuildStatus(guid.EntityId);

			//If the query was successful then they ARE in a guild.
			return guildStatus.ResultCode == CharacterGuildMembershipStatusResponseCode.Success;
		}

		private PendingGuildInviteData GeneratePendingInviteData(NetworkEntityGuid inviterGuid, int guildId)
		{
			return new PendingGuildInviteData(guildId, inviterGuid);
		}

		private static async Task SendGuildInviteResponse(IHubConnectionMessageContext<IRemoteSocialHubClient> context, GuildMemberInviteResponseCode code, NetworkEntityGuid inviteeGuid)
		{
			await context.Clients.Caller.ReceiveGuildInviteResponseAsync(new GuildMemberInviteResponseModel(code, inviteeGuid));
		}
	}
}
