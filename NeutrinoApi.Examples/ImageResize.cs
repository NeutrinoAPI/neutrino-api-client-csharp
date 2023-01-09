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
                
                // The resize mode to use, we support 3 main resizing modes:
                // • scale Resize to within the width and height specified while preserving aspect ratio. In this
                //   mode the width or height will be automatically adjusted to fit the aspect ratio
                // • pad Resize to exactly the width and height specified while preserving aspect ratio and pad
                //   any space left over. Any padded space will be filled in with the 'bg-color' value
                // • crop Resize to exactly the width and height specified while preserving aspect ratio and crop
                //   any space which fall outside the area. The cropping window is centered on the original image
                { "resize-mode", "scale" },
                
                // The width to resize to (in px)
                { "width", "32" },
                
                // The output image format, can be either png or jpg
                { "format", "png" },
                
                // The URL or Base64 encoded Data URL for the source image. You can also upload an image file
                // directly using multipart/form-data
                { "image-url", "https://www.neutrinoapi.com/img/LOGO.png" },
                
                // The image background color in hexadecimal notation (e.g. #0000ff). For PNG output the special
                // value of 'transparent' can also be used. For JPG output the default is black (#000000)
                { "bg-color", "transparent" },
                
                // The height to resize to (in px). If you don't set this field then the height will be automatic
                // based on the requested width and image aspect ratio
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