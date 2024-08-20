using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class BinLookup
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The BIN or IIN number. This is the first 6, 8 or 10 digits of a card number, use 8 (or more)
                // digits for the highest level of accuracy
                { "bin-number", "48334884" },
                
                // Pass in the customers IP address and we will return some extra information about them
                { "customer-ip", "" }
            };

            var response = neutrinoApiClient.BinLookup(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The BIN number returned. You may count the number of digits in this field to determine if the BIN
                // is likely to be based on an 8-digit or 6-digit card
                Console.WriteLine("bin-number: {0}",
                    data.TryGetProperty("bin-number", out var binNumber) ? binNumber.ToString() : "NULL");
                
                // The card brand (e.g. Visa or Mastercard)
                Console.WriteLine("card-brand: {0}",
                    data.TryGetProperty("card-brand", out var cardBrand) ? cardBrand.ToString() : "NULL");
                
                // The card category. There are many different card categories the most common card categories are:
                // CLASSIC, BUSINESS, CORPORATE, PLATINUM, PREPAID
                Console.WriteLine("card-category: {0}",
                    data.TryGetProperty("card-category", out var cardCategory) ? cardCategory.ToString() : "NULL");
                
                // The card type, will always be one of: DEBIT, CREDIT, CHARGE CARD
                Console.WriteLine("card-type: {0}",
                    data.TryGetProperty("card-type", out var cardType) ? cardType.ToString() : "NULL");
                
                // The full country name of the issuer
                Console.WriteLine("country: {0}",
                    data.TryGetProperty("country", out var country) ? country.ToString() : "NULL");
                
                // The ISO 2-letter country code of the issuer
                Console.WriteLine("country-code: {0}",
                    data.TryGetProperty("country-code", out var countryCode) ? countryCode.ToString() : "NULL");
                
                // The ISO 3-letter country code of the issuer
                Console.WriteLine("country-code3: {0}",
                    data.TryGetProperty("country-code3", out var countryCode3) ? countryCode3.ToString() : "NULL");
                
                // ISO 4217 currency code associated with the country of the issuer
                Console.WriteLine("currency-code: {0}",
                    data.TryGetProperty("currency-code", out var currencyCode) ? currencyCode.ToString() : "NULL");
                
                // True if the customers IP is listed on one of our blocklists, see the IP Blocklist API
                Console.WriteLine("ip-blocklisted: {0}",
                    data.TryGetProperty("ip-blocklisted", out var ipBlocklisted) ? ipBlocklisted.ToString() : "NULL");
                
                // An array of strings indicating which blocklists this IP is listed on
                Console.WriteLine("ip-blocklists: {0}",
                    data.TryGetProperty("ip-blocklists", out var ipBlocklists) ? ipBlocklists.ToString() : "NULL");
                
                // The city of the customers IP (if detectable)
                Console.WriteLine("ip-city: {0}",
                    data.TryGetProperty("ip-city", out var ipCity) ? ipCity.ToString() : "NULL");
                
                // The country of the customers IP
                Console.WriteLine("ip-country: {0}",
                    data.TryGetProperty("ip-country", out var ipCountry) ? ipCountry.ToString() : "NULL");
                
                // The ISO 2-letter country code of the customers IP
                Console.WriteLine("ip-country-code: {0}",
                    data.TryGetProperty("ip-country-code", out var ipCountryCode) ? ipCountryCode.ToString() : "NULL");
                
                // The ISO 3-letter country code of the customers IP
                Console.WriteLine("ip-country-code3: {0}",
                    data.TryGetProperty("ip-country-code3", out var ipCountryCode3) ? ipCountryCode3.ToString() : "NULL");
                
                // True if the customers IP country matches the BIN country
                Console.WriteLine("ip-matches-bin: {0}",
                    data.TryGetProperty("ip-matches-bin", out var ipMatchesBin) ? ipMatchesBin.ToString() : "NULL");
                
                // The region of the customers IP (if detectable)
                Console.WriteLine("ip-region: {0}",
                    data.TryGetProperty("ip-region", out var ipRegion) ? ipRegion.ToString() : "NULL");
                
                // Is this a commercial/business use card
                Console.WriteLine("is-commercial: {0}",
                    data.TryGetProperty("is-commercial", out var isCommercial) ? isCommercial.ToString() : "NULL");
                
                // Is this a prepaid or prepaid reloadable card
                Console.WriteLine("is-prepaid: {0}",
                    data.TryGetProperty("is-prepaid", out var isPrepaid) ? isPrepaid.ToString() : "NULL");
                
                // The card issuer
                Console.WriteLine("issuer: {0}",
                    data.TryGetProperty("issuer", out var issuer) ? issuer.ToString() : "NULL");
                
                // The card issuers phone number
                Console.WriteLine("issuer-phone: {0}",
                    data.TryGetProperty("issuer-phone", out var issuerPhone) ? issuerPhone.ToString() : "NULL");
                
                // The card issuers website
                Console.WriteLine("issuer-website: {0}",
                    data.TryGetProperty("issuer-website", out var issuerWebsite) ? issuerWebsite.ToString() : "NULL");
                
                // Is this a valid BIN or IIN number
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