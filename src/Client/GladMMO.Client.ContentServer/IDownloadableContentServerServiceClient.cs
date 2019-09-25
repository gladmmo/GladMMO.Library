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
	public interface IDownloadableContentServerServiceClient
	{
		/// <summary>
		/// Attempts to get a new URL that can be used to upload the world.
		/// If successful the URl contained in the response will contain a valid upload
		/// URL which can be used to upload world content.
		/// </summary>
		/// <returns>A model representing the result of the world URL generation request.</returns>
		[RequiresAuthentication]
		[Post("/api/World/create")]
		Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewWorldUploadUrl();

		/// <summary>
		/// Attempts to get a PATCH uploaded world's content.
		/// If successful the Token contained in the response will contain a valid upload
		/// URL which can be used to update the world content.
		/// </summary>
		/// <returns>A model representing the result of the world URL update request.</returns>
		[RequiresAuthentication]
		[Patch("/api/World/{id}")]
		Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> RequestUpdateExistingWorld([AliasAs("id")] long worldId);

		/// <summary>
		/// Attempts to get a new URL that can be used to upload the avatar.
		/// If successful the URl contained in the response will contain a valid upload
		/// URL which can be used to upload avatar content.
		/// </summary>
		/// <returns>A model representing the result of the avatar URL generation request.</returns>
		[RequiresAuthentication]
		[Post("/api/avatar/create")]
		Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewAvatarUploadUrl();

		/// <summary>
		/// Attempts to get a PATCH uploaded avatar's content.
		/// If successful the Token contained in the response will contain a valid upload
		/// URL which can be used to update the avatar content.
		/// </summary>
		/// <returns>A model representing the result of the avatar URL update request.</returns>
		[RequiresAuthentication]
		[Patch("/api/avatar/{id}")]
		Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> RequestUpdateExistingAvatar([AliasAs("id")] long avatarId);

		/// <summary>
		/// Attempts to get a new URL that can be used to upload the creature.
		/// If successful the URl contained in the response will contain a valid upload
		/// URL which can be used to upload creature content.
		/// </summary>
		/// <returns>A model representing the result of the creature URL generation request.</returns>
		[RequiresAuthentication]
		[Post("/api/Creature/create")]
		Task<ResponseModel<ContentUploadToken, ContentUploadResponseCode>> GetNewCreatureModelUploadUrl();

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