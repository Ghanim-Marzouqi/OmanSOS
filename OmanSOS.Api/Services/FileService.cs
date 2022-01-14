using Microsoft.AspNetCore.Mvc;
using OmanSOS.Api.Interfaces;
using OmanSOS.Core.Constants;
using OmanSOS.Core.ViewModels;

namespace OmanSOS.Api.Services
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public FileService(IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
        }

        public async Task<string?> UploadFile(IFormFile file, string directory)
        {
            directory ??= string.Empty;
            var target = Path.Combine(_hostEnvironment.ContentRootPath, Config.DefaultFileDirectory, directory);

            if (!Directory.Exists(target))
                Directory.CreateDirectory(target);

            if (file == null || file.Length < 0)
                return null;

            var filePath = Path.Combine(target, GetUniqueFileName(file.FileName));
            await using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return filePath;
        }

        public FileViewModel? DownloadFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return null;

            var file = new FileContentResult(File.ReadAllBytes(filePath), "application/octet-stream")
            {
                FileDownloadName = Path.GetFileName(filePath)
            };

            return new FileViewModel
            {
                FileContent = file.FileContents,
                FileName = file.FileDownloadName,
                FileType = file.ContentType
            };
        }

        public void DeleteFile(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                return;

            File.Delete(filePath);
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return string.Concat(Path.GetFileNameWithoutExtension(fileName)
                , "_"
                , Guid.NewGuid().ToString().AsSpan(0, 4)
                , Path.GetExtension(fileName));
        }
    }
}
