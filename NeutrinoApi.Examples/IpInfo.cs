using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class IpInfo
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // An IPv4 or IPv6 address. Accepts standard IP notation and also CIDR notation
                { "ip", "1.1.1.1" },
                
                // Do a reverse DNS (PTR) lookup. This option can add extra delay to the request so only use it if
                // you need it
                { "reverse-lookup", "false" }
            };

            var response = neutrinoApiClient.IpInfo(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // Name of the city (if detectable)
                Console.WriteLine("city: {0}",
                    data.TryGetProperty("city", out var city) ? city.ToString() : "NULL");
                
                // ISO 2-letter continent code
                Console.WriteLine("continent-code: {0}",
                    data.TryGetProperty("continent-code", out var continentCode) ? continentCode.ToString() : "NULL");
                
                // Full country name
                Console.WriteLine("country: {0}",
                    data.TryGetProperty("country", out var country) ? country.ToString() : "NULL");
                
                // ISO 2-letter country code
                Console.WriteLine("country-code: {0}",
                    data.TryGetProperty("country-code", out var countryCode) ? countryCode.ToString() : "NULL");
                
                // ISO 3-letter country code
                Console.WriteLine("country-code3: {0}",
                    data.TryGetProperty("country-code3", out var countryCode3) ? countryCode3.ToString() : "NULL");
                
                // ISO 4217 currency code associated with the country
                Console.WriteLine("currency-code: {0}",
                    data.TryGetProperty("currency-code", out var currencyCode) ? currencyCode.ToString() : "NULL");
                
                // The IPs host domain (only set if reverse-lookup has been used)
                Console.WriteLine("host-domain: {0}",
                    data.TryGetProperty("host-domain", out var hostDomain) ? hostDomain.ToString() : "NULL");
                
                // The IPs full hostname (only set if reverse-lookup has been used)
                Console.WriteLine("hostname: {0}",
                    data.TryGetProperty("hostname", out var hostname) ? hostname.ToString() : "NULL");
                
                // The IPv4 or IPv6 address returned
                Console.WriteLine("ip: {0}",
                    data.TryGetProperty("ip", out var ip) ? ip.ToString() : "NULL");
                
                // True if this is a bogon IP address such as a private network, local network or reserved address
                Console.WriteLine("is-bogon: {0}",
                    data.TryGetProperty("is-bogon", out var isBogon) ? isBogon.ToString() : "NULL");
                
                // True if this is a IPv4 mapped IPv6 address
                Console.WriteLine("is-v4-mapped: {0}",
                    data.TryGetProperty("is-v4-mapped", out var isV4Mapped) ? isV4Mapped.ToString() : "NULL");
                
                // True if this is a IPv6 address. False if IPv4
                Console.WriteLine("is-v6: {0}",
                    data.TryGetProperty("is-v6", out var isV6) ? isV6.ToString() : "NULL");
                
                // Location latitude
                Console.WriteLine("latitude: {0}",
                    data.TryGetProperty("latitude", out var latitude) ? latitude.ToString() : "NULL");
                
                // Location longitude
                Console.WriteLine("longitude: {0}",
                    data.TryGetProperty("longitude", out var longitude) ? longitude.ToString() : "NULL");
                
                // Name of the region (if detectable)
                Console.WriteLine("region: {0}",
                    data.TryGetProperty("region", out var region) ? region.ToString() : "NULL");
                
                // ISO 3166-2 region code (if detectable)
                Console.WriteLine("region-code: {0}",
                    data.TryGetProperty("region-code", out var regionCode) ? regionCode.ToString() : "NULL");
                
                // Structure of a valid ip-info -> timezone response
                Console.WriteLine("timezone: {0}",
                    data.TryGetProperty("timezone", out var timezone) ? timezone.ToString() : "NULL");
                
                // True if this is a valid IPv4 or IPv6 address
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