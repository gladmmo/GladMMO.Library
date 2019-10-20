using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace GladMMO.Social.ManualTest
{
	class Program
	{
		static async Task Main(string[] args)
		{
			IAuthenticationService authService = Refit.RestService.For<IAuthenticationService>("http://192.168.0.3:5001");

			Console.Write($"Username: ");
			string username = Console.ReadLine();

			Console.Write($"Password: ");
			string password = Console.ReadLine();

			Console.Write($"CharacterId: ");
			string characterId = Console.ReadLine();

			string token = (await authService.TryAuthenticate(new AuthenticationRequestModel(username, password))
				.ConfigureAwait(false)).AccessToken;

			HubConnection connection = new HubConnectionBuilder()
				.WithUrl("http://192.168.0.3:5008/realtime/textchat", options =>
				{
					options.Headers.Add(SocialNetworkConstants.CharacterIdRequestHeaderName, characterId);
					options.AccessTokenProvider = () => Task.FromResult(token);
				})
				.Build();

			await connection.StartAsync()
				.ConfigureAwait(false);

			connection.RegisterClientInterface<IRemoteSocialTextChatHubClient>(new TestClientHandler());

			SignalRForwardedIRemoteSocialTextChatHubClient client = new SignalRForwardedIRemoteSocialTextChatHubClient(connection);

			while(true)
			{
				/*string input = Console.ReadLine();

				if(input.Contains("/guild"))
				{
					await client.SendGuildChannelTextChatMessageAsync(new GuildChatMessageRequestModel(input))
						.ConfigureAwait(false);
				}
				else
					await client.SendZoneChannelTextChatMessageAsync(new ZoneChatMessageRequestModel(input))
						.ConfigureAwait(false);*/
			}
		}
	}

	public class TestClientHandler : IRemoteSocialTextChatHubClient
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
	}
}
