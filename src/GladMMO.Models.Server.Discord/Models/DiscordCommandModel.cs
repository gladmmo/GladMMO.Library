using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Abstract model type for Discord command types.
	/// </summary>
	/// <typeparam name="TChildModelType">The derived command type.</typeparam>
	[JsonObject]
	public abstract class DiscordCommandModel<TChildModelType>
		where TChildModelType : DiscordCommandModel<TChildModelType>
	{
		/// <summary>
		/// Represents the name of the command type.
		/// </summary>
		[JsonProperty]
		public static string CommandName { get; private set; } = typeof(TChildModelType).Name.Replace("Command", "");

		protected DiscordCommandModel()
		{

		}
	}
}
