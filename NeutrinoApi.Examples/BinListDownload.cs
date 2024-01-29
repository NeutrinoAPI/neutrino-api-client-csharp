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
                
                // Include 8-digit and higher BIN codes. This option includes all 6-digit BINs and all 8-digit and
                // higher BINs (including some 9, 10 and 11 digit BINs where available)
                { "include-8digit", "false" },
                
                // Include all BINs and all available fields in the CSV file (overrides any values set for
                // 'include-iso3' or 'include-8digit')
                { "include-all", "false" },
                
                // Set this option to 'gzip' to have the output file compressed using gzip
                { "output-encoding", "" }
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