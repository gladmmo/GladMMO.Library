using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Newtonsoft.Json;

namespace GladMMO
{
	/// <summary>
	/// Response model used to respond to URL download requests for worlds.
	/// Either contains the resource URL or will contain an error code.
	/// </summary>
	[JsonObject]
	public sealed class ContentDownloadURLResponse : ISucceedable, IResponseModel<ContentDownloadURLResponseCode>
	{
		/// <inheritdoc />
		[JsonIgnore]
		public bool isSuccessful => ResultCode == ContentDownloadURLResponseCode.Success;

		/// <summary>
		/// A valid URL to download a world resource from.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)] //can be null if the request failed.
		public string DownloadURL { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public ContentDownloadURLResponseCode ResultCode { get; private set; }

		/// <inheritdoc />
		public ContentDownloadURLResponse(ContentDownloadURLResponseCode resultCode)
		{
			if(!Enum.IsDefined(typeof(ContentDownloadURLResponseCode), resultCode)) throw new ArgumentOutOfRangeException(nameof(resultCode), "Value should be defined in the WorldDownloadURLResponseCode enum.");

			if(resultCode == ContentDownloadURLResponseCode.Success)
				throw new ArgumentException("Cannot provide Success to a failing response ctor.", nameof(resultCode));

			ResultCode = resultCode;
		}

		/// <inheritdoc />
		public ContentDownloadURLResponse(string downloadUrl)
		{
			if(string.IsNullOrWhiteSpace(downloadUrl)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(downloadUrl));

			DownloadURL = downloadUrl;
			ResultCode = ContentDownloadURLResponseCode.Success;
		}

		/// <summary>
		/// Serializer ctor. DO NOT CALL
		/// </summary>
		public ContentDownloadURLResponse()
		{

		}
	}
}