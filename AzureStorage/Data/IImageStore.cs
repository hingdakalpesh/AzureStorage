using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureStorage.Data
{
    public interface IImageStore
    {
        public Task UploadImageAsync(IFormFile file, string containerName, string user);
        public Task DownloadImageAsync(string containerName, Uri blobUri);
        public Task DeleteImageAsync(string containerName, Uri blobUri);
        public Task<List<Uri>> GetImagesUriAsync(string containerName, string userName);
    }
}
