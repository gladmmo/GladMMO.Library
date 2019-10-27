using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class GuildMemberInviteRequestModel : BaseSocialModel
	{
		[JsonProperty]
		public NetworkEntityGuid MemberToInviteEntityGuid { get; private set; }

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GuildMemberInviteRequestModel()
		{
			
		}
	}
}
