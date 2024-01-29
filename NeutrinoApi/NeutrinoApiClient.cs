// <copyright file="NeutrinoApiClient.cs" company="NeutrinoApi">
//     Copyright NeutrinoApi
// </copyright>

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NeutrinoApi
{
    /// <summary>Make a request to the Neutrino API</summary>
    public class NeutrinoApiClient
    {
        /// <summary>MulticloudEndpoint server</summary>
        public const string MulticloudEndpoint = "https://neutrinoapi.net/";
        /// <summary>AwsEndpoint server</summary>
        public const string AwsEndpoint = "https://aws.neutrinoapi.net/";
        /// <summary>GcpEndpoint server</summary>
        public const string GcpEndpoint = "https://gcp.neutrinoapi.net/";
        /// <summary>MsAzureEndpoint server</summary>
        public const string MsAzureEndpoint = "https://msa.neutrinoapi.net/";
        
        private readonly string _apiKey;
        private readonly string _userId;
        private readonly string _baseUrl;
        
        private static HttpClient client = new HttpClient();
        private const int DefaultTimeoutInSeconds = 300;
        
        /// <summary>NeutrinoApiClient constructor</summary>
        public NeutrinoApiClient(string userId, string apiKey)
        {
            _userId = userId;
            _apiKey = apiKey;
            _baseUrl = MulticloudEndpoint;
            client.DefaultRequestHeaders.Add("User-ID", _userId);
            client.DefaultRequestHeaders.Add("Api-Key", _apiKey);
            client.Timeout = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
        }

        /// <summary>NeutrinoApiClient constructor using base URL override</summary>
        public NeutrinoApiClient(string userId, string apiKey, string baseUrl)
        {
            _userId = userId;
            _apiKey = apiKey;
            _baseUrl = baseUrl;
            client.DefaultRequestHeaders.Add("User-ID", _userId);
            client.DefaultRequestHeaders.Add("Api-Key", _apiKey);
            client.Timeout = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
        }

        /// <summary>Detect bad words, swear words and profanity in a given text</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>censor-character</term>
        ///         <description>The character to use to censor out the bad words found</description>
        ///     </item>
        ///     <item>
        ///         <term>catalog</term>
        ///         <description>Which catalog of bad words to use</description>
        ///     </item>
        ///     <item>
        ///         <term>content</term>
        ///         <description>The content to scan</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/bad-word-filter</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse BadWordFilter(Dictionary<string, string> paramDict)
        {
            return ExecRequest("POST", "bad-word-filter", paramDict, default, 30);
        }

        /// <summary>Download our entire BIN database for direct use on your own systems</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>include-iso3</term>
        ///         <description>Include ISO 3-letter country codes and ISO 3-letter currency codes in the data</description>
        ///     </item>
        ///     <item>
        ///         <term>include-8digit</term>
        ///         <description>Include 8-digit and higher BIN codes</description>
        ///     </item>
        ///     <item>
        ///         <term>include-all</term>
        ///         <description>Include all BINs and all available fields in the CSV file (overrides any values set for 'include-iso3' or 'include-8digit')</description>
        ///     </item>
        ///     <item>
        ///         <term>output-encoding</term>
        ///         <description>Set this option to 'gzip' to have the output file compressed using gzip</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/bin-list-download</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse BinListDownload(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "bin-list-download", paramDict, outputFilePath, 30);
        }

        /// <summary>Perform a BIN (Bank Identification Number) or IIN (Issuer Identification Number) lookup</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>bin-number</term>
        ///         <description>The BIN or IIN number</description>
        ///     </item>
        ///     <item>
        ///         <term>customer-ip</term>
        ///         <description>Pass in the customers IP address and we will return some extra information about them</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/bin-lookup</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse BinLookup(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "bin-lookup", paramDict, default, 10);
        }

        /// <summary>Browser bot can extract content, interact with keyboard and mouse events, and execute JavaScript on a website</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>delay</term>
        ///         <description>Delay in seconds to wait before capturing any page data</description>
        ///     </item>
        ///     <item>
        ///         <term>ignore-certificate-errors</term>
        ///         <description>Ignore any TLS/SSL certificate errors and load the page anyway</description>
        ///     </item>
        ///     <item>
        ///         <term>selector</term>
        ///         <description>Extract content from the page DOM using this selector</description>
        ///     </item>
        ///     <item>
        ///         <term>url</term>
        ///         <description>The URL to load</description>
        ///     </item>
        ///     <item>
        ///         <term>timeout</term>
        ///         <description>Timeout in seconds</description>
        ///     </item>
        ///     <item>
        ///         <term>exec</term>
        ///         <description>Execute JavaScript on the website</description>
        ///     </item>
        ///     <item>
        ///         <term>user-agent</term>
        ///         <description>Override the browsers default user-agent string with this one</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/browser-bot</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse BrowserBot(Dictionary<string, string> paramDict)
        {
            return ExecRequest("POST", "browser-bot", paramDict, default, 300);
        }

        /// <summary>A currency and unit conversion tool</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>from-value</term>
        ///         <description>The value to convert from (e.g. 10.95)</description>
        ///     </item>
        ///     <item>
        ///         <term>from-type</term>
        ///         <description>The type of the value to convert from (e.g. USD)</description>
        ///     </item>
        ///     <item>
        ///         <term>to-type</term>
        ///         <description>The type to convert to (e.g. EUR)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/convert</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse Convert(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "convert", paramDict, default, 10);
        }

        /// <summary>Retrieve domain name details and detect potentially malicious or dangerous domains</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>host</term>
        ///         <description>A domain name</description>
        ///     </item>
        ///     <item>
        ///         <term>live</term>
        ///         <description>For domains that we have never seen before then perform various live checks and realtime reconnaissance</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/domain-lookup</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse DomainLookup(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "domain-lookup", paramDict, default, 120);
        }

        /// <summary>Parse, validate and clean an email address</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>email</term>
        ///         <description>An email address</description>
        ///     </item>
        ///     <item>
        ///         <term>fix-typos</term>
        ///         <description>Automatically attempt to fix typos in the address</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/email-validate</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse EmailValidate(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "email-validate", paramDict, default, 30);
        }

        /// <summary>SMTP based email address verification</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>email</term>
        ///         <description>An email address</description>
        ///     </item>
        ///     <item>
        ///         <term>fix-typos</term>
        ///         <description>Automatically attempt to fix typos in the address</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/email-verify</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse EmailVerify(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "email-verify", paramDict, default, 120);
        }

        /// <summary>Geocode an address, partial address or just the name of a place</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>address</term>
        ///         <description>The full address</description>
        ///     </item>
        ///     <item>
        ///         <term>house-number</term>
        ///         <description>The house/building number to locate</description>
        ///     </item>
        ///     <item>
        ///         <term>street</term>
        ///         <description>The street/road name to locate</description>
        ///     </item>
        ///     <item>
        ///         <term>city</term>
        ///         <description>The city/town name to locate</description>
        ///     </item>
        ///     <item>
        ///         <term>county</term>
        ///         <description>The county/region name to locate</description>
        ///     </item>
        ///     <item>
        ///         <term>state</term>
        ///         <description>The state name to locate</description>
        ///     </item>
        ///     <item>
        ///         <term>postal-code</term>
        ///         <description>The postal code to locate</description>
        ///     </item>
        ///     <item>
        ///         <term>country-code</term>
        ///         <description>Limit result to this country (the default is no country bias)</description>
        ///     </item>
        ///     <item>
        ///         <term>language-code</term>
        ///         <description>The language to display results in</description>
        ///     </item>
        ///     <item>
        ///         <term>fuzzy-search</term>
        ///         <description>If no matches are found for the given address</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/geocode-address</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse GeocodeAddress(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "geocode-address", paramDict, default, 30);
        }

        /// <summary>Convert a geographic coordinate (latitude and longitude) into a real world address</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>latitude</term>
        ///         <description>The location latitude in decimal degrees format</description>
        ///     </item>
        ///     <item>
        ///         <term>longitude</term>
        ///         <description>The location longitude in decimal degrees format</description>
        ///     </item>
        ///     <item>
        ///         <term>language-code</term>
        ///         <description>The language to display results in</description>
        ///     </item>
        ///     <item>
        ///         <term>zoom</term>
        ///         <description>The zoom level to respond with: address - the most precise address available street - the street level city - the city level state - the state level country - the country level </description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/geocode-reverse</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse GeocodeReverse(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "geocode-reverse", paramDict, default, 30);
        }

        /// <summary>Connect to the global mobile cellular network and retrieve the status of a mobile device</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>number</term>
        ///         <description>A phone number</description>
        ///     </item>
        ///     <item>
        ///         <term>country-code</term>
        ///         <description>ISO 2-letter country code</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/hlr-lookup</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse HlrLookup(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "hlr-lookup", paramDict, default, 30);
        }

        /// <summary>Check the reputation of an IP address, domain name or URL against a comprehensive list of blacklists and blocklists</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>host</term>
        ///         <description>An IP address</description>
        ///     </item>
        ///     <item>
        ///         <term>list-rating</term>
        ///         <description>Only check lists with this rating or better</description>
        ///     </item>
        ///     <item>
        ///         <term>zones</term>
        ///         <description>Only check these DNSBL zones/hosts</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/host-reputation</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse HostReputation(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "host-reputation", paramDict, default, 120);
        }

        /// <summary>Clean and sanitize untrusted HTML</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>output-type</term>
        ///         <description>The level of sanitization</description>
        ///     </item>
        ///     <item>
        ///         <term>content</term>
        ///         <description>The HTML content</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/html-clean</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse HtmlClean(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "html-clean", paramDict, outputFilePath, 30);
        }

        /// <summary>Render HTML content to PDF, JPG or PNG</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>margin</term>
        ///         <description>The document margin (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>css</term>
        ///         <description>Inject custom CSS into the HTML</description>
        ///     </item>
        ///     <item>
        ///         <term>image-width</term>
        ///         <description>If rendering to an image format (PNG or JPG) use this image width (in pixels)</description>
        ///     </item>
        ///     <item>
        ///         <term>footer</term>
        ///         <description>The footer HTML to insert into each page</description>
        ///     </item>
        ///     <item>
        ///         <term>format</term>
        ///         <description>Which format to output</description>
        ///     </item>
        ///     <item>
        ///         <term>zoom</term>
        ///         <description>Set the zoom factor when rendering the page (2.0 for double size</description>
        ///     </item>
        ///     <item>
        ///         <term>title</term>
        ///         <description>The document title</description>
        ///     </item>
        ///     <item>
        ///         <term>content</term>
        ///         <description>The HTML content</description>
        ///     </item>
        ///     <item>
        ///         <term>page-width</term>
        ///         <description>Set the PDF page width explicitly (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>timeout</term>
        ///         <description>Timeout in seconds</description>
        ///     </item>
        ///     <item>
        ///         <term>margin-right</term>
        ///         <description>The document right margin (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>grayscale</term>
        ///         <description>Render the final document in grayscale</description>
        ///     </item>
        ///     <item>
        ///         <term>margin-left</term>
        ///         <description>The document left margin (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>page-size</term>
        ///         <description>Set the document page size</description>
        ///     </item>
        ///     <item>
        ///         <term>delay</term>
        ///         <description>Number of seconds to wait before rendering the page (can be useful for pages with animations etc)</description>
        ///     </item>
        ///     <item>
        ///         <term>ignore-certificate-errors</term>
        ///         <description>Ignore any TLS/SSL certificate errors</description>
        ///     </item>
        ///     <item>
        ///         <term>page-height</term>
        ///         <description>Set the PDF page height explicitly (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>image-height</term>
        ///         <description>If rendering to an image format (PNG or JPG) use this image height (in pixels)</description>
        ///     </item>
        ///     <item>
        ///         <term>header</term>
        ///         <description>The header HTML to insert into each page</description>
        ///     </item>
        ///     <item>
        ///         <term>margin-top</term>
        ///         <description>The document top margin (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>margin-bottom</term>
        ///         <description>The document bottom margin (in mm)</description>
        ///     </item>
        ///     <item>
        ///         <term>bg-color</term>
        ///         <description>For image rendering set the background color in hexadecimal notation (e.g. #0000ff)</description>
        ///     </item>
        ///     <item>
        ///         <term>landscape</term>
        ///         <description>Set the document to landscape orientation</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/html-render</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse HtmlRender(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "html-render", paramDict, outputFilePath, 300);
        }

        /// <summary>Resize an image and output as either JPEG or PNG</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>resize-mode</term>
        ///         <description>The resize mode to use</description>
        ///     </item>
        ///     <item>
        ///         <term>width</term>
        ///         <description>The width to resize to (in px)</description>
        ///     </item>
        ///     <item>
        ///         <term>format</term>
        ///         <description>The output image format</description>
        ///     </item>
        ///     <item>
        ///         <term>image-url</term>
        ///         <description>The URL or Base64 encoded Data URL for the source image</description>
        ///     </item>
        ///     <item>
        ///         <term>bg-color</term>
        ///         <description>The image background color in hexadecimal notation (e.g. #0000ff)</description>
        ///     </item>
        ///     <item>
        ///         <term>height</term>
        ///         <description>The height to resize to (in px)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/image-resize</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse ImageResize(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "image-resize", paramDict, outputFilePath, 20);
        }

        /// <summary>Watermark one image with another image</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>resize-mode</term>
        ///         <description>The resize mode to use</description>
        ///     </item>
        ///     <item>
        ///         <term>format</term>
        ///         <description>The output image format</description>
        ///     </item>
        ///     <item>
        ///         <term>width</term>
        ///         <description>If set resize the resulting image to this width (in px)</description>
        ///     </item>
        ///     <item>
        ///         <term>image-url</term>
        ///         <description>The URL or Base64 encoded Data URL for the source image</description>
        ///     </item>
        ///     <item>
        ///         <term>position</term>
        ///         <description>The position of the watermark image</description>
        ///     </item>
        ///     <item>
        ///         <term>watermark-url</term>
        ///         <description>The URL or Base64 encoded Data URL for the watermark image</description>
        ///     </item>
        ///     <item>
        ///         <term>opacity</term>
        ///         <description>The opacity of the watermark (0 to 100)</description>
        ///     </item>
        ///     <item>
        ///         <term>bg-color</term>
        ///         <description>The image background color in hexadecimal notation (e.g. #0000ff)</description>
        ///     </item>
        ///     <item>
        ///         <term>height</term>
        ///         <description>If set resize the resulting image to this height (in px)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/image-watermark</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse ImageWatermark(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "image-watermark", paramDict, outputFilePath, 20);
        }

        /// <summary>The IP Blocklist API will detect potentially malicious or dangerous IP addresses</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>ip</term>
        ///         <description>An IPv4 or IPv6 address</description>
        ///     </item>
        ///     <item>
        ///         <term>vpn-lookup</term>
        ///         <description>Include public VPN provider IP addresses</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/ip-blocklist</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse IpBlocklist(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "ip-blocklist", paramDict, default, 10);
        }

        /// <summary>This API is a direct feed to our IP blocklist data</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>format</term>
        ///         <description>The data format</description>
        ///     </item>
        ///     <item>
        ///         <term>cidr</term>
        ///         <description>Output IPs using CIDR notation</description>
        ///     </item>
        ///     <item>
        ///         <term>ip6</term>
        ///         <description>Output the IPv6 version of the blocklist</description>
        ///     </item>
        ///     <item>
        ///         <term>category</term>
        ///         <description>The category of IP addresses to include in the download file</description>
        ///     </item>
        ///     <item>
        ///         <term>output-encoding</term>
        ///         <description>Set this option to 'gzip' to have the output file compressed using gzip</description>
        ///     </item>
        ///     <item>
        ///         <term>checksum</term>
        ///         <description>Do not download the file but just return the current files MurmurHash3 checksum</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/ip-blocklist-download</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse IpBlocklistDownload(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "ip-blocklist-download", paramDict, outputFilePath, 30);
        }

        /// <summary>Get location information about an IP address and do reverse DNS (PTR) lookups</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>ip</term>
        ///         <description>IPv4 or IPv6 address</description>
        ///     </item>
        ///     <item>
        ///         <term>reverse-lookup</term>
        ///         <description>Do a reverse DNS (PTR) lookup</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/ip-info</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse IpInfo(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "ip-info", paramDict, default, 10);
        }

        /// <summary>Execute a realtime network probe against an IPv4 or IPv6 address</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>ip</term>
        ///         <description>IPv4 or IPv6 address</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/ip-probe</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse IpProbe(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "ip-probe", paramDict, default, 120);
        }

        /// <summary>Make an automated call to any valid phone number and playback an audio message</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>number</term>
        ///         <description>The phone number to call</description>
        ///     </item>
        ///     <item>
        ///         <term>limit</term>
        ///         <description>Limit the total number of calls allowed to the supplied phone number</description>
        ///     </item>
        ///     <item>
        ///         <term>audio-url</term>
        ///         <description>A URL to a valid audio file</description>
        ///     </item>
        ///     <item>
        ///         <term>limit-ttl</term>
        ///         <description>Set the TTL in number of days that the 'limit' option will remember a phone number (the default is 1 day and the maximum is 365 days)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/phone-playback</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse PhonePlayback(Dictionary<string, string> paramDict)
        {
            return ExecRequest("POST", "phone-playback", paramDict, default, 30);
        }

        /// <summary>Parse, validate and get location information about a phone number</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>number</term>
        ///         <description>A phone number</description>
        ///     </item>
        ///     <item>
        ///         <term>country-code</term>
        ///         <description>ISO 2-letter country code</description>
        ///     </item>
        ///     <item>
        ///         <term>ip</term>
        ///         <description>Pass in a users IP address and we will assume numbers are based in the country of the IP address</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/phone-validate</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse PhoneValidate(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "phone-validate", paramDict, default, 10);
        }

        /// <summary>Make an automated call to any valid phone number and playback a unique security code</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>number</term>
        ///         <description>The phone number to send the verification code to</description>
        ///     </item>
        ///     <item>
        ///         <term>country-code</term>
        ///         <description>ISO 2-letter country code</description>
        ///     </item>
        ///     <item>
        ///         <term>security-code</term>
        ///         <description>Pass in your own security code</description>
        ///     </item>
        ///     <item>
        ///         <term>language-code</term>
        ///         <description>The language to playback the verification code in</description>
        ///     </item>
        ///     <item>
        ///         <term>code-length</term>
        ///         <description>The number of digits to use in the security code (between 4 and 12)</description>
        ///     </item>
        ///     <item>
        ///         <term>limit</term>
        ///         <description>Limit the total number of calls allowed to the supplied phone number</description>
        ///     </item>
        ///     <item>
        ///         <term>playback-delay</term>
        ///         <description>The delay in milliseconds between the playback of each security code</description>
        ///     </item>
        ///     <item>
        ///         <term>limit-ttl</term>
        ///         <description>Set the TTL in number of days that the 'limit' option will remember a phone number (the default is 1 day and the maximum is 365 days)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/phone-verify</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse PhoneVerify(Dictionary<string, string> paramDict)
        {
            return ExecRequest("POST", "phone-verify", paramDict, default, 30);
        }

        /// <summary>Generate a QR code as a PNG image</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>width</term>
        ///         <description>The width of the QR code (in px)</description>
        ///     </item>
        ///     <item>
        ///         <term>fg-color</term>
        ///         <description>The QR code foreground color</description>
        ///     </item>
        ///     <item>
        ///         <term>bg-color</term>
        ///         <description>The QR code background color</description>
        ///     </item>
        ///     <item>
        ///         <term>content</term>
        ///         <description>The content to encode into the QR code (e.g. a URL or a phone number)</description>
        ///     </item>
        ///     <item>
        ///         <term>height</term>
        ///         <description>The height of the QR code (in px)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <param name="outputFilePath">Location on disk to store the response.</param>
        /// <link>https://www.neutrinoapi.com/api/qr-code</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse QrCode(Dictionary<string, string> paramDict, string outputFilePath)
        {
            return ExecRequest("POST", "qr-code", paramDict, outputFilePath, 20);
        }

        /// <summary>Send a unique security code to any mobile device via SMS</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>number</term>
        ///         <description>The phone number to send a verification code to</description>
        ///     </item>
        ///     <item>
        ///         <term>country-code</term>
        ///         <description>ISO 2-letter country code</description>
        ///     </item>
        ///     <item>
        ///         <term>security-code</term>
        ///         <description>Pass in your own security code</description>
        ///     </item>
        ///     <item>
        ///         <term>language-code</term>
        ///         <description>The language to send the verification code in</description>
        ///     </item>
        ///     <item>
        ///         <term>code-length</term>
        ///         <description>The number of digits to use in the security code (must be between 4 and 12)</description>
        ///     </item>
        ///     <item>
        ///         <term>limit</term>
        ///         <description>Limit the total number of SMS allowed to the supplied phone number</description>
        ///     </item>
        ///     <item>
        ///         <term>brand-name</term>
        ///         <description>Set a custom brand or product name in the verification message</description>
        ///     </item>
        ///     <item>
        ///         <term>limit-ttl</term>
        ///         <description>Set the TTL in number of days that the 'limit' option will remember a phone number (the default is 1 day and the maximum is 365 days)</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/sms-verify</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse SmsVerify(Dictionary<string, string> paramDict)
        {
            return ExecRequest("POST", "sms-verify", paramDict, default, 30);
        }

        /// <summary>Parse, validate and get detailed user-agent information from a user agent string or from client hints</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>ua</term>
        ///         <description>The user-agent string to lookup</description>
        ///     </item>
        ///     <item>
        ///         <term>ua-version</term>
        ///         <description>For client hints this corresponds to the 'UA-Full-Version' header or 'uaFullVersion' from NavigatorUAData</description>
        ///     </item>
        ///     <item>
        ///         <term>ua-platform</term>
        ///         <description>For client hints this corresponds to the 'UA-Platform' header or 'platform' from NavigatorUAData</description>
        ///     </item>
        ///     <item>
        ///         <term>ua-platform-version</term>
        ///         <description>For client hints this corresponds to the 'UA-Platform-Version' header or 'platformVersion' from NavigatorUAData</description>
        ///     </item>
        ///     <item>
        ///         <term>ua-mobile</term>
        ///         <description>For client hints this corresponds to the 'UA-Mobile' header or 'mobile' from NavigatorUAData</description>
        ///     </item>
        ///     <item>
        ///         <term>device-model</term>
        ///         <description>For client hints this corresponds to the 'UA-Model' header or 'model' from NavigatorUAData</description>
        ///     </item>
        ///     <item>
        ///         <term>device-brand</term>
        ///         <description>This parameter is only used in combination with 'device-model' when doing direct device lookups without any user-agent data</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/ua-lookup</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse UaLookup(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "ua-lookup", paramDict, default, 10);
        }

        /// <summary>Parse, analyze and retrieve content from the supplied URL</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>url</term>
        ///         <description>The URL to probe</description>
        ///     </item>
        ///     <item>
        ///         <term>fetch-content</term>
        ///         <description>If this URL responds with html</description>
        ///     </item>
        ///     <item>
        ///         <term>ignore-certificate-errors</term>
        ///         <description>Ignore any TLS/SSL certificate errors and load the URL anyway</description>
        ///     </item>
        ///     <item>
        ///         <term>timeout</term>
        ///         <description>Timeout in seconds</description>
        ///     </item>
        ///     <item>
        ///         <term>retry</term>
        ///         <description>If the request fails for any reason try again this many times</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/url-info</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse UrlInfo(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "url-info", paramDict, default, 30);
        }

        /// <summary>Check if a security code sent via SMS Verify or Phone Verify is valid</summary>
        /// <list type="bullet">
        ///     <listheader>
        ///         <term>Param</term>
        ///         <description>The parameters this Api accepts are:</description>
        ///     </listheader>
        ///     <item>
        ///         <term>security-code</term>
        ///         <description>The security code to verify</description>
        ///     </item>
        ///     <item>
        ///         <term>limit-by</term>
        ///         <description>If set then enable additional brute-force protection by limiting the number of attempts by the supplied value</description>
        ///     </item>
        /// </list>
        /// <param name="paramDict">The Api request parameters.</param>
        /// <link>https://www.neutrinoapi.com/api/verify-security-code</link>
        /// <returns>Returns an ApiResponse object on success or failure</returns>
        public ApiResponse VerifySecurityCode(Dictionary<string, string> paramDict)
        {
            return ExecRequest("GET", "verify-security-code", paramDict, default, 30);
        }

        /// <summary>
        /// Make a request to the Neutrino API
        /// </summary>
        /// <param name="httpMethod"></param>
        /// <param name="endpoint"></param>
        /// <param name="paramDict"></param>
        /// <param name="outputFilePath"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns>ApiResponse object on success or failure</returns>
        public ApiResponse ExecRequest(string httpMethod, string endpoint, Dictionary<string, string> paramDict,
            string outputFilePath, int timeoutInSeconds)
        {
            try
            {
                using (var encodedParams = new FormUrlEncodedContent(paramDict))
                {
                    string url;
                    if (httpMethod.Equals("GET"))
                    {
                        var paramStr = encodedParams.ReadAsStringAsync().Result;
                        url = $"{_baseUrl}{endpoint}?{paramStr}";
                    }
                    else
                    {
                        url = $"{_baseUrl}{endpoint}";
                    }
                    using (var request = new HttpRequestMessage())
                    {
                        request.Method = httpMethod.Equals("POST") ? HttpMethod.Post : HttpMethod.Get;
                        request.RequestUri = new Uri(url);
                        if (httpMethod.Equals("POST")) request.Content = encodedParams;
                        using (var cancellationToken = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutInSeconds)))
                        using (var responseTask = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken.Token))
                        {
                            var response = responseTask.Result;
                            var content = response.Content;
                            var statusCode = (int)response.StatusCode;
                            var contentType = content.Headers.ContentType.ToString();
                            if (response.IsSuccessStatusCode)
                            {
                                ApiResponse apiResponse;
                                if (contentType.Contains("application/json"))
                                {
                                    var jsonStr = responseTask.Result.Content.ReadAsStringAsync().Result;
                                    apiResponse = ApiResponse.OfData(statusCode, contentType,
                                        JsonSerializer.Deserialize<JsonElement>(jsonStr));
                                }
                                else if (outputFilePath != null)
                                {
                                    using (Stream streamToWriteTo =
                                           File.Open(outputFilePath, FileMode.Create))
                                    using (var streamToReadFrom = responseTask.Result.Content.ReadAsStreamAsync())
                                    {
                                        streamToReadFrom.Result.CopyTo(streamToWriteTo);
                                        streamToWriteTo.Close();
                                        apiResponse =
                                            ApiResponse.OfOutputFilePath(statusCode, contentType, outputFilePath);
                                    }
                                }
                                else
                                {
                                    apiResponse = ApiResponse.OfHttpStatus(statusCode, contentType,
                                        ApiErrorCode.ApiGatewayError,
                                        responseTask.Result.Content.ReadAsStringAsync().Result);
                                }
                                return apiResponse;
                            }
                            if (!response.IsSuccessStatusCode)
                            {
                                ApiResponse apiResponse;
                                var rawResponse = responseTask.Result.Content.ReadAsStringAsync().Result;
                                if (contentType.Contains("application/json"))
                                {
                                    var json = JsonSerializer.Deserialize<JsonElement>(rawResponse);
                                    json.TryGetProperty("api-error", out var errorCodeElement);
                                    json.TryGetProperty("api-error-msg", out var errorMessageElement);
                                    apiResponse = ApiResponse.OfHttpStatus(statusCode, contentType,
                                        int.Parse(errorCodeElement.ToString() ?? "0"),
                                        errorMessageElement.ToString() ?? "");
                                }
                                else
                                {
                                    apiResponse = ApiResponse.OfHttpStatus(statusCode, contentType,
                                        ApiErrorCode.ApiGatewayError, rawResponse);
                                }
                                return apiResponse;
                            }
                        }
                    }
                }
            }
            catch (IOException e)
            {
                return ApiResponse.OfErrorCause(ApiErrorCode.NetworkIoError, e);
            }
            catch (FormatException e)
            {
                return ApiResponse.OfErrorCause(ApiErrorCode.UrlParsingError, e);
            }
            catch (HttpRequestException e)
            {
                return ApiResponse.OfErrorCause(ApiErrorCode.NetworkIoError, e);
            }
            catch (ArgumentException e)
            {
                return ApiResponse.OfErrorCause(ApiErrorCode.BadUrl, e);
            }
            catch (AggregateException e)
            {
                foreach (var ie in e.Flatten().InnerExceptions)
                {
                    if (ie is TaskCanceledException)
                    {
                        return ApiResponse.OfErrorCause(ApiErrorCode.ReadTimeout, ie);
                    }
                    if (ie is HttpRequestException)
                    {
                        return ApiResponse.OfErrorCause(ApiErrorCode.TlsProtocolError, ie);
                    }
                }
            }
            return default;
        }
    }
}
