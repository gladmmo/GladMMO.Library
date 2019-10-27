using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GladMMO
{
	/// <summary>
	/// Only really used for failure.
	/// </summary>
	public sealed class GuildMemberInviteResponseModel : BaseSocialModel, IResponseModel<GuildMemberInviteResponseCode>, ISucceedable
	{
		[JsonProperty]
		public GuildMemberInviteResponseCode ResultCode { get; private set; }

		[JsonProperty]
		public NetworkEntityGuid InvitedEntityGuid { get; private set; }

		[JsonIgnore]
		public bool isSuccessful => ResultCode == GuildMemberInviteResponseCode.Success;

		public GuildMemberInviteResponseModel(GuildMemberInviteResponseCode resultCode)
		{
			if (!Enum.IsDefined(typeof(GuildMemberInviteResponseCode), resultCode)) throw new InvalidEnumArgumentException(nameof(resultCode), (int) resultCode, typeof(GuildMemberInviteResponseCode));

			ResultCode = resultCode;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private GuildMemberInviteResponseModel()
		{
			
		}
	}

	public enum GuildMemberInviteResponseCode
	{
		/// <summary>
		/// Indicates successful invite sent.
		/// </summary>
		Success = 1,

		GeneralServerError = 2,

		/// <summary>
		/// Indicates that the player is in a guild already.
		/// </summary>
		PlayerAlreadyInGuild = 3,

		/// <summary>
		/// Indicates the player declined to join the guild.
		/// </summary>
		PlayerDeclinedGuildInvite = 4,

		/// <summary>
		/// Indicates if a guild invite for the player is already pending.
		/// </summary>
		PlayerAlreadyHasPendingInvite = 5,

		/// <summary>
		/// Indicates the player isn't found.
		/// </summary>
		PlayerNotFound = 6,
	}
}
