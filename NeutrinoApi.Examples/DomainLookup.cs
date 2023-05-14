using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class DomainLookup
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // A domain name, hostname, FQDN, URL, HTML link or email address to lookup
                { "host", "neutrinoapi.com" },
                
                // For domains that we have never seen before then perform various live checks and realtime
                // reconnaissance. NOTE: this option may add additional non-deterministic delay to the request, if
                // you require consistently fast API response times or just want to check our domain blocklists then
                // you can disable this option
                { "live", "true" }
            };

            var response = neutrinoApiClient.DomainLookup(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The number of days since the domain was registered. A domain age of under 90 days is generally
                // considered to be potentially risky. A value of 0 indicates no registration date was found for
                // this domain
                Console.WriteLine("age: {0}",
                    data.TryGetProperty("age", out var age) ? age.ToString() : "NULL");
                
                // An array of strings indicating which blocklist categories this domain is listed on. Current
                // categories are: phishing, malware, spam, anonymizer, nefarious
                Console.WriteLine("blocklists: {0}",
                    data.TryGetProperty("blocklists", out var blocklists) ? blocklists.ToString() : "NULL");
                
                // The primary domain of the DNS provider for this domain
                Console.WriteLine("dns-provider: {0}",
                    data.TryGetProperty("dns-provider", out var dnsProvider) ? dnsProvider.ToString() : "NULL");
                
                // The primary domain name excluding any subdomains. This is also referred to as the second-level
                // domain (SLD)
                Console.WriteLine("domain: {0}",
                    data.TryGetProperty("domain", out var domain) ? domain.ToString() : "NULL");
                
                // The fully qualified domain name (FQDN)
                Console.WriteLine("fqdn: {0}",
                    data.TryGetProperty("fqdn", out var fqdn) ? fqdn.ToString() : "NULL");
                
                // This domain is hosting adult content such as porn, webcams, escorts, etc
                Console.WriteLine("is-adult: {0}",
                    data.TryGetProperty("is-adult", out var isAdult) ? isAdult.ToString() : "NULL");
                
                // Is this domain under a government or military TLD
                Console.WriteLine("is-gov: {0}",
                    data.TryGetProperty("is-gov", out var isGov) ? isGov.ToString() : "NULL");
                
                // Consider this domain malicious as it is currently listed on at least 1 blocklist
                Console.WriteLine("is-malicious: {0}",
                    data.TryGetProperty("is-malicious", out var isMalicious) ? isMalicious.ToString() : "NULL");
                
                // Is this domain under an OpenNIC TLD
                Console.WriteLine("is-opennic: {0}",
                    data.TryGetProperty("is-opennic", out var isOpennic) ? isOpennic.ToString() : "NULL");
                
                // True if this domain is unseen and is currently being processed in the background. This field only
                // matters when the 'live' lookup setting has been explicitly disabled and indicates that not all
                // domain data my be present yet
                Console.WriteLine("is-pending: {0}",
                    data.TryGetProperty("is-pending", out var isPending) ? isPending.ToString() : "NULL");
                
                // Is the FQDN a subdomain of the primary domain
                Console.WriteLine("is-subdomain: {0}",
                    data.TryGetProperty("is-subdomain", out var isSubdomain) ? isSubdomain.ToString() : "NULL");
                
                // The primary domain of the email provider for this domain. An empty value indicates the domain has
                // no valid MX records
                Console.WriteLine("mail-provider: {0}",
                    data.TryGetProperty("mail-provider", out var mailProvider) ? mailProvider.ToString() : "NULL");
                
                // The domains estimated global traffic rank with the highest rank being 1. A value of 0 indicates
                // the domain is currently ranked outside of the top 1M of domains
                Console.WriteLine("rank: {0}",
                    data.TryGetProperty("rank", out var rank) ? rank.ToString() : "NULL");
                
                // The ISO date this domain was registered or first seen on the internet. An empty value indicates
                // we could not reliably determine the date
                Console.WriteLine("registered-date: {0}",
                    data.TryGetProperty("registered-date", out var registeredDate) ? registeredDate.ToString() : "NULL");
                
                // The IANA registrar ID (0 if no registrar ID was found)
                Console.WriteLine("registrar-id: {0}",
                    data.TryGetProperty("registrar-id", out var registrarId) ? registrarId.ToString() : "NULL");
                
                // The name of the domain registrar owning this domain
                Console.WriteLine("registrar-name: {0}",
                    data.TryGetProperty("registrar-name", out var registrarName) ? registrarName.ToString() : "NULL");
                
                // An array of objects containing details on which specific blocklist sensors have detected this
                // domain
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
                
                // The top-level domain (TLD)
                Console.WriteLine("tld: {0}",
                    data.TryGetProperty("tld", out var tld) ? tld.ToString() : "NULL");
                
                // For a country code top-level domain (ccTLD) this will contain the associated ISO 2-letter country
                // code
                Console.WriteLine("tld-cc: {0}",
                    data.TryGetProperty("tld-cc", out var tldCc) ? tldCc.ToString() : "NULL");
                
                // True if a valid domain was found. For a domain to be considered valid it must be registered and
                // have valid DNS NS records
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