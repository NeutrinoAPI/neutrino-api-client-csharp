# NeutrinoAPI C# Native SDK

C# client using the native HTTP client

The official API client and SDK built by [NeutrinoAPI](https://www.neutrinoapi.com/)

| Feature          |        |
|------------------|--------|
| Platform Version | >= 3.1 |
| HTTP Library     | Native |
| JSON Library     | Native |
| HTTP/2           | No     |
| HTTP/3           | No     |
| CodeGen Version  | 4.6.12 |

## Getting started

First you will need a user ID and API key pair: [SignUp](https://www.neutrinoapi.com/signup/)

## To Initialize 
```csharp
var neutrinoApiClient = new NeutrinoApiClient("<your-user-id>", "<your-api-key>");
```

## Running Examples

* Install Visual Studio
* Open the `neutrino-api-client-native.sln` solution file
* Install .Net Core 2.1 as instructed by IDE, if necessary
* Click Run (F5), the default example is IpInfo

You can find examples of all APIs in _NeutrinoApi.Examples_

## For Support 
[Contact Us](https://www.neutrinoapi.com/contact-us/)

## To Build on Linux/CLI
```sh
$ dotnet restore
$ dotnet exec <path-to-dotnet-sdk>/MSBuild.dll
```

## Running Examples on Linux/CLI

```sh
$  dotnet NeutrinoApi.Examples/bin/Debug/netcoreapp2.1/NeutrinoApi.Examples.dll IpInfo
```
