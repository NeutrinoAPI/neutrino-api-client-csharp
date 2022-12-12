using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class IpBlocklist
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // An IPv4 or IPv6 address. Accepts standard IP notation (with or without port number), CIDR
                // notation and IPv6 compressed notation. If multiple IPs are passed using comma-separated values
                // the first non-bogon address on the list will be checked
                { "ip", "104.244.72.115" },
                
                // Include public VPN provider IP addresses. NOTE: For more advanced VPN detection including the
                // ability to identify private and stealth VPNs use the IP Probe API
                { "vpn-lookup", "false" }
            };

            var response = neutrinoApiClient.IpBlocklist(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // An array of strings indicating which blocklist categories this IP is listed on
                Console.WriteLine("blocklists: {0}",
                    data.TryGetProperty("blocklists", out var blocklists) ? blocklists.ToString() : "NULL");
                
                // The CIDR address for this listing (only set if the IP is listed)
                Console.WriteLine("cidr: {0}",
                    data.TryGetProperty("cidr", out var cidr) ? cidr.ToString() : "NULL");
                
                // The IP address
                Console.WriteLine("ip: {0}",
                    data.TryGetProperty("ip", out var ip) ? ip.ToString() : "NULL");
                
                // IP is hosting a malicious bot or is part of a botnet. This is a broad category which includes
                // brute-force crackers
                Console.WriteLine("is-bot: {0}",
                    data.TryGetProperty("is-bot", out var isBot) ? isBot.ToString() : "NULL");
                
                // IP has been flagged as a significant attack source by DShield (dshield.org)
                Console.WriteLine("is-dshield: {0}",
                    data.TryGetProperty("is-dshield", out var isDshield) ? isDshield.ToString() : "NULL");
                
                // IP is hosting an exploit finding bot or is running exploit scanning software
                Console.WriteLine("is-exploit-bot: {0}",
                    data.TryGetProperty("is-exploit-bot", out var isExploitBot) ? isExploitBot.ToString() : "NULL");
                
                // IP is part of a hijacked netblock or a netblock controlled by a criminal organization
                Console.WriteLine("is-hijacked: {0}",
                    data.TryGetProperty("is-hijacked", out var isHijacked) ? isHijacked.ToString() : "NULL");
                
                // Is this IP on a blocklist
                Console.WriteLine("is-listed: {0}",
                    data.TryGetProperty("is-listed", out var isListed) ? isListed.ToString() : "NULL");
                
                // IP is involved in distributing or is running malware
                Console.WriteLine("is-malware: {0}",
                    data.TryGetProperty("is-malware", out var isMalware) ? isMalware.ToString() : "NULL");
                
                // IP has been detected as an anonymous web proxy or anonymous HTTP proxy
                Console.WriteLine("is-proxy: {0}",
                    data.TryGetProperty("is-proxy", out var isProxy) ? isProxy.ToString() : "NULL");
                
                // IP address is hosting a spam bot, comment spamming or any other spamming type software
                Console.WriteLine("is-spam-bot: {0}",
                    data.TryGetProperty("is-spam-bot", out var isSpamBot) ? isSpamBot.ToString() : "NULL");
                
                // IP is running a hostile web spider / web crawler
                Console.WriteLine("is-spider: {0}",
                    data.TryGetProperty("is-spider", out var isSpider) ? isSpider.ToString() : "NULL");
                
                // IP is involved in distributing or is running spyware
                Console.WriteLine("is-spyware: {0}",
                    data.TryGetProperty("is-spyware", out var isSpyware) ? isSpyware.ToString() : "NULL");
                
                // IP is a Tor node or running a Tor related service
                Console.WriteLine("is-tor: {0}",
                    data.TryGetProperty("is-tor", out var isTor) ? isTor.ToString() : "NULL");
                
                // IP belongs to a public VPN provider (only set if the 'vpn-lookup' option is enabled)
                Console.WriteLine("is-vpn: {0}",
                    data.TryGetProperty("is-vpn", out var isVpn) ? isVpn.ToString() : "NULL");
                
                // The unix time when this IP was last seen on any blocklist. IPs are automatically removed after 7
                // days therefor this value will never be older than 7 days
                Console.WriteLine("last-seen: {0}",
                    data.TryGetProperty("last-seen", out var lastSeen) ? lastSeen.ToString() : "NULL");
                
                // The number of blocklists the IP is listed on
                Console.WriteLine("list-count: {0}",
                    data.TryGetProperty("list-count", out var listCount) ? listCount.ToString() : "NULL");
                
                // An array of objects containing details on which specific sensors detected the IP
                var sensors = data.GetProperty("sensors");
                Console.WriteLine("sensors:");
                for (var i = 0; i < sensors.GetArrayLength(); i++)
                {
                    var item = sensors[i];

                    // The primary blocklist category this sensor belongs to
                    Console.WriteLine("    blocklist: {0}",
                        item.TryGetProperty("blocklist", out var itemBlocklist) ? itemBlocklist.ToString() : "NULL");

                    // Contains details about the sensor source and what type of malicious activity was detected
                    Console.WriteLine("    description: {0}",
                        item.TryGetProperty("description", out var itemDescription) ? itemDescription.ToString() : "NULL");

                    // The sensor ID. This is a permanent and unique ID for each sensor
                    Console.WriteLine("    id: {0}",
                        item.TryGetProperty("id", out var itemId) ? itemId.ToString() : "NULL");

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