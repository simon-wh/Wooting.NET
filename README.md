# Wooting.NET

[![Build status](https://ci.appveyor.com/api/projects/status/gjnc6snr88246xh5?svg=true)](https://ci.appveyor.com/project/simon-wh/wooting-net)

Simple wrapper library for [wooting-rgb-sdk](https://github.com/WootingKb/wooting-rgb-sdk)

## Installation

You can install Wooting.NET from the [nuget package](https://www.nuget.org/packages/Wooting.NET/1.0.0). Once done, you need to obtain the appropriate rgb/analog sdk dlls (get whatever you require). You need to add those to your project and make sure they are copied to the output directory. (You'll need to make sure the dll's are exactly `wooting-rgb-sdk.dll` and not `wooting-rgb-sdk64.dll` or else it won't work)

## Basic Usage

Have a look at the test project for some more examples.

The `WootingKey.Keys` enum is available to make it simpler to apply operations to certain keys

### RGB

Requires  `wooting-rgb-sdk.dll` to be included alongside the wrapper

```c#
RGBControl.IsConnected(); //Check if a keyboard is connected
RGBControl.SetKey(WootingKey.Keys.Esc, 255, 0, 0, true); //Set the Escape key to red
```

### Analog

The original Analog SDK has been deprecated and replaced with a [new one found here](http://github.com/WootingKb/wooting-analog-sdk). You can find the C# Library to use it on [Nuget](https://www.nuget.org/packages/WootingAnalogSDK.NET) and [GitHub](https://github.com/WootingKb/wooting-analog-wrappers). There's also a guide for getting started with the Analog SDK in C# [found here](https://dev.wooting.io/wooting-analog-sdk-guide/c-guide/)
