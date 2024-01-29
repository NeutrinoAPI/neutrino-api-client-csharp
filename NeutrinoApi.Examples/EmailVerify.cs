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
                
                // The domain name of this email address
                Console.WriteLine("domain: {0}",
                    data.TryGetProperty("domain", out var domain) ? domain.ToString() : "NULL");
                
                // True if this address has any domain name or DNS related errors. Check the 'domain-status' field
                // for the detailed error reason
                Console.WriteLine("domain-error: {0}",
                    data.TryGetProperty("domain-error", out var domainError) ? domainError.ToString() : "NULL");
                
                // The email domain status, possible values are:
                // • ok - the domain is in working order and can receive email
                // • invalid - the domain is not a conformant hostname. May contain invalid syntax or characters
                // • no-service - the domain owner has indicated there is no mail service on the domain (also
                //   known as the 'Null MX')
                // • no-mail - the domain has no valid MX records so cannot receive email
                // • mx-invalid - MX records contain invalid or non-conformant hostname values
                // • mx-bogon - MX records point to bogon IP addresses
                // • resolv-error - MX records do not resolve to any valid IP addresses
                Console.WriteLine("domain-status: {0}",
                    data.TryGetProperty("domain-status", out var domainStatus) ? domainStatus.ToString() : "NULL");
                
                // The complete email address. If you enabled the 'fix-typos' option then this will be the corrected
                // address
                Console.WriteLine("email: {0}",
                    data.TryGetProperty("email", out var email) ? email.ToString() : "NULL");
                
                // True if this email domain has a catch-all policy. A catch-all domain will accept mail for any
                // username so therefor the 'smtp-status' will always be 'ok'
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
                
                // True if this address is from a free email provider
                Console.WriteLine("is-freemail: {0}",
                    data.TryGetProperty("is-freemail", out var isFreemail) ? isFreemail.ToString() : "NULL");
                
                // True if this address likely belongs to a person. False if this is a role based address, e.g.
                // admin@, help@, office@, etc.
                Console.WriteLine("is-personal: {0}",
                    data.TryGetProperty("is-personal", out var isPersonal) ? isPersonal.ToString() : "NULL");
                
                // The first resolved IP address of the primary MX server, may be empty if there are domain errors
                // present
                Console.WriteLine("mx-ip: {0}",
                    data.TryGetProperty("mx-ip", out var mxIp) ? mxIp.ToString() : "NULL");
                
                // The domain name of the email hosting provider
                Console.WriteLine("provider: {0}",
                    data.TryGetProperty("provider", out var provider) ? provider.ToString() : "NULL");
                
                // The raw SMTP response message received during verification
                Console.WriteLine("smtp-response: {0}",
                    data.TryGetProperty("smtp-response", out var smtpResponse) ? smtpResponse.ToString() : "NULL");
                
                // The SMTP username verification status for this address:
                // • ok - verification was successful, this is a real username that can receive mail
                // • absent - this username or domain is not registered with the email service provider
                // • invalid - not a valid email address, check the 'domain-status' field for specific details
                // • unresponsive - the mail servers for this domain have repeatedly timed-out or refused multiple
                //   connection attempts
                // • unknown - sorry, we could not reliably determine the status of this username
                Console.WriteLine("smtp-status: {0}",
                    data.TryGetProperty("smtp-status", out var smtpStatus) ? smtpStatus.ToString() : "NULL");
                
                // True if this address has any syntax errors or is not in RFC compliant formatting
                Console.WriteLine("syntax-error: {0}",
                    data.TryGetProperty("syntax-error", out var syntaxError) ? syntaxError.ToString() : "NULL");
                
                // True if any typos have been fixed. The 'fix-typos' option must be enabled for this to work
                Console.WriteLine("typos-fixed: {0}",
                    data.TryGetProperty("typos-fixed", out var typosFixed) ? typosFixed.ToString() : "NULL");
                
                // Is this a valid email address. To be valid an email must have: correct syntax, a registered and
                // active domain name, correct DNS records and operational MX servers
                Console.WriteLine("valid: {0}",
                    data.TryGetProperty("valid", out var valid) ? valid.ToString() : "NULL");
                
                // True if this email address has passed SMTP username verification. Check the 'smtp-status' and
                // 'domain-status' fields for specific verification details
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