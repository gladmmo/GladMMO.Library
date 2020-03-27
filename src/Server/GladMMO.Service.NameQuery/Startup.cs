using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
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
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
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

			RegisterDatabaseServices(services);
		}

		private void RegisterDatabaseServices([JetBrains.Annotations.NotNull] IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			services.AddDbContext<CharacterDatabaseContext>(o =>
			{
				//Fuck configuration, I'm sick of it and we can't check it into source control
				//so we're using enviroment variables for sensitive deployment specific values.
#if AZURE_RELEASE || AZURE_DEBUG
				try
				{
					o.UseMySql(Environment.GetEnvironmentVariable(GladMMOServiceConstants.CHARACTER_DATABASE_CONNECTION_STRING_ENV_VAR_PATH));
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to register Authentication Database. Make sure Env Variable path: {GladMMOServiceConstants.AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH} is correctly configured.", e);
				}
#else
				o.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");
#endif
			});

			services.AddDbContext<ContentDatabaseContext>(o =>
			{
				//Fuck configuration, I'm sick of it and we can't check it into source control
				//so we're using enviroment variables for sensitive deployment specific values.
#if AZURE_RELEASE || AZURE_DEBUG
				try
				{
					o.UseMySql(Environment.GetEnvironmentVariable(GladMMOServiceConstants.CONTENT_DATABASE_CONNECTION_STRING_ENV_VAR_PATH));
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to register Authentication Database. Make sure Env Variable path: {GladMMOServiceConstants.AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH} is correctly configured.", e);
				}
#else
				o.UseMySql("Server=127.0.0.1;Database=guardians.gameserver;Uid=root;Pwd=test;");
#endif
			});

			services.AddTransient<ICharacterRepository, DatabaseBackedCharacterRepository>();
			services.AddTransient<ICreatureEntryRepository, DatabaseBackedCreatureEntryRepository>();
			services.AddTransient<IGuildEntryRepository, DatabaseBackedGuildEntryRepository>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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
