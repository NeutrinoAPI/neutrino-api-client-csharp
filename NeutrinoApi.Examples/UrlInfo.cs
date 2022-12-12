using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class UrlInfo
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The URL to probe
                { "url", "https://www.neutrinoapi.com/" },
                
                // If this URL responds with html, text, json or xml then return the response. This option is useful
                // if you want to perform further processing on the URL content (e.g. with the HTML Extract or HTML
                // Clean APIs)
                { "fetch-content", "false" },
                
                // Ignore any TLS/SSL certificate errors and load the URL anyway
                { "ignore-certificate-errors", "false" },
                
                // Timeout in seconds. Give up if still trying to load the URL after this number of seconds
                { "timeout", "60" },
                
                // If the request fails for any reason try again this many times
                { "retry", "0" }
            };

            var response = neutrinoApiClient.UrlInfo(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The actual content this URL responded with. Only set if the 'fetch-content' option was used
                Console.WriteLine("content: {0}",
                    data.TryGetProperty("content", out var content) ? content.ToString() : "NULL");
                
                // The encoding format the URL uses
                Console.WriteLine("content-encoding: {0}",
                    data.TryGetProperty("content-encoding", out var contentEncoding) ? contentEncoding.ToString() : "NULL");
                
                // The size of the URL content in bytes
                Console.WriteLine("content-size: {0}",
                    data.TryGetProperty("content-size", out var contentSize) ? contentSize.ToString() : "NULL");
                
                // The content-type this URL serves
                Console.WriteLine("content-type: {0}",
                    data.TryGetProperty("content-type", out var contentType) ? contentType.ToString() : "NULL");
                
                // True if this URL responded with an HTTP OK (200) status
                Console.WriteLine("http-ok: {0}",
                    data.TryGetProperty("http-ok", out var httpOk) ? httpOk.ToString() : "NULL");
                
                // True if this URL responded with an HTTP redirect
                Console.WriteLine("http-redirect: {0}",
                    data.TryGetProperty("http-redirect", out var httpRedirect) ? httpRedirect.ToString() : "NULL");
                
                // The HTTP status code this URL responded with. An HTTP status of 0 indicates a network level issue
                Console.WriteLine("http-status: {0}",
                    data.TryGetProperty("http-status", out var httpStatus) ? httpStatus.ToString() : "NULL");
                
                // The HTTP status message assoicated with the status code
                Console.WriteLine("http-status-message: {0}",
                    data.TryGetProperty("http-status-message", out var httpStatusMessage) ? httpStatusMessage.ToString() : "NULL");
                
                // True if an error occurred while loading the URL. This includes network errors, TLS errors and
                // timeouts
                Console.WriteLine("is-error: {0}",
                    data.TryGetProperty("is-error", out var isError) ? isError.ToString() : "NULL");
                
                // True if a timeout occurred while loading the URL. You can set the timeout with the request
                // parameter 'timeout'
                Console.WriteLine("is-timeout: {0}",
                    data.TryGetProperty("is-timeout", out var isTimeout) ? isTimeout.ToString() : "NULL");
                
                // The ISO 2-letter language code of the page. Extracted from either the HTML document or via HTTP
                // headers
                Console.WriteLine("language-code: {0}",
                    data.TryGetProperty("language-code", out var languageCode) ? languageCode.ToString() : "NULL");
                
                // The time taken to load the URL content in seconds
                Console.WriteLine("load-time: {0}",
                    data.TryGetProperty("load-time", out var loadTime) ? loadTime.ToString() : "NULL");
                
                // A key-value map of the URL query paramaters
                Console.WriteLine("query: {0}",
                    data.TryGetProperty("query", out var query) ? query.ToString() : "NULL");
                
                // Is this URL actually serving real content
                Console.WriteLine("real: {0}",
                    data.TryGetProperty("real", out var real) ? real.ToString() : "NULL");
                
                // The servers IP geo-location: full city name (if detectable)
                Console.WriteLine("server-city: {0}",
                    data.TryGetProperty("server-city", out var serverCity) ? serverCity.ToString() : "NULL");
                
                // The servers IP geo-location: full country name
                Console.WriteLine("server-country: {0}",
                    data.TryGetProperty("server-country", out var serverCountry) ? serverCountry.ToString() : "NULL");
                
                // The servers IP geo-location: ISO 2-letter country code
                Console.WriteLine("server-country-code: {0}",
                    data.TryGetProperty("server-country-code", out var serverCountryCode) ? serverCountryCode.ToString() : "NULL");
                
                // The servers hostname (PTR record)
                Console.WriteLine("server-hostname: {0}",
                    data.TryGetProperty("server-hostname", out var serverHostname) ? serverHostname.ToString() : "NULL");
                
                // The IP address of the server hosting this URL
                Console.WriteLine("server-ip: {0}",
                    data.TryGetProperty("server-ip", out var serverIp) ? serverIp.ToString() : "NULL");
                
                // The name of the server software hosting this URL
                Console.WriteLine("server-name: {0}",
                    data.TryGetProperty("server-name", out var serverName) ? serverName.ToString() : "NULL");
                
                // The servers IP geo-location: full region name (if detectable)
                Console.WriteLine("server-region: {0}",
                    data.TryGetProperty("server-region", out var serverRegion) ? serverRegion.ToString() : "NULL");
                
                // The document title
                Console.WriteLine("title: {0}",
                    data.TryGetProperty("title", out var title) ? title.ToString() : "NULL");
                
                // The fully qualified URL. This may be different to the URL requested if http-redirect is true
                Console.WriteLine("url: {0}",
                    data.TryGetProperty("url", out var url) ? url.ToString() : "NULL");
                
                // The URL path
                Console.WriteLine("url-path: {0}",
                    data.TryGetProperty("url-path", out var urlPath) ? urlPath.ToString() : "NULL");
                
                // The URL port
                Console.WriteLine("url-port: {0}",
                    data.TryGetProperty("url-port", out var urlPort) ? urlPort.ToString() : "NULL");
                
                // The URL protocol, usually http or https
                Console.WriteLine("url-protocol: {0}",
                    data.TryGetProperty("url-protocol", out var urlProtocol) ? urlProtocol.ToString() : "NULL");
                
                // Is this a valid well-formed URL
                Console.WriteLine("valid: {0}",
                    data.TryGetProperty("valid", out var valid) ? valid.ToString() : "NULL");
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