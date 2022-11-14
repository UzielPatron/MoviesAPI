using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace MoviesAPI.Services
{
    public class AzureFileStorage : IFileStorage
    {
        private string _connectionString;

        public AzureFileStorage(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("AzureStorage");
        }

        public async Task DeleteFile(string route, string container)
        {
            if (string.IsNullOrEmpty(route)) return;

            var client = new BlobContainerClient(_connectionString, container);
            await client.CreateIfNotExistsAsync();

            var file = Path.GetFileName(route);
            var blob = client.GetBlobClient(file);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<string> EditFile(byte[] content, string extension, string container, string route, string contentType)
        {
            await DeleteFile(route, container);
            return await SaveFile(content, extension, container, contentType);
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container, string contentType)
        {
            var client = new BlobContainerClient(_connectionString, container);
            await client.CreateIfNotExistsAsync();
            client.SetAccessPolicy(PublicAccessType.Blob);

            string fileName = $"{Guid.NewGuid()}{extension}";
            var blob = client.GetBlobClient(fileName);

            var blobUploadOptions = new BlobUploadOptions();
            var blobHttpHeaders = new BlobHttpHeaders();
        
            blobHttpHeaders.ContentType = contentType;
            blobUploadOptions.HttpHeaders = blobHttpHeaders;

            await blob.UploadAsync(new BinaryData(content), blobUploadOptions);

            return blob.Uri.ToString();
        }
    }
}
