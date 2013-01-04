using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Engine.Input
{
    public class Keyboard
    {
        [DllImport("User32.dll")]
        public static extern short GetAsyncKeyState(int vKey);

        Control openGlControl;
        public KeyPressEventHandler KeyPressEvent;

        class KeyState
        {
            bool keyPressDetected = false;
            public bool Held { get; set; }
            public bool Pressed { get; set; }

            public KeyState()
            {
                Held = false;
                Pressed = false;
            }

            internal void OnDown()
            {
                if (Held == false)
                {
                    keyPressDetected = true;
                }
                Held = true;
            }

            internal void OnUp()
            {
                Held = false;
            }

            internal void Process()
            {
                Pressed = false;
                if (keyPressDetected)
                {
                    Pressed = true;
                    keyPressDetected = false;
                }
            }
        }
        Dictionary<Keys, KeyState> keyStates = new Dictionary<Keys, KeyState>();

        public Keyboard(Control openGlControl)
        {
            this.openGlControl = openGlControl;
            openGlControl.KeyDown += new KeyEventHandler(OnKeyDown);
            openGlControl.KeyUp += new KeyEventHandler(OnKeyUp);
            openGlControl.KeyPress += new KeyPressEventHandler(OnKeyPress);
        }

        void OnKeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPressEvent != null)
            {
                KeyPressEvent(sender, e);
            }
        }

        void OnKeyUp(object sender, KeyEventArgs e)
        {
            EnsureKeyStateExists(e.KeyCode);
            keyStates[e.KeyCode].OnUp();
        }

        void OnKeyDown(object sender, KeyEventArgs e)
        {
            EnsureKeyStateExists(e.KeyCode);
            keyStates[e.KeyCode].OnDown();
        }

        private void EnsureKeyStateExists(Keys key)
        {
            if (!keyStates.Keys.Contains(key))
            {
                keyStates.Add(key, new KeyState());
            }
        }

        public bool IsKeyPressed(Keys key)
        {
            EnsureKeyStateExists(key);
            return keyStates[key].Pressed;
        }

        public bool IsKeyHeld(Keys key)
        {
            EnsureKeyStateExists(key);
            return keyStates[key].Held;
        }

        public void Process()
        {
            ProcessControlKeys();

            foreach (KeyState state in keyStates.Values)
            {
                // Reset state
                state.Pressed = false;
                state.Process();
            }
        }

        private bool PollKeyPress(Keys key)
        {
            return (GetAsyncKeyState((int)key) != 0);
        }

        private void ProcessControlKeys()
        {
            UpdateControlKey(Keys.Left);
            UpdateControlKey(Keys.Right);
            UpdateControlKey(Keys.Up);
            UpdateControlKey(Keys.Down);
            UpdateControlKey(Keys.LMenu); // left alt key
        }

        private void UpdateControlKey(Keys key)
        {
            if (PollKeyPress(key))
            {
                OnKeyDown(this, new KeyEventArgs(key));
            }
            else
            {
                OnKeyUp(this, new KeyEventArgs(key));
            }
        }
    }
}
