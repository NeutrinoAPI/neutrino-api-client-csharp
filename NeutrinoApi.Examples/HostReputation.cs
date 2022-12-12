using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class HostReputation
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // An IP address, domain name, FQDN or URL. If you supply a domain/URL it will be checked against
                // the URI DNSBL lists
                { "host", "neutrinoapi.com" },
                
                // Only check lists with this rating or better
                { "list-rating", "3" },
                
                // Only check these DNSBL zones/hosts. Multiple zones can be supplied as comma-separated values
                { "zones", "" }
            };

            var response = neutrinoApiClient.HostReputation(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The IP address or host name
                Console.WriteLine("host: {0}",
                    data.TryGetProperty("host", out var host) ? host.ToString() : "NULL");
                
                // Is this host blacklisted
                Console.WriteLine("is-listed: {0}",
                    data.TryGetProperty("is-listed", out var isListed) ? isListed.ToString() : "NULL");
                
                // The number of DNSBLs the host is listed on
                Console.WriteLine("list-count: {0}",
                    data.TryGetProperty("list-count", out var listCount) ? listCount.ToString() : "NULL");
                
                // Array of objects for each DNSBL checked
                var lists = data.GetProperty("lists");
                Console.WriteLine("lists:");
                for (var i = 0; i < lists.GetArrayLength(); i++)
                {
                    var item = lists[i];

                    // True if the host is currently black-listed
                    Console.WriteLine("    is-listed: {0}",
                        item.TryGetProperty("is-listed", out var itemIsListed) ? itemIsListed.ToString() : "NULL");

                    // The hostname of the DNSBL
                    Console.WriteLine("    list-host: {0}",
                        item.TryGetProperty("list-host", out var itemListHost) ? itemListHost.ToString() : "NULL");

                    // The name of the DNSBL
                    Console.WriteLine("    list-name: {0}",
                        item.TryGetProperty("list-name", out var itemListName) ? itemListName.ToString() : "NULL");

                    // The list rating [1-3] with 1 being the best rating and 3 the lowest rating
                    Console.WriteLine("    list-rating: {0}",
                        item.TryGetProperty("list-rating", out var itemListRating) ? itemListRating.ToString() : "NULL");

                    // The DNSBL server response time in milliseconds
                    Console.WriteLine("    response-time: {0}",
                        item.TryGetProperty("response-time", out var itemResponseTime) ? itemResponseTime.ToString() : "NULL");

                    // The specific return code for this listing (only set if listed)
                    Console.WriteLine("    return-code: {0}",
                        item.TryGetProperty("return-code", out var itemReturnCode) ? itemReturnCode.ToString() : "NULL");

                    // The TXT record returned for this listing (only set if listed)
                    Console.WriteLine("    txt-record: {0}",
                        item.TryGetProperty("txt-record", out var itemTxtRecord) ? itemTxtRecord.ToString() : "NULL");

                    Console.WriteLine();
                }
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