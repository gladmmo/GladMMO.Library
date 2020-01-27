using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	/// <summary>
	/// Contract for types that implement async endpoints for
	/// a Refit service.
	/// </summary>
	/// <typeparam name="TRestInterfaceType"></typeparam>
	public class BaseAsyncEndpointService<TRestInterfaceType>
	{
		/// <summary>
		/// Future for the generated service.
		/// </summary>
		private Task<TRestInterfaceType> GeneratedRestService { get; }

		/// <inheritdoc />
		public BaseAsyncEndpointService(Task<string> futureEndpoint)
		{
			if(futureEndpoint == null) throw new ArgumentNullException(nameof(futureEndpoint));
			GeneratedRestService = BuildRestService(futureEndpoint);
		}

		public BaseAsyncEndpointService(Task<string> futureEndpoint, RefitSettings settings)
		{
			if(futureEndpoint == null) throw new ArgumentNullException(nameof(futureEndpoint));
			if(settings == null) throw new ArgumentNullException(nameof(settings));

			GeneratedRestService = BuildRestService(futureEndpoint, settings);
		}

		private async Task<TRestInterfaceType> BuildRestService(Task<string> futureEndpoint)
		{
			if(futureEndpoint == null) throw new ArgumentNullException(nameof(futureEndpoint));

			return RestService.For<TRestInterfaceType>(await futureEndpoint.ConfigureAwaitFalse());
		}

		private async Task<TRestInterfaceType> BuildRestService(Task<string> futureEndpoint, RefitSettings settings)
		{
			if(futureEndpoint == null) throw new ArgumentNullException(nameof(futureEndpoint));

			return RestService.For<TRestInterfaceType>(await futureEndpoint.ConfigureAwaitFalse(), settings);
		}

		/// <summary>
		/// Indicates if the service finally knows the endpoint.
		/// </summary>
		/// <returns>Indicates if the endpoint is available.</returns>
		protected bool IsEndpointAvailable()
		{
			return GeneratedRestService.IsCompleted && !GeneratedRestService.IsFaulted;
		}

		protected Task<TRestInterfaceType> GetService()
		{
			return GeneratedRestService;
		}
	}
}
