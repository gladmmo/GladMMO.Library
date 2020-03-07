using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GladMMO
{
	//From ProjectVindictive: https://github.com/HelloKitty/ProjectVindictive.Library/blob/master/src/ProjectVindictive.Server.AmazonS3/Extensions/ProjectVindictiveAmazonS3Extensions.cs
	public static class ProjectVindictiveAmazonS3Extensions
	{
		/// <summary>
		/// Adds the an AmazonS3 <see cref="IStorageUrlBuilder"/> registeration ot the container.
		/// Additionally sets up S3 communication and credentials for the application.
		/// Requires <see cref="AmazonS3Config"/> in the configuration.
		/// </summary>
		/// <param name="services">The service container.</param>
		/// <param name="configuration">A configuration object with <see cref="AmazonS3Config"/> registered.</param>
		public static IServiceCollection AddS3Service(this IServiceCollection services, IConfiguration configuration)
		{
			//To communicate with all S3 regions we'll need to enable signature version 4 which uses a newer
			//signature computation. Some regions don't support older versions so this is REQUIRED
			AWSConfigsS3.UseSignatureVersion4 = true;

			//TODO: Handle credentials properly
			//TODO: Don't let this go into prod.

			services.RegisterConfigOptions<AmazonS3Config>(configuration);
			services.AddSingleton<IStorageUrlBuilder, AmazonS3URLBuilder>();
			services.AddTransient<IAmazonS3, AmazonS3Client>(provider =>
			{
				return new AmazonS3Client(provider.GetService<AWSCredentials>(), RegionEndpoint.USEast2);
			});

			services.AddSingleton<AWSCredentials>(provider => new EnvironmentVariablesAWSCredentials());

			return services;
		}
	}
}