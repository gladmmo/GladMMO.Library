using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	//TODO: This is mostly a copy-paste from Worlds, we need to significantly extend and refactor this for public avatars and private avatars support.
	[Route("api/[controller]")]
	public class AvatarController : BaseCustomControllerController<AvatarEntryModel>
	{
		public AvatarController(IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger, 
			UserContentType contentType)
			: base(claimsReader, logger, contentType)
		{
		}

		[HttpPost("{id}/uploaded")]
		[AuthorizeJwt]
		public async Task<IActionResult> SetAvatarAsUploaded([FromRoute(Name = "id")] long worldId, 
			[FromServices] IAvatarEntryRepository avatarEntryRepository, 
			[FromServices] IContentResourceExistenceVerifier contentExistenceVerifier)
		{
			//At this point, the user is telling us they finished uploading the world.
			//They could be lying so we should check that the resource exists AND
			//we should also check that it's an asset bundle and gather some information from the header.

			//First we verify a world exists with this id
			if(!await avatarEntryRepository.ContainsAsync(worldId).ConfigureAwait(false))
			{
				//TODO: We should say something more specific
				return BadRequest();
			}

			AvatarEntryModel model = await avatarEntryRepository.RetrieveAsync(worldId)
				.ConfigureAwait(false);

			//Check the model is associated with this account. Only 1 account can own a world resource
			if(model.AccountId != ClaimsReader.GetUserIdInt(User))
				return Unauthorized();

			//Now that we know the world is in the database and the account making this authorized requests owns it
			//we can now actually check that the resource exists on the storeage system
			//TODO: This relies on some outdated API/deprecated stuff.
			bool resourceExists = await contentExistenceVerifier.VerifyResourceExists(UserContentType.Avatar, model.StorageGuid)
				.ConfigureAwait(false); //TODO: Don't hardcore bucket name

			//TODO: Be more descriptive
			if(!resourceExists)
				return NotFound();

			//Ok, so the user IS the resource owner AND he did upload something, so let's validate the assetbundle header.
			//TODO: Refactor this into an object that does the validation and generates readable data
			//TODO: Actually implement asset bundle validation
			//We haven't implemented this yet, we should do asset bundle parsing and validation
			//This REALLY important to prevent invalid bundles from being uploaded
			//or content that isn't even an asset bundle being uploaded
			//See: https://github.com/HearthSim/UnityPack/wiki/Format-Documentation

			//For now, since it's unimplemented let's just set it validated
			await avatarEntryRepository.SetWorldValidated(model.AvatarId)
				.ConfigureAwait(false);

			return Ok();
		}

		protected override AvatarEntryModel GenerateNewModel()
		{
			int userId = ClaimsReader.GetUserIdInt(User);

			return new AvatarEntryModel(userId, HttpContext.Connection.RemoteIpAddress.ToString(), Guid.NewGuid());
		}
	}
}