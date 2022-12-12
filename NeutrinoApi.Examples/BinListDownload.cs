using System;
using System.Collections.Generic;
using System.IO;

namespace NeutrinoApi.Examples
{
    /// <summary>BinListDownload</summary>
    public static class BinListDownload
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var outputFilePath = $"{Path.GetTempPath()}bin-list-download_{Guid.NewGuid().ToString()}.png";
            var parameters = new Dictionary<string, string>
            {
                
                // Include ISO 3-letter country codes and ISO 3-letter currency codes in the data. These will be
                // added to columns 10 and 11 respectively
                { "include-iso3", "false" },
                
                // Include 8-digit and higher BIN codes. Use this option if you want to download BINs with more than
                // 6-digits
                { "include-8digit", "false" }
            };

            var response = neutrinoApiClient.BinListDownload(parameters, outputFilePath);
            if (response.IsOK())
            {
                // API request successful, print out the file path
                Console.WriteLine("API Response OK, output saved to: {0}, content type: {1}", response.File, response.ContentType);
            }
            else
            {
                Console.Error.WriteLine("API Error: {0}, Error Code: {1}, HTTP Status Code: {2}", 
                    response.ErrorMessage, 
                    response.ErrorCode.ToString(), 
                    response.HttpStatusCode.ToString()); // you should handle this gracefully!
                Console.Error.WriteLine($"{response.ErrorCause}");
            }
        }
    }
}