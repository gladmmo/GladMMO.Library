using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Core;
using Refit;

namespace GladMMO
{
	public class Startup
	{
		//Changed in ASP Core 2.0
		public Startup(IConfiguration config)
		{
			if(config == null) throw new ArgumentNullException(nameof(config));

			GeneralConfiguration = config;
		}

		public IConfiguration GeneralConfiguration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			//https://stackoverflow.com/questions/4926676/mono-https-webrequest-fails-with-the-authentication-or-decryption-has-failed
			ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => true;
			ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
			ServicePointManager.CheckCertificateRevocationList = false;

			services.Configure<IISOptions>(options =>
			{
				options.AutomaticAuthentication = false;
			});

			// Add framework services.
			services.AddMvc()
				.RegisterHealthCheckController();

			services.AddLogging();
			services.AddOptions();
			services.Configure<AuthenticationServerConfigurationModel>(GeneralConfiguration.GetSection("AuthConfig"));

			//We need to immediately resolve the authserver config options because we need them to regiter openiddict
			IOptions<AuthenticationServerConfigurationModel> authOptions = services.BuildServiceProvider()
				.GetService<IOptions<AuthenticationServerConfigurationModel>>();

			services.AddAuthentication();

			//TODO: Renable
			services.AddDbContext<GuardiansZoneAuthenticationDbContext>(options =>
			{
				//TODO: Setup db options

				//On local builds we don't want to use config. We want to default to local
#if !DEBUG_LOCAL && !RELEASE_LOCAL
				options.UseMySql(authOptions.Value.AuthenticationDatabaseString);
#else
				options.UseMySql("Server=127.0.0.1;Database=guardians.zoneauth;Uid=root;Pwd=test;");
#endif
				options.UseOpenIddict<int>();
			});

			//Below is the OpenIddict registration
			//This is the recommended setup from the official Github: https://github.com/openiddict/openiddict-core
			services.AddIdentity<ZoneServerApplicationUser, GuardiansApplicationRole>(options =>
				{
					//These disable the ridiculous requirements that the defauly password scheme has
					options.Password.RequireNonAlphanumeric = false;

					//For some reason I can't figure out how to get the JWT middleware to spit out sub claims
					//so we need to map the Identity to expect nameidentifier
					options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
					options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
					options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
				})
				.AddEntityFrameworkStores<GuardiansZoneAuthenticationDbContext>()
				.AddDefaultTokenProviders();

			services.AddOpenIddict<int>(options =>
			{
				// Register the Entity Framework stores.
				options.AddEntityFrameworkCoreStores<GuardiansZoneAuthenticationDbContext>();

				// Register the ASP.NET Core MVC binder used by OpenIddict.
				// Note: if you don't call this method, you won't be able to
				// bind OpenIdConnectRequest or OpenIdConnectResponse parameters.
				options.AddMvcBinders();

				//This controller endpoint/action was specified in the HaloLive documentation: https://github.com/HaloLive/Documentation
				options.EnableTokenEndpoint(authOptions.Value.AuthenticationControllerEndpoint); // Enable the token endpoint (required to use the password flow).
				options.AllowPasswordFlow(); // Allow client applications to use the grant_type=password flow.
				options.AllowRefreshTokenFlow();
				options.UseJsonWebTokens();

#warning Don't deploy this into production; we should use HTTPS. Even if it is behind IIS or HAProxy etc.
				options.DisableHttpsRequirement();
				try
				{
					//Loads the cert from the specified path
					options.AddSigningCertificate(X509Certificate2Loader.Create(Path.Combine(Directory.GetCurrentDirectory(), authOptions.Value.JwtSigningX509Certificate2Path)).Load());
				}
				catch(Exception e)
				{
					throw new InvalidOperationException($"Failed to load cert at Path: {authOptions.Value.JwtSigningX509Certificate2Path} with Root: {Directory.GetCurrentDirectory()}. Error: {e.Message} \n\n Stack: {e.StackTrace}", e);
				}

				options.SetIssuer(new Uri(@"https://zoneauth.vrguardians.net"));
				options.RequireClientIdentification();
			});

#warning This is just for the test build, we don't actually want to do this
			services.Configure<IdentityOptions>(options =>
			{
				options.Password.RequireDigit = false;
				options.Password.RequiredLength = 1;
				options.Password.RequireUppercase = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
			});

			//TODO: Don't hardcode cert
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

			//This provides JwtBearer support for Authorize attribute/header
			services.AddJustAuthorization(cert);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();
			app.UseHsts();
			app.UseHttpsRedirection(); //non-playfab compatible auth servers can redirect HTTPS
			app.UseAuthentication();
			loggerFactory.RegisterGuardiansLogging(GeneralConfiguration);
			loggerFactory.AddDebug();
			app.UseMvcWithDefaultRoute();
		}
	}
}
