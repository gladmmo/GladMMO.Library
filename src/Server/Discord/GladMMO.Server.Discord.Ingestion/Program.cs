using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace GladMMO
{
	class Program
	{
		static async Task Main(string[] args)
		{
			CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

			await using (var container = BuildServiceContainer())
			{
				//Let's go FOREVER baby
				while (true)
				{
					await using(var scope = container.BeginLifetimeScope())
					{
						var client = scope.Resolve<DiscordSocketClient>();

						client.Log += LogAsync;
						scope.Resolve<CommandService>().Log += LogAsync;

						// Tokens should be considered secret data and never hard-coded.
						// We can read from the environment variable to avoid hardcoding.
						await client.LoginAsync(TokenType.Bot, "NzM5NzU0NTczNzgyMzE5MTM2.XyfEIg.h6kXJp7dSO1aav3_HiapmfgWQTg");
						await client.StartAsync();

						// Here we initialize the logic required to register our commands.
						await scope.Resolve<CommandHandlingService>().InitializeAsync();

						await cancelTokenSource.Token;
					}
				}
			}
		}

		private static Task LogAsync(LogMessage arg)
		{
			Console.WriteLine($"{arg.Severity}: {arg.Message}");
			return Task.CompletedTask;
		}

		private static IContainer BuildServiceContainer()
		{
			ContainerBuilder builder = new ContainerBuilder();

			builder.RegisterType<DiscordSocketClient>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CommandService>()
				.InstancePerLifetimeScope();

			builder.RegisterType<HttpClient>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CommandHandlingService>()
				.InstancePerLifetimeScope();

			return builder.Build();
		}
	}
}
