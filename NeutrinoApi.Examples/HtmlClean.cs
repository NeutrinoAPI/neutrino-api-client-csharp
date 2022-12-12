using System;
using System.Collections.Generic;
using System.IO;

namespace NeutrinoApi.Examples
{
    /// <summary>HtmlClean</summary>
    public static class HtmlClean
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var outputFilePath = $"{Path.GetTempPath()}html-clean_{Guid.NewGuid().ToString()}.txt";
            var parameters = new Dictionary<string, string>
            {
                
                // The level of sanitization, possible values are: plain-text: reduce the content to plain text only
                // (no HTML tags at all) simple-text: allow only very basic text formatting tags like b, em, i,
                // strong, u basic-html: allow advanced text formatting and hyper links basic-html-with-images: same
                // as basic html but also allows image tags advanced-html: same as basic html with images but also
                // allows many more common HTML tags like table, ul, dl, pre
                { "output-type", "plain-text" },
                
                // The HTML content. This can be either a URL to load from, a file upload or an HTML content string
                { "content", "<div>Some HTML to clean...</div><script>alert()</script>" }
            };

            var response = neutrinoApiClient.HtmlClean(parameters, outputFilePath);
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