using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class PhonePlayback
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The phone number to call. Must be in valid international format
                { "number", "+12106100045" },
                
                // Limit the total number of calls allowed to the supplied phone number, if the limit is reached
                // within the TTL then error code 14 will be returned
                { "limit", "3" },
                
                // A URL to a valid audio file. Accepted audio formats are:
                // • MP3
                // • WAV
                // • OGG You can use the following MP3 URL for testing:
                //   https://www.neutrinoapi.com/test-files/test1.mp3
                { "audio-url", "https://www.neutrinoapi.com/test-files/test1.mp3" },
                
                // Set the TTL in number of days that the 'limit' option will remember a phone number (the default
                // is 1 day and the maximum is 365 days)
                { "limit-ttl", "1" }
            };

            var response = neutrinoApiClient.PhonePlayback(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // True if the call is being made now
                Console.WriteLine("calling: {0}",
                    data.TryGetProperty("calling", out var calling) ? calling.ToString() : "NULL");
                
                // True if this a valid phone number
                Console.WriteLine("number-valid: {0}",
                    data.TryGetProperty("number-valid", out var numberValid) ? numberValid.ToString() : "NULL");
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