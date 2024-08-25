using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class GeocodeAddress
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The full address, partial address or name of a place to try and locate. Comma separated address
                // components are preferred.
                { "address", "1 Molesworth Street, Thorndon, Wellington 6011" },
                
                // The house/building number to locate
                { "house-number", "" },
                
                // The street/road name to locate
                { "street", "" },
                
                // The city/town name to locate
                { "city", "" },
                
                // The county/region name to locate
                { "county", "" },
                
                // The state name to locate
                { "state", "" },
                
                // The postal code to locate
                { "postal-code", "" },
                
                // Limit result to this country (the default is no country bias)
                { "country-code", "" },
                
                // The language to display results in, available languages are:
                // • de, en, es, fr, it, pt, ru, zh
                { "language-code", "en" },
                
                // If no matches are found for the given address, start performing a recursive fuzzy search until a
                // geolocation is found. This option is recommended for processing user input or implementing
                // auto-complete. We use a combination of approximate string matching and data cleansing to find
                // possible location matches
                { "fuzzy-search", "false" }
            };

            var response = neutrinoApiClient.GeocodeAddress(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The number of possible matching locations found
                Console.WriteLine("found: {0}",
                    data.TryGetProperty("found", out var found) ? found.ToString() : "NULL");
                
                // Array of matching location objects
                var locations = data.GetProperty("locations");
                Console.WriteLine("locations:");
                for (var i = 0; i < locations.GetArrayLength(); i++)
                {
                    var item = locations[i];

                    // The complete address using comma-separated values
                    Console.WriteLine("    address: {0}",
                        item.TryGetProperty("address", out var itemAddress) ? itemAddress.ToString() : "NULL");

                    // The components which make up the address such as road, city, state, etc
                    Console.WriteLine("    address-components: {0}",
                        item.TryGetProperty("address-components", out var itemAddressComponents) ? itemAddressComponents.ToString() : "NULL");

                    // The city of the location
                    Console.WriteLine("    city: {0}",
                        item.TryGetProperty("city", out var itemCity) ? itemCity.ToString() : "NULL");

                    // The country of the location
                    Console.WriteLine("    country: {0}",
                        item.TryGetProperty("country", out var itemCountry) ? itemCountry.ToString() : "NULL");

                    // The ISO 2-letter country code of the location
                    Console.WriteLine("    country-code: {0}",
                        item.TryGetProperty("country-code", out var itemCountryCode) ? itemCountryCode.ToString() : "NULL");

                    // The ISO 3-letter country code of the location
                    Console.WriteLine("    country-code3: {0}",
                        item.TryGetProperty("country-code3", out var itemCountryCode3) ? itemCountryCode3.ToString() : "NULL");

                    // ISO 4217 currency code associated with the country
                    Console.WriteLine("    currency-code: {0}",
                        item.TryGetProperty("currency-code", out var itemCurrencyCode) ? itemCurrencyCode.ToString() : "NULL");

                    // The location latitude
                    Console.WriteLine("    latitude: {0}",
                        item.TryGetProperty("latitude", out var itemLatitude) ? itemLatitude.ToString() : "NULL");

                    // Array of strings containing any location tags associated with the address. Tags are additional
                    // pieces of metadata about a specific location, there are thousands of different tags. Some
                    // examples of tags: shop, office, cafe, bank, pub
                    Console.WriteLine("    location-tags: {0}",
                        item.TryGetProperty("location-tags", out var itemLocationTags) ? itemLocationTags.ToString() : "NULL");

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
                    Console.WriteLine("    location-type: {0}",
                        item.TryGetProperty("location-type", out var itemLocationType) ? itemLocationType.ToString() : "NULL");

                    // The location longitude
                    Console.WriteLine("    longitude: {0}",
                        item.TryGetProperty("longitude", out var itemLongitude) ? itemLongitude.ToString() : "NULL");

                    // The formatted address using local standards suitable for printing on an envelope
                    Console.WriteLine("    postal-address: {0}",
                        item.TryGetProperty("postal-address", out var itemPostalAddress) ? itemPostalAddress.ToString() : "NULL");

                    // The postal code for the location
                    Console.WriteLine("    postal-code: {0}",
                        item.TryGetProperty("postal-code", out var itemPostalCode) ? itemPostalCode.ToString() : "NULL");

                    // The ISO 3166-2 region code for the location
                    Console.WriteLine("    region-code: {0}",
                        item.TryGetProperty("region-code", out var itemRegionCode) ? itemRegionCode.ToString() : "NULL");

                    // The state of the location
                    Console.WriteLine("    state: {0}",
                        item.TryGetProperty("state", out var itemState) ? itemState.ToString() : "NULL");

                    // Structure of a ip-info -> timezone response
                    Console.WriteLine("    timezone: {0}",
                        item.TryGetProperty("timezone", out var itemTimezone) ? itemTimezone.ToString() : "NULL");

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