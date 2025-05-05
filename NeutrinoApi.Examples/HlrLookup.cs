using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class HlrLookup
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // A phone number
                { "number", "+12106100045" },
                
                // ISO 2-letter country code, assume numbers are based in this country. If not set numbers are
                // assumed to be in international format (with or without the leading + sign)
                { "country-code", "" }
            };

            var response = neutrinoApiClient.HlrLookup(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The phone number country
                Console.WriteLine("country: {0}",
                    data.TryGetProperty("country", out var country) ? country.ToString() : "NULL");
                
                // The number location as an ISO 2-letter country code
                Console.WriteLine("country-code: {0}",
                    data.TryGetProperty("country-code", out var countryCode) ? countryCode.ToString() : "NULL");
                
                // The number location as an ISO 3-letter country code
                Console.WriteLine("country-code3: {0}",
                    data.TryGetProperty("country-code3", out var countryCode3) ? countryCode3.ToString() : "NULL");
                
                // ISO 4217 currency code associated with the country
                Console.WriteLine("currency-code: {0}",
                    data.TryGetProperty("currency-code", out var currencyCode) ? currencyCode.ToString() : "NULL");
                
                // The currently used network/carrier name
                Console.WriteLine("current-network: {0}",
                    data.TryGetProperty("current-network", out var currentNetwork) ? currentNetwork.ToString() : "NULL");
                
                // The HLR lookup status, possible values are:
                // • ok - the HLR lookup was successful and the device is connected
                // • absent - the number was once registered but the device has been switched off or out of
                //   network range for some time
                // • unknown - the number is not known by the mobile network
                // • invalid - the number is not a valid mobile MSISDN number
                // • fixed-line - the number is a registered fixed-line not mobile
                // • voip - the number has been detected as a VOIP line
                // • failed - the HLR lookup has failed, we could not determine the real status of this number
                Console.WriteLine("hlr-status: {0}",
                    data.TryGetProperty("hlr-status", out var hlrStatus) ? hlrStatus.ToString() : "NULL");
                
                // Was the HLR lookup successful. If true then this is a working and registered cell-phone or mobile
                // device (SMS and phone calls will be delivered)
                Console.WriteLine("hlr-valid: {0}",
                    data.TryGetProperty("hlr-valid", out var hlrValid) ? hlrValid.ToString() : "NULL");
                
                // The mobile IMSI number (International Mobile Subscriber Identity)
                Console.WriteLine("imsi: {0}",
                    data.TryGetProperty("imsi", out var imsi) ? imsi.ToString() : "NULL");
                
                // The international calling code
                Console.WriteLine("international-calling-code: {0}",
                    data.TryGetProperty("international-calling-code", out var internationalCallingCode) ? internationalCallingCode.ToString() : "NULL");
                
                // The number represented in full international format
                Console.WriteLine("international-number: {0}",
                    data.TryGetProperty("international-number", out var internationalNumber) ? internationalNumber.ToString() : "NULL");
                
                // True if this is a mobile number (only true with 100% certainty, if the number type is unknown
                // this value will be false)
                Console.WriteLine("is-mobile: {0}",
                    data.TryGetProperty("is-mobile", out var isMobile) ? isMobile.ToString() : "NULL");
                
                // Has this number been ported to another network
                Console.WriteLine("is-ported: {0}",
                    data.TryGetProperty("is-ported", out var isPorted) ? isPorted.ToString() : "NULL");
                
                // Is this number currently roaming from its origin country
                Console.WriteLine("is-roaming: {0}",
                    data.TryGetProperty("is-roaming", out var isRoaming) ? isRoaming.ToString() : "NULL");
                
                // The number represented in local dialing format
                Console.WriteLine("local-number: {0}",
                    data.TryGetProperty("local-number", out var localNumber) ? localNumber.ToString() : "NULL");
                
                // The number location. Could be a city, region or country depending on the type of number
                Console.WriteLine("location: {0}",
                    data.TryGetProperty("location", out var location) ? location.ToString() : "NULL");
                
                // The mobile MCC number (Mobile Country Code)
                Console.WriteLine("mcc: {0}",
                    data.TryGetProperty("mcc", out var mcc) ? mcc.ToString() : "NULL");
                
                // The mobile MNC number (Mobile Network Code)
                Console.WriteLine("mnc: {0}",
                    data.TryGetProperty("mnc", out var mnc) ? mnc.ToString() : "NULL");
                
                // The mobile MSC number (Mobile Switching Center)
                Console.WriteLine("msc: {0}",
                    data.TryGetProperty("msc", out var msc) ? msc.ToString() : "NULL");
                
                // The mobile MSIN number (Mobile Subscription Identification Number)
                Console.WriteLine("msin: {0}",
                    data.TryGetProperty("msin", out var msin) ? msin.ToString() : "NULL");
                
                // Contains any additional details about the current network such as former network names and mobile
                // technology utilized
                Console.WriteLine("network-tags: {0}",
                    data.TryGetProperty("network-tags", out var networkTags) ? networkTags.ToString() : "NULL");
                
                // The number type, possible values are:
                // • mobile
                // • fixed-line
                // • premium-rate
                // • toll-free
                // • voip
                // • unknown
                Console.WriteLine("number-type: {0}",
                    data.TryGetProperty("number-type", out var numberType) ? numberType.ToString() : "NULL");
                
                // True if this a valid phone number
                Console.WriteLine("number-valid: {0}",
                    data.TryGetProperty("number-valid", out var numberValid) ? numberValid.ToString() : "NULL");
                
                // The origin network/carrier name
                Console.WriteLine("origin-network: {0}",
                    data.TryGetProperty("origin-network", out var originNetwork) ? originNetwork.ToString() : "NULL");
                
                // The ported to network/carrier name (only set if the number has been ported)
                Console.WriteLine("ported-network: {0}",
                    data.TryGetProperty("ported-network", out var portedNetwork) ? portedNetwork.ToString() : "NULL");
                
                // If the number is currently roaming, the ISO 2-letter country code of the roaming in country
                Console.WriteLine("roaming-country-code: {0}",
                    data.TryGetProperty("roaming-country-code", out var roamingCountryCode) ? roamingCountryCode.ToString() : "NULL");
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