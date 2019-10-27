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
		public string MemberToInvite { get; private set; }

		public GuildMemberInviteRequestModel([JetBrains.Annotations.NotNull] string memberToInvite)
		{
			if (string.IsNullOrWhiteSpace(memberToInvite)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(memberToInvite));

			MemberToInvite = memberToInvite;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GuildMemberInviteRequestModel()
		{
			
		}
	}
}
