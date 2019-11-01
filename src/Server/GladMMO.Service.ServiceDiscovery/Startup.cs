using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Consul;
//using Consul.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public class Startup
	{
		//Changed in ASP Core 2.0
		public Startup(IConfiguration config)
		{
			if(config == null) throw new ArgumentNullException(nameof(config));

			Configuration = config;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddMvc()
				.RegisterHealthCheckController();

			services.AddLogging();

#if AZURE_RELEASE || AZURE_DEBUG
			services.AddSingleton<IRegionbasedNameEndpointResolutionRepository, AzureStaticEndpointRepository>();
#else
			//We're using an inmemory store for now that we populate with the file stored data
			//It needs to be singleton because we're doing it in memory and reloading per request would be bad
			//services.AddDbContext<NamedEndpointDbContext>(options => options.UseInMemoryDatabase(), ServiceLifetime.Singleton);
			//services.AddTransient<IRegionbasedNameEndpointResolutionRepository, DatabaseContextBasedRegionBasedNameEndpointResolutionRepository>();

			//We use a config file for now. Can move to Consul or db at another time.
			services.AddSingleton<IRegionNamedEndpointStoreRepository, FilestoreBasedRegionNamedEndpointStoreRepository>();
			services.AddSingleton<IRegionbasedNameEndpointResolutionRepository, FilestoreBasedRegionNamedEndpointStoreRepository>();

			//TODO: We don't actually want to use config files for this. But we do for now.
			//On local builds we want to use a different file
	#if !DEBUG_LOCAL && !RELEASE_LOCAL
			services.AddSingleton<IRegionalServiceFilePathBuilder, DeployedRegionalServiceFilePathBuilder>();
	#else
			services.AddSingleton<IRegionalServiceFilePathBuilder, LocalRegionalServiceFilePathBuilder>();
	#endif

#endif
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();

			//This adds CloudWatch AWS logging to this app
			loggerFactory.RegisterGuardiansLogging(Configuration);
			loggerFactory.AddDebug();

			app.UseMvcWithDefaultRoute();
		}
	}
}
