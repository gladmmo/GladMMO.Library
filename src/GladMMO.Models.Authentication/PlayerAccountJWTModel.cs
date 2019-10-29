using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Player specified JWT Token model.
	/// </summary>
	[JsonObject]
	public sealed class PlayerAccountJWTModel : JWTModel
	{
		[CanBeNull]
		[JsonProperty(PropertyName = "playfab_token", Required = Required.Default)] //optional because could be a valid token
		public string PlayfabAuthenticationToken { get; internal set; }

		[CanBeNull]
		[JsonProperty(PropertyName = "playfab_id", Required = Required.Default)] //optional because could be a valid token
		public string PlayfabId { get; internal set; }

		/// <summary>
		/// Creates a PlayerAccountJWTModel that contains an valid non-null <see cref="accessToken"/>.
		/// </summary>
		/// <param name="accessToken"></param>
		public PlayerAccountJWTModel([NotNull] string accessToken)
			: base(accessToken)
		{

		}

		/// <summary>
		/// Creates an invalid <see cref="PlayerAccountJWTModel"/> that contains an <see cref="error"/> and <see cref="errorDescription"/>.
		/// </summary>
		/// <param name="error"></param>
		/// <param name="errorDescription"></param>
		public PlayerAccountJWTModel([NotNull] string error, [NotNull] string errorDescription)
			: base(error, errorDescription)
		{

		}

		/// <summary>
		/// Serializer ctor
		/// </summary>
		[JsonConstructor]
		protected PlayerAccountJWTModel() 
			: base()
		{

		}

		private bool CheckIfTokenIsValid()
		{
			return !String.IsNullOrEmpty(AccessToken) && String.IsNullOrEmpty(Error) && String.IsNullOrEmpty(ErrorDescription);
		}
	}
}
