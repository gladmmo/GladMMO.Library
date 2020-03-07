using System; using FreecraftCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GladMMO
{
	public class AzureBlobStorageResourceExistenceVerifier : IContentResourceExistenceVerifier
	{
		private CloudBlobClient BlobClient { get; }

		public AzureBlobStorageResourceExistenceVerifier(CloudBlobClient blobClient)
		{
			BlobClient = blobClient;
		}

		public async Task<bool> VerifyResourceExists(UserContentType contentType, Guid contentGuid)
		{
			//Container name format is {contentType}s.
			CloudBlobContainer container = BlobClient.GetContainerReference($"{contentType.ToString()}s");

			SharedAccessBlobPolicy sasConstraints = new SharedAccessBlobPolicy();
			sasConstraints.SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(30);
			sasConstraints.Permissions = SharedAccessBlobPermissions.Read; //download or retrievial should be READ ONLY.

			return await container.GetBlockBlobReference($"{contentGuid.ToString()}.bin")
				.ExistsAsync();
		}
	}
}
