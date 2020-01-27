using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class CharacterZoneOnHubConnectionEventListener : IOnHubConnectionEventListener
	{
		private ILogger<CharacterZoneOnHubConnectionEventListener> Logger { get; }

		private ISocialServiceToGameServiceClient SocialToGameClient { get; }

		private IClaimsPrincipalReader ClaimsReader { get; }

		/// <inheritdoc />
		public CharacterZoneOnHubConnectionEventListener(
			[JetBrains.Annotations.NotNull] ILogger<CharacterZoneOnHubConnectionEventListener> logger, 
			[JetBrains.Annotations.NotNull] ISocialServiceToGameServiceClient socialToGameClient, 
			[JetBrains.Annotations.NotNull] IClaimsPrincipalReader claimsReader)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			SocialToGameClient = socialToGameClient ?? throw new ArgumentNullException(nameof(socialToGameClient));
			ClaimsReader = claimsReader ?? throw new ArgumentNullException(nameof(claimsReader));
		}

		/// <inheritdoc />
		public async Task<HubOnConnectionState> OnConnected([JetBrains.Annotations.NotNull] Hub hubConnectedTo)
		{
			if(hubConnectedTo == null) throw new ArgumentNullException(nameof(hubConnectedTo));

			//We should never be here unless auth worked
			//so we can assume that and just try to request character session data
			//for the account.
			CharacterSessionDataResponse characterSessionDataResponse = await SocialToGameClient.GetCharacterSessionDataByAccount(ClaimsReader.GetAccountIdInt(hubConnectedTo.Context.User))
				.ConfigureAwaitFalse();

			//TODO: To support website chat we shouldn't disconnect just because they don't have a zone session.
			//If the session data request fails we should just abort
			//and disconnect, the user shouldn't be connecting
			if(!characterSessionDataResponse.isSuccessful)
			{
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"Failed to Query SessionData for AccountId: {ClaimsReader.GetAccountId(hubConnectedTo.Context.User)} Reason: {characterSessionDataResponse.ResultCode}");

				//TODO: Eventually we don't want to do this.
				return HubOnConnectionState.Abort;
			}

			//This is ABSOLUTELY CRITICAL we need to validate that the character header they sent actually
			//is the character they have a session as
			//NOT CHECKING THIS IS EQUIVALENT TO LETTING USERS PRETEND THEY ARE ANYONE!
			if(hubConnectedTo.Context.UserIdentifier != characterSessionDataResponse.CharacterId.ToString())
			{
				//We can log account name and id here, because they were successfully authed.
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"User with AccountId: {ClaimsReader.GetAccountName(hubConnectedTo.Context.User)}:{ClaimsReader.GetAccountId(hubConnectedTo.Context.User)} attempted to spoof as CharacterId: {hubConnectedTo.Context.UserIdentifier} but had session for CharacterID: {characterSessionDataResponse.CharacterId}.");

				return HubOnConnectionState.Abort;
			}

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Recieved SessionData: Id: {characterSessionDataResponse.CharacterId} ZoneId: {characterSessionDataResponse.ZoneId}");

			//TODO: We should have group name builders. Not hardcoded
			//Join the zoneserver's chat channel group
			await hubConnectedTo.Groups.AddToGroupAsync(hubConnectedTo.Context.ConnectionId, $"zone:{characterSessionDataResponse.ZoneId}", hubConnectedTo.Context.ConnectionAborted)
				.ConfigureAwaitFalseVoid();

			return HubOnConnectionState.Success;
		}
	}
}
