using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class ControlTrigger
    {
        IntPtr joystick;
        int index;
        bool top = false; // The triggers are treated as axes and need 
                          // splitting up
        float deadZone = 0.24f;
        public float Value { get; private set; }

        public ControlTrigger(IntPtr joystick, int index, bool top)
        {
            this.joystick = joystick;
            this.index = index;
            this.top = top;
        }

        public void Update()
        {
            Value = MapZeroToOne(Sdl.SDL_JoystickGetAxis(joystick, index));
        }

        private float MapZeroToOne(short value)
        {
            float output = ((float)value / short.MaxValue);

            if (top == false)
            {
                if (output > 0)
                {
                    output = 0;
                }
                output = Math.Abs(output);
            }

            // Be careful of rounding error
            output = Math.Min(output, 1.0f);
            output = Math.Max(output, 0.0f);

            if (Math.Abs(output) < deadZone)
            {
                output = 0;
            }
            return output;
        }

    }
}
