using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IO;

namespace AzureStorage.Data
{
    public class StorageHelper
    {
        public static bool IsImage(IFormFile file)
        {
            if (file.ContentType.Contains("image"))
            {
                return true;
            }

            string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

            return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
        }

        public static string SenitizeUserName(string userName)
        {
            if (userName is null)
            {
                return "Anonymous";
            }
            return userName.Substring(0, userName.IndexOf('@'));
        }

        /// <summary> 
        /// string GetRandomBlobName(string filename): Generates a unique random file name to be uploaded  
        /// </summary> 
        public static string GetRandomBlobName(string path, string fileName)
        {
            string ext = Path.GetExtension(fileName);
            return Path.Combine(path, string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext));
        }
        public static string GetFileExtension(IFormFile file)
        {
            return Path.GetExtension(file.Name);
        }
    }
}
