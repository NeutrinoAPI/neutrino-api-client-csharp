using System;
using System.Collections.Generic;
using System.IO;

namespace NeutrinoApi.Examples
{
    /// <summary>ImageResize</summary>
    public static class ImageResize
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var outputFilePath = $"{Path.GetTempPath()}image-resize_{Guid.NewGuid().ToString()}.png";
            var parameters = new Dictionary<string, string>
            {
                
                // The width to resize to (in px) while preserving aspect ratio
                { "width", "32" },
                
                // The output image format, can be either png or jpg
                { "format", "png" },
                
                // The URL or Base64 encoded Data URL for the source image (you can also upload an image file
                // directly in which case this field is ignored)
                { "image-url", "https://www.neutrinoapi.com/img/LOGO.png" },
                
                // The height to resize to (in px) while preserving aspect ratio. If you don't set this field then
                // the height will be automatic based on the requested width and images aspect ratio
                { "height", "32" }
            };

            var response = neutrinoApiClient.ImageResize(parameters, outputFilePath);
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