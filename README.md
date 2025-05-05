# NeutrinoAPI C# Native SDK

Neutrino API C# client using the native HTTP library

The official API client and SDK built by [NeutrinoAPI](https://www.neutrinoapi.com/)

| Feature          |        |
|------------------|--------|
| Platform Version | >= 3.1 |
| HTTP Library     | Native |
| JSON Library     | Native |
| HTTP/2           | No     |
| HTTP/3           | No     |
| CodeGen Version  | 4.7.1  |

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

Set the __'your-user-id'__ and __'your-api-key'__ values in the example to retrieve real API responses

## For Support 
[Contact Us](https://www.neutrinoapi.com/contact-us/)

## To Build on Linux/CLI
```sh
$ dotnet restore
$ dotnet exec <path-to-dotnet-sdk>/MSBuild.dll
```

## Running Examples on Linux/CLI

```sh
$  dotnet NeutrinoApi.Examples/bin/Debug/net6.0/NeutrinoApi.Examples.dll IpInfo
```
