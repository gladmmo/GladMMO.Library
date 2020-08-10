using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO.Discord
{
	[JsonObject]
	public sealed class TestCommand : DiscordCommandModel<TestCommand>
	{
		[JsonProperty]
		public string Message { get; private set; }

		public TestCommand(string message)
		{
			Message = message;
		}

		[JsonConstructor]
		internal TestCommand()
		{
			
		}
	}
}
