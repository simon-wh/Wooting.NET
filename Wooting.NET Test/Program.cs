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
            TestAnalog();
            TestRGB();
        }
        
        static void TestAnalog()
        {
            Console.WriteLine("Wooting Analog reader testing!");
            Console.WriteLine($"wooting_kbd_connected: {AnalogReader.IsConnected()}");

            Console.WriteLine("Set disconnected cb");
            AnalogReader.SetDisconnectedCallback((DisconnectedCallback)dc_cb);
            Stopwatch watch = new Stopwatch();
            Console.WriteLine("Reading Analog data from the Esc key for 10 seconds. Press any key to start...");
            Console.ReadKey();
            watch.Start();
            int lastValue = -1;
            do
            {
                int value = AnalogReader.ReadAnalog(WootingKey.Keys.Esc);
                if (lastValue != value)
                {
                    lastValue = value;
                    Console.WriteLine(value);
                }
            } while (watch.Elapsed.Seconds < 10);
            watch.Stop();

            Console.WriteLine("Going to read the buffer, please press down some keys for 3 seconds");
            Console.ReadKey();
            Thread.Sleep(3000);
            List<AnalogReader.AnalogRaw> buffer = AnalogReader.ReadFullBuffer(16);
            Console.WriteLine($"{buffer.Count} items read");
            for (int i = 0; i < buffer.Count; i++)
            {
                AnalogReader.AnalogRaw raw = buffer[i];
                Console.WriteLine($"Scan code: {raw.scan_code}, Value: {raw.analog_value}");
            }
            Console.ReadKey();
        }


        static void dc_cb()
        {
            Console.WriteLine("it disconnected");
        }

        static void TestRGB()
        {
            Console.WriteLine("Wooting Rgb Control testing!");
            Console.WriteLine($"wooting_rgb_kbd_connected: {RGBControl.IsConnected()}");
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
            for (byte i = 0; i < RGBControl.MaxRGBCols; i++)
            {
                for (byte j = 0; j < RGBControl.MaxRGBRows; j++)
                {
                    keys[j, i] = new KeyColour(0, 0, 0);
                }
            }
            RGBControl.SetFull(keys);
            RGBControl.UpdateKeyboard();
            Thread.Sleep(1000);

            for (byte i = 0; i < RGBControl.MaxRGBCols; i++)
            {
                for (byte j = 0; j < RGBControl.MaxRGBRows; j++)
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
