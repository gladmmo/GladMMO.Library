using System;
using Newtonsoft.Json;

namespace GladMMO
{
	//From ProjectVindictive: https://github.com/HelloKitty/ProjectVindictive.Library/blob/master/src/ProjectVindictive.Models.UserContentManagement/RequestedUrlResponseModel.cs
	/// <summary>
	/// Response model used for when a user requests a URL.
	/// </summary>
	[JsonObject]
	public sealed class RequestedUrlResponseModel : IResponseModel<RequestedUrlResponseCode>, ISucceedable
	{
		//TODO: Create response code system
		/// <summary>
		/// Indicates if the 
		/// </summary>
		[JsonIgnore]
		public bool isSuccessful => ResultCode == RequestedUrlResponseCode.Success;

		/// <summary>
		/// The URL for the upload.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		public string UploadUrl { get; private set; }

		/// <summary>
		/// If not-null then it contains the error message.
		/// </summary>
		[JsonProperty(Required = Required.AllowNull)]
		public string ErrorMessage { get; private set; }

		/// <inheritdoc />
		[JsonProperty]
		public RequestedUrlResponseCode ResultCode { get; private set; }

		/// <summary>
		/// The GUID for the world uploaded.
		/// </summary>
		[JsonProperty]
		public long WorldId { get; private set; }

		private RequestedUrlResponseModel(string errorMessage, RequestedUrlResponseCode code)
		{
			if(string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(errorMessage));
			if(!Enum.IsDefined(typeof(RequestedUrlResponseCode), code)) throw new ArgumentOutOfRangeException(nameof(code), "Value should be defined in the UploadUrlResponseCode enum.");

			ErrorMessage = errorMessage;
		}

		private RequestedUrlResponseModel(string url, long worldId)
		{
			if(string.IsNullOrWhiteSpace(url)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(url));
			if(worldId <= 0) throw new ArgumentOutOfRangeException(nameof(worldId));

			UploadUrl = url;
			WorldId = worldId;
			ResultCode = RequestedUrlResponseCode.Success;
		}

		public static RequestedUrlResponseModel CreateSuccess(string url, long worldId)
		{
			return new RequestedUrlResponseModel(url, worldId);
		}

		public static RequestedUrlResponseModel CreateFailure(string errorMessage, RequestedUrlResponseCode code)
		{
			if(string.IsNullOrWhiteSpace(errorMessage)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(errorMessage));
			if(!Enum.IsDefined(typeof(RequestedUrlResponseCode), code)) throw new ArgumentOutOfRangeException(nameof(code), "Value should be defined in the UploadUrlResponseCode enum.");

			if(code == RequestedUrlResponseCode.Success)
				throw new ArgumentException($"Cannot use {nameof(RequestedUrlResponseCode.Success)} for failure.", nameof(code));

			return new RequestedUrlResponseModel(errorMessage, code);
		}

		/// <summary>
		/// Serializer ctor. DO NOT CALL.
		/// </summary>
		public RequestedUrlResponseModel()
		{

		}
	}
}