using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class SmsVerify
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The phone number to send a verification code to
                { "number", "+12106100045" },
                
                // ISO 2-letter country code, assume numbers are based in this country. If not set numbers are
                // assumed to be in international format (with or without the leading + sign)
                { "country-code", "" },
                
                // Pass in your own security code. This is useful if you have implemented TOTP or similar 2FA
                // methods. If not set then we will generate a secure random code
                { "security-code", "" },
                
                // The language to send the verification code in, available languages are:
                // • de - German
                // • en - English
                // • es - Spanish
                // • fr - French
                // • it - Italian
                // • pt - Portuguese
                // • ru - Russian
                { "language-code", "en" },
                
                // The number of digits to use in the security code (must be between 4 and 12)
                { "code-length", "5" },
                
                // Limit the total number of SMS allowed to the supplied phone number, if the limit is reached
                // within the TTL then error code 14 will be returned
                { "limit", "10" },
                
                // Set a custom brand or product name in the verification message
                { "brand-name", "" },
                
                // Set the TTL in number of days that the 'limit' option will remember a phone number (the default
                // is 1 day and the maximum is 365 days)
                { "limit-ttl", "1" }
            };

            var response = neutrinoApiClient.SmsVerify(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // True if this a valid phone number
                Console.WriteLine("number-valid: {0}",
                    data.TryGetProperty("number-valid", out var numberValid) ? numberValid.ToString() : "NULL");
                
                // The security code generated, you can save this code to perform your own verification or you can
                // use the Verify Security Code API
                Console.WriteLine("security-code: {0}",
                    data.TryGetProperty("security-code", out var securityCode) ? securityCode.ToString() : "NULL");
                
                // True if the SMS has been sent
                Console.WriteLine("sent: {0}",
                    data.TryGetProperty("sent", out var sent) ? sent.ToString() : "NULL");
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