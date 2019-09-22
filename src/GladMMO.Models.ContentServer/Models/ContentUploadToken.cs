using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class ContentUploadToken
	{
		/// <summary>
		/// The URL for the upload.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		public string UploadUrl { get; private set; }

		/// <summary>
		/// The ID for the content upload.
		/// </summary>
		[JsonProperty]
		public long ContentId { get; private set; }

		/// <summary>
		/// The GUID for the content upload.
		/// </summary>
		[JsonProperty]
		public Guid ContentGuid { get; private set; }

		public ContentUploadToken([NotNull] string uploadUrl, long contentId, Guid contentGuid)
		{
			if(string.IsNullOrWhiteSpace(uploadUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(uploadUrl));
			if(contentId <= 0) throw new ArgumentOutOfRangeException(nameof(contentId));

			UploadUrl = uploadUrl;
			ContentId = contentId;
			ContentGuid = contentGuid;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private ContentUploadToken()
		{
			
		}
	}
}
