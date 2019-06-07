using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GladMMO
{
	public sealed class AzureBlobStorageURLBuilder : IStorageUrlBuilder
	{
		private CloudBlobClient BlobClient { get; }

		public AzureBlobStorageURLBuilder([JetBrains.Annotations.NotNull] CloudBlobClient blobClient)
		{
			BlobClient = blobClient ?? throw new ArgumentNullException(nameof(blobClient));
		}

		public async Task<string> BuildUploadUrl(UserContentType contentType, Guid key)
		{
			//Container name format is {contentType}s.
			CloudBlobContainer container = BlobClient.GetContainerReference($"{contentType.ToString().ToLower()}s");

			SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
			sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(30);
			sasConstraints.Permissions = SharedAccessBlobPermissions.Write | SharedAccessBlobPermissions.Create;

			ICloudBlob blob = container.GetBlockBlobReference($"{key.ToString()}");

			return new Uri(blob.Uri, blob.GetSharedAccessSignature(sasConstraints)).ToString();
		}

		public async Task<string> BuildRetrivalUrl(UserContentType contentType, Guid key)
		{
			//Container name format is {contentType}s.
			CloudBlobContainer container = BlobClient.GetContainerReference($"{contentType.ToString()}s");

			SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
			sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(30);
			sasConstraints.Permissions = SharedAccessBlobPermissions.Read; //download or retrievial should be READ ONLY.

			ICloudBlob blob = await container.GetBlobReferenceFromServerAsync($"{key.ToString()}.bin");

			return new Uri(blob.Uri, blob.GetSharedAccessSignature(sasConstraints)).ToString();
		}
	}
}
