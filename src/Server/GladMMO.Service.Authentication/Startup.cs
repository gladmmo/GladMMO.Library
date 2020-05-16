using System; using FreecraftCore;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Refit;
using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;
using OpenIddict.Server.AspNetCore;
using static OpenIddict.Abstractions.OpenIddictConstants;

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

			services.AddGladMMOCORS();

			services.Configure<IISOptions>(options =>
			{
				options.AutomaticAuthentication = false;
			});

			services.AddHttpsRedirection(options =>
			{
				options.RedirectStatusCode = StatusCodes.Status308PermanentRedirect;
				options.HttpsPort = 443;
			});

			// Add framework services.
			services.AddMvc(options => options.EnableEndpointRouting = false)
				.AddNewtonsoftJson()
				.RegisterHealthCheckController();

			services.AddLogging();
			services.AddOptions();
			services.Configure<AuthenticationServerConfigurationModel>(GeneralConfiguration.GetSection("AuthConfig"));

			//We need to immediately resolve the authserver config options because we need them to regiter openiddict
			IOptions<AuthenticationServerConfigurationModel> authOptions = services.BuildServiceProvider()
				.GetService<IOptions<AuthenticationServerConfigurationModel>>();

			services.AddAuthentication();

			services.AddSingleton<IAuthenticationService>(provider =>
			{
				//We need to find the listening address.
				PreferredEndpoint address = provider.GetService<PreferredEndpoint>();
				return RestService.For<IAuthenticationService>($"{address.Endpoint}:{address.Port}");
			});

			services.AddDbContext<GuardiansAuthenticationDbContext>(options =>
			{
				//Fuck configuration, I'm sick of it and we can't check it into source control
				//so we're using enviroment variables for sensitive deployment specific values.
#if AZURE_RELEASE || AZURE_DEBUG
				try
				{
					options.UseMySql(Environment.GetEnvironmentVariable(GladMMOServiceConstants.AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH));
				}
				catch (Exception e)
				{
					throw new InvalidOperationException($"Failed to register Authentication Database. Make sure Env Variable path: {GladMMOServiceConstants.AUTHENTICATION_DATABASE_CONNECTION_STRING_ENV_VAR_PATH} is correctly configured.", e);
				}
#else
				options.UseMySql(authOptions.Value.AuthenticationDatabaseString);
#endif
				options.UseOpenIddict();
			});

			//Below is the OpenIddict registration
			//This is the recommended setup from the official Github: https://github.com/openiddict/openiddict-core
			services.AddIdentity<GuardiansApplicationUser, GuardiansApplicationRole>(options =>
				{
					//These disable the ridiculous requirements that the defauly password scheme has
					options.Password.RequireNonAlphanumeric = false;

					//For some reason I can't figure out how to get the JWT middleware to spit out sub claims
					//so we need to map the Identity to expect nameidentifier
					options.ClaimsIdentity.UserIdClaimType = OpenIddictConstants.Claims.Subject;
					options.ClaimsIdentity.RoleClaimType = OpenIddictConstants.Claims.Role;
					options.ClaimsIdentity.UserNameClaimType = OpenIddictConstants.Claims.Name;
				})
				.AddEntityFrameworkStores<GuardiansAuthenticationDbContext>()
				.AddDefaultTokenProviders();

			services.AddOpenIddict()
				.AddCore(options =>
				{
					// Configure OpenIddict to use the Entity Framework Core stores and entities.
					options.UseEntityFrameworkCore()
						.UseDbContext<GuardiansAuthenticationDbContext>();
				})
				.AddServer(options =>
				{
					// Enable the authorization, logout, token and userinfo endpoints.
					options.SetTokenEndpointUris(authOptions.Value.AuthenticationControllerEndpoint);

					// Note: the Mvc.Client sample only uses the code flow and the password flow, but you
					// can enable the other flows if you need to support implicit or client credentials.
					options.AllowPasswordFlow()
						   .AllowRefreshTokenFlow();

#warning Don't deploy this into production; we should use HTTPS. Even if it is behind IIS or HAProxy etc.
					try
					{
						//Loads the cert from the specified path
						X509Certificate2 certificate = X509Certificate2Loader.Create(Path.Combine(Directory.GetCurrentDirectory(), authOptions.Value.JwtSigningX509Certificate2Path)).Load();
						options.AddSigningCertificate(certificate);
						options.AddEncryptionCertificate(certificate);
					}
					catch(Exception e)
					{
						throw new InvalidOperationException($"Failed to load cert at Path: {authOptions.Value.JwtSigningX509Certificate2Path} with Root: {Directory.GetCurrentDirectory()}. Error: {e.Message} \n\n Stack: {e.StackTrace}", e);
					}

					//TODO: Support release too.
#if AZURE_RELEASE || AZURE_DEBUG
					options.SetIssuer(new Uri(@"https://test-guardians-auth.azurewebsites.net"));
#else
					options.SetIssuer(new Uri(@"https://auth.vrguardians.net"));
#endif
					options.AcceptAnonymousClients();
					options.DisableAccessTokenEncryption();

					// Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
					options.UseAspNetCore()
						.EnableStatusCodePagesIntegration()
						.EnableTokenEndpointPassthrough()
						.DisableTransportSecurityRequirement(); // During development, you can disable the HTTPS requirement.

					// Note: if you don't want to specify a client_id when sending
					// a token or revocation request, uncomment the following line:
					//
					// options.AcceptAnonymousClients();

					// Note: if you want to process authorization and token requests
					// that specify non-registered scopes, uncomment the following line:
					//
					// options.DisableScopeValidation();

					// Note: if you don't want to use permissions, you can disable
					// permission enforcement by uncommenting the following lines:
					//
					// options.IgnoreEndpointPermissions()
					//        .IgnoreGrantTypePermissions()
					//        .IgnoreScopePermissions();

					// Note: when issuing access tokens used by third-party APIs
					// you don't own, you can disable access token encryption:
					//
					// options.DisableAccessTokenEncryption();
				})

				// Register the OpenIddict validation components.
				.AddValidation(options =>
				{
					// Configure the audience accepted by this resource server.
					// The value MUST match the audience associated with the
					// "demo_api" scope, which is used by ResourceController.
					options.AddAudiences("auth_server");

					// Import the configuration from the local OpenIddict server instance.
					options.UseLocalServer();

					// Register the ASP.NET Core host.
					options.UseAspNetCore();
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

			services.AddSingleton<IPlayfabAuthenticationClient>(provider =>
				{
					//TODO: make Playfab endpoint configurable.
					return RestService.For<IPlayfabAuthenticationClient>($@"https://{63815}.playfabapi.com:443");
				});

			//"server=127.0.0.1;port=3307;user=root;password=test;database=proudmoore_world Timeout=9000"
			services.AddDbContext<wotlk_authContext>(builder =>
				{
					builder.UseMySql("server=127.0.0.1;port=3307;user=root;password=test;database=wotlk_auth");
				})
				.AddEntityFrameworkMySql();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
#warning Do not deploy exceptions page into production
			app.UseDeveloperExceptionPage();
			app.UseHsts();
			//app.UseHttpsRedirection(); //Fucking PlayFab queries this server like a mongoloid and doesn't respect HTTPS redirects
			app.UseAuthentication();
			//loggerFactory.RegisterGuardiansLogging(GeneralConfiguration);

			//app.UseMiddleware<PlayfabAuthenticationFromOpenIddictResponseMiddleware>();

			app.UseGladMMOCORSMiddleware();
			app.UseMvcWithDefaultRoute();
		}
	}
}
