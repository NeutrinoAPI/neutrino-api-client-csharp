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
                { "selector", ".button" },
                
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
                { "exec", "[click('#button-id'), sleep(1), click('.class'), keys('1234'), enter()]" },
                
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
                
                // The size of the returned content in bytes
                Console.WriteLine("content-size: {0}",
                    data.TryGetProperty("content-size", out var contentSize) ? contentSize.ToString() : "NULL");
                
                // Array containing all the elements matching the supplied selector
                var elements = data.GetProperty("elements");
                Console.WriteLine("elements:");
                for (var i = 0; i < elements.GetArrayLength(); i++)
                {
                    var item = elements[i];

                    // The 'class' attribute of the element
                    Console.WriteLine("    class: {0}",
                        item.TryGetProperty("class", out var itemClass) ? itemClass.ToString() : "NULL");

                    // The 'href' attribute of the element
                    Console.WriteLine("    href: {0}",
                        item.TryGetProperty("href", out var itemHref) ? itemHref.ToString() : "NULL");

                    // The raw HTML of the element
                    Console.WriteLine("    html: {0}",
                        item.TryGetProperty("html", out var itemHtml) ? itemHtml.ToString() : "NULL");

                    // The 'id' attribute of the element
                    Console.WriteLine("    id: {0}",
                        item.TryGetProperty("id", out var itemId) ? itemId.ToString() : "NULL");

                    // The plain-text content of the element with normalized whitespace
                    Console.WriteLine("    text: {0}",
                        item.TryGetProperty("text", out var itemText) ? itemText.ToString() : "NULL");

                    Console.WriteLine();
                }
                
                // Contains the error message if an error has occurred ('is-error' will be true)
                Console.WriteLine("error-message: {0}",
                    data.TryGetProperty("error-message", out var errorMessage) ? errorMessage.ToString() : "NULL");
                
                // If you executed any JavaScript this array holds the results as objects
                var execResults = data.GetProperty("exec-results");
                Console.WriteLine("exec-results:");
                for (var i = 0; i < execResults.GetArrayLength(); i++)
                {
                    var item = execResults[i];

                    // The result of the executed JavaScript statement. Will be empty if the statement returned nothing
                    Console.WriteLine("    result: {0}",
                        item.TryGetProperty("result", out var itemResult) ? itemResult.ToString() : "NULL");

                    // The JavaScript statement that was executed
                    Console.WriteLine("    statement: {0}",
                        item.TryGetProperty("statement", out var itemStatement) ? itemStatement.ToString() : "NULL");

                    Console.WriteLine();
                }
                
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
                
                // The HTTP servers hostname (PTR/RDNS record)
                Console.WriteLine("server-hostname: {0}",
                    data.TryGetProperty("server-hostname", out var serverHostname) ? serverHostname.ToString() : "NULL");
                
                // The HTTP servers IP address
                Console.WriteLine("server-ip: {0}",
                    data.TryGetProperty("server-ip", out var serverIp) ? serverIp.ToString() : "NULL");
                
                // The document title
                Console.WriteLine("title: {0}",
                    data.TryGetProperty("title", out var title) ? title.ToString() : "NULL");
                
                // The requested URL. This may not be the same as the final destination URL, if the URL redirects
                // then it will be set in 'http-redirect-url' and 'is-http-redirect' will also be true
                Console.WriteLine("url: {0}",
                    data.TryGetProperty("url", out var url) ? url.ToString() : "NULL");
                
                // Structure of url-components
                Console.WriteLine("url-components: {0}",
                    data.TryGetProperty("url-components", out var urlComponents) ? urlComponents.ToString() : "NULL");
                
                // True if the URL supplied is valid
                Console.WriteLine("url-valid: {0}",
                    data.TryGetProperty("url-valid", out var urlValid) ? urlValid.ToString() : "NULL");
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