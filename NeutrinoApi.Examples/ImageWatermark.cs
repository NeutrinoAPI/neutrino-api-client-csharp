using System;
using System.Collections.Generic;
using System.IO;

namespace NeutrinoApi.Examples
{
    /// <summary>ImageWatermark</summary>
    public static class ImageWatermark
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var outputFilePath = $"{Path.GetTempPath()}image-watermark_{Guid.NewGuid().ToString()}.png";
            var parameters = new Dictionary<string, string>
            {
                
                // The output image format, can be either png or jpg
                { "format", "png" },
                
                // If set resize the resulting image to this width (in px) while preserving aspect ratio
                { "width", "" },
                
                // The URL or Base64 encoded Data URL for the source image (you can also upload an image file
                // directly in which case this field is ignored)
                { "image-url", "https://www.neutrinoapi.com/img/LOGO.png" },
                
                // The position of the watermark image, possible values are: center, top-left, top-center,
                // top-right, bottom-left, bottom-center, bottom-right
                { "position", "center" },
                
                // The URL or Base64 encoded Data URL for the watermark image (you can also upload an image file
                // directly in which case this field is ignored)
                { "watermark-url", "https://www.neutrinoapi.com/img/icons/security.png" },
                
                // The opacity of the watermark (0 to 100)
                { "opacity", "50" },
                
                // If set resize the resulting image to this height (in px) while preserving aspect ratio
                { "height", "" }
            };

            var response = neutrinoApiClient.ImageWatermark(parameters, outputFilePath);
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