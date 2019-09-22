using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public abstract class BaseCustomContentController<TContentType> : AuthorizationReadyController 
		where TContentType : IClientContentPersistable
	{
		protected UserContentType ContentType { get; }

		protected BaseCustomContentController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger, 
			UserContentType contentType) 
			: base(claimsReader, logger)
		{
			if(!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int)contentType, typeof(UserContentType));

			ContentType = contentType;
		}

		/// <summary>
		/// POST request that requests an a download URL for a content.
		/// The user must be authorized.
		/// </summary>
		/// <returns>A <see cref="ContentDownloadURLResponse"/> that either contains error information or the upload URL if it was successful.</returns>
		[HttpPost("{id}/downloadurl")]
		[AuthorizeJwt]
		[NoResponseCache]
		public async Task<IActionResult> RequestContentDownloadURL(
			[FromRoute(Name = "id")] long contentId,
			[FromServices] ICustomContentRepository<TContentType> contentEntryRepository,
			[FromServices] IStorageUrlBuilder urlBuilder,
			[FromServices] IContentDownloadAuthroizationValidator downloadAuthorizer)
		{
			if(contentEntryRepository == null) throw new ArgumentNullException(nameof(contentEntryRepository));

			//TODO: We want to rate limit access to this API
			//TODO: We should use both app logging but also another logging service that always gets hit

			//TODO: Consolidate this shared logic between controllers
			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Recieved {nameof(RequestContentDownloadURL)} request from {ClaimsReader.GetUserName(User)}:{ClaimsReader.GetUserId(User)}.");

			//TODO: We should probably check the flags of content to see if it's private (IE hidden from user). Or if it's unlisted or removed.
			//It's possible a user is requesting a content that doesn't exist
			//Could be malicious or it could have been deleted for whatever reason
			if(!await contentEntryRepository.ContainsAsync(contentId).ConfigureAwait(false))
				return Json(new ContentDownloadURLResponse(ContentDownloadURLResponseCode.NoContentId));

			//TODO: Refactor this into a validation dependency
			//Now we need to do some validation to determine if they should even be downloading this content
			//we do not want people downloading a content they have no business of going to
			int userId = ClaimsReader.GetUserIdInt(User);

			//TODO: Need to NOT call it world content
			if(!await downloadAuthorizer.CanUserAccessWorldContet(userId, contentId))
				return Json(new ContentDownloadURLResponse(ContentDownloadURLResponseCode.AuthorizationFailed));

			//We can get the URL from the urlbuilder if we provide the content storage GUID
			string downloadUrl = await urlBuilder.BuildRetrivalUrl(ContentType, (await contentEntryRepository.RetrieveAsync(contentId)).StorageGuid);

			//TODO: Should we be validating S3 availability?
			if(String.IsNullOrEmpty(downloadUrl))
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to create content upload URL for {ClaimsReader.GetUserName(User)}:{ClaimsReader.GetUserId(User)} with ID: {contentId}.");

				return Json(new ContentDownloadURLResponse(ContentDownloadURLResponseCode.ContentDownloadServiceUnavailable));
			}

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Success. Sending {ClaimsReader.GetUserName(User)} URL: {downloadUrl}");

			return Json(new ContentDownloadURLResponse(downloadUrl));
		}

		/// <summary>
		/// PATCH request that requests to update an already uploaded URL for a content.
		/// The user must be authorized and MUST own the content.
		/// </summary>
		/// <returns>A <see cref="ContentUploadToken"/> that either contains error information or the upload URL if it was successful.</returns>
		[HttpPatch("{id}")]
		[AuthorizeJwt]
		public async Task<IActionResult> UpdateUploadedContent(
			[FromRoute(Name = "id")] long contentId,
			[FromServices] ICustomContentRepository<TContentType> contentEntryRepository,
			[FromServices] IStorageUrlBuilder urlBuilder)
		{
			//Content must exist.
			if (!await contentEntryRepository.ContainsAsync(contentId))
				return BuildFailedResponseModel(ContentUploadResponseCode.InvalidRequest);

			//Unlike creation, we just load an existing one.
			TContentType content = await contentEntryRepository.RetrieveAsync(contentId)
				.ConfigureAwait(false);

			//WE MUST MAKE SURE THE AUTHORIZED USER OWNS THE CONTENT!!
			if(ClaimsReader.GetUserIdInt(User) != content.AccountId)
				return BuildFailedResponseModel(ContentUploadResponseCode.AuthorizationFailed);

			return await GenerateUploadTokenResponse(urlBuilder, content);
		}

		/// <summary>
		/// POST request that requests an upload URL for a content.
		/// The user must be authorized.
		/// </summary>
		/// <returns>A <see cref="ContentUploadToken"/> that either contains error information or the upload URL if it was successful.</returns>
		[HttpPost("create")]
		[AuthorizeJwt]
		public async Task<IActionResult> RequestContentUploadUrl(
			[FromServices] ICustomContentRepository<TContentType> contentEntryRepository, 
			[FromServices] IStorageUrlBuilder urlBuilder)
		{
			if(contentEntryRepository == null) throw new ArgumentNullException(nameof(contentEntryRepository));

			//TODO: We want to rate limit access to this API
			//TODO: We should use both app logging but also another logging service that always gets hit

			if(Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Recieved {nameof(RequestContentUploadUrl)} request from {ClaimsReader.GetUserName(User)}:{ClaimsReader.GetUserId(User)}.");

			//TODO: Check if the result is valid? We should maybe return bool from this API (we do return bool from this API now)
			//The idea is to create an entry which will contain a GUID. From that GUID we can then generate the upload URL
			TContentType content = GenerateNewModel();
			bool result = await contentEntryRepository.TryCreateAsync(content);

			return await GenerateUploadTokenResponse(urlBuilder, content);
		}

		private async Task<IActionResult> GenerateUploadTokenResponse(IStorageUrlBuilder urlBuilder, TContentType content)
		{
			string uploadUrl = await urlBuilder.BuildUploadUrl(ContentType, content.StorageGuid);

			if (String.IsNullOrEmpty(uploadUrl))
			{
				if (Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Failed to create content upload URL for {ClaimsReader.GetUserName(User)}:{ClaimsReader.GetUserId(User)} with GUID: {content.StorageGuid}.");

				return BuildFailedResponseModel(ContentUploadResponseCode.ServiceUnavailable);
			}

			if (Logger.IsEnabled(LogLevel.Information))
				Logger.LogInformation($"Success. Sending {ClaimsReader.GetUserName(User)} URL: {uploadUrl}");

			return BuildSuccessfulResponseModel(new ContentUploadToken(uploadUrl, content.ContentId, content.StorageGuid));
		}

		protected abstract TContentType GenerateNewModel();
	}
}
