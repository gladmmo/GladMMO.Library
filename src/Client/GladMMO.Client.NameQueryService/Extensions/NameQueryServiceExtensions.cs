using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GladMMO
{
	public static class NameQueryServiceExtensions
	{
		/// <summary>
		/// Retrieves the name of the entity
		/// from the provided <see cref="entity"/>.
		/// </summary>
		/// <param name="nameQueryService">Service being extended.</param>
		/// <param name="entity">The id of the entity.</param>
		/// <returns>Result of the namequery.</returns>
		public static Task<NameQueryResponse> RetrieveAsync([JetBrains.Annotations.NotNull] this INameQueryService nameQueryService, NetworkEntityGuid entity)
		{
			if(nameQueryService == null) throw new ArgumentNullException(nameof(nameQueryService));
			return nameQueryService.RetrieveAsync(entity.RawGuidValue);
		}
	}
}
