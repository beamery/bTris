using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class ControlStick
    {
        IntPtr joystick;
        int axisIdX = 0;
        int axisIdY = 0;
        float deadZone = 0.2f;

        public float X { get; private set; }
        public float Y { get; private set; }

        public ControlStick(IntPtr joystick, int axisIdX, int axisIdY)
        {
            this.joystick = joystick;
            this.axisIdX = axisIdX;
            this.axisIdY = axisIdY;
        }

        public void Update()
        {
            X = MapMinusOneToOne(Sdl.SDL_JoystickGetAxis(joystick, axisIdX));
            Y = MapMinusOneToOne(Sdl.SDL_JoystickGetAxis(joystick, axisIdY));

        }

        private float MapMinusOneToOne(short value)
        {
            float output = ((float)value / short.MaxValue);

            // Be careful of rounding error
            output = Math.Min(output, 1.0f);
            output = Math.Max(output, -1.0f);

            if (Math.Abs(output) < deadZone)
            {
                output = 0;
            }
            return output;
        }
    }
}
