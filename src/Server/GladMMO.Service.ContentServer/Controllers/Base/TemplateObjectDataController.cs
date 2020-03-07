using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public abstract class TemplateObjectDataController<TTemplateEntryType, TTemplateTransferType, TInstanceRepositoryType> : AuthorizationReadyController
		where TInstanceRepositoryType : IGenericRepositoryCrudable<int, TTemplateEntryType>, ITemplateableWorldObjectRepository<TTemplateEntryType>
		where TTemplateTransferType : class
		where TTemplateEntryType : IDatabaseModelKeyable
	{
		protected TemplateObjectDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger)
			: base(claimsReader, logger)
		{

		}

		[ProducesJson]
		[HttpGet("template/{id}")]
		public async Task<IActionResult> GetObjectTemplate([FromRoute(Name = "id")] int objectTemplateId,
			[FromServices] [NotNull] TInstanceRepositoryType objectTemplateRepository,
			[FromServices] [NotNull] ITypeConverterProvider<TTemplateEntryType, TTemplateTransferType> tableToNetworkModelConverter)
		{
			if (objectTemplateRepository == null) throw new ArgumentNullException(nameof(objectTemplateRepository));
			if (tableToNetworkModelConverter == null) throw new ArgumentNullException(nameof(tableToNetworkModelConverter));

			//If unknown templateId, then just indicate such.
			if(!await objectTemplateRepository.ContainsAsync(objectTemplateId))
				return BuildFailedResponseModel(SceneContentQueryResponseCode.TemplateUnknown);

			//Load the model, convert and send back.
			TTemplateEntryType entryModel = await objectTemplateRepository.RetrieveAsync(objectTemplateId);
			TTemplateTransferType templateModel = tableToNetworkModelConverter.Convert(entryModel);

			return BuildSuccessfulResponseModel(templateModel);
		}

		[ProducesJson]
		[HttpGet("{world}/template")]
		public async Task<IActionResult> GetObjectTemplatesForWorld([FromRoute(Name = "world")] int worldId,
			[FromServices] TInstanceRepositoryType objectTemplateRepository,
			[FromServices] ITypeConverterProvider<TTemplateEntryType, TTemplateTransferType> tableToNetworkModelConverter)
		{
			//We can actually get all the templates FROM the creature instances.
			IReadOnlyCollection<TTemplateEntryType> models = await objectTemplateRepository.RetrieveTemplatesByWorldIdAsync((int)worldId);

			if(models.Count == 0)
				return BuildFailedResponseModel(ContentEntryCollectionResponseCode.NoneFound);

			TTemplateTransferType[] templateModels = models
				.Distinct(DatabaseModelKeyableEquailityComparer<TTemplateEntryType>.Instance)
				.Select(tableToNetworkModelConverter.Convert)
				.ToArray();

			return BuildSuccessfulResponseModel(new ObjectEntryCollectionModel<TTemplateTransferType>(templateModels));
		}
	}
}
