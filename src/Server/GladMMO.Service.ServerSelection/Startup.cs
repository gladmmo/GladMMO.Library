using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GladMMO
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc()
				.RegisterHealthCheckController();

			services.AddLogging();

			services.AddResponseCaching();

#if !DEBUG_LOCAL && !RELEASE_LOCAL
			//TODO: Support database/consul/register gameservers
			//Probably wanna support healthchecks at some point too
			services.AddSingleton<IGameServersStoreRepository, DefaultDevelopmentGameServersRepository>();
#else
			services.AddSingleton<IGameServersStoreRepository, LocalDevelopmentGameServersRepository>();
#endif
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();

			//This allows response caching to work in app too
			//the headers should do caching elsewhere too but this is another level
			//to help keep up with potential high load scenarios where many users are at the selection screen.
			app.UseResponseCaching();

			loggerFactory.RegisterGuardiansLogging(Configuration);

			app.UseMvc();
		}
	}
}
