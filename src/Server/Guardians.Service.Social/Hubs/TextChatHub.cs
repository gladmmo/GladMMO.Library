﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Guardians
{
	[AuthorizeJwt]
	public sealed class TextChatHub : AuthorizationReadySignalRHub
	{
		private ISocialServiceToGameServiceClient SocialToGameClient { get; }

		//I am not happy about this, but we need to maintain some state so that we know what zone a connection is in.
		private IConnectionToZoneMappable ZoneLookupService { get; }

		/// <inheritdoc />
		public TextChatHub(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadySignalRHub> logger, 
			[JetBrains.Annotations.NotNull] ISocialServiceToGameServiceClient socialToGameClient,
			[JetBrains.Annotations.NotNull] IConnectionToZoneMappable zoneLookupService) 
			: base(claimsReader, logger)
		{
			SocialToGameClient = socialToGameClient ?? throw new ArgumentNullException(nameof(socialToGameClient));
			ZoneLookupService = zoneLookupService ?? throw new ArgumentNullException(nameof(zoneLookupService));
		}

		public void Test(string message)
		{
			//Send only to same zone
			//TODO: Have a group name builder, don't hardcore
			this.Clients.Group($"zone:{ZoneLookupService.Retrieve(Context.ConnectionId)}").SendCoreAsync("Test", new object[1] { $"{this.Context.ConnectionId}:{this.Context.UserIdentifier}: {message}"});
		}

		/// <inheritdoc />
		public override async Task OnConnectedAsync()
		{
			await base.OnConnectedAsync()
				.ConfigureAwait(false);

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Account Connected: {ClaimsReader.GetUserName(Context.User)}:{ClaimsReader.GetUserId(Context.User)}");

			//We should never be here unless auth worked
			//so we can assume that and just try to request character session data
			//for the account.
			CharacterSessionDataResponse characterSessionDataResponse = await SocialToGameClient.GetCharacterSessionDataByAccount(ClaimsReader.GetUserIdInt(this.Context.User))
				.ConfigureAwait(false);

			//If the session data request fails we should just abort
			//and disconnect, the user shouldn't be connecting
			if(!characterSessionDataResponse.isSuccessful)
			{
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"Failed to Query SessionData for AccountId: {ClaimsReader.GetUserId(Context.User)} Reason: {characterSessionDataResponse.ResultCode}");

				Context.Abort();
				return;
			}

			//This is ABSOLUTELY CRITICAL we need to validate that the character header they sent actually
			//is the character they have a session as
			//NOT CHECKING THIS IS EQUIVALENT TO LETTING USERS PRETEND THEY ARE ANYONE!
			if(Context.UserIdentifier != characterSessionDataResponse.CharacterId.ToString())
			{
				//We can log account name and id here, because they were successfully authed.
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"User with AccountId: {ClaimsReader.GetUserName(Context.User)}:{ClaimsReader.GetUserId(Context.User)} attempted to spoof as CharacterId: {Context.UserIdentifier} but had session for CharacterID: {characterSessionDataResponse.CharacterId}.");

				this.Context.Abort();
				return;
			}

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Recieved SessionData: Id: {characterSessionDataResponse.CharacterId} ZoneId: {characterSessionDataResponse.ZoneId}");

			//TODO: We should have group name builders. Not hardcoded
			//Join the zoneserver's chat channel group
			await Groups.AddToGroupAsync(Context.ConnectionId, $"zone:{characterSessionDataResponse.ZoneId}", Context.ConnectionAborted)
				.ConfigureAwait(false);

			//Registers for lookup so that we can tell where a connection is zone-wise.
			ZoneLookupService.Register(Context.ConnectionId, characterSessionDataResponse.ZoneId);
		}

		/// <inheritdoc />
		public override Task OnDisconnectedAsync(Exception exception)
		{
			if(ZoneLookupService.Contains(Context.ConnectionId))
				ZoneLookupService.Unregister(Context.ConnectionId);

			return base.OnDisconnectedAsync(exception);
		}
	}
}
