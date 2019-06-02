using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	public sealed class CharacterGuildMembershipStatusResponse : IResponseModel<CharacterGuildMembershipStatusResponseCode>, ISucceedable
	{
		/// <inheritdoc />
		[JsonProperty]
		public CharacterGuildMembershipStatusResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == CharacterGuildMembershipStatusResponseCode.Success;

		[JsonProperty]
		public int GuildId { get; private set; }

		/// <inheritdoc />
		public CharacterGuildMembershipStatusResponse(CharacterGuildMembershipStatusResponseCode resultCode)
		{
			//TODO: Verify that it's not success.
			ResultCode = resultCode;
		}

		/// <inheritdoc />
		public CharacterGuildMembershipStatusResponse(int guildId)
		{
			if(guildId <= 0) throw new ArgumentOutOfRangeException(nameof(guildId), "Should use other ctor if a guild does not exist for the character.");

			ResultCode = CharacterGuildMembershipStatusResponseCode.Success;
			GuildId = guildId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private CharacterGuildMembershipStatusResponse()
		{
			
		}
	}

	public enum CharacterGuildMembershipStatusResponseCode
	{
		/// <summary>
		/// Indicates the creation was successful.
		/// </summary>
		Success = 1,

		/// <summary>
		/// Indicates that the character is not in a guild.
		/// </summary>
		NoGuild = 2,

		/// <summary>
		/// Indicates an unknown server error.
		/// </summary>
		GeneralServerError = 3,
	}
}
