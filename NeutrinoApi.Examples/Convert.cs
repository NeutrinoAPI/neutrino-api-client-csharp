using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class Convert
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The value to convert from (e.g. 10.95)
                { "from-value", "100" },
                
                // The type of the value to convert from (e.g. USD)
                { "from-type", "USD" },
                
                // The type to convert to (e.g. EUR)
                { "to-type", "EUR" }
            };

            var response = neutrinoApiClient.Convert(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The type of the value being converted from
                Console.WriteLine("from-type: {0}",
                    data.TryGetProperty("from-type", out var fromType) ? fromType.ToString() : "NULL");
                
                // The value being converted from
                Console.WriteLine("from-value: {0}",
                    data.TryGetProperty("from-value", out var fromValue) ? fromValue.ToString() : "NULL");
                
                // The result of the conversion in string format
                Console.WriteLine("result: {0}",
                    data.TryGetProperty("result", out var result) ? result.ToString() : "NULL");
                
                // The result of the conversion as a floating-point number
                Console.WriteLine("result-float: {0}",
                    data.TryGetProperty("result-float", out var resultFloat) ? resultFloat.ToString() : "NULL");
                
                // The type being converted to
                Console.WriteLine("to-type: {0}",
                    data.TryGetProperty("to-type", out var toType) ? toType.ToString() : "NULL");
                
                // True if the conversion was successful and produced a valid result
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