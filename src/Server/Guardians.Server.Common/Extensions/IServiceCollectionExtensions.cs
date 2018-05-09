﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Guardians
{
	/// <summary>
	/// Extensions for the <see cref="IServiceCollection"/> Type.
	/// </summary>
	public static class IServiceCollectionExtensions
	{
		//TODO: Doc
		public static IServiceCollection AddJwtAuthorization(this IServiceCollection services, X509Certificate2 jwtCertificate)
			=> services.AddJwtAuthorization<GuardiansApplicationUser, GuardiansApplicationRole>(jwtCertificate);

		public static IServiceCollection AddJwtAuthorization<TUser, TRole>(this IServiceCollection services, X509Certificate2 jwtCertificate) 
			where TUser : class 
			where TRole : class
		{
			if(services == null) throw new ArgumentNullException(nameof(services));

			//Service required for reading the JWT claims.
			services.AddSingleton<IClaimsPrincipalReader, ClaimsPrincipalReader>();

			//We also need to enable identity
			services.AddIdentity<TUser, TRole>(options =>
			{
				//These disable the ridiculous requirements that the default password scheme has
				options.Password.RequireNonAlphanumeric = false;

				//For some reason I can't figure out how to get the JWT middleware to spit out sub claims
				//so we need to map the Identity to expect nameidentifier
				options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
				options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
				options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
			});

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(o =>
				{
					o.RequireHttpsMetadata = true;

					o.TokenValidationParameters = new TokenValidationParameters()
					{
						IssuerSigningKey = new X509SecurityKey(jwtCertificate),

#warning This is bad. We should validate the signing key and other fields in production
						ValidateIssuerSigningKey = false,
						ValidateAudience = false,
						ValidateIssuer = false,
						ValidateLifetime = false, //temporary until we come up with a solution

						NameClaimType = OpenIdConnectConstants.Claims.Name,
						RoleClaimType = OpenIdConnectConstants.Claims.Role
					};
				});

			return services;
		}
	}
}