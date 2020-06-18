using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/namequery/GameObject")]
	public class GameObjectNameQueryController : BaseNameQueryController
	{
		public ITrinityGameObjectTemplateRepository GameObjectTemplateRepository { get; }

		/// <inheritdoc />
		public GameObjectNameQueryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger,
			[JetBrains.Annotations.NotNull] ITrinityGameObjectTemplateRepository gameObjectTemplateRepository)
			: base(claimsReader, logger)
		{
			GameObjectTemplateRepository = gameObjectTemplateRepository ?? throw new ArgumentNullException(nameof(gameObjectTemplateRepository));
		}

		protected override async Task<JsonResult> EntityNameQuery(ObjectGuid entityGuid)
		{
			if(entityGuid.TypeId <= 0)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			bool knownId = await GameObjectTemplateRepository.ContainsAsync((uint)entityGuid.Entry);

			//TODO: JSON Response
			if(!knownId)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			//TODO: Make accessing template more efficient than loading ALL navaigation properties.
			//Else if it is a known id we should grab the name of the creature
			var template = await GameObjectTemplateRepository.RetrieveAsync((uint)entityGuid.Entry);

			return BuildSuccessfulResponseModel(new NameQueryResponse(template.Name));
		}
	}
}
