using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Implement of <see cref="IUserIdProvider"/> which checks
	/// for a Character ID header in the connection request and uses it as
	/// the character id.
	/// </summary>
	public sealed class SignalRPlayerCharacterUserIdProvider : IUserIdProvider
	{
		public ILogger<SignalRPlayerCharacterUserIdProvider> Logger { get; }

		private IClaimsPrincipalReader ClaimsReader { get; }

		public SignalRPlayerCharacterUserIdProvider([JetBrains.Annotations.NotNull] ILogger<SignalRPlayerCharacterUserIdProvider> logger, [JetBrains.Annotations.NotNull] IClaimsPrincipalReader claimsReader)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			ClaimsReader = claimsReader ?? throw new ArgumentNullException(nameof(claimsReader));
		}

		/// <inheritdoc />
		public string GetUserId(HubConnectionContext connection)
		{
			//TODO: This could fail if they don't put the header in
			ProjectVersionStage.AssertBeta();

			//We trust the client to send us a header that contains the character id
			//You may be freaking out, but we aren't taking the client at face value here.
			//This is and MUST be verified in the Hub's OnConnected method to prevent
			//malicious uses from spoofing.
			int characterId = connection.GetHttpContext().Request.GetTypedHeaders().Get<int>(SocialNetworkConstants.CharacterIdRequestHeaderName);

			if(characterId <= 0)
			{
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"Encountered client: {ClaimsReader.GetUserName(connection.User)}:{ClaimsReader.GetUserId(connection.User)} with invalid characterId {characterId}");

				connection.Abort();
				return String.Empty;
			}

			return characterId.ToString();
		}
	}
}
