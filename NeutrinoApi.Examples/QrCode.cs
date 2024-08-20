using System;
using System.Collections.Generic;
using System.IO;

namespace NeutrinoApi.Examples
{
    /// <summary>QrCode</summary>
    public static class QrCode
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var outputFilePath = $"{Path.GetTempPath()}qr-code_{Guid.NewGuid().ToString()}.png";
            var parameters = new Dictionary<string, string>
            {
                
                // The barcode format to output. Accepted formats are: qr, c128
                { "code-format", "qr" },
                
                // The width of the QR code (in px)
                { "width", "256" },
                
                // The QR code foreground color
                { "fg-color", "#000000" },
                
                // The QR code background color
                { "bg-color", "#ffffff" },
                
                // The content to encode into the QR code (e.g. a URL or a phone number)
                { "content", "https://www.neutrinoapi.com/signup/" },
                
                // The height of the QR code (in px)
                { "height", "256" }
            };

            var response = neutrinoApiClient.QrCode(parameters, outputFilePath);
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