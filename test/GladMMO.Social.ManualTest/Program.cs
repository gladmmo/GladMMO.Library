using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO.Social.ManualTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			// https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = MyRemoteCertificateValidationCallback;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.CheckCertificateRevocationList = false;

			IAuthenticationService authService = Refit.RestService.For<IAuthenticationService>(@"https://72.190.177.214:443");

			Console.Write($"Username: ");
			string username = Console.ReadLine();

			Console.Write($"Password: ");
			string password = Console.ReadLine();

			Console.Write($"CharacterId: ");
			string characterId = Console.ReadLine();

			string token = (await authService.TryAuthenticate(new AuthenticationRequestModel(username, password))
				.ConfigureAwaitFalse()).AccessToken;

			HubConnection connection = new HubConnectionBuilder()
				.WithUrl("http://127.0.0.1:7777/realtime/social", options =>
				{
					options.Headers.Add(SocialNetworkConstants.CharacterIdRequestHeaderName, characterId);
					options.AccessTokenProvider = () => Task.FromResult(token);
				})
				.Build();

			await connection.StartAsync()
				.ConfigureAwaitFalseVoid();

			connection.RegisterClientInterface<IRemoteSocialHubClient>(new TestClientHandler());

			SignalRForwardedIRemoteSocialHubClient client = new SignalRForwardedIRemoteSocialHubClient(connection);

			while(true)
			{
				string input = Console.ReadLine();

				await client.SendTestMessageAsync(new TestSocialModel(input));

				/*if(input.Contains("/guild"))
				{
					await client.SendGuildChannelTextChatMessageAsync(new GuildChatMessageRequestModel(input))
						.ConfigureAwaitFalse();
				}
				else
					await client.SendZoneChannelTextChatMessageAsync(new ZoneChatMessageRequestModel(input))
						.ConfigureAwaitFalse();*/
			}
		}

		//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
		private static bool MyRemoteCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslpolicyerrors)
		{
			return true;
		}
	}

	public class TestClientHandler : IRemoteSocialHubClient
	{
		/*public Task RecieveZoneChannelTextChatMessageAsync(ZoneChatMessageEventModel message)
		{
			Console.WriteLine($"[{message.ChannelMessage.Data.TargetChannel}] User {message.ChannelMessage.EntityGuid}: {message.ChannelMessage.Data.Message}");

			return Task.CompletedTask;
		}

		/// <inheritdoc />
		public Task RecieveGuildChannelTextChatMessageAsync(GuildChatMessageEventModel message)
		{
			Console.WriteLine($"[{message.ChannelMessage.Data.TargetChannel}] User {message.ChannelMessage.EntityGuid}: {message.ChannelMessage.Data.Message}");

			return Task.CompletedTask;
		}*/

		public async Task ReceiveGuildInviteResponseAsync(GuildMemberInviteResponseModel message)
		{
			throw new NotImplementedException();
		}

		public async Task ReceiveGuildInviteEventAsync(GuildMemberInviteEventModel message)
		{
			throw new NotImplementedException();
		}

		public async Task ReceiveGuildMemberJoinedEventAsync(GuildMemberJoinedEventModel message)
		{
			throw new NotImplementedException();
		}

		public async Task ReceiveGuildStatusChangedEventAsync(GuildStatusChangedEventModel message)
		{
			throw new NotImplementedException();
		}
	}
}
