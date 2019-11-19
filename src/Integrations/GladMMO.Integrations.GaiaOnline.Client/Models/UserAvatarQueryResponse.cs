using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GladMMO.GaiaOnline
{
	//From GaiaOnline Library
	//TODO: Add HaloLive response interfaces
	//TODO: They use different encoding for XML than the default XmlSerializer
	//profiles?mode=lookup&avatar_username=
	//Example: http://gaiaonline.com/profiles?mode=lookup&avatar_username=lm_a_kitty_cat
	//<response status="200" avatarPath="ava/9f/8b/28c7853cf78b9f.png?t=1503043840_6.00_10" userId="16223135" />
	/// <summary>
	/// Response model sent back by one of the endpoints in <see cref="IGaiaOnlineQueryClient"/>.
	/// Contains a manually encoded HTTP response code. This indicates the state of the model and the status of the response.
	/// It does not match the actual HTTP response code sent back, which is always 200.
	/// </summary>
	[XmlType("response")]
	public sealed class UserAvatarQueryResponse : ISucceedable
	{
		//TODO: Extract to interface
		/// <summary>
		/// The psuedo-response code for the response. Contains the actual response code
		/// that will indicate success or failure. Actal response code is different and is always 200.
		/// </summary>
		[XmlAttribute("status")]
		public int ResponseStatusCode { get; set; } //must be public setter for the XmlSerializer. May switch to DataContract instead.

		[XmlIgnore]
		public bool isSuccessful => ResponseStatusCode == 200;

		//This could be potentially null or empty if the request failed.
		/// <summary>
		/// The relative path to an avatar's URL.
		/// Fits the format: ava/{a}/{b}/{c}.png?t={d}
		/// d is currently unknown at this time.
		/// </summary>
		[XmlAttribute("avatarPath")]
		public string AvatarRelativeUrlPath { get; set; } //must be public setter for the XmlSerializer. May switch to DataContract instead.

		//This should be unique, though I can't prove that, and should
		//also be possible to map to username or avatar path. No endpoint is known for that yet though.
		//XML has issues with empty strings deserializing to ints, maybe only Mono? So we'll keep this as a string
		//This will only ever be "" when an error occurs anyway
		//TODO: Any use for this at the momemt?
		/// <summary>
		/// Unique UserId related to the Avatar.
		/// </summary>
		[XmlAttribute("userId")]
		public string UserId { get; set; } //must be public setter for the XmlSerializer. May switch to DataContract instead.

		public UserAvatarQueryResponse(int responseStatusCode, string avatarRelativeUrlPath, string userId)
		{
			if(string.IsNullOrWhiteSpace(avatarRelativeUrlPath)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(avatarRelativeUrlPath));
			if(string.IsNullOrWhiteSpace(userId)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(userId));

			ResponseStatusCode = responseStatusCode;
			AvatarRelativeUrlPath = avatarRelativeUrlPath;
			UserId = userId;
		}

		//TODO: Can we make this protected? Or will XML cry?
		public UserAvatarQueryResponse()
		{

		}

		/// <inheritdoc />
		public override string ToString()
		{
			//Just format it nicely.
			return $"{nameof(ResponseStatusCode)}: {ResponseStatusCode} {nameof(AvatarRelativeUrlPath)}: {AvatarRelativeUrlPath} {nameof(UserId)}: {UserId}";
		}
	}
}