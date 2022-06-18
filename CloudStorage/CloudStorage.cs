using System;
using System.Collections.Generic;
using System.Text;

namespace docHttpTrigger.CloudStorage
{
    using Google.Apis.Auth.OAuth2;
    using Google.Cloud.Storage.V1;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using System.IO;
    using System.Text.Json;
    using System.Threading.Tasks;

    namespace AspNetCoreWebApp.CloudStorage
    {
        public class GoogleCloudStorage : ICloudStorage
        {
            private readonly GoogleCredential googleCredential;
            private readonly StorageClient storageClient;
            private readonly string bucketName;

            public GoogleCloudStorage(IConfiguration configuration)
            {
                googleCredential = GoogleCredential.FromFile(Environment.CurrentDirectory + "\\"+ configuration.GetValue<string>("GoogleCredentialFile"));
                storageClient = StorageClient.Create(googleCredential);
                bucketName = configuration.GetValue<string>("GoogleCloudStorageBucket");
            }

            public async Task<string> UploadFileAsync(string fileNameForStorage, string doc)
            {
                try
                {
                    byte[] byteArray = Encoding.UTF8.GetBytes(doc);
                    using (var memoryStream = new MemoryStream(byteArray))
                    {
                        var dataObject = await storageClient.UploadObjectAsync(bucketName, fileNameForStorage, "application/json", memoryStream);
                        return dataObject.MediaLink;
                    }
                }
                catch (Exception ex)
                {

                }

                return string.Empty;
            }

            public async Task DeleteFileAsync(string fileNameForStorage)
            {
                await storageClient.DeleteObjectAsync(bucketName, fileNameForStorage);
            }
        }
    }
}
