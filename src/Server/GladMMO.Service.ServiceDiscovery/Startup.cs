using System; using FreecraftCore;
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
			services.AddGladMMOCORS();

			// Add framework services.
			services.AddMvc()
				.RegisterHealthCheckController();

			services.AddLogging();

			//DefaultServiceEndpointRepository : IServiceEndpointRepository
			services.AddTransient<IServiceEndpointRepository, DefaultServiceEndpointRepository>();
			services.AddDbContext<GlobalDatabaseContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3306;Database=guardians.global;Uid=root;Pwd=test;"); });
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();

			//This adds CloudWatch AWS logging to this app
			loggerFactory.RegisterGuardiansLogging(Configuration);

			app.UseGladMMOCORSMiddleware();

			app.UseMvcWithDefaultRoute();
		}
	}
}
