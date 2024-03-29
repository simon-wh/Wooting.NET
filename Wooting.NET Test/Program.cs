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

        static void TestRGB()
        {
            if (!RGBControl.IsConnected())
                return;

            var count = RGBControl.GetDeviceCount();
            var infos = new RGBDeviceInfo[count];
            for (byte i = 0; i < count; i++)
            {
                RGBControl.SetControlDevice(i);
                var device = RGBControl.GetDeviceInfo();
                Console.WriteLine($"Found device: Connected: {device.Connected}, Model: {device.Model}, Type: {device.DeviceType}, Max Rows: {device.MaxRows}, Max Cols: {device.MaxColumns}, Max Keycode: {device.KeycodeLimit}");
                infos[i] = device;
            }

            for (byte idx = 0; idx < count; idx++)
            {
                Console.WriteLine($"Setting {infos[idx].Model} to red");

                RGBControl.SetControlDevice(idx);
                var device = infos[idx];
                KeyColour[,] keys = new KeyColour[RGBControl.MaxRGBRows, RGBControl.MaxRGBCols];
                for (byte i = 0; i < device.MaxColumns; i++)
                {
                    for (byte j = 0; j < device.MaxRows; j++)
                    {
                        keys[j, i] = new KeyColour(255, 0, 0);
                    }
                }
                RGBControl.SetFull(keys);
                RGBControl.UpdateKeyboard();
            }

            Console.WriteLine("Set all keyboards to red. Press any key to continue.");
            Console.ReadKey();

            for (byte idx = 0; idx < count; idx++)
            {
                RGBControl.SetControlDevice(idx);

                var device = infos[idx];


                if (device.DeviceType != DeviceType.Keypad3Key)
                {
                    Console.WriteLine($"Setting {device.Model}'s WASD keys to white");

                    RGBControl.SetKey(WootingKey.Keys.W, 255, 255, 255);
                    RGBControl.SetKey(WootingKey.Keys.A, 255, 255, 255);
                    RGBControl.SetKey(WootingKey.Keys.S, 255, 255, 255);
                    RGBControl.SetKey(WootingKey.Keys.D, 255, 255, 255);
                }
                else
                {
                    Console.WriteLine($"Setting {device.Model}'s 3 primary keys to white");


                    RGBControl.SetKey(2, 1, 255, 255, 255);
                    RGBControl.SetKey(2, 3, 255, 255, 255);
                    RGBControl.SetKey(2, 5, 255, 255, 255);
                }

                RGBControl.UpdateKeyboard();
            }

            Console.WriteLine("Set groups of keys to white on all devices. Press any key to continue.");
            Console.ReadKey();

            for (byte idx = 0; idx < count; idx++)
            {
                var device = infos[idx];
                RGBControl.SetControlDevice(idx);

                Console.WriteLine($"Setting {device.Model}'s to yellow one key at a time.");

                KeyColour[,] keys = new KeyColour[RGBControl.MaxRGBRows, RGBControl.MaxRGBCols];
                for (byte i = 0; i < device.MaxColumns; i++)
                {
                    for (byte j = 0; j < device.MaxRows; j++)
                    {
                        Console.WriteLine($"Setting the key, ROW:{j}, COL:{i}");
                        keys[j, i] = new KeyColour(255, 255, 0);
                        RGBControl.SetFull(keys);
                        RGBControl.UpdateKeyboard();
                        Thread.Sleep(33);
                    }
                }
            }

            Console.WriteLine("Press any key to reset all colors to default...");
            Console.ReadKey();

            // for (byte idx = 0; idx < count; idx++)
            // {
            //     RGBControl.SetControlDevice(idx);
            //     RGBControl.ResetRGB();
            // }
            RGBControl.Close();
        }
    }
}
