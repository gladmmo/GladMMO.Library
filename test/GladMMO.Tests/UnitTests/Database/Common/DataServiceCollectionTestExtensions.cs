using System; using FreecraftCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GladMMO
{
	public static class DataServiceCollectionTestExtensions
	{
		public static IServiceCollection AddDefaultDataTestServices(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddLogging();
			return serviceCollection;
		}

		public static IServiceCollection AddTestDatabaseContext<TDbContextType>(this IServiceCollection serviceCollection)
			where TDbContextType : DbContext
		{
			serviceCollection.AddDbContext<TDbContextType>(o =>
				{
					//Every call should be a fresh database
					o.UseInMemoryDatabase(Guid.NewGuid().ToString());
				})
				.AddEntityFrameworkInMemoryDatabase();

			return serviceCollection;
		}
	}
}