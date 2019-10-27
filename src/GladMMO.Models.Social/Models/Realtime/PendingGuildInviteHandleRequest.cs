using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GladMMO
{
	/// <summary>
	/// Represents a player's response to a received <see cref="GuildMemberInviteEventModel"/>.
	/// </summary>
	public sealed class PendingGuildInviteHandleRequest : BaseSocialModel, ISucceedable
	{
		/// <summary>
		/// Indicates if the pending invite was accepted.
		/// </summary>
		[JsonProperty]
		public bool isSuccessful { get; private set; }

		public PendingGuildInviteHandleRequest(bool isSuccessful)
		{
			this.isSuccessful = isSuccessful;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private PendingGuildInviteHandleRequest()
		{
			
		}
	}
}
