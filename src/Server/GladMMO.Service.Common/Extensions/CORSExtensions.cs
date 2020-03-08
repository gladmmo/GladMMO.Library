using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GladMMO
{
	public static class CORSExtensions
	{
		private const string CORS_POLICY_STRING = "AllowAll";

		public static IServiceCollection AddGladMMOCORS(this IServiceCollection services)
		{
#warning This is only good for development, not production.
			services.AddCors(options =>
			{
				options.AddPolicy(CORS_POLICY_STRING,
					builder =>
					{
						builder
							.AllowAnyOrigin()
							.AllowAnyMethod()
							.AllowAnyHeader();
					});
			});

			return services;
		}

		public static IApplicationBuilder UseGladMMOCORSMiddleware(this IApplicationBuilder app)
		{
			app.UseCors(CORS_POLICY_STRING);
			return app;
		}
	}
}
