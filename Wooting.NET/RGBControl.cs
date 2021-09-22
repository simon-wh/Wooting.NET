using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace Wooting
{
    public enum DeviceType {
        /// 10 Keyless Keyboard. E.g. Wooting One
	    KeyboardTKL = 1,
	
        /// Full Size keyboard. E.g. Wooting Two
        Keyboard = 2
    }

    public enum LayoutType {
        Unknown = -1,

	    ANSI = 0,
	
        ISO = 1
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RGBDeviceInfo {
        public bool Connected { get; private set; }

        public string Model { get; private set; }

        public byte MaxRows { get; private set; }

        public byte MaxColumns { get; private set; }

        public byte KeycodeLimit { get; private set; }

        public DeviceType DeviceType { get; private set; }

        private bool _useV2Inteface {get; set;}

        public LayoutType LayoutType { get; private set; }

    }

    public static class RGBControl
    {
        private const string sdkDLL = "wooting-rgb-sdk";

        public const int MaxRGBRows = 6;
        public const int MaxRGBCols = 21;

        /// <summary>
        /// Check if keyboard connected.
        ///
        /// This function offers a quick check if the keyboard is connected.This doesn't open the keyboard or influences reading.
        /// It is recommended to poll this function at the start of your application and after a disconnect.
        /// </summary>
        /// <returns>This function returns true (1) if keyboard is found.</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_kbd_connected", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsConnected();

        /// <summary>
        /// Set callback for when a keyboard disconnects.
        /// The callback will be called when a Wooting keyboard disconnects.This will trigger after a failed color change.
        /// </summary>
        /// <param name="cb">The function pointer of the callback</param>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_set_disconnected_cb", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetDisconnectedCallback(DisconnectedCallback cb);

        /// <summary>
        /// 
        /// This function will restore all the colours to the colours that were originally on the keyboard.
        /// </summary>
        /// <returns>None</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_reset_rgb", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ResetRGB();

        /// <summary>
        /// Reset all colors on keyboard to the original colors. 
        /// This function will restore all the colours to the colours that were originally on the keyboard and closes the keyboard handle.This function
        /// should be called when you close the application.
        /// </summary>
        /// <returns>None</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_close", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool Close();

        /// <summary>
        /// Directly reset 1 key on the keyboard to the original color.
        /// This function will directly reset the color of 1 key on the keyboard.This will not influence the keyboard color array.
        /// Use this function for simple applifications, like a notification.Use the array functions if you want to change the entire keyboard.
        /// </summary>
        /// <param name="row">The horizontal index of the key</param>
        /// <param name="column">The vertical index of the key</param>
        /// <returns>This functions return true (1) if the colour is reset.</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_direct_reset_key", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool ResetKey(byte row, byte column);

        /// <summary>
        /// Directly reset 1 key on the keyboard to the original color.
        /// This function will directly reset the color of 1 key on the keyboard.This will not influence the keyboard color array.
        /// Use this function for simple applifications, like a notification.Use the array functions if you want to change the entire keyboard.
        /// </summary>
        /// <param name="key">The key to be reset</param>
        /// <returns>This functions return true (1) if the colour is reset.</returns>
        public static bool ResetKey(WootingKey.Keys key)
        {
            if (WootingKey.KeyMap.TryGetValue(key, out (byte row, byte column) index))
                return ResetKey(index.row, index.column);

            return false;
        }

        /// <summary>
        /// Send the colors from the color array to the keyboard.
        /// This function will send the changes made with the wooting_rgb_array_**_** functions to the keyboard.
        /// </summary>
        /// <returns>This functions return true (1) if the colours are updated.</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_array_update_keyboard", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool UpdateKeyboard();

        /// <summary>
        /// Change the auto update flag for the wooting_rgb_array_**_** functions.
        /// This function can be used to set a auto update trigger after every change with a wooting_rgb_array_** _** function.
        /// Standard is set to false.
        /// </summary>
        /// <param name="auto_update">Change the auto update flag</param>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_array_auto_update", CallingConvention = CallingConvention.Cdecl)]
        public static extern void SetAutoUpdate([MarshalAs(UnmanagedType.I1)] bool auto_update);

        /// <summary>
        /// Directly set and update 1 key on the keyboard.
        /// This function will directly change the color of 1 key on the keyboard.This will not influence the keyboard color array.
        /// Use this function for simple applifications, like a notification.Use the array functions if you want to change the entire keyboard.
        /// </summary>
        /// <param name="row">The horizontal index of the key</param>
        /// <param name="column">The vertical index of the key</param>
        /// <param name="red">A 0-255 value of the red color</param>
        /// <param name="green">A 0-255 value of the green color</param>
        /// <param name="blue">A 0-255 value of the blue color</param>
        /// <returns>This functions return true (1) if the colour is set.</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_direct_set_key", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool _DirectSetKey(byte row, byte column, byte red, byte green, byte blue);

        /// <summary>
        /// Set a single color in the colour array.
        /// This function will set a single color in the colour array.This will not directly update the keyboard(unless the flag is set), so it can be called frequently.For example in a loop that updates the entire keyboard, if you don't want to send a C array from a different programming language.
        /// </summary>
        /// <param name="row">The horizontal index of the key</param>
        /// <param name="column">The vertical index of the key</param>
        /// <param name="red">A 0-255 value of the red color</param>
        /// <param name="green">A 0-255 value of the green color</param>
        /// <param name="blue">A 0-255 value of the blue color</param>
        /// <returns>This functions return true (1) if the colours are changed (optional: updated).</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_array_set_single", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool _SetKey(byte row, byte column, byte red, byte green, byte blue);

        /// <summary>
        /// Set the colour of a key
        /// </summary>
        /// <param name="row">The horizontal index of the key</param>
        /// <param name="column">The vertical index of the key</param>
        /// <param name="red">A 0-255 value of the red color</param>
        /// <param name="green">A 0-255 value of the green color</param>
        /// <param name="blue">A 0-255 value of the blue color</param>
        /// <param name="direct">Determines if this is set directly to the keyboard or if it is stored in the keyboard color array</param>
        /// <returns>This functions return true (1) if the colours are changed (optional: updated)</returns>
        public static bool SetKey(byte row, byte column, byte red, byte green, byte blue, bool direct = false)
        {
            if (direct)
                return _DirectSetKey(row, column, red, green, blue);
            else
                return _SetKey(row, column, red, green, blue);
        }

        /// <summary>
        /// Set the colour of a key
        /// </summary>
        /// <param name="key">The key to be coloured</param>
        /// <param name="red">A 0-255 value of the red color</param>
        /// <param name="green">A 0-255 value of the green color</param>
        /// <param name="blue">A 0-255 value of the blue color</param>
        /// <param name="direct">Determines if this is set directly to the keyboard or if it is stored in the keyboard color array</param>
        /// <returns>This functions return true (1) if the colours are changed (optional: updated)</returns>
        public static bool SetKey(WootingKey.Keys key, byte red, byte green, byte blue, bool direct = false)
        {
            if (WootingKey.KeyMap.TryGetValue(key, out (byte row, byte column) index))
                return SetKey(index.row, index.column, red, green, blue, direct);

            return false;
        }

        /// <summary>
        /// Set the colour of a key
        /// </summary>
        /// <param name="key">The key to be coloured</param>
        /// <param name="colour">The colour for the key</param>
        /// <param name="direct">Determines if this is set directly to the keyboard or if it is stored in the keyboard color array</param>
        /// <returns>This functions return true (1) if the colours are changed (optional: updated)</returns>
        public static bool SetKey(WootingKey.Keys key, KeyColour colour, bool direct = false)
        {
            if (WootingKey.KeyMap.TryGetValue(key, out (byte row, byte column) index))
                return SetKey(index.row, index.column, colour.r, colour.g, colour.b, direct);

            return false;
        }

        /// <summary>
        /// Set a full colour array.
        /// This function will set a complete color array.This will not directly update the keyboard (unless the flag is set). 
        /// Use our online tool to generate a color array:
        /// If you use a non-C language it is recommended to use the wooting_rgb_array_set_single function to change the colors to avoid compatibility issues.
        /// </summary>
        /// <param name="colors_buffer">Pointer to a buffer of a full color array</param>
        /// <returns>This functions return true (1) if the colours are changed (optional: updated).</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_rgb_array_set_full", CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool SetFull([MarshalAs(UnmanagedType.LPArray, SizeConst = MaxRGBRows * MaxRGBCols)] KeyColour[,] colors_buffer);


        [DllImport(sdkDLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr wooting_rgb_device_info();
        
        /// <summary>
        /// This function returns a struct which provides various relevant details about the currently connected device. E.g. max rgb rows, columns, etc
        /// </summary>
        /// <returns></returns>
        public static RGBDeviceInfo GetDeviceInfo() {
            return Marshal.PtrToStructure<RGBDeviceInfo>(wooting_rgb_device_info());
        }


        [DllImport(sdkDLL, CallingConvention = CallingConvention.Cdecl)]
        private static extern LayoutType wooting_rgb_device_layout();

        /// <summary>
        /// This function returns an enum flag indicating the layout, e.g. ISO. See LayoutType for options. It will return Unkown if no device is connected or it failed to get the layout info from the device
        /// </summary>
        /// <returns></returns>
        public static LayoutType DeviceLayout() {
            return wooting_rgb_device_layout();
        }
    }
}

