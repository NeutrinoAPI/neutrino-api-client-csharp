using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class EmailValidate
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // An email address
                { "email", "tech@neutrinoapi.com" },
                
                // Automatically attempt to fix typos in the address
                { "fix-typos", "false" }
            };

            var response = neutrinoApiClient.EmailValidate(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // The email domain
                Console.WriteLine("domain: {0}",
                    data.TryGetProperty("domain", out var domain) ? domain.ToString() : "NULL");
                
                // True if this address has a domain error (e.g. no valid mail server records)
                Console.WriteLine("domain-error: {0}",
                    data.TryGetProperty("domain-error", out var domainError) ? domainError.ToString() : "NULL");
                
                // The email address. If you have used the fix-typos option then this will be the fixed address
                Console.WriteLine("email: {0}",
                    data.TryGetProperty("email", out var email) ? email.ToString() : "NULL");
                
                // True if this address is a disposable, temporary or darknet related email address
                Console.WriteLine("is-disposable: {0}",
                    data.TryGetProperty("is-disposable", out var isDisposable) ? isDisposable.ToString() : "NULL");
                
                // True if this address is a free-mail address
                Console.WriteLine("is-freemail: {0}",
                    data.TryGetProperty("is-freemail", out var isFreemail) ? isFreemail.ToString() : "NULL");
                
                // True if this address belongs to a person. False if this is a role based address, e.g. admin@,
                // help@, office@, etc.
                Console.WriteLine("is-personal: {0}",
                    data.TryGetProperty("is-personal", out var isPersonal) ? isPersonal.ToString() : "NULL");
                
                // The email service provider domain
                Console.WriteLine("provider: {0}",
                    data.TryGetProperty("provider", out var provider) ? provider.ToString() : "NULL");
                
                // True if this address has a syntax error
                Console.WriteLine("syntax-error: {0}",
                    data.TryGetProperty("syntax-error", out var syntaxError) ? syntaxError.ToString() : "NULL");
                
                // True if typos have been fixed
                Console.WriteLine("typos-fixed: {0}",
                    data.TryGetProperty("typos-fixed", out var typosFixed) ? typosFixed.ToString() : "NULL");
                
                // Is this a valid email
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