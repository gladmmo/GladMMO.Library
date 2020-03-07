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
		private ICreatureEntryRepository CreatureEntryRepository { get; }

		/// <inheritdoc />
		public CreatureNameQueryController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger,
			[JetBrains.Annotations.NotNull] ICreatureEntryRepository creatureEntryRepository)
			: base(claimsReader, logger)
		{
			CreatureEntryRepository = creatureEntryRepository ?? throw new ArgumentNullException(nameof(creatureEntryRepository));
		}

		protected override async Task<JsonResult> EntityNameQuery(ObjectGuid entityGuid)
		{
			if(entityGuid.TypeId <= 0)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			bool knownId = await CreatureEntryRepository.ContainsAsync(entityGuid.Entry);

			//TODO: JSON Response
			if(!knownId)
				return BuildFailedResponseModel(NameQueryResponseCode.UnknownIdError);

			//TODO: Make accessing template more efficient than loading ALL navaigation properties.
			//Else if it is a known id we should grab the name of the character
			CreatureEntryModel entryModel = await CreatureEntryRepository.RetrieveAsync(entityGuid.Entry, true);

			return BuildSuccessfulResponseModel(new NameQueryResponse(entryModel.CreatureTemplate.CreatureName));
		}
	}
}
