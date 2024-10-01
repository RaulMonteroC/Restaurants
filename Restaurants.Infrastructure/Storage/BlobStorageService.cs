using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Restaurants.Domain.Interfaces;
using Restaurants.Infrastructure.Configuration;

namespace Restaurants.Infrastructure.Storage;

public class BlobStorageService(IOptions<BlobStorageSettings> settings) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = settings.Value;
    private const string BLOB_RESOURCE = "b";

    public async Task<string> UploadBlobAsync(Stream data, string filename)
    {
        var blobClient =
            new BlobServiceClient(_blobStorageSettings.ConnectionString)
               .GetBlobContainerClient(_blobStorageSettings.LogosContainerName)
               .GetBlobClient(filename.Replace(" ",""));

        await blobClient.UploadAsync(data);

        return blobClient.Uri.ToString();
    }

    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (blobUrl == null)
            return null;

        var sasBuilder = new BlobSasBuilder
                         {
                             BlobContainerName = _blobStorageSettings.LogosContainerName,
                             Resource = BLOB_RESOURCE,
                             StartsOn = DateTimeOffset.UtcNow,
                             ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
                             BlobName = GetBlobNameFromUri(blobUrl)
                         };
        
        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);

        var sasToken = sasBuilder.ToSasQueryParameters(new StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
                                 .ToString();

        return $"{blobUrl}?{sasToken}";
    }

    private string GetBlobNameFromUri(string blobUrl) => new Uri(blobUrl).Segments.Last();
}