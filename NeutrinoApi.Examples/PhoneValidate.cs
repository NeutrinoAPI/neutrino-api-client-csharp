using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class PhoneValidate
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // A phone number. This can be in international format (E.164) or local format. If passing local
                // format you must also set either the 'country-code' OR 'ip' options as well
                { "number", "+6495552000" },
                
                // ISO 2-letter country code, assume numbers are based in this country. If not set numbers are
                // assumed to be in international format (with or without the leading + sign)
                { "country-code", "" },
                
                // Pass in a users IP address and we will assume numbers are based in the country of the IP address
                { "ip", "" }
            };

            var response = neutrinoApiClient.PhoneValidate(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The phone number country
                Console.WriteLine("country: {0}",
                    data.TryGetProperty("country", out var country) ? country.ToString() : "NULL");
                
                // The phone number country as an ISO 2-letter country code
                Console.WriteLine("country-code: {0}",
                    data.TryGetProperty("country-code", out var countryCode) ? countryCode.ToString() : "NULL");
                
                // The phone number country as an ISO 3-letter country code
                Console.WriteLine("country-code3: {0}",
                    data.TryGetProperty("country-code3", out var countryCode3) ? countryCode3.ToString() : "NULL");
                
                // ISO 4217 currency code associated with the country
                Console.WriteLine("currency-code: {0}",
                    data.TryGetProperty("currency-code", out var currencyCode) ? currencyCode.ToString() : "NULL");
                
                // The international calling code
                Console.WriteLine("international-calling-code: {0}",
                    data.TryGetProperty("international-calling-code", out var internationalCallingCode) ? internationalCallingCode.ToString() : "NULL");
                
                // The number represented in full international format (E.164)
                Console.WriteLine("international-number: {0}",
                    data.TryGetProperty("international-number", out var internationalNumber) ? internationalNumber.ToString() : "NULL");
                
                // True if this is a mobile number. If the number type is unknown this value will be false
                Console.WriteLine("is-mobile: {0}",
                    data.TryGetProperty("is-mobile", out var isMobile) ? isMobile.ToString() : "NULL");
                
                // The number represented in local dialing format
                Console.WriteLine("local-number: {0}",
                    data.TryGetProperty("local-number", out var localNumber) ? localNumber.ToString() : "NULL");
                
                // The phone number location. Could be the city, region or country depending on the type of number
                Console.WriteLine("location: {0}",
                    data.TryGetProperty("location", out var location) ? location.ToString() : "NULL");
                
                // The network/carrier who owns the prefix (this only works for some countries, use HLR lookup for
                // global network detection)
                Console.WriteLine("prefix-network: {0}",
                    data.TryGetProperty("prefix-network", out var prefixNetwork) ? prefixNetwork.ToString() : "NULL");
                
                // The number type based on the number prefix. Possible values are:
                // • mobile
                // • fixed-line
                // • premium-rate
                // • toll-free
                // • voip
                // • unknown (use HLR lookup)
                Console.WriteLine("type: {0}",
                    data.TryGetProperty("type", out var type) ? type.ToString() : "NULL");
                
                // Is this a valid phone number
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