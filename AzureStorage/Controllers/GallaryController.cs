using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureStorage.Data;
using AzureStorage.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace AzureStorage.Controllers
{
    public class GallaryController : Controller
    {
        public IImageStore ImageStore { get; }
        public GallaryController(IImageStore imageStore)
        {
            ImageStore = imageStore;
        }
        const string containerName = "images";

        public async Task<IActionResult> Index()
        {
            var images = await ImageStore.GetImagesUriAsync(containerName, User.Identity.Name);

            return View(images);
        }
        public IActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Upload")]
        public async Task<IActionResult> UploadPost()
        {
            var images = Request.Form.Files;
            if (images.Count == 0)
                return BadRequest("No files received from the upload");

            foreach (var image in images)
            {
                if (StorageHelper.IsImage(image))
                {
                    await ImageStore.UploadImageAsync(image, containerName, User.Identity.Name);
                }
                else
                {
                    //Send error back to the user.
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Uri blobUri)
        {
            await ImageStore.DeleteImageAsync(containerName, blobUri);
            return RedirectToAction(nameof(Index));
        }
       
    }
}
