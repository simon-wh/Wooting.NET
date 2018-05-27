using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Wooting
{
    public static class WootingKey
    {
        public enum Keys
        {
            None = 0,
            //Row 0
            Esc,
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
            PrintScreen,
            PauseBreak,
            Mode_ScrollLock,
            A1,
            A2,
            A3,
            Mode,
            //Row 1
            Tilda,
            N1,
            N2,
            N3,
            N4,
            N5,
            N6,
            N7,
            N8,
            N9,
            N0,
            Minus,
            Equals,
            Backspace,
            Insert,
            Home,
            PageUp,
            NumLock,
            NumSlash,
            NumMulti,
            NumMinus,
            //Row 2
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
            ANSI_Backslash,
            Delete,
            End,
            PageDown,
            Num7,
            Num8,
            Num9,
            NumPlus,
            //Row 3
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
            SemiColon,
            Apostophe,
            ISO_Hash,
            Enter,
            Num4,
            Num5,
            Num6,
            //Row 4
            LeftShift,
            ISO_Blackslash,
            Z,
            X,
            C,
            V,
            B,
            N,
            M,
            Comma,
            Period,
            Slash,
            RightShift,
            Up,
            Num1,
            Num2,
            Num3,
            NumEnter,
            //Row 5
            LeftCtrl,
            LeftWin,
            LeftAlt,
            Space,
            RightAlt,
            RightWin,
            Function,
            RightControl,
            Left,
            Down,
            Right,
            Num0,
            NumPeriod
        }

        /// <summary>
        /// A mapping from the Keys enum to the corresponding coordinates
        /// </summary>
        public static Dictionary<Keys, (byte, byte)> KeyMap = new Dictionary<Keys, (byte, byte)> {
            //Row 0
            { Keys.Esc, (0,0) },
            { Keys.F1, (0,2) },
            { Keys.F2, (0,3) },
            { Keys.F3, (0,4) },
            { Keys.F4, (0,5) },
            { Keys.F5, (0,6) },
            { Keys.F6, (0,7) },
            { Keys.F7, (0,8) },
            { Keys.F8, (0,9) },
            { Keys.F9, (0,10) },
            { Keys.F10, (0,11) },
            { Keys.F11, (0,12) },
            { Keys.F12, (0,13) },
            { Keys.PrintScreen, (0,14) },
            { Keys.PauseBreak, (0,15) },
            { Keys.Mode_ScrollLock, (0,16) },
            { Keys.A1, (0,17) },
            { Keys.A2, (0,18) },
            { Keys.A3, (0,19) },
            { Keys.Mode, (0,20) },

            //Row 1
            { Keys.Tilda, (1,0) },
            { Keys.N1, (1,1) },
            { Keys.N2, (1,2) },
            { Keys.N3, (1,3) },
            { Keys.N4, (1,4) },
            { Keys.N5, (1,5) },
            { Keys.N6, (1,6) },
            { Keys.N7, (1,7) },
            { Keys.N8, (1,8) },
            { Keys.N9, (1,9) },
            { Keys.N0, (1,10) },
            { Keys.Minus, (1,11) },
            { Keys.Equals, (1,12) },
            { Keys.Backspace, (1,13) },
            { Keys.Insert, (1,14) },
            { Keys.Home, (1,15) },
            { Keys.PageUp, (1,16) },
            { Keys.NumLock, (1,17) },
            { Keys.NumSlash, (1,18) },
            { Keys.NumMulti, (1,19) },
            { Keys.NumMinus, (1,20) },

            //Row2
            { Keys.Tab, (2,0) },
            { Keys.Q, (2,1) },
            { Keys.W, (2,2) },
            { Keys.E, (2,3) },
            { Keys.R, (2,4) },
            { Keys.T, (2,5) },
            { Keys.Y, (2,6) },
            { Keys.U, (2,7) },
            { Keys.I, (2,8) },
            { Keys.O, (2,9) },
            { Keys.P, (2,10) },
            { Keys.OpenBracket, (2,11) },
            { Keys.CloseBracket, (2,12) },
            { Keys.ANSI_Backslash, (2,13) },
            { Keys.Delete, (2,14) },
            { Keys.End, (2,15) },
            { Keys.PageDown, (2,16) },
            { Keys.Num7, (2,17) },
            { Keys.Num8, (2,18) },
            { Keys.Num9, (2,19) },
            { Keys.NumPlus, (2,20) },

            //Row3
            { Keys.CapsLock, (3,0) },
            { Keys.A, (3,1) },
            { Keys.S, (3,2) },
            { Keys.D, (3,3) },
            { Keys.F, (3,4) },
            { Keys.G, (3,5) },
            { Keys.H, (3,6) },
            { Keys.J, (3,7) },
            { Keys.K, (3,8) },
            { Keys.L, (3,9) },
            { Keys.SemiColon, (3,10) },
            { Keys.Apostophe, (3,11) },
            { Keys.ISO_Hash, (3,12) },
            { Keys.Enter, (3,13) },
            { Keys.Num4, (3,17) },
            { Keys.Num5, (3,18) },
            { Keys.Num6, (3,19) },

            //Row4
            { Keys.LeftShift, (4,0) },
            { Keys.ISO_Blackslash, (4,1) },
            { Keys.Z, (4,2) },
            { Keys.X, (4,3) },
            { Keys.C, (4,4) },
            { Keys.V, (4,5) },
            { Keys.B, (4,6) },
            { Keys.N, (4,7) },
            { Keys.M, (4,8) },
            { Keys.Comma, (4,9) },
            { Keys.Period, (4,10) },
            { Keys.Slash, (4,11) },

            { Keys.RightShift, (4,13) },

            { Keys.Up, (4,15) },

            { Keys.Num1, (4,17) },
            { Keys.Num2, (4,18) },
            { Keys.Num3, (4,19) },
            { Keys.NumEnter, (4,20) },

            //Row5
            { Keys.LeftCtrl, (5,0) },
            { Keys.LeftWin, (5,1) },
            { Keys.LeftAlt, (5,2) },



            { Keys.Space, (5,6) },



            { Keys.RightAlt, (5,10) },
            { Keys.RightWin, (5,11) },
            { Keys.Function, (5,12) },
            { Keys.RightControl, (5,13) },
            { Keys.Left, (5,14) },
            { Keys.Down, (5,15) },
            { Keys.Right, (5,16) },

            { Keys.Num0, (5,18) },
            { Keys.NumPeriod, (5,19) },
        };
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyColour
    {
        public byte r;
        public byte g;
        public byte b;

        public KeyColour(byte r, byte g, byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }
    }
}