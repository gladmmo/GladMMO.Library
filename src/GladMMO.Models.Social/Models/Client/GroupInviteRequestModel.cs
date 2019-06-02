using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Data model for group invitation requests.
	/// </summary>
	[JsonObject]
	public sealed class GroupInviteRequestModel
	{
		/// <summary>
		/// Indicates if the invite request is valid.
		/// </summary>
		[JsonIgnore]
		public bool isValidInviteRequest => EntityToInvite != null && EntityToInvite != NetworkEntityGuid.Empty; 

		/// <summary>
		/// The GUID of the entity to invite.
		/// </summary>
		[JsonProperty]
		public NetworkEntityGuid EntityToInvite { get; private set; }

		/// <inheritdoc />
		public GroupInviteRequestModel([JetBrains.Annotations.NotNull] NetworkEntityGuid entityToInvite)
		{
			EntityToInvite = entityToInvite ?? throw new ArgumentNullException(nameof(entityToInvite));
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private GroupInviteRequestModel()
		{
			
		}
	}
}
