using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO.Discord
{
	[JsonObject]
	public abstract class DiscordCommandModel
	{
		/// <summary>
		/// Represents the name of the command type.
		/// </summary>
		[JsonProperty]
		public abstract string CommandName { get; protected set; }

		protected DiscordCommandModel()
		{

		}
	}

	/// <summary>
	/// Abstract model type for Discord command types.
	/// </summary>
	/// <typeparam name="TChildModelType">The derived command type.</typeparam>
	[JsonObject]
	public abstract class DiscordCommandModel<TChildModelType> : DiscordCommandModel
		where TChildModelType : DiscordCommandModel<TChildModelType>
	{
		internal static string InternalCommandName = typeof(TChildModelType).Name.Replace("Command", "");

		/// <inheritdoc />
		[JsonProperty]
		public override string CommandName { get; protected set; } = InternalCommandName;

		protected DiscordCommandModel()
		{

		}
	}
}
