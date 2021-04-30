using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Library_Final_Project.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _environment;

        public FileService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        /// <summary>
        /// AddFileAsync
        /// </summary>
        /// <param name="file"></param>
        /// <returns>Image path</returns>
        public async Task<string> AddFileAsync(IFormFile file)
        {
            string filePath = _environment.WebRootPath + "/img/" + file.FileName;
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fs);
            }
            return $"/img/{file.FileName}";
        }

        /// <summary>
        /// CreateDirectory
        /// </summary>
        public void CreateDirectory()
        {
            string dirPath = _environment.WebRootPath + "/img";
            if (Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }
        }

        /// <summary>
        /// DeleteFile
        /// </summary>
        /// <param name="filePath"></param>
        public void DeleteFile(string filePath)
        {
            filePath = _environment.WebRootPath + filePath;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
