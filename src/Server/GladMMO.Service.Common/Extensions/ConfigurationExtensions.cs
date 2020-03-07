using System; using FreecraftCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GladMMO
{
	public static class ConfigurationExtensions
	{
		/// <summary>
		/// Registers <see cref="IOptions{TOptions}"/> in the service container.
		/// The config must be available in the provided <see cref="configuration"/>.
		/// </summary>
		/// <param name="services">Service container to register to.</param>
		/// <param name="configuration">The configuration object.</param>
		/// <returns>The service collection to fluently build on.</returns>
		public static IServiceCollection RegisterDatabaseConfigOptions(this IServiceCollection services, IConfigurationRoot configuration)
		{
			if(services == null) throw new ArgumentNullException(nameof(services));
			if(configuration == null) throw new ArgumentNullException(nameof(configuration));

			return services.RegisterConfigOptions<DatabaseConfigModel>(configuration);
		}

		/// <summary>
		/// Registers <see cref="IOptions{TOptions}"/> in the service container.
		/// The config must be available in the provided <see cref="configuration"/>.
		/// </summary>
		/// <param name="services">Service container to register to.</param>
		/// <param name="configuration">The configuration object.</param>
		/// <returns>The service collection to fluently build on.</returns>
		public static IServiceCollection RegisterConfigOptions<TConfigType>(this IServiceCollection services, IConfiguration configuration) 
			where TConfigType : class
		{
			if(services == null) throw new ArgumentNullException(nameof(services));
			if(configuration == null) throw new ArgumentNullException(nameof(configuration));

			//Fluent return
			return services.Configure<TConfigType>(options => configuration.GetSection(typeof(TConfigType).Name).Bind(options)); //there a better way?
		}

		/// <summary>
		/// Gets the configuration object from the service container.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <returns>The options model if available. Throws otherwise.</returns>
		public static IOptions<TConfigType> GetConfig<TConfigType>(this IServiceCollection services) 
			where TConfigType : class, new()
		{
			IOptions<TConfigType> dbConfig = services.BuildServiceProvider().GetService<IOptions<TConfigType>>();

			return dbConfig;
		}

		/// <summary>
		/// Gets the configuration object from the service container.
		/// </summary>
		/// <param name="services">Service container.</param>
		/// <returns>The options model if available. Throws otherwise.</returns>
		public static IOptions<DatabaseConfigModel> GetDatabaseConfig(this IServiceCollection services)
		{
			IOptions<DatabaseConfigModel> dbConfig = services.GetConfig<DatabaseConfigModel>();

			if(dbConfig == null)
				throw new InvalidOperationException($"Failed to load {nameof(DatabaseConfigModel)} configuration.");

			if(String.IsNullOrWhiteSpace(dbConfig.Value.ConnectionString))
				throw new InvalidOperationException("No connection string is found in the configuration.");

			return dbConfig;
		}
	}
}
