using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class VerifySecurityCode
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The security code to verify
                { "security-code", "123456" },
                
                // If set then enable additional brute-force protection by limiting the number of attempts by the
                // supplied value. This can be set to any unique identifier you would like to limit by, for example
                // a hash of the users email, phone number or IP address. Requests to this API will be ignored after
                // approximately 10 failed verification attempts
                { "limit-by", "" }
            };

            var response = neutrinoApiClient.VerifySecurityCode(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // True if the code is valid
                Console.WriteLine("verified: {0}",
                    data.TryGetProperty("verified", out var verified) ? verified.ToString() : "NULL");
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