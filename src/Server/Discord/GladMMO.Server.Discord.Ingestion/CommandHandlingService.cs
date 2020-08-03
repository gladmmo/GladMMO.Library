using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace GladMMO
{
	public class CommandHandlingService
	{
		private CommandService CommandService { get; }

		private DiscordSocketClient Client { get; }

		private IServiceProvider Container { get; }

		public CommandHandlingService(CommandService commandService, DiscordSocketClient client, ILifetimeScope scope)
		{
			CommandService = commandService;
			Client = client;

			// Hook CommandExecuted to handle post-command-execution logic.
			CommandService.CommandExecuted += CommandExecutedAsync;

			// Hook MessageReceived so we can process each message to see
			// if it qualifies as a command.
			Client.MessageReceived += MessageReceivedAsync;

			Container = new AutofacServiceProvider(scope);
		}

		public async Task InitializeAsync()
		{
			// Register modules that are public and inherit ModuleBase<T>.
			await CommandService.AddModulesAsync(Assembly.GetEntryAssembly(), Container);
		}

		public async Task MessageReceivedAsync(SocketMessage rawMessage)
		{
			// Ignore system messages, or messages from other bots
			if(!(rawMessage is SocketUserMessage message)) 
				return;

			if(message.Source != MessageSource.User) 
				return;

			// This value holds the offset where the prefix ends
			var argPos = 0;

			// Perform prefix check. You may want to replace this with
			// (!message.HasCharPrefix('!', ref argPos))
			// for a more traditional command format like !help.
			if (message.HasMentionPrefix(Client.CurrentUser, ref argPos))
				return;
			else
			{
				//Don't continue if it's not a command
				if (!message.HasCharPrefix('!', ref argPos))
					return;
			}

			SocketCommandContext context = new SocketCommandContext(Client, message);

			// Perform the execution of the command. In this method,
			// the command service will perform precondition and parsing check
			// then execute the command if one is matched.
			await CommandService.ExecuteAsync(context, argPos, Container);

			// Note that normally a result will be returned by this format, but here
			// we will handle the result in CommandExecutedAsync,
		}

		public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
		{
			// command is unspecified when there was a search failure (command not found); we don't care about these errors
			if(!command.IsSpecified)
				return;

			// the command was successful, we don't care about this result, unless we want to log that a command succeeded.
			if(result.IsSuccess)
				return;

			// the command failed, let's notify the user that something happened.
			await context.Channel.SendMessageAsync($"error: {result}");
		}
	}
}
