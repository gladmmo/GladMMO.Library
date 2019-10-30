using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using GladNet;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class RegisterZoneServerAccountInitializable : IGameInitializable
	{
		private IAuthTokenRepository AuthenticationTokenRepository { get; }

		private IZoneAuthenticationService ZoneAuthService { get; }

		private ILog Logger { get; }

		public RegisterZoneServerAccountInitializable([NotNull] IAuthTokenRepository authenticationTokenRepository,
			[NotNull] IZoneAuthenticationService zoneAuthService,
			[NotNull] ILog logger)
		{
			ZoneAuthService = zoneAuthService ?? throw new ArgumentNullException(nameof(zoneAuthService));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			AuthenticationTokenRepository = authenticationTokenRepository ?? throw new ArgumentNullException(nameof(authenticationTokenRepository));
		}

		public async Task OnGameInitialized()
		{
			//TODO: Handle errors.
			ZoneServerAccountRegistrationResponse response = await ZoneAuthService.CreateZoneServerAccount(new ZoneServerAccountRegistrationRequest());

			//We've created a new account! Nowe need to auth with it to get a valid JWT to be used througout the rest of the application
			JWTModel jwtModel = await ZoneAuthService.TryAuthenticate(new AuthenticationRequestModel(response.ZoneUserName, response.ZonePassword));

			//TODO: Better handle failure.
			if(!jwtModel.isTokenValid)
				throw new InvalidOperationException($"Failed to authenticate zoneserver.");

			if(Logger.IsInfoEnabled)
				Logger.Info($"Registered ZoneAccount: {response.ZoneUserName} with Id: {response.ZoneId}");

			//We now have a valid ephemeral zone account and auth token
			//so we should be alright to finally authorize ourselves with future zone role requests.
			AuthenticationTokenRepository.Update(jwtModel.AccessToken);
		}
	}
}
