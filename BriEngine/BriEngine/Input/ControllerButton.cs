using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class ControllerButton
    {
        IntPtr joystick;
        int buttonId;

        public bool Held { get; private set; }

        bool wasHeld = false;
        public bool Pressed { get; private set; }

        public ControllerButton(IntPtr joystick, int buttonId)
        {
            this.joystick = joystick;
            this.buttonId = buttonId;
        }

        public void Update()
        {
            Pressed = false;
            byte buttonState = Sdl.SDL_JoystickGetButton(joystick, buttonId);
            Held = (buttonState == 1);

            if (Held)
            {
                if (wasHeld == false)
                {
                    Pressed = true;
                }
                wasHeld = true;
            }
            else
            {
                wasHeld = false;
            }
        }
    }
}
