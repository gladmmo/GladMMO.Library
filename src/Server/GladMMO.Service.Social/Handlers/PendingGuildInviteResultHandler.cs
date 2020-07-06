using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class PendingGuildInviteResultHandler : BaseSignalRMessageHandler<PendingGuildInviteHandleRequest, IRemoteSocialHubClient>
	{
		[JetBrains.Annotations.NotNull]
		public ILogger<GuildMemberInviteRequestModelHandler> Logger { get; }

		private IEntityGuidMappable<PendingGuildInviteData> PendingInviteData { get; }

		private IRepositoryFactory<ITrinityGuildMembershipRepository> CharacterGuildMembershipRepositoryFactory { get; }

		public PendingGuildInviteResultHandler([JetBrains.Annotations.NotNull] ILogger<GuildMemberInviteRequestModelHandler> logger,
			[JetBrains.Annotations.NotNull] IEntityGuidMappable<PendingGuildInviteData> pendingInviteData,
			[JetBrains.Annotations.NotNull] IRepositoryFactory<ITrinityGuildMembershipRepository> characterGuildMembershipRepository)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			PendingInviteData = pendingInviteData ?? throw new ArgumentNullException(nameof(pendingInviteData));
			CharacterGuildMembershipRepositoryFactory = characterGuildMembershipRepository ?? throw new ArgumentNullException(nameof(characterGuildMembershipRepository));
		}

		protected override async Task OnMessageRecieved(IHubConnectionMessageContext<IRemoteSocialHubClient> context, PendingGuildInviteHandleRequest payload)
		{
			//Really shouldn't happen, don't tell client anything.
			if (!PendingInviteData.ContainsKey(context.CallerGuid))
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"User: {context.CallerGuid} tried to claim pending guild invite but none existed.");

				return;
			}

			PendingGuildInviteData inviteData = PendingInviteData.RetrieveEntity(context.CallerGuid);
			
			//Now we can check if they wanted to join
			if (payload.isSuccessful)
			{
				bool guildJoinResult = false;

				//Important that we dispose of this, since this handler is not created every request.
				using (var characterGuildMembershipRepositoryContainer = CharacterGuildMembershipRepositoryFactory.Create())
				{
					//When successful, we must add them to the guild database
					//and then alert the guild channel (everyone in the guild) that they joined
					//AND let the client itself know that it joined a guild.
					guildJoinResult = await characterGuildMembershipRepositoryContainer.Repository.TryCreateAsync(CreateNewGuildMembership(context, inviteData));
				}

				//This should never happen
				if (!guildJoinResult)
				{
					if (Logger.IsEnabled(LogLevel.Error))
						Logger.LogError($"User: {context.CallerGuid} tried to join Guild: {inviteData.GuildId} but failed.");

					return;
				}

				//TODO: Don't hardcode this
				//$"guild:{GuildStatusMappable.RetrieveEntity(guid).GuildId}
				//Their membership is within the database now.
				//Broadcast to everyone that a player joined the guild.
				await context.Clients.Group($"guild:{inviteData.GuildId}").ReceiveGuildMemberJoinedEventAsync(new GuildMemberJoinedEventModel(context.CallerGuid));
				await context.Groups.AddToGroupAsync(context.HubConntext.ConnectionId, $"guild:{inviteData.GuildId}");

				//Let the local client know that their guild status also changed.
				await context.Clients.RetrievePlayerClient(context.CallerGuid).ReceiveGuildStatusChangedEventAsync(new GuildStatusChangedEventModel(context.CallerGuid, inviteData.GuildId));
			}
			else
			{
				//On failure, we just remove the pending invite
				//and let the inviting client know that they declined an invite.
				PendingInviteData.RemoveEntityEntry(context.CallerGuid);

				IRemoteSocialHubClient inviterClient = context.Clients.RetrievePlayerClient(inviteData.InviterGuid);

				//Tell the inviter this failed.
				await inviterClient.ReceiveGuildInviteResponseAsync(new GuildMemberInviteResponseModel(GuildMemberInviteResponseCode.PlayerDeclinedGuildInvite, context.CallerGuid));
			}
		}

		private static GuildMember CreateNewGuildMembership(IHubConnectionMessageContext<IRemoteSocialHubClient> context, PendingGuildInviteData inviteData)
		{
			return new GuildMember()
			{
				Guid = (uint) context.CallerGuid.CurrentObjectGuid,
				Guildid = (uint) inviteData.GuildId
			};
		}
	}
}
