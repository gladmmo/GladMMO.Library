using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;

namespace GladMMO
{
	//From ProjectVindictive: https://github.com/HelloKitty/ProjectVindictive.Library/blob/master/src/ProjectVindictive.Server.AmazonS3/Services/AmazonS3URLBuilder.cs
	/// <summary>
	/// A <see cref="IStorageUrlBuilder"/> implementation that uses S3 to serve/build upload URLs.
	/// </summary>
	public sealed class AmazonS3URLBuilder : IStorageUrlBuilder
	{
		//TODO: Hide credentials from this config
		/// <summary>
		/// Amazon config settings.
		/// </summary>
		private IOptions<AmazonS3Config> AmazonConfig { get; }

		/// <summary>
		/// The credentials for the AWS client.
		/// </summary>
		private AWSCredentials AWSCredentials { get; }

		public AmazonS3URLBuilder(IOptions<AmazonS3Config> amazonConfig, AWSCredentials awsCredentials)
		{
			if(amazonConfig == null) throw new ArgumentNullException(nameof(amazonConfig));
			if(awsCredentials == null) throw new ArgumentNullException(nameof(awsCredentials));

			AmazonConfig = amazonConfig;
			AWSCredentials = awsCredentials;

			//Validate the config
			if(String.IsNullOrWhiteSpace(AmazonConfig.Value.BucketName))
				throw new InvalidOperationException($"Provided Amazon bucket config was invalid.");
		}

		//TODO: Why should this expose handling for contenttype. Create a factory service for it.
		/// <inheritdoc />
		public Task<string> BuildUploadUrl(UserContentType contentType, Guid key)
		{
			return GetPresignedS3URL(key, HttpVerb.PUT);
		}

		private Task<string> GetPresignedS3URL(Guid key, HttpVerb httpVerb)
		{
			using(IAmazonS3 client = new AmazonS3Client(AWSCredentials, new Amazon.S3.AmazonS3Config() { SignatureVersion = "V4", RegionEndpoint = RegionEndpoint.USEast2, SignatureMethod = SigningAlgorithm.HmacSHA256 }))
			{
				GetPreSignedUrlRequest request = GeneratePresignedRequest(key, httpVerb);

				return Task.FromResult(client.GetPreSignedURL(request));
			}
		}

		/// <summary>
		/// Generates an S3 <see cref="GetPreSignedUrlRequest"/> with the specified accept <see cref="HttpVerb"/>.
		/// PUT allows for uploads.
		/// </summary>
		/// <param name="key">The GUID name of the resource.</param>
		/// <param name="httpVerb">The HTTP verb to accept for the request.</param>
		/// <returns></returns>
		private GetPreSignedUrlRequest GeneratePresignedRequest(Guid key, HttpVerb httpVerb)
		{
			return new GetPreSignedUrlRequest()
			{
				//TODO: Use the content type to determine bucket
				BucketName = AmazonConfig.Value.BucketName,

				//We just use the GUID as the key. This should be enough
				Key = key.ToString(),

				//TODO: Handle expiry better. Research if this will cause expiry during uploads
				Expires = DateTime.Now.AddMinutes(5),

				Verb = httpVerb
			};
		}

		//TODO: Why should this expose handling for contenttype. Create a factory service for it.
		/// <inheritdoc />
		public Task<string> BuildRetrivalUrl(UserContentType contentType, Guid key)
		{
			return GetPresignedS3URL(key, HttpVerb.GET);
		}
	}
}