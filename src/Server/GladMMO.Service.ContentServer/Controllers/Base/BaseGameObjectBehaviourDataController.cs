using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Base controller for <see cref="GameObjectEntryModel"/> attached behaviours.
	/// </summary>
	/// <typeparam name="TInstanceEntryModelType"></typeparam>
	/// <typeparam name="TInstanceTransportModelType"></typeparam>
	/// <typeparam name="TInstanceRepositoryType"></typeparam>
	public abstract class BaseGameObjectBehaviourDataController<TInstanceEntryModelType, TInstanceTransportModelType, TInstanceRepositoryType> : InstanceObjectDataController<TInstanceEntryModelType, TInstanceTransportModelType, TInstanceRepositoryType>
		where TInstanceRepositoryType : IInstanceableWorldObjectRepository<TInstanceEntryModelType>, IGenericRepositoryCrudable<int, TInstanceEntryModelType>
		where TInstanceEntryModelType : IInstanceableWorldObjectModel, IModelEntryUpdateable<TInstanceEntryModelType>
		where TInstanceTransportModelType : class
	{
		protected BaseGameObjectBehaviourDataController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		public override Task<IActionResult> CreateObjectInstance([FromRoute(Name = "id")] long worldId,
			[FromServices] TInstanceRepositoryType instanceObjectRepository,
			[FromServices] IFactoryCreatable<TInstanceEntryModelType, WorldInstanceableEntryModelCreationContext> defaultInstanceEntryModelFactory,
			[FromServices] ITypeConverterProvider<TInstanceEntryModelType, TInstanceTransportModelType> tableToNetworkModelConverter)
		{
			return Task.FromResult((IActionResult)BadRequest($"This request is not valid for {typeof(TInstanceTransportModelType).Name}."));
		}

		public override async Task<IActionResult> UpdateObjectInstance([FromBody] [NotNull] TInstanceTransportModelType model,
			[FromRoute(Name = "id")] int objectId,
			[FromServices] [NotNull] TInstanceRepositoryType instanceObjectRepository,
			[FromServices] [NotNull] ITypeConverterProvider<TInstanceTransportModelType, TInstanceEntryModelType> networkToTableConverter)
		{
			//So, this one is abit different since there is no created instance.
			//The client needs to create it, we don't assign a default behavior.
			//So it's possible it DOESN'T exist.
			if(await instanceObjectRepository.ContainsAsync(objectId))
				return await base.UpdateObjectInstance(model, objectId, instanceObjectRepository, networkToTableConverter);
			else
			{
				//it doesn't exist, we need to try to create it.
				TInstanceEntryModelType entryModel = networkToTableConverter.Convert(model);

				await instanceObjectRepository.TryCreateAsync(entryModel);
			}

			return Ok();
		}
	}
}
