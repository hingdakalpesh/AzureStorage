using Microsoft.Azure.Storage.Blob;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Storage;

namespace AzureStorage.Data
{
    public class ImageStore : IImageStore
    {
        public ICloudStorage CloudStorage { get; }
        public ImageStore(ICloudStorage cloudStorage)
        {
            CloudStorage = cloudStorage;
        }

        public async Task UploadImageAsync(IFormFile file, string containerName, string userName)
        {
            var blobContainer = await CloudStorage.GetContainerReferenceAysnc(containerName);

            string senitizedUserName = StorageHelper.SenitizeUserName(userName);
            string fileName = StorageHelper.GetRandomBlobName(senitizedUserName, file.FileName);

            var blockBlob = blobContainer.GetBlockBlobReference(fileName);
            blockBlob.Properties.ContentType = file.ContentType;

            using Stream stream = file.OpenReadStream();
            await blockBlob.UploadFromStreamAsync(stream);
            await blockBlob.SetPropertiesAsync();
            stream.Close();
        }

        public async Task DownloadImageAsync(string containerName, Uri blobUri)
        {
            
        }

        public async Task DeleteImageAsync(string containerName, Uri blobUri)
        {
            var blob = CloudStorage.GetBlobReference(containerName, blobUri);
            await blob.DeleteAsync();
        }

        public async Task<List<Uri>> GetImagesUriAsync(string containerName, string userName)
        {
            var blobContainer = await CloudStorage.GetContainerReferenceAysnc(containerName);

            List<Uri> listBlobUri = new List<Uri>();
            string senitizedUserName = StorageHelper.SenitizeUserName(userName);
            var blobResultSegment = await blobContainer.ListBlobsSegmentedAsync(prefix: senitizedUserName, useFlatBlobListing: true, blobListingDetails: BlobListingDetails.None, 5000, null, null, null);
            var listBlobItems = blobResultSegment.Results.OfType<CloudBlockBlob>();
            listBlobUri = listBlobItems.Select(i => i.Uri).ToList();
            return listBlobUri;
        }
    }
}
