using Microsoft.AspNetCore.Authentication;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureStorage.Data
{
    public interface ICloudStorage
    {
        public CloudStorageAccount GetCloudStorageAccount();
        public CloudBlobClient GetBlobClient();
        public CloudBlockBlob GetBlobReference(string containerName, Uri blobUri);
        public Task<CloudBlobContainer> GetContainerReferenceAysnc(string containerName);
    }
}
