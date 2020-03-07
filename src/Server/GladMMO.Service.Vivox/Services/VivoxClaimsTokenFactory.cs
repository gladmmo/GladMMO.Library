﻿using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinterpret.Net;

namespace GladMMO
{
	public sealed class VivoxClaimsTokenFactory : IFactoryCreatable<VivoxTokenClaims, VivoxTokenClaimsCreationContext>
	{
		//TODO: Make Issuer configurable
		private static string VIVOX_ISSUER = "vrguardian-vrg-dev";

		//TODO: Make Issuer configurable
		private static string VIVOX_DOMAIN = "vdx5.vivox.com";

		public VivoxTokenClaims Create([JetBrains.Annotations.NotNull] VivoxTokenClaimsCreationContext context)
		{
			if (context == null) throw new ArgumentNullException(nameof(context));

			string actionType = ConvertActionType(context.Action);

			switch (context.Action)
			{
				case VivoxAction.Login:
					return new VivoxTokenClaims(VIVOX_ISSUER, ComputeExpiryTime(), actionType, 1, $"sip:.{VIVOX_ISSUER}.{context.CharacterId}.@{VIVOX_DOMAIN}", null, null);
				case VivoxAction.JoinChannel:
					return HandleChannelJoinTokenCreation(context, actionType);
				default:
					throw new NotImplementedException($"TODO: Implement token generation for VivoxAction: {context.Action}");
			}
			
		}

		private static VivoxTokenClaims HandleChannelJoinTokenCreation(VivoxTokenClaimsCreationContext context, string actionType)
		{
			if (context.Channel == null)
				throw new ArgumentException($"Provided {nameof(VivoxTokenClaimsCreationContext)} for ChannelJoin lacks channel data.");

			//Properties for 3D audio
			//$"!p-{_audibleDistance}-{_conversationalDistance}-{_audioFadeIntensityByDistance.ToString("0.000", new System.Globalization.CultureInfo("en-US"))}-{(int) _audioFadeModel}";
			//Default values found in Vivox source
			/*_audibleDistance = 32;
			_conversationalDistance = 1;
			_audioFadeIntensityByDistance = 1.0f;
			_audioFadeModel = AudioFadeModel.InverseByDistance;*/
			string props = context.Channel.isPositionalChannel ? $"!p-{32}-{1}-{1.0f.ToString("0.000", new System.Globalization.CultureInfo("en-US"))}-{(int)1}" : String.Empty;
			
			//From ChannelId in the Vivox API assembly: $"sip:confctl-{GetUriDesignator(_type)}-{_issuer}.{_name}{props}@{_domain}"
			string channelURI = $"sip:confctl-{(context.Channel.isPositionalChannel ? "d" : "g")}-{VIVOX_ISSUER}.{context.Channel.ChannelName}{props}@{VIVOX_DOMAIN}";
			return new VivoxTokenClaims(VIVOX_ISSUER, ComputeExpiryTime(), actionType, 1, $"sip:.{VIVOX_ISSUER}.{context.CharacterId}.@{VIVOX_DOMAIN}", channelURI, null);
		}

		//We don't currently use this. It was a good idea but may not be supported as usernames for vivox.
		private unsafe string ComputeCharacterString(int contextCharacterId)
		{
			ObjectGuid playerGuid = new ObjectGuidBuilder()
				.WithType(EntityTypeId.TYPEID_PLAYER)
				.WithId(contextCharacterId)
				.Build();

			//Access raw memory of the guid.
			ulong playerRawGuid = playerGuid.RawGuidValue;
			byte* rawValue = (byte*) &playerRawGuid;

			//The idea here is we use the player's 64bit guid value directly
			//as the string character value. That way it can be moved to and from
			//the player guid efficiently.
			return Encoding.ASCII.GetString(rawValue, sizeof(ulong));
		}

		private static int ComputeExpiryTime()
		{
			//90 seconds is the example time found here: https://docs.vivox.com/v5/general/unity/5_1_0/Default.htm#AccessTokenDeveloperGuide/GeneratingTokensOnClientUnity.htm%3FTocPath%3DUnity%7CAccess%2520Token%2520Developer%2520Guide%7C_____6
			//This is basicallt from Vivox GetLoginToken. It's what they do with the provided TimeSpan.
			return (int) DateTimeOffset.UtcNow.AddSeconds(90).ToUnixTimeSeconds();
		}

		private static string ConvertActionType(VivoxAction action)
		{
			switch (action)
			{
				case VivoxAction.Login:
					return "login";
					break;
				case VivoxAction.JoinChannel:
					return "join";
				default:
					throw new NotImplementedException($"TODO: Implement string generation for VivoxAction: {action}");
			}
		}
	}
}
