using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using Wooting;

namespace Wooting_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TestRGB();
        }
        
        static void dc_cb()
        {
            Console.WriteLine("it disconnected");
        }

        static void TestRGB()
        {
            Console.WriteLine("Wooting Rgb Control testing!");
            RGBDeviceInfo device = RGBControl.GetDeviceInfo();
            Console.WriteLine($"Initial Device Info has got: Connected: {device.Connected}, Model: {device.Model}, Type: {device.DeviceType}, Max Rows: {device.MaxRows}, Max Cols: {device.MaxColumns}, Max Keycode: {device.KeycodeLimit}");

            bool connected = RGBControl.IsConnected();
            Console.WriteLine($"wooting_rgb_kbd_connected: {connected}");
            

            if (!connected) return;

            device = RGBControl.GetDeviceInfo();
            Console.WriteLine($"Device Info has got: Connected: {device.Connected}, Model: {device.Model}, Type: {device.DeviceType}, Max Rows: {device.MaxRows}, Max Cols: {device.MaxColumns}, Max Keycode: {device.KeycodeLimit}");

            //Console.WriteLine("Turning on auto-update!");
            //RGBControl.wooting_rgb_array_auto_update(true);
            Console.WriteLine("Set disconnected cb");
            RGBControl.SetDisconnectedCallback((DisconnectedCallback)dc_cb);
            Console.WriteLine("Setting some keys directly!");
            Console.WriteLine($"Setting ESC green: {RGBControl.SetKey(WootingKey.Keys.Esc, 0, 255, 0)}");
            RGBControl.UpdateKeyboard();
            Console.WriteLine("Setting Enter Red");
            RGBControl.SetKey(WootingKey.Keys.Enter, 255, 0, 0, true);
            Console.WriteLine("Setting G blue");
            RGBControl.SetKey(WootingKey.Keys.G, 0, 0, 255, true);
            Console.WriteLine("Setting Mode/Scroll Lock green");
            RGBControl.SetKey(WootingKey.Keys.Mode_ScrollLock, 0, 255, 0, true);
            Thread.Sleep(5000);

            Console.WriteLine("Setting the keyboard blank!");
            KeyColour[,] keys = new KeyColour[RGBControl.MaxRGBRows, RGBControl.MaxRGBCols];
            for (byte i = 0; i < device.MaxColumns; i++)
            {
                for (byte j = 0; j < device.MaxRows; j++)
                {
                    keys[j, i] = new KeyColour(0, 0, 0);
                }
            }
            RGBControl.SetFull(keys);
            RGBControl.UpdateKeyboard();
            Thread.Sleep(1000);

            for (byte i = 0; i < device.MaxColumns; i++)
            {
                for (byte j = 0; j < device.MaxRows; j++)
                {
                    Console.WriteLine($"Setting the key, ROW:{j}, COL:{i}");
                    keys[j, i] = new KeyColour(255, 255, 0);
                    RGBControl.SetFull(keys);
                    RGBControl.UpdateKeyboard();
                    Thread.Sleep(100);
                }
            }
            Console.WriteLine("press any key to reset all colors to default");
            Console.ReadKey();
            RGBControl.Reset();
            Console.ReadKey();
        }
    }
}
