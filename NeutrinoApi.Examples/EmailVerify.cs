using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class EmailVerify
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

            var response = neutrinoApiClient.EmailVerify(parameters);
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
                
                // True if this email domain has a catch-all policy (it will accept mail for any username)
                Console.WriteLine("is-catch-all: {0}",
                    data.TryGetProperty("is-catch-all", out var isCatchAll) ? isCatchAll.ToString() : "NULL");
                
                // True if the mail server responded with a temporary failure (either a 4xx response code or
                // unresponsive server). You can retry this address later, we recommend waiting at least 15 minutes
                // before retrying
                Console.WriteLine("is-deferred: {0}",
                    data.TryGetProperty("is-deferred", out var isDeferred) ? isDeferred.ToString() : "NULL");
                
                // True if this address is a disposable, temporary or darknet related email address
                Console.WriteLine("is-disposable: {0}",
                    data.TryGetProperty("is-disposable", out var isDisposable) ? isDisposable.ToString() : "NULL");
                
                // True if this address is a free-mail address
                Console.WriteLine("is-freemail: {0}",
                    data.TryGetProperty("is-freemail", out var isFreemail) ? isFreemail.ToString() : "NULL");
                
                // True if this address is for a person. False if this is a role based address, e.g. admin@, help@,
                // office@, etc.
                Console.WriteLine("is-personal: {0}",
                    data.TryGetProperty("is-personal", out var isPersonal) ? isPersonal.ToString() : "NULL");
                
                // The email service provider domain
                Console.WriteLine("provider: {0}",
                    data.TryGetProperty("provider", out var provider) ? provider.ToString() : "NULL");
                
                // The raw SMTP response message received during verification
                Console.WriteLine("smtp-response: {0}",
                    data.TryGetProperty("smtp-response", out var smtpResponse) ? smtpResponse.ToString() : "NULL");
                
                // The SMTP verification status for the address:
                // • ok - SMTP verification was successful, this is a real address that can receive mail
                // • invalid - this is not a valid email address (has either a domain or syntax error)
                // • absent - this address is not registered with the email service provider
                // • unresponsive - the mail server(s) for this address timed-out or refused to open an SMTP
                //   connection
                // • unknown - sorry, we could not reliably determine the real status of this address (this
                //   address may or may not exist)
                Console.WriteLine("smtp-status: {0}",
                    data.TryGetProperty("smtp-status", out var smtpStatus) ? smtpStatus.ToString() : "NULL");
                
                // True if this address has a syntax error
                Console.WriteLine("syntax-error: {0}",
                    data.TryGetProperty("syntax-error", out var syntaxError) ? syntaxError.ToString() : "NULL");
                
                // True if typos have been fixed
                Console.WriteLine("typos-fixed: {0}",
                    data.TryGetProperty("typos-fixed", out var typosFixed) ? typosFixed.ToString() : "NULL");
                
                // Is this a valid email address (syntax and domain is valid)
                Console.WriteLine("valid: {0}",
                    data.TryGetProperty("valid", out var valid) ? valid.ToString() : "NULL");
                
                // True if this address has passed SMTP verification. Check the smtp-status and smtp-response fields
                // for specific verification details
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