using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace GladMMO
{
	public sealed class EchoCommandHandler : ModuleBase<SocketCommandContext>
	{
		// [Remainder] takes the rest of the command's arguments as one argument, rather than splitting every space
		[Command("echo")]
		public async Task EchoAsync([Remainder] string text)
		{
			// Insert a ZWSP before the text to prevent triggering other bots!
			await ReplyAsync('\u200B' + text);
		}
	}
}
