using System;
using System.Collections.Generic;
using System.IO;

namespace NeutrinoApi.Examples
{
    /// <summary>IpBlocklistDownload</summary>
    public static class IpBlocklistDownload
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var outputFilePath = $"{Path.GetTempPath()}ip-blocklist-download_{Guid.NewGuid().ToString()}.csv";
            var parameters = new Dictionary<string, string>
            {
                
                // The data format. Can be either CSV or TXT
                { "format", "csv" },
                
                // Include public VPN provider addresses, this option is only available for Tier 3 or higher
                // accounts. Adds any IPs which are solely listed as VPN providers, IPs that are listed on multiple
                // sensors will still be included without enabling this option. WARNING: This adds at least an
                // additional 8 million IP addresses to the download if not using CIDR notation
                { "include-vpn", "false" },
                
                // Output IPs using CIDR notation. This option should be preferred but is off by default for
                // backwards compatibility
                { "cidr", "false" },
                
                // Output the IPv6 version of the blocklist, the default is to output IPv4 only. Note that this
                // option enables CIDR notation too as this is the only notation currently supported for IPv6
                { "ip6", "false" }
            };

            var response = neutrinoApiClient.IpBlocklistDownload(parameters, outputFilePath);
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