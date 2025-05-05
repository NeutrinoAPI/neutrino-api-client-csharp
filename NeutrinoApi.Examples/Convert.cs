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
                { "to-type", "EUR" },
                
                // Convert using the rate on a historical date, accepted date formats are: YYYY-MM-DD, YYYY-MM,
                // YYYY. Historical rates are stored with daily granularity so the date format YYYY-MM-DD is
                // preferred for the highest precision. If an invalid date or a date too far into the past is
                // supplied then the API will respond with 'valid' as false and an empty 'historical-date'
                { "historical-date", "" }
            };

            var response = neutrinoApiClient.Convert(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The full name of the type being converted from
                Console.WriteLine("from-name: {0}",
                    data.TryGetProperty("from-name", out var fromName) ? fromName.ToString() : "NULL");
                
                // The standard UTF-8 symbol used to represent the type being converted from
                Console.WriteLine("from-symbol: {0}",
                    data.TryGetProperty("from-symbol", out var fromSymbol) ? fromSymbol.ToString() : "NULL");
                
                // The type of the value being converted from
                Console.WriteLine("from-type: {0}",
                    data.TryGetProperty("from-type", out var fromType) ? fromType.ToString() : "NULL");
                
                // The value being converted from
                Console.WriteLine("from-value: {0}",
                    data.TryGetProperty("from-value", out var fromValue) ? fromValue.ToString() : "NULL");
                
                // If a historical conversion was made using the 'historical-date' request option this will contain
                // the exact date used for the conversion in ISO format: YYYY-MM-DD
                Console.WriteLine("historical-date: {0}",
                    data.TryGetProperty("historical-date", out var historicalDate) ? historicalDate.ToString() : "NULL");
                
                // The result of the conversion in string format
                Console.WriteLine("result: {0}",
                    data.TryGetProperty("result", out var result) ? result.ToString() : "NULL");
                
                // The result of the conversion as a floating-point number
                Console.WriteLine("result-float: {0}",
                    data.TryGetProperty("result-float", out var resultFloat) ? resultFloat.ToString() : "NULL");
                
                // The full name of the type being converted to
                Console.WriteLine("to-name: {0}",
                    data.TryGetProperty("to-name", out var toName) ? toName.ToString() : "NULL");
                
                // The standard UTF-8 symbol used to represent the type being converted to
                Console.WriteLine("to-symbol: {0}",
                    data.TryGetProperty("to-symbol", out var toSymbol) ? toSymbol.ToString() : "NULL");
                
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