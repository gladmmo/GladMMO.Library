using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.Util;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public sealed class AmazonS3ResourceExistenceVerifier : IContentResourceExistenceVerifier
	{
		private IAmazonS3 CloudClient { get; }

		private ILogger<AmazonS3ResourceExistenceVerifier> Logger { get; }

		public AmazonS3ResourceExistenceVerifier([JetBrains.Annotations.NotNull] IAmazonS3 cloudClient,
			[JetBrains.Annotations.NotNull] ILogger<AmazonS3ResourceExistenceVerifier> logger)
		{
			CloudClient = cloudClient ?? throw new ArgumentNullException(nameof(cloudClient));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		public async Task<bool> VerifyResourceExists(UserContentType contentType, Guid contentGuid)
		{
			throw new NotImplementedException($"TODO: Finish properly implementing AWS S3 with bucket.");
			//This is actually how the old AWS client worked: https://github.com/aws/aws-sdk-net/blob/master/sdk/src/Services/S3/Custom/_bcl/IO/S3FileInfo.cs
			//Kinda bad design tbh Amazon lol
			try
			{
				var request = new GetObjectMetadataRequest
				{
					BucketName = "TODO:UNKNOWN",
					Key = contentGuid.ToString().Replace('\\', '/') //S3helper.EncodeKey: https://github.com/aws/aws-sdk-net/blob/b691e46e57a3e24477e6a5fa2e849da44db7002f/sdk/src/Services/S3/Custom/_bcl/IO/S3Helper.cs
				};
				((Amazon.Runtime.Internal.IAmazonWebServiceRequest)request).AddBeforeRequestHandler(FileIORequestEventHandler);

				// If the object doesn't exist then a "NotFound" will be thrown
				await CloudClient.GetObjectMetadataAsync(request)
					.ConfigureAwait(false);

				return true;
			}
			catch(AmazonS3Exception e)
			{
				if(Logger.IsEnabled(LogLevel.Error))
					Logger.LogError($"Encountered AWS Error: {e.Message}");
				return false;
			}
		}

		//From: https://github.com/aws/aws-sdk-net/blob/b691e46e57a3e24477e6a5fa2e849da44db7002f/sdk/src/Services/S3/Custom/_bcl/IO/S3Helper.cs
		internal static void FileIORequestEventHandler(object sender, RequestEventArgs args)
		{
			WebServiceRequestEventArgs wsArgs = args as WebServiceRequestEventArgs;
			if(wsArgs != null)
			{
				string currentUserAgent = wsArgs.Headers[AWSSDKUtils.UserAgentHeader];
				wsArgs.Headers[AWSSDKUtils.UserAgentHeader] = currentUserAgent + " FileIO";
			}
		}
	}
}
