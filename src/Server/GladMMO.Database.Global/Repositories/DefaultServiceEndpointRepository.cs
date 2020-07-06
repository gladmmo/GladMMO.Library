using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GladMMO
{
	public sealed class DefaultServiceEndpointRepository : IServiceEndpointRepository
	{
		private GlobalDatabaseContext Context { get; }

		public DefaultServiceEndpointRepository([JetBrains.Annotations.NotNull] GlobalDatabaseContext context)
		{
			Context = context ?? throw new ArgumentNullException(nameof(context));
		}

		public async Task<bool> ContainsAsync([JetBrains.Annotations.NotNull] ServiceEndpointKey key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			return await Context
				.ServiceEndpoints
				.AnyAsync(se => se.Locale == key.Locale && se.Mode == key.Mode && se.Service.ServiceName == key.ServiceName);
		}

		public async Task<bool> TryCreateAsync([JetBrains.Annotations.NotNull] ServiceEndpointModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			await Context
				.AddAsync(model);

			return await Context.SaveChangesAsync(true) != 0;
		}

		public async Task<ServiceEndpointModel> RetrieveAsync([JetBrains.Annotations.NotNull] ServiceEndpointKey key, bool includeNavigationProperties = false)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			return await Context
				.ServiceEndpoints
				.FirstAsync(se => se.Locale == key.Locale && se.Mode == key.Mode && se.Service.ServiceName == key.ServiceName);
		}

		public async Task<bool> TryDeleteAsync([JetBrains.Annotations.NotNull] ServiceEndpointKey key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			if (!await ContainsAsync(key))
				return false;

			var model = await RetrieveAsync(key);

			Context.ServiceEndpoints
				.Remove(model);

			return await Context.SaveChangesAsync(true) != 0;
		}

		public async Task UpdateAsync( ServiceEndpointKey key, [JetBrains.Annotations.NotNull] ServiceEndpointModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			Context.Update(model);
			await Context.SaveChangesAsync(true);
		}
	}
}
