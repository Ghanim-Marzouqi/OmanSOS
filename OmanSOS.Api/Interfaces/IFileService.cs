using OmanSOS.Core.ViewModels;

namespace OmanSOS.Api.Interfaces;

public interface IFileService
{
    Task<string?> UploadFile(IFormFile file, string directory);

    FileViewModel? DownloadFile(string? filePath);

    void DeleteFile(string? filePath);
}
