using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	//TODO: We should seperate editor service from the general content server that the client reads.
	//From ProjectVindictive: https://github.com/HelloKitty/ProjectVindictive.Library/blob/master/src/ProjectVindictive.SDK.Editor.Build/Client/IUserContentManagementServiceClient.cs
	//TODO: Automate user-agent SDK version headers
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IContentServerServiceClient
	{
		/// <summary>
		/// Attempts to get a new URL that can be used to upload the world.
		/// If successful the URl contained in the response will contain a valid upload
		/// URL which can be used to upload world content.
		/// </summary>
		/// <param name="authToken">The user authentication token.</param>
		/// <returns>A model representing the result of the world URL generation request.</returns>
		[Post("/api/World/create")]
		Task<RequestedUrlResponseModel> GetNewWorldUploadUrl([AuthenticationToken] string authToken);

		/// <summary>
		/// Attempts to get a new URL that can be used to upload the avatar.
		/// If successful the URl contained in the response will contain a valid upload
		/// URL which can be used to upload avatar content.
		/// </summary>
		/// <param name="authToken">The user authentication token.</param>
		/// <returns>A model representing the result of the avatar URL generation request.</returns>
		[Post("/api/avatar/create")]
		Task<RequestedUrlResponseModel> GetNewAvatarUploadUrl([AuthenticationToken] string authToken);

		/// <summary>
		/// Attempts to get a new URL that can be used to upload the creature.
		/// If successful the URl contained in the response will contain a valid upload
		/// URL which can be used to upload creature content.
		/// </summary>
		/// <param name="authToken">The user authentication token.</param>
		/// <returns>A model representing the result of the creature URL generation request.</returns>
		[Post("/api/avatar/create")]
		Task<RequestedUrlResponseModel> GetNewCreatureModelUploadUrl([AuthenticationToken] string authToken);

		//TODO: Doc
		[RequiresAuthentication]
		[Post("/api/World/{id}/downloadurl")]
		Task<ContentDownloadURLResponse> RequestWorldDownloadUrl([AliasAs("id")] long worldId);

		//TODO: Doc
		[RequiresAuthentication]
		[Post("/api/avatar/{id}/downloadurl")]
		Task<ContentDownloadURLResponse> RequestAvatarDownloadUrl([AliasAs("id")] long avatarId);

		//TODO: Doc
		[RequiresAuthentication]
		[Post("/api/Creature/{id}/downloadurl")]
		Task<ContentDownloadURLResponse> RequestCreatureModelDownloadUrl([AliasAs("id")] long creatureId);
	}
}