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
	[Route("api/namequery/creature")]
	public class CreatureNameQueryController : BaseNameQueryController
	{
		private ITrinityCreatureTemplateRepository CreatureTemplateRepository { get; }

		/// <inheritdoc />
		public CreatureNameQueryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger,
			[JetBrains.Annotations.NotNull] ITrinityCreatureTemplateRepository creatureTemplateRepository)
			: base(claimsReader, logger)
		{
			CreatureTemplateRepository = creatureTemplateRepository ?? throw new ArgumentNullException(nameof(creatureTemplateRepository));
		}

		protected override async Task<JsonResult> EntityNameQuery(ObjectGuid entityGuid)
		{
			if(entityGuid.TypeId <= 0)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			bool knownId = await CreatureTemplateRepository.ContainsAsync((uint)entityGuid.Entry);

			//TODO: JSON Response
			if(!knownId)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			//TODO: Make accessing template more efficient than loading ALL navaigation properties.
			//Else if it is a known id we should grab the name of the creature
			CreatureTemplate template = await CreatureTemplateRepository.RetrieveAsync((uint)entityGuid.Entry);

			return BuildSuccessfulResponseModel(new NameQueryResponse(template.Name));
		}
	}
}
