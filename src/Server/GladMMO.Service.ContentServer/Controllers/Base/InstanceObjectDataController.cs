using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GladMMO.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public abstract class InstanceObjectDataController<TInstanceEntryModelType, TInstanceTransportModelType, TInstanceRepositoryType> : AuthorizationReadyController
		where TInstanceRepositoryType : IInstanceableWorldObjectRepository<TInstanceEntryModelType>, IGenericRepositoryCrudable<int, TInstanceEntryModelType>
		where TInstanceEntryModelType : IInstanceableWorldObjectModel, IModelEntryUpdateable<TInstanceEntryModelType>
		where TInstanceTransportModelType : class
	{
		protected InstanceObjectDataController(IClaimsPrincipalReader claimsReader,
			ILogger<AuthorizationReadyController> logger)
			: base(claimsReader, logger)
		{

		}

		[ProducesJson]
		[HttpGet("{world}/instance")]
		public async Task<IActionResult> GetObjectEntries([FromRoute(Name = "world")] long worldId,
			[FromServices] TInstanceRepositoryType instanceObjectRepository,
			[FromServices] ITypeConverterProvider<TInstanceEntryModelType, TInstanceTransportModelType> tableToNetworkModelConverter)
		{
			IReadOnlyCollection<TInstanceEntryModelType> models = await instanceObjectRepository.RetrieveAllWorldIdAsync((int) worldId);

			if (models.Count == 0)
				return BuildFailedResponseModel(CreatureCollectionResponseCode.NoneFound);

			TInstanceTransportModelType[] instanceModels = models
				.Select(tableToNetworkModelConverter.Convert)
				.ToArray();

			return BuildSuccessfulResponseModel(new ObjectEntryCollectionModel<TInstanceTransportModelType>(instanceModels));
		}

		//TODO: Eventually we need to require authorization, because they need to own the world.
		//[AuthorizeJwt]
		[ProducesJson]
		[HttpPost("{world}/instance")]
		public async Task<IActionResult> CreateObjectInstance([FromRoute(Name = "world")] long worldId,
			[FromServices] [NotNull] TInstanceRepositoryType instanceObjectRepository,
			[FromServices] [NotNull] IFactoryCreatable<TInstanceEntryModelType, WorldInstanceableEntryModelCreationContext> defaultInstanceEntryModelFactory,
			[FromServices] [NotNull] ITypeConverterProvider<TInstanceEntryModelType, TInstanceTransportModelType> tableToNetworkModelConverter)
		{
			if (instanceObjectRepository == null) throw new ArgumentNullException(nameof(instanceObjectRepository));
			if (defaultInstanceEntryModelFactory == null) throw new ArgumentNullException(nameof(defaultInstanceEntryModelFactory));
			if (tableToNetworkModelConverter == null) throw new ArgumentNullException(nameof(tableToNetworkModelConverter));

			TInstanceEntryModelType entry = defaultInstanceEntryModelFactory.Create(new WorldInstanceableEntryModelCreationContext(worldId));

			bool result = await instanceObjectRepository.TryCreateAsync(entry);

			//No known reason that this should fail.
			if (!result)
				return BuildFailedResponseModel(SceneContentQueryResponseCode.GeneralServerError);

			//Otherwise, it has been created so let's get it
			return await GetObjectInstance(entry.ObjectInstanceId, instanceObjectRepository, tableToNetworkModelConverter);
		}

		[ProducesJson]
		[HttpGet("instance/{id}")]
		public async Task<IActionResult> GetObjectInstance([FromRoute(Name = "id")] int objectId,
			[FromServices] TInstanceRepositoryType instanceObjectRepository,
			[FromServices] ITypeConverterProvider<TInstanceEntryModelType, TInstanceTransportModelType> tableToNetworkModelConverter)
		{
			//If unknown templateId, then just indicate such.
			if (!await instanceObjectRepository.ContainsAsync(objectId))
				return BuildFailedResponseModel(SceneContentQueryResponseCode.TemplateUnknown);

			//Load the model, convert and send back.
			TInstanceEntryModelType entryModel = await instanceObjectRepository.RetrieveAsync(objectId);
			TInstanceTransportModelType instanceModel = tableToNetworkModelConverter.Convert(entryModel);

			return BuildSuccessfulResponseModel(instanceModel);
		}

		[HttpPut("instance/{id}")]
		public async Task<IActionResult> UpdateObjectInstance(
			[FromBody] [NotNull] TInstanceTransportModelType model,
			[FromRoute(Name = "id")] int objectId,
			[FromServices] [NotNull] TInstanceRepositoryType instanceObjectRepository,
			[FromServices] [NotNull] ITypeConverterProvider<TInstanceTransportModelType, TInstanceEntryModelType> networkToTableConverter)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));
			if (instanceObjectRepository == null) throw new ArgumentNullException(nameof(instanceObjectRepository));
			if(networkToTableConverter == null) throw new ArgumentNullException(nameof(networkToTableConverter));

			//It's TRUE that they should be sending a valid GUID.
			//But it doesn't matter, it's not this action's responsibility to deal with it.
			//if(model.Guid.EntryId != creatureId)
			//	return BadRequest();

			TInstanceEntryModelType entryModel = await instanceObjectRepository.RetrieveAsync(objectId);

			//The idea here is we CONVERT the incoming data model to the table model type
			//and then we tell the table model type it needs to update.
			entryModel.Update(networkToTableConverter.Convert(model));

			//Then we save it.
			await instanceObjectRepository.UpdateAsync(objectId, entryModel);

			return Ok();
		}
	}
}
