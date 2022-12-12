using System;
using System.Collections.Generic;
using System.Text.Json;

namespace NeutrinoApi.Examples
{
    public static class UaLookup
    {
        /// <summary>Main</summary>
        public static void ExecRequest()
        {
            var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
            var parameters = new Dictionary<string, string>
            {
                
                // The user-agent string to lookup. For client hints use the 'UA' header or the JSON data directly
                // from 'navigator.userAgentData.brands' or 'navigator.userAgentData.getHighEntropyValues()'
                { "ua", "Mozilla/5.0 (Linux; Android 11; SM-G9980U1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.101 Mobile Safari/537.36" },
                
                // For client hints this corresponds to the 'UA-Full-Version' header or 'uaFullVersion' from
                // NavigatorUAData
                { "ua-version", "" },
                
                // For client hints this corresponds to the 'UA-Platform' header or 'platform' from NavigatorUAData
                { "ua-platform", "" },
                
                // For client hints this corresponds to the 'UA-Platform-Version' header or 'platformVersion' from
                // NavigatorUAData
                { "ua-platform-version", "" },
                
                // For client hints this corresponds to the 'UA-Mobile' header or 'mobile' from NavigatorUAData
                { "ua-mobile", "" },
                
                // For client hints this corresponds to the 'UA-Model' header or 'model' from NavigatorUAData. You
                // can also use this parameter to lookup a device directly by its model name, model code or hardware
                // code, on android you can get the model name from:
                // https://developer.android.com/reference/android/os/Build.html#MODEL
                { "device-model", "" },
                
                // This parameter is only used in combination with 'device-model' when doing direct device lookups
                // without any user-agent data. Set this to the brand or manufacturer name, this is required for
                // accurate device detection with ambiguous model names. On android you can get the device brand
                // from: https://developer.android.com/reference/android/os/Build#MANUFACTURER
                { "device-brand", "" }
            };

            var response = neutrinoApiClient.UaLookup(parameters);
            if (response.IsOK())
            {
                var data = response.Data;

                // API request successful, print out the response data
                Console.WriteLine("API Response OK:");
                
                // If the client is a web browser which underlying browser engine does it use
                Console.WriteLine("browser-engine: {0}",
                    data.TryGetProperty("browser-engine", out var browserEngine) ? browserEngine.ToString() : "NULL");
                
                // If the client is a web browser which year was this browser version released
                Console.WriteLine("browser-release: {0}",
                    data.TryGetProperty("browser-release", out var browserRelease) ? browserRelease.ToString() : "NULL");
                
                // The device brand / manufacturer
                Console.WriteLine("device-brand: {0}",
                    data.TryGetProperty("device-brand", out var deviceBrand) ? deviceBrand.ToString() : "NULL");
                
                // The device display height in CSS 'px'
                Console.WriteLine("device-height-px: {0}",
                    data.TryGetProperty("device-height-px", out var deviceHeightPx) ? deviceHeightPx.ToString() : "NULL");
                
                // The device model
                Console.WriteLine("device-model: {0}",
                    data.TryGetProperty("device-model", out var deviceModel) ? deviceModel.ToString() : "NULL");
                
                // The device model code
                Console.WriteLine("device-model-code: {0}",
                    data.TryGetProperty("device-model-code", out var deviceModelCode) ? deviceModelCode.ToString() : "NULL");
                
                // The device display pixel ratio (the ratio of the resolution in physical pixels to the resolution
                // in CSS pixels)
                Console.WriteLine("device-pixel-ratio: {0}",
                    data.TryGetProperty("device-pixel-ratio", out var devicePixelRatio) ? devicePixelRatio.ToString() : "NULL");
                
                // The device display PPI (pixels per inch)
                Console.WriteLine("device-ppi: {0}",
                    data.TryGetProperty("device-ppi", out var devicePpi) ? devicePpi.ToString() : "NULL");
                
                // The average device price on release in USD
                Console.WriteLine("device-price: {0}",
                    data.TryGetProperty("device-price", out var devicePrice) ? devicePrice.ToString() : "NULL");
                
                // The year when this device model was released
                Console.WriteLine("device-release: {0}",
                    data.TryGetProperty("device-release", out var deviceRelease) ? deviceRelease.ToString() : "NULL");
                
                // The device display resolution in physical pixels (e.g. 720x1280)
                Console.WriteLine("device-resolution: {0}",
                    data.TryGetProperty("device-resolution", out var deviceResolution) ? deviceResolution.ToString() : "NULL");
                
                // The device display width in CSS 'px'
                Console.WriteLine("device-width-px: {0}",
                    data.TryGetProperty("device-width-px", out var deviceWidthPx) ? deviceWidthPx.ToString() : "NULL");
                
                // Is this a mobile device (e.g. a phone or tablet)
                Console.WriteLine("is-mobile: {0}",
                    data.TryGetProperty("is-mobile", out var isMobile) ? isMobile.ToString() : "NULL");
                
                // Is this a WebView / embedded software client
                Console.WriteLine("is-webview: {0}",
                    data.TryGetProperty("is-webview", out var isWebview) ? isWebview.ToString() : "NULL");
                
                // The client software name
                Console.WriteLine("name: {0}",
                    data.TryGetProperty("name", out var name) ? name.ToString() : "NULL");
                
                // The full operating system name
                Console.WriteLine("os: {0}",
                    data.TryGetProperty("os", out var os) ? os.ToString() : "NULL");
                
                // The operating system family. The major OS families are: Android, Windows, macOS, iOS, Linux
                Console.WriteLine("os-family: {0}",
                    data.TryGetProperty("os-family", out var osFamily) ? osFamily.ToString() : "NULL");
                
                // The operating system full version
                Console.WriteLine("os-version: {0}",
                    data.TryGetProperty("os-version", out var osVersion) ? osVersion.ToString() : "NULL");
                
                // The operating system major version
                Console.WriteLine("os-version-major: {0}",
                    data.TryGetProperty("os-version-major", out var osVersionMajor) ? osVersionMajor.ToString() : "NULL");
                
                // The user agent type, possible values are:
                // • desktop
                // • phone
                // • tablet
                // • wearable
                // • tv
                // • console
                // • email
                // • library
                // • robot
                // • unknown
                Console.WriteLine("type: {0}",
                    data.TryGetProperty("type", out var type) ? type.ToString() : "NULL");
                
                // The user agent string
                Console.WriteLine("ua: {0}",
                    data.TryGetProperty("ua", out var ua) ? ua.ToString() : "NULL");
                
                // The client software full version
                Console.WriteLine("version: {0}",
                    data.TryGetProperty("version", out var version) ? version.ToString() : "NULL");
                
                // The client software major version
                Console.WriteLine("version-major: {0}",
                    data.TryGetProperty("version-major", out var versionMajor) ? versionMajor.ToString() : "NULL");
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