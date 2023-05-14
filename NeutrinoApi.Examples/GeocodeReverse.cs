using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class GeocodeReverse
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The location latitude in decimal degrees format
                { "latitude", "-41.2775847" },
                
                // The location longitude in decimal degrees format
                { "longitude", "174.7775229" },
                
                // The language to display results in, available languages are:
                // • de, en, es, fr, it, pt, ru
                { "language-code", "en" },
                
                // The zoom level to respond with:
                // • address - the most precise address available
                // • street - the street level
                // • city - the city level
                // • state - the state level
                // • country - the country level
                { "zoom", "address" }
            };

            var response = neutrinoApiClient.GeocodeReverse(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The complete address using comma-separated values
                Console.WriteLine("address: {0}",
                    data.TryGetProperty("address", out var address) ? address.ToString() : "NULL");
                
                // The components which make up the address such as road, city, state, etc
                Console.WriteLine("address-components: {0}",
                    data.TryGetProperty("address-components", out var addressComponents) ? addressComponents.ToString() : "NULL");
                
                // The city of the location
                Console.WriteLine("city: {0}",
                    data.TryGetProperty("city", out var city) ? city.ToString() : "NULL");
                
                // The country of the location
                Console.WriteLine("country: {0}",
                    data.TryGetProperty("country", out var country) ? country.ToString() : "NULL");
                
                // The ISO 2-letter country code of the location
                Console.WriteLine("country-code: {0}",
                    data.TryGetProperty("country-code", out var countryCode) ? countryCode.ToString() : "NULL");
                
                // The ISO 3-letter country code of the location
                Console.WriteLine("country-code3: {0}",
                    data.TryGetProperty("country-code3", out var countryCode3) ? countryCode3.ToString() : "NULL");
                
                // ISO 4217 currency code associated with the country
                Console.WriteLine("currency-code: {0}",
                    data.TryGetProperty("currency-code", out var currencyCode) ? currencyCode.ToString() : "NULL");
                
                // True if these coordinates map to a real location
                Console.WriteLine("found: {0}",
                    data.TryGetProperty("found", out var found) ? found.ToString() : "NULL");
                
                // The location latitude
                Console.WriteLine("latitude: {0}",
                    data.TryGetProperty("latitude", out var latitude) ? latitude.ToString() : "NULL");
                
                // Array of strings containing any location tags associated with the address. Tags are additional
                // pieces of metadata about a specific location, there are thousands of different tags. Some
                // examples of tags: shop, office, cafe, bank, pub
                Console.WriteLine("location-tags: {0}",
                    data.TryGetProperty("location-tags", out var locationTags) ? locationTags.ToString() : "NULL");
                
                // The detected location type ordered roughly from most to least precise, possible values are:
                // • address - indicates a precise street address
                // • street - accurate to the street level but may not point to the exact location of the
                //   house/building number
                // • city - accurate to the city level, this includes villages, towns, suburbs, etc
                // • postal-code - indicates a postal code area (no house or street information present)
                // • railway - location is part of a rail network such as a station or railway track
                // • natural - indicates a natural feature, for example a mountain peak or a waterway
                // • island - location is an island or archipelago
                // • administrative - indicates an administrative boundary such as a country, state or province
                Console.WriteLine("location-type: {0}",
                    data.TryGetProperty("location-type", out var locationType) ? locationType.ToString() : "NULL");
                
                // The location longitude
                Console.WriteLine("longitude: {0}",
                    data.TryGetProperty("longitude", out var longitude) ? longitude.ToString() : "NULL");
                
                // The formatted address using local standards suitable for printing on an envelope
                Console.WriteLine("postal-address: {0}",
                    data.TryGetProperty("postal-address", out var postalAddress) ? postalAddress.ToString() : "NULL");
                
                // The postal code for the location
                Console.WriteLine("postal-code: {0}",
                    data.TryGetProperty("postal-code", out var postalCode) ? postalCode.ToString() : "NULL");
                
                // The ISO 3166-2 region code for the location
                Console.WriteLine("region-code: {0}",
                    data.TryGetProperty("region-code", out var regionCode) ? regionCode.ToString() : "NULL");
                
                // The state of the location
                Console.WriteLine("state: {0}",
                    data.TryGetProperty("state", out var state) ? state.ToString() : "NULL");
                
                // Map containing timezone details
                Console.WriteLine("timezone: {0}",
                    data.TryGetProperty("timezone", out var timezone) ? timezone.ToString() : "NULL");
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