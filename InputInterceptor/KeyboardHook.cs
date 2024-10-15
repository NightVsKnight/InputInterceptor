using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

using Filter = System.UInt16;

namespace InputInterceptorNS
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "<Pending>")]
    public class KeyboardHook : Hook<KeyStroke>
    {

        private struct KeyData
        {

            public KeyCode Code;
            public Boolean Shift;

        }

        private static readonly Dictionary<Char, KeyData> KeyDictionary;
        private static readonly KeyData QuestionMark;
        private static readonly Dictionary<Keys, KeyCode> VirtualKeyCodeToKeyCode;

        static KeyboardHook()
        {
            KeyDictionary = new Dictionary<Char, KeyData>
            {
                { '`', new KeyData { Code = KeyCode.Tilde } },
                { '1', new KeyData { Code = KeyCode.One } },
                { '2', new KeyData { Code = KeyCode.Two } },
                { '3', new KeyData { Code = KeyCode.Three } },
                { '4', new KeyData { Code = KeyCode.Four } },
                { '5', new KeyData { Code = KeyCode.Five } },
                { '6', new KeyData { Code = KeyCode.Six } },
                { '7', new KeyData { Code = KeyCode.Seven } },
                { '8', new KeyData { Code = KeyCode.Eight } },
                { '9', new KeyData { Code = KeyCode.Nine } },
                { '0', new KeyData { Code = KeyCode.Zero } },
                { '-', new KeyData { Code = KeyCode.Dash } },
                { '=', new KeyData { Code = KeyCode.Equals } },
                { 'q', new KeyData { Code = KeyCode.Q } },
                { 'w', new KeyData { Code = KeyCode.W } },
                { 'e', new KeyData { Code = KeyCode.E } },
                { 'r', new KeyData { Code = KeyCode.R } },
                { 't', new KeyData { Code = KeyCode.T } },
                { 'y', new KeyData { Code = KeyCode.Y } },
                { 'u', new KeyData { Code = KeyCode.U } },
                { 'i', new KeyData { Code = KeyCode.I } },
                { 'o', new KeyData { Code = KeyCode.O } },
                { 'p', new KeyData { Code = KeyCode.P } },
                { '[', new KeyData { Code = KeyCode.OpenBracketBrace } },
                { ']', new KeyData { Code = KeyCode.CloseBracketBrace } },
                { 'a', new KeyData { Code = KeyCode.A } },
                { 's', new KeyData { Code = KeyCode.S } },
                { 'd', new KeyData { Code = KeyCode.D } },
                { 'f', new KeyData { Code = KeyCode.F } },
                { 'g', new KeyData { Code = KeyCode.G } },
                { 'h', new KeyData { Code = KeyCode.H } },
                { 'j', new KeyData { Code = KeyCode.J } },
                { 'k', new KeyData { Code = KeyCode.K } },
                { 'l', new KeyData { Code = KeyCode.L } },
                { ';', new KeyData { Code = KeyCode.Semicolon } },
                { '\'', new KeyData { Code = KeyCode.Apostrophe } },
                { '\\', new KeyData { Code = KeyCode.Backslash } },
                { 'z', new KeyData { Code = KeyCode.Z } },
                { 'x', new KeyData { Code = KeyCode.X } },
                { 'c', new KeyData { Code = KeyCode.C } },
                { 'v', new KeyData { Code = KeyCode.V } },
                { 'b', new KeyData { Code = KeyCode.B } },
                { 'n', new KeyData { Code = KeyCode.N } },
                { 'm', new KeyData { Code = KeyCode.M } },
                { ',', new KeyData { Code = KeyCode.Comma } },
                { '.', new KeyData { Code = KeyCode.Dot } },
                { '/', new KeyData { Code = KeyCode.Slash } },
                { ' ', new KeyData { Code = KeyCode.Space } },
                { '~', new KeyData { Code = KeyCode.Tilde, Shift = true } },
                { '!', new KeyData { Code = KeyCode.One, Shift = true } },
                { '@', new KeyData { Code = KeyCode.Two, Shift = true } },
                { '#', new KeyData { Code = KeyCode.Three, Shift = true } },
                { '$', new KeyData { Code = KeyCode.Four, Shift = true } },
                { '%', new KeyData { Code = KeyCode.Five, Shift = true } },
                { '^', new KeyData { Code = KeyCode.Six, Shift = true } },
                { '&', new KeyData { Code = KeyCode.Seven, Shift = true } },
                { '*', new KeyData { Code = KeyCode.Eight, Shift = true } },
                { '(', new KeyData { Code = KeyCode.Nine, Shift = true } },
                { ')', new KeyData { Code = KeyCode.Zero, Shift = true } },
                { '_', new KeyData { Code = KeyCode.Dash, Shift = true } },
                { '+', new KeyData { Code = KeyCode.Equals, Shift = true } },
                { 'Q', new KeyData { Code = KeyCode.Q, Shift = true } },
                { 'W', new KeyData { Code = KeyCode.W, Shift = true } },
                { 'E', new KeyData { Code = KeyCode.E, Shift = true } },
                { 'R', new KeyData { Code = KeyCode.R, Shift = true } },
                { 'T', new KeyData { Code = KeyCode.T, Shift = true } },
                { 'Y', new KeyData { Code = KeyCode.Y, Shift = true } },
                { 'U', new KeyData { Code = KeyCode.U, Shift = true } },
                { 'I', new KeyData { Code = KeyCode.I, Shift = true } },
                { 'O', new KeyData { Code = KeyCode.O, Shift = true } },
                { 'P', new KeyData { Code = KeyCode.P, Shift = true } },
                { '{', new KeyData { Code = KeyCode.OpenBracketBrace, Shift = true } },
                { '}', new KeyData { Code = KeyCode.CloseBracketBrace, Shift = true } },
                { 'A', new KeyData { Code = KeyCode.A, Shift = true } },
                { 'S', new KeyData { Code = KeyCode.S, Shift = true } },
                { 'D', new KeyData { Code = KeyCode.D, Shift = true } },
                { 'F', new KeyData { Code = KeyCode.F, Shift = true } },
                { 'G', new KeyData { Code = KeyCode.G, Shift = true } },
                { 'H', new KeyData { Code = KeyCode.H, Shift = true } },
                { 'J', new KeyData { Code = KeyCode.J, Shift = true } },
                { 'K', new KeyData { Code = KeyCode.K, Shift = true } },
                { 'L', new KeyData { Code = KeyCode.L, Shift = true } },
                { ':', new KeyData { Code = KeyCode.Semicolon, Shift = true } },
                { '"', new KeyData { Code = KeyCode.Apostrophe, Shift = true } },
                { '|', new KeyData { Code = KeyCode.Backslash, Shift = true } },
                { 'Z', new KeyData { Code = KeyCode.Z, Shift = true } },
                { 'X', new KeyData { Code = KeyCode.X, Shift = true } },
                { 'C', new KeyData { Code = KeyCode.C, Shift = true } },
                { 'V', new KeyData { Code = KeyCode.V, Shift = true } },
                { 'B', new KeyData { Code = KeyCode.B, Shift = true } },
                { 'N', new KeyData { Code = KeyCode.N, Shift = true } },
                { 'M', new KeyData { Code = KeyCode.M, Shift = true } },
                { '<', new KeyData { Code = KeyCode.Comma, Shift = true } },
                { '>', new KeyData { Code = KeyCode.Dot, Shift = true } },
                { '?', new KeyData { Code = KeyCode.Slash, Shift = true } }
            };
            QuestionMark = new KeyData { Code = KeyCode.Slash, Shift = true };

            VirtualKeyCodeToKeyCode = new Dictionary<Keys, KeyCode>
            {
                //None
                //LButton
                //RButton
                //Cancel
                //MButton
                //XButton1
                //XButton2
                { Keys.Back, KeyCode.Backspace },
                { Keys.Tab, KeyCode.Tab },
                //LineFeed
                //Clear
                { Keys.Return, KeyCode.Enter },
                //ShiftKey
                { Keys.ControlKey, KeyCode.Control },
                { Keys.Menu, KeyCode.Alt },
                //Pause
                { Keys.Capital, KeyCode.CapsLock },
                //...
                { Keys.Escape, KeyCode.Escape },
                //...
                { Keys.Space, KeyCode.Space },
                { Keys.PageUp, KeyCode.PageUp },
                { Keys.PageDown, KeyCode.PageDown },
                { Keys.End, KeyCode.End },
                { Keys.Home, KeyCode.Home },
                { Keys.Left, KeyCode.Left },
                { Keys.Up, KeyCode.Up },
                { Keys.Right, KeyCode.Right },
                { Keys.Down, KeyCode.Down },
                //{ Keys.Select, KeyCode.Select },
                //{ Keys.Print, KeyCode.Print },
                //{ Keys.Execute, KeyCode.Execute },
                { Keys.Snapshot, KeyCode.PrintScreen },
                { Keys.Insert, KeyCode.Insert },
                { Keys.Delete, KeyCode.Delete },
                //Keys.Help, KeyCode.Help
                { Keys.D0, KeyCode.Zero },
                { Keys.D1, KeyCode.One },
                { Keys.D2, KeyCode.Two },
                { Keys.D3, KeyCode.Three },
                { Keys.D4, KeyCode.Four },
                { Keys.D5, KeyCode.Five },
                { Keys.D6, KeyCode.Six },
                { Keys.D7, KeyCode.Seven },
                { Keys.D8, KeyCode.Eight },
                { Keys.D9, KeyCode.Nine },
                { Keys.A, KeyCode.A },
                { Keys.B, KeyCode.B },
                { Keys.C, KeyCode.C },
                { Keys.D, KeyCode.D },
                { Keys.E, KeyCode.E },
                { Keys.F, KeyCode.F },
                { Keys.G, KeyCode.G },
                { Keys.H, KeyCode.H },
                { Keys.I, KeyCode.I },
                { Keys.J, KeyCode.J },
                { Keys.K, KeyCode.K },
                { Keys.L, KeyCode.L },
                { Keys.M, KeyCode.M },
                { Keys.N, KeyCode.N },
                { Keys.O, KeyCode.O },
                { Keys.P, KeyCode.P },
                { Keys.Q, KeyCode.Q },
                { Keys.R, KeyCode.R },
                { Keys.S, KeyCode.S },
                { Keys.T, KeyCode.T },
                { Keys.U, KeyCode.U },
                { Keys.V, KeyCode.V },
                { Keys.W, KeyCode.W },
                { Keys.X, KeyCode.X },
                { Keys.Y, KeyCode.Y },
                { Keys.Z, KeyCode.Z },
                { Keys.LWin, KeyCode.LeftWindowsKey },
                { Keys.RWin, KeyCode.RightWindowsKey },
                { Keys.Apps, KeyCode.Menu },
                //Sleep
                { Keys.NumPad0, KeyCode.Numpad0 },
                { Keys.NumPad1, KeyCode.Numpad1 },
                { Keys.NumPad2, KeyCode.Numpad2 },
                { Keys.NumPad3, KeyCode.Numpad3 },
                { Keys.NumPad4, KeyCode.Numpad4 },
                { Keys.NumPad5, KeyCode.Numpad5 },
                { Keys.NumPad6, KeyCode.Numpad6 },
                { Keys.NumPad7, KeyCode.Numpad7 },
                { Keys.NumPad8, KeyCode.Numpad8 },
                { Keys.NumPad9, KeyCode.Numpad9 },
                { Keys.Multiply, KeyCode.NumpadAsterisk },
                { Keys.Add, KeyCode.NumpadPlus },
                //Keys.Separator, KeyCode.NumpadSeparator
                { Keys.Subtract, KeyCode.NumpadMinus },
                //Keys.Decimal, KeyCode.NumpadDecimal
                { Keys.Divide, KeyCode.NumpadDivide },
                { Keys.F1, KeyCode.F1 },
                { Keys.F2, KeyCode.F2 },
                { Keys.F3, KeyCode.F3 },
                { Keys.F4, KeyCode.F4 },
                { Keys.F5, KeyCode.F5 },
                { Keys.F6, KeyCode.F6 },
                { Keys.F7, KeyCode.F7 },
                { Keys.F8, KeyCode.F8 },
                { Keys.F9, KeyCode.F9 },
                { Keys.F10, KeyCode.F10 },
                { Keys.F11, KeyCode.F11 },
                { Keys.F12, KeyCode.F12 },
                //F13-F24
                { Keys.NumLock, KeyCode.NumLock },
                { Keys.Scroll, KeyCode.ScrollLock },
                { Keys.LShiftKey, KeyCode.LeftShift },
                { Keys.RShiftKey, KeyCode.RightShift },
                //Keys.LControlKey, KeyCode.LeftControl
                //Keys.RControlKey, KeyCode.RightControl
                //Keys.LMenu, KeyCode.LeftAlt
                //Keys.RMenu, KeyCode.RightAlt
                //Browser...
                //Volume...
                //Media...
                //Launch...
                //...
                { Keys.OemSemicolon, KeyCode.Semicolon },
                { Keys.Oemplus, KeyCode.Equals },
                { Keys.Oemcomma, KeyCode.Comma },
                { Keys.OemMinus, KeyCode.Dash },
                { Keys.OemPeriod, KeyCode.Dot },
                { Keys.OemQuestion, KeyCode.Slash },
                { Keys.Oemtilde, KeyCode.Tilde },
                { Keys.OemOpenBrackets, KeyCode.OpenBracketBrace },
                { Keys.OemBackslash, KeyCode.Backslash },
                { Keys.OemCloseBrackets, KeyCode.CloseBracketBrace },
                { Keys.OemQuotes, KeyCode.Apostrophe }
                //OemClear
            };
        }

        public static KeyCode FromVirtualKeyCode(Keys vkCode)
        {
            switch (vkCode)
            {
                case Keys.LControlKey:
                case Keys.RControlKey:
                    vkCode = Keys.ControlKey;
                    break;
                case Keys.LMenu:
                case Keys.RMenu:
                    vkCode = Keys.Menu;
                    break;
            }
            KeyCode keyCode;
            if (!VirtualKeyCodeToKeyCode.TryGetValue(vkCode, out keyCode))
            {
                keyCode = KeyCode.UNKNOWN;
            }
            return keyCode;
        }

        public KeyboardHook(KeyboardFilter filter = KeyboardFilter.None, CallbackAction callback = null) :
            base((Filter)filter, InputInterceptor.IsKeyboard, callback)
        { }

        public KeyboardHook(CallbackAction callback) :
            base((Filter)KeyboardFilter.All, InputInterceptor.IsKeyboard, callback)
        { }

        protected override void CallbackWrapper(ref Stroke stroke)
        {
            this.Callback(ref stroke.Key);
        }

        public Boolean SetKeyState(KeyCode code, KeyState state)
        {
            if (this.CanSimulateInput)
            {
                Stroke stroke = new Stroke();
                stroke.Key.Code = code;
                stroke.Key.State = state;
                return InputInterceptor.Send(this.Context, this.AnyDevice, ref stroke, 1) == 1;
            }
            return false;
        }

        public Boolean SimulateKeyDown(KeyCode code)
        {
            return this.SetKeyState(code, KeyState.Down);
        }

        public Boolean SimulateKeyUp(KeyCode code)
        {
            return this.SetKeyState(code, KeyState.Up);
        }

        public Boolean SimulateKeyPress(KeyCode code, Int32 releaseDelay = 75)
        {
            if (this.SimulateKeyDown(code))
            {
                Thread.Sleep(releaseDelay);
                return this.SimulateKeyUp(code);
            }
            return false;
        }

        public Boolean SimulateInput(String text, Int32 delayBetweenKeyPresses = 50, Int32 releaseDelay = 75)
        {
            Boolean shiftDown = false;
            foreach (Char letter in text)
            {
                KeyData keyData;
                if (!KeyDictionary.TryGetValue(letter, out keyData))
                    keyData = QuestionMark;
                if (keyData.Shift != shiftDown)
                {
                    if (keyData.Shift)
                    {
                        if (!this.SetKeyState(KeyCode.LeftShift, KeyState.Down))
                            return false;
                    }
                    else
                    {
                        if (!this.SetKeyState(KeyCode.LeftShift, KeyState.Up))
                            return false;
                    }
                    shiftDown = keyData.Shift;
                }
                if (!this.SimulateKeyPress(keyData.Code, releaseDelay))
                    return false;
                Thread.Sleep(delayBetweenKeyPresses);
            }
            if (shiftDown)
            {
                if (!this.SetKeyState(KeyCode.LeftShift, KeyState.Up))
                    return false;
            }
            return true;
        }

    }

}
