using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	/// <summary>
	/// Base controller type for static read-only client content type.
	/// </summary>
	/// <typeparam name="TContentEntryType">The content entry type.</typeparam>
	/// <typeparam name="TContentRepositoryType">The repository dependency type.</typeparam>
	/// <typeparam name="TContentTransferType">The transfer DTO model type.</typeparam>
	public abstract class StaticClientOnlyContentController<TContentEntryType, TContentRepositoryType, TContentTransferType> : AuthorizationReadyController
		where TContentRepositoryType : IGenericRepositoryCrudable<int, TContentEntryType>, IEntireTableQueryable<TContentEntryType>
		where TContentTransferType : class
		where TContentEntryType : IDatabaseModelKeyable
	{
		protected StaticClientOnlyContentController(IClaimsPrincipalReader claimsReader, ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{

		}

		//TODO: Enable caching outside of development
		//[ResponseCache(Duration = 5000)]
		[ProducesJson]
		[HttpGet]
		public async Task<IActionResult> GetClientStaticContent([FromServices] [NotNull] TContentRepositoryType repository, 
			[FromServices] [NotNull] ITypeConverterProvider<TContentEntryType, TContentTransferType> converter)
		{
			if (repository == null) throw new ArgumentNullException(nameof(repository));
			if (converter == null) throw new ArgumentNullException(nameof(converter));

			TContentEntryType[] contentEntryTypes = await repository.RetrieveAllAsync();

			return Json(contentEntryTypes);
		}
	}
}