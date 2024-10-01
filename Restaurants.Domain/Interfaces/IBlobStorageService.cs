namespace Restaurants.Domain.Interfaces;

public interface IBlobStorageService
{
    Task<string> UploadBlobAsync(Stream data, string filename);
    string? GetBlobSasUrl(string? blobUrl);
}