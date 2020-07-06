using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
			services.AddMvc(options =>
				{
					options.EnableEndpointRouting = false;
				})
				.AddNewtonsoftJson()
				.RegisterHealthCheckController();

			X509Certificate2 cert = null;
			string certPath = "Certs/TestCert.pfx";

			try
			{
				cert = X509Certificate2Loader.Create(certPath).Load();
			}
			catch(Exception e)
			{
				throw new System.InvalidOperationException($"Failed to load {nameof(X509Certificate2)} from Path: {certPath} \n\n StackTrace: {e.StackTrace}", e);
			}

			services.AddResponseCaching();
			services.AddLogging();

			//This provides JwtBearer support for Authorize attribute/header
			services.AddJwtAuthorization(cert);

			services.AddTransient<IFactoryCreatable<VivoxTokenClaims, VivoxTokenClaimsCreationContext>, VivoxClaimsTokenFactory>();
			services.AddTransient<IVivoxTokenSignService, DefaultLocalVivoxTokenSigningService>();

			RegisterDatabaseServices(services);
		}

		private void RegisterDatabaseServices([JetBrains.Annotations.NotNull] IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			//"server=127.0.0.1;port=3307;user=root;password=test;database=proudmoore_world Timeout=9000"
			services.AddDbContext<wotlk_charactersContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3307;user=root;password=test;database=wotlk_characters"); })
				.AddEntityFrameworkMySql();

			services.AddDbContext<wotlk_worldContext>(builder => { builder.UseMySql("server=127.0.0.1;port=3307;user=root;password=test;database=wotlk_world"); })
				.AddEntityFrameworkMySql();

			services.AddTransient<ITrinityCharacterRepository, TrinityCoreCharacterRepository>();
			services.AddTransient<ITrinityCharacterActionBarRepository, TrinityCoreCharacterActionBarRepository>();
			services.AddTransient<ITrinityGuildMembershipRepository, TrinityCoreGuildMembershipRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();

			app.UseResponseCaching();
			app.UseAuthentication();

			loggerFactory.RegisterGuardiansLogging(Configuration);

			app.UseMvcWithDefaultRoute();
		}
	}
}
