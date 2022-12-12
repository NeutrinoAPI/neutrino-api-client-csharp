using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class BrowserBot
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // Delay in seconds to wait before capturing any page data, executing selectors or JavaScript
                { "delay", "3" },
                
                // Ignore any TLS/SSL certificate errors and load the page anyway
                { "ignore-certificate-errors", "false" },
                
                // Extract content from the page DOM using this selector. Commonly known as a CSS selector, you can
                // find a good reference here
                { "selector", ".header-link" },
                
                // The URL to load
                { "url", "https://www.neutrinoapi.com/" },
                
                // Timeout in seconds. Give up if still trying to load the page after this number of seconds
                { "timeout", "30" },
                
                // Execute JavaScript on the website. This parameter accepts JavaScript as either a string
                // containing JavaScript or for sending multiple separate statements a JSON array or POST array can
                // also be used. If a statement returns any value it will be returned in the 'exec-results'
                // response. You can also use the following specially defined user interaction functions:
                // sleep(seconds); Just wait/sleep for the specified number of seconds. click('selector'); Click on
                // the first element matching the given selector. focus('selector'); Focus on the first element
                // matching the given selector. keys('characters'); Send the specified keyboard characters. Use
                // click() or focus() first to send keys to a specific element. enter(); Send the Enter key. tab();
                // Send the Tab key.
                { "exec", "[]" },
                
                // Override the browsers default user-agent string with this one
                { "user-agent", "" }
            };

            var response = neutrinoApiClient.BrowserBot(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The complete raw, decompressed and decoded page content. Usually will be either HTML, JSON or XML
                Console.WriteLine("content: {0}",
                    data.TryGetProperty("content", out var content) ? content.ToString() : "NULL");
                
                // Array containing all the elements matching the supplied selector. Each element object will
                // contain the text content, HTML content and all current element attributes
                Console.WriteLine("elements: {0}",
                    data.TryGetProperty("elements", out var elements) ? elements.ToString() : "NULL");
                
                // Contains the error message if an error has occurred ('is-error' will be true)
                Console.WriteLine("error-message: {0}",
                    data.TryGetProperty("error-message", out var errorMessage) ? errorMessage.ToString() : "NULL");
                
                // If you executed any JavaScript this array holds the results as objects
                Console.WriteLine("exec-results: {0}",
                    data.TryGetProperty("exec-results", out var execResults) ? execResults.ToString() : "NULL");
                
                // The redirected URL if the URL responded with an HTTP redirect
                Console.WriteLine("http-redirect-url: {0}",
                    data.TryGetProperty("http-redirect-url", out var httpRedirectUrl) ? httpRedirectUrl.ToString() : "NULL");
                
                // The HTTP status code the URL returned
                Console.WriteLine("http-status-code: {0}",
                    data.TryGetProperty("http-status-code", out var httpStatusCode) ? httpStatusCode.ToString() : "NULL");
                
                // The HTTP status message the URL returned
                Console.WriteLine("http-status-message: {0}",
                    data.TryGetProperty("http-status-message", out var httpStatusMessage) ? httpStatusMessage.ToString() : "NULL");
                
                // True if an error has occurred loading the page. Check the 'error-message' field for details
                Console.WriteLine("is-error: {0}",
                    data.TryGetProperty("is-error", out var isError) ? isError.ToString() : "NULL");
                
                // True if the HTTP status is OK (200)
                Console.WriteLine("is-http-ok: {0}",
                    data.TryGetProperty("is-http-ok", out var isHttpOk) ? isHttpOk.ToString() : "NULL");
                
                // True if the URL responded with an HTTP redirect
                Console.WriteLine("is-http-redirect: {0}",
                    data.TryGetProperty("is-http-redirect", out var isHttpRedirect) ? isHttpRedirect.ToString() : "NULL");
                
                // True if the page is secured using TLS/SSL
                Console.WriteLine("is-secure: {0}",
                    data.TryGetProperty("is-secure", out var isSecure) ? isSecure.ToString() : "NULL");
                
                // True if a timeout occurred while loading the page. You can set the timeout with the request
                // parameter 'timeout'
                Console.WriteLine("is-timeout: {0}",
                    data.TryGetProperty("is-timeout", out var isTimeout) ? isTimeout.ToString() : "NULL");
                
                // The ISO 2-letter language code of the page. Extracted from either the HTML document or via HTTP
                // headers
                Console.WriteLine("language-code: {0}",
                    data.TryGetProperty("language-code", out var languageCode) ? languageCode.ToString() : "NULL");
                
                // The number of seconds taken to load the page (from initial request until DOM ready)
                Console.WriteLine("load-time: {0}",
                    data.TryGetProperty("load-time", out var loadTime) ? loadTime.ToString() : "NULL");
                
                // The document MIME type
                Console.WriteLine("mime-type: {0}",
                    data.TryGetProperty("mime-type", out var mimeType) ? mimeType.ToString() : "NULL");
                
                // Map containing all the HTTP response headers the URL responded with
                Console.WriteLine("response-headers: {0}",
                    data.TryGetProperty("response-headers", out var responseHeaders) ? responseHeaders.ToString() : "NULL");
                
                // Map containing details of the TLS/SSL setup
                Console.WriteLine("security-details: {0}",
                    data.TryGetProperty("security-details", out var securityDetails) ? securityDetails.ToString() : "NULL");
                
                // The HTTP servers IP address
                Console.WriteLine("server-ip: {0}",
                    data.TryGetProperty("server-ip", out var serverIp) ? serverIp.ToString() : "NULL");
                
                // The document title
                Console.WriteLine("title: {0}",
                    data.TryGetProperty("title", out var title) ? title.ToString() : "NULL");
                
                // The page URL
                Console.WriteLine("url: {0}",
                    data.TryGetProperty("url", out var url) ? url.ToString() : "NULL");
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