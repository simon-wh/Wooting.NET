using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Linq;

namespace Wooting
{
    public static class AnalogReader
    {
#if LINUX
        private const string sdkDLL = "wooting-analog-reader.so";
#else
        private const string sdkDLL = "wooting-analog-reader.dll";
#endif

        [StructLayout(LayoutKind.Sequential)]
        public struct AnalogRaw
        {
            public byte scan_code;
            public byte analog_value;
        }

        /// <summary>
        /// Check if keyboard connected.
        /// This function offers a quick check if the keyboard is connected.This doesn't open the keyboard or influences reading.
        /// It is recommended to poll this function at the start of your application before starting reading.
        /// </summary>
        /// <returns>This function returns true (1) if keyboard is found.</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_kbd_connected")]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool IsConnected();

        /// <summary>
        /// Set callback for when a keyboard disconnect.
        /// The callback will be called when a Wooting keyboard disconnects.Right now this will only trigger after a failed read.
        /// </summary>
        /// <param name="cb"></param>
        [DllImport(sdkDLL, EntryPoint = "wooting_set_disconnected_cb")]
        public static extern void SetDisconnectedCallback(DisconnectedCallback cb);

        /// <summary>
        /// Get the analog value of a single key.
        /// This function returns an analog value of a single key.
        /// It is not necesarry to initialize the keyboard before reading, but if the keyboard is not connected this function will return 0.
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns>Analog value from 0->255</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_read_analog")]
        public static extern byte ReadAnalog(byte row, byte column);

        /// <summary>
        /// Get the analog value of a single key.
        /// This function returns an analog value of a single key.
        /// It is not necesarry to initialize the keyboard before reading, but if the keyboard is not connected this function will return 0.
        /// </summary>
        /// <param name="key">The key to be read</param>
        /// <returns>The current analog value</returns>
        public static byte ReadAnalog(WootingKey.Keys key)
        {
            if (WootingKey.KeyMap.TryGetValue(key, out (byte row, byte column) index))
                return ReadAnalog(index.row, index.column);

            return 0;
        }

        /// <summary>
        /// Get the full analog buffer.
        /// This function can be used to get a buffer of all the keys that are pressed up to a maximum of 16 keys.This can be used for easier access to the keys that are currently pressed.
        /// It is not necesarry to initialize the keyboard before reading, but if the keyboard is not connected this function will return -1.
        /// </summary>
        /// <param name="data">A buffer to put the read data into. Expects an array of wooting_full_buffer.</param>
        /// <param name="items">Amount of items in the array of the data buffer</param>
        /// <returns>This function returns items written and -1 on error.</returns>
        [DllImport(sdkDLL, EntryPoint = "wooting_read_full_buffer")]
        public static extern int ReadFullBuffer([In][Out][MarshalAs(UnmanagedType.LPArray, SizeParamIndex=1)] AnalogRaw[] data, uint items);

        /// <summary>
        /// Get the full analog buffer.
        /// This function can be used to get a buffer of all the keys that are pressed up to a maximum of 16 keys.This can be used for easier access to the keys that are currently pressed.
        /// It is not necesarry to initialize the keyboard before reading, but if the keyboard is not connected this function will return an empty list.
        /// </summary>
        /// <param name="no">The amount of items in the buffer to attempt to read</param>
        /// <returns>The List of all the Analog data that was read</returns>
        public static List<AnalogRaw> ReadFullBuffer(uint no)
        {
            AnalogRaw[] data = new AnalogRaw[no];
            int ret = ReadFullBuffer(data, no);
            if (ret <= 0)
                return new List<AnalogRaw>();
            return data.Take(ret).ToList();
        }

        public enum CODES {
            Escape,
            F1,
            F2,
            F3,
            F4,
            F5,
            F6,
            F7,
            F8,
            F9,
            F10,
            F11,
            F12,
            Printscreen,
            Pause,
            Mode,
            Tilde,
            Number1,
            Number2,
            Number3,
            Number4,
            Number5,
            Number6,
            Number7,
            Number8,
            Number9,
            Number0,
            Underscore,
            Plus,
            Backspace,
            Insert,
            Home,
            Tab,
            Q,
            W,
            E,
            R,
            T,
            Y,
            U,
            I,
            O,
            P,
            OpenBracket,
            CloseBracket,
            Backslash,
            Delete,
            End,
            CapsLock,
            A,
            S,
            D,
            F,
            G,
            H,
            J,
            K,
            L,
            Colon,
            Quote,
            Enter,
            PageUp,
            PageDown,
            Up,
            ModifierLeftShift,
            Z,
            X,
            C,
            V,
            B,
            N,
            M,
            Comma,
            Dot,
            Slash,
            ModifierRightShift,
            Left,
            Down,
            Right,
            ModifierRightCtrl,
            ModifierLeftCtrl,
            ModifierLeftUi,
            ModifierLeftAlt,
            Spacebar,
            ModifierRightAlt,
            ModifierRightUi,
            FnKey,
            ExtraIso
        }
    }
}
