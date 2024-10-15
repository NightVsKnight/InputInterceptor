using System;
using System.Collections.Generic;
using System.Threading;

using Filter = System.UInt16;

namespace InputInterceptorNS
{

    public class KeyboardHook : Hook<KeyStroke>
    {

        private struct KeyData
        {

            public KeyCode Code;
            public Boolean Shift;

        }

        private static readonly Dictionary<Char, KeyData> KeyDictionary;
        private static readonly KeyData QuestionMark;

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
