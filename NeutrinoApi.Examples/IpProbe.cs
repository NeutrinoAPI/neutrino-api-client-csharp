using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class IpProbe
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // IPv4 or IPv6 address
                { "ip", "194.233.98.38" }
            };

            var response = neutrinoApiClient.IpProbe(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The age of the autonomous system (AS) in number of years since registration
                Console.WriteLine("as-age: {0}",
                    data.TryGetProperty("as-age", out var asAge) ? asAge.ToString() : "NULL");
                
                // The autonomous system (AS) CIDR range
                Console.WriteLine("as-cidr: {0}",
                    data.TryGetProperty("as-cidr", out var asCidr) ? asCidr.ToString() : "NULL");
                
                // The autonomous system (AS) ISO 2-letter country code
                Console.WriteLine("as-country-code: {0}",
                    data.TryGetProperty("as-country-code", out var asCountryCode) ? asCountryCode.ToString() : "NULL");
                
                // The autonomous system (AS) ISO 3-letter country code
                Console.WriteLine("as-country-code3: {0}",
                    data.TryGetProperty("as-country-code3", out var asCountryCode3) ? asCountryCode3.ToString() : "NULL");
                
                // The autonomous system (AS) description / company name
                Console.WriteLine("as-description: {0}",
                    data.TryGetProperty("as-description", out var asDescription) ? asDescription.ToString() : "NULL");
                
                // Array of all the domains associated with the autonomous system (AS)
                Console.WriteLine("as-domains: {0}",
                    data.TryGetProperty("as-domains", out var asDomains) ? asDomains.ToString() : "NULL");
                
                // The autonomous system (AS) number
                Console.WriteLine("asn: {0}",
                    data.TryGetProperty("asn", out var asn) ? asn.ToString() : "NULL");
                
                // Full city name (if detectable)
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
                
                // The IPs host domain
                Console.WriteLine("host-domain: {0}",
                    data.TryGetProperty("host-domain", out var hostDomain) ? hostDomain.ToString() : "NULL");
                
                // The IPs full hostname (PTR)
                Console.WriteLine("hostname: {0}",
                    data.TryGetProperty("hostname", out var hostname) ? hostname.ToString() : "NULL");
                
                // The IP address
                Console.WriteLine("ip: {0}",
                    data.TryGetProperty("ip", out var ip) ? ip.ToString() : "NULL");
                
                // True if this is a bogon IP address such as a private network, local network or reserved address
                Console.WriteLine("is-bogon: {0}",
                    data.TryGetProperty("is-bogon", out var isBogon) ? isBogon.ToString() : "NULL");
                
                // True if this IP belongs to a hosting company. Note that this can still be true even if the
                // provider type is VPN/proxy, this occurs in the case that the IP is detected as both types
                Console.WriteLine("is-hosting: {0}",
                    data.TryGetProperty("is-hosting", out var isHosting) ? isHosting.ToString() : "NULL");
                
                // True if this IP belongs to an internet service provider. Note that this can still be true even if
                // the provider type is VPN/proxy, this occurs in the case that the IP is detected as both types
                Console.WriteLine("is-isp: {0}",
                    data.TryGetProperty("is-isp", out var isIsp) ? isIsp.ToString() : "NULL");
                
                // True if this IP ia a proxy
                Console.WriteLine("is-proxy: {0}",
                    data.TryGetProperty("is-proxy", out var isProxy) ? isProxy.ToString() : "NULL");
                
                // True if this is a IPv4 mapped IPv6 address
                Console.WriteLine("is-v4-mapped: {0}",
                    data.TryGetProperty("is-v4-mapped", out var isV4Mapped) ? isV4Mapped.ToString() : "NULL");
                
                // True if this is a IPv6 address. False if IPv4
                Console.WriteLine("is-v6: {0}",
                    data.TryGetProperty("is-v6", out var isV6) ? isV6.ToString() : "NULL");
                
                // True if this IP ia a VPN
                Console.WriteLine("is-vpn: {0}",
                    data.TryGetProperty("is-vpn", out var isVpn) ? isVpn.ToString() : "NULL");
                
                // A description of the provider (usually extracted from the providers website)
                Console.WriteLine("provider-description: {0}",
                    data.TryGetProperty("provider-description", out var providerDescription) ? providerDescription.ToString() : "NULL");
                
                // The domain name of the provider
                Console.WriteLine("provider-domain: {0}",
                    data.TryGetProperty("provider-domain", out var providerDomain) ? providerDomain.ToString() : "NULL");
                
                // The detected provider type, possible values are:
                // • isp - IP belongs to an internet service provider. This includes both mobile, home and
                //   business internet providers
                // • hosting - IP belongs to a hosting company. This includes website hosting, cloud computing
                //   platforms and colocation facilities
                // • vpn - IP belongs to a VPN provider
                // • proxy - IP belongs to a proxy service. This includes HTTP/SOCKS proxies and browser based
                //   proxies
                // • university - IP belongs to a university/college/campus
                // • government - IP belongs to a government department. This includes military facilities
                // • commercial - IP belongs to a commercial entity such as a corporate headquarters or company
                //   office
                // • unknown - could not identify the provider type
                Console.WriteLine("provider-type: {0}",
                    data.TryGetProperty("provider-type", out var providerType) ? providerType.ToString() : "NULL");
                
                // The website URL for the provider
                Console.WriteLine("provider-website: {0}",
                    data.TryGetProperty("provider-website", out var providerWebsite) ? providerWebsite.ToString() : "NULL");
                
                // Full region name (if detectable)
                Console.WriteLine("region: {0}",
                    data.TryGetProperty("region", out var region) ? region.ToString() : "NULL");
                
                // ISO 3166-2 region code (if detectable)
                Console.WriteLine("region-code: {0}",
                    data.TryGetProperty("region-code", out var regionCode) ? regionCode.ToString() : "NULL");
                
                // True if this is a valid IPv4 or IPv6 address
                Console.WriteLine("valid: {0}",
                    data.TryGetProperty("valid", out var valid) ? valid.ToString() : "NULL");
                
                // The domain of the VPN provider (may be empty if the VPN domain is not detectable)
                Console.WriteLine("vpn-domain: {0}",
                    data.TryGetProperty("vpn-domain", out var vpnDomain) ? vpnDomain.ToString() : "NULL");
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