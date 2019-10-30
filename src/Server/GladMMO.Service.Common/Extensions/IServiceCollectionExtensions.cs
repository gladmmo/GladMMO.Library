using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Common.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GladMMO
{
	/// <summary>
	/// Extensions for the <see cref="IServiceCollection"/> Type.
	/// </summary>
	public static class IServiceCollectionExtensions
	{
		//TODO: Doc
		public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, X509Certificate2 jwtCertificate)
			=> services.AddJwtAuthorization<GuardiansApplicationUser, GuardiansApplicationRole>(jwtCertificate);

		//TODO: Rename
		public static IServiceCollection AddJustAuthorization(this IServiceCollection services, X509Certificate2 jwtCertificate)
		{
			//This is CRITICAL for now.
			//See https://github.com/aspnet/Security/issues/1043
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

			//Service required for reading the JWT claims.
			services.AddSingleton<IClaimsPrincipalReader, ClaimsPrincipalReader>();

			//See: https://github.com/openiddict/openiddict-core/issues/436
			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(o =>
				{
#warning This is bad. Set this to true in prod
					o.RequireHttpsMetadata = false;

					o.TokenValidationParameters = new TokenValidationParameters()
					{
						IssuerSigningKey = new X509SecurityKey(jwtCertificate),

#warning This is bad. We should validate the signing key and other fields in production
						ValidateIssuerSigningKey = false,
						ValidateAudience = false,
						ValidateIssuer = false,
						ValidateLifetime = false, //temporary until we come up with a solution

						NameClaimType = /*OpenIdConnectConstants.Claims.Name*/"name",
						RoleClaimType = /*OpenIdConnectConstants.Claims.Role*/"role"
					};
				});

			return services;
		}

		//TODO: Figure out why referencing OpenIdConnectConstants causes missing method exceptions.
		public static IServiceCollection AddJwtAuthorization<TUser, TRole>(this IServiceCollection services, X509Certificate2 jwtCertificate) 
			where TUser : class 
			where TRole : class
		{
			if(services == null) throw new ArgumentNullException(nameof(services));

			AddJustAuthorization(services, jwtCertificate);

			//will cause identity data to be overridden
			//We also need to enable identity
			services.AddIdentity<TUser, TRole>(options =>
			{
				//These disable the ridiculous requirements that the default password scheme has
				options.Password.RequireNonAlphanumeric = false;

				//For some reason I can't figure out how to get the JWT middleware to spit out sub claims
				//so we need to map the Identity to expect nameidentifier
				options.ClaimsIdentity.UserIdClaimType = /*OpenIdConnectConstants.Claims.Subject*/"sub";
				options.ClaimsIdentity.RoleClaimType = /*OpenIdConnectConstants.Claims.Role*/"role";
				options.ClaimsIdentity.UserNameClaimType = /*OpenIdConnectConstants.Claims.Name*/"name";
			});

			return services;
		}

		public static IServiceCollection AddCommonLoggingAdapter([JetBrains.Annotations.NotNull] this IServiceCollection services)
		{
			if (services == null) throw new ArgumentNullException(nameof(services));

			return services.AddSingleton<ILog, CommonLoggingASPCoreLoggingAdapter>();
		}
	}
}