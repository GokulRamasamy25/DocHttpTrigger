using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace docHttpTrigger.CloudStorage
{
    public interface ICloudStorage
    {
        Task<string> UploadFileAsync(string fileNameForStorage, string doc);
        Task DeleteFileAsync(string fileNameForStorage);
    }
}
