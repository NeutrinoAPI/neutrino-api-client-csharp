using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class BadWordFilter
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The character to use to censor out the bad words found
                { "censor-character", "" },
                
                // Which catalog of bad words to use, we currently maintain two bad word catalogs:
                // • strict - the largest database of bad words which includes profanity, obscenity, sexual, rude,
                //   cuss, dirty, swear and objectionable words and phrases. This catalog is suitable for
                //   environments of all ages including educational or children's content
                // • obscene - like the strict catalog but does not include any mild profanities, idiomatic
                //   phrases or words which are considered formal terminology. This catalog is suitable for adult
                //   environments where certain types of bad words are considered OK
                { "catalog", "strict" },
                
                // The content to scan. This can be either a URL to load from, a file upload (multipart/form-data)
                // or an HTML content string
                { "content", "https://en.wikipedia.org/wiki/Profanity" }
            };

            var response = neutrinoApiClient.BadWordFilter(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // An array of the bad words found
                Console.WriteLine("bad-words-list: {0}",
                    data.TryGetProperty("bad-words-list", out var badWordsList) ? badWordsList.ToString() : "NULL");
                
                // Total number of bad words detected
                Console.WriteLine("bad-words-total: {0}",
                    data.TryGetProperty("bad-words-total", out var badWordsTotal) ? badWordsTotal.ToString() : "NULL");
                
                // The censored content (only set if censor-character has been set)
                Console.WriteLine("censored-content: {0}",
                    data.TryGetProperty("censored-content", out var censoredContent) ? censoredContent.ToString() : "NULL");
                
                // Does the text contain bad words
                Console.WriteLine("is-bad: {0}",
                    data.TryGetProperty("is-bad", out var isBad) ? isBad.ToString() : "NULL");
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