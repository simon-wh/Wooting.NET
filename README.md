# Wooting.NET

[![Build status](https://ci.appveyor.com/api/projects/status/gjnc6snr88246xh5?svg=true)](https://ci.appveyor.com/project/simon-wh/wooting-net)

Simple wrapper library for [wooting-rgb-control](https://github.com/PastaJ36/wooting-rgb-control) and [wooting-analog-reader](https://github.com/PastaJ36/wooting-analog-reader)

## Basic Usage

Have a look at the test project for some more examples.

The `WootingKey.Keys` enum is available to make it simpler to apply operations to certain keys

### RGB

Requires  `wooting-rgb-control.dll` to be included alongside the wrapper

```c#
RGBControl.IsConnected(); //Check if a keyboard is connected
RGBControl.SetKey(WootingKey.Keys.Esc, 255, 0, 0, true) //Set the Escape key to red
```

### Analog

Requires `wooting-analog-reader.dll` to be included alongside the wrapper

```c#
byte data = AnalogReader.ReadAnalog(WootingKey.Keys.Esc) //Get the analog data from the Esc key
```

