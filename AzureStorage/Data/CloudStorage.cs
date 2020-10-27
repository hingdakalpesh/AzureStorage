using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace AzureStorage.Data
{
    public class CloudStorage : ICloudStorage
    {
        public IConfiguration Configuration { get; }
        public CloudStorage(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public CloudStorageAccount GetCloudStorageAccount()
        {
            try
            {
                return CloudStorageAccount.Parse(Configuration.GetConnectionString("AzureStorageConnection"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CloudBlobClient GetBlobClient()
        {
            try
            {
                return GetCloudStorageAccount().CreateCloudBlobClient();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<CloudBlobContainer> GetContainerReferenceAysnc(string containerName)
        {
            try
            {
                var blobClient = GetBlobClient();
                var blobContainer = blobClient.GetContainerReference(containerName);
                await blobContainer.CreateIfNotExistsAsync(BlobContainerPublicAccessType.Blob, null, null);
                return blobContainer;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CloudBlockBlob GetBlobReference(string containerName, Uri blobUri)
        {
            try
            {
                var blobClient = GetBlobClient();
                var blobContainer = blobClient.GetContainerReference(containerName);
                var blob = blobContainer.GetBlockBlobReference(new CloudBlockBlob(blobUri).Name);
                return blob;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
