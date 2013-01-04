using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class XboxController : IDisposable
    {
        IntPtr joystick;
        // Analog Sticks
        public ControlStick LeftControlStick { get; private set; }
        public ControlStick RightControlStick { get; private set; }

        // ABXY Buttons
        public ControllerButton ButtonA { get; private set; }
        public ControllerButton ButtonB { get; private set; }
        public ControllerButton ButtonX { get; private set; }
        public ControllerButton ButtonY { get; private set; }

        // Front shoulder buttons
        public ControllerButton ButtonLB { get; private set; }
        public ControllerButton ButtonRB { get; private set; }

        // Start and Back
        public ControllerButton ButtonBack { get; private set; }
        public ControllerButton ButtonStart { get; private set; }

        // Pressing in the analog sticks
        public ControllerButton ButtonL3 { get; private set; }
        public ControllerButton ButtonR3 { get; private set; }

        public ControlTrigger RightTrigger { get; private set; }
        public ControlTrigger LeftTrigger { get; private set; }

        public DPad Dpad { get; private set; }

        public XboxController(int player)
        {
            joystick = Sdl.SDL_JoystickOpen(player);
            LeftControlStick = new ControlStick(joystick, 0, 1);
            RightControlStick = new ControlStick(joystick, 4, 3);
            ButtonA = new ControllerButton(joystick, 0);
            ButtonB = new ControllerButton(joystick, 1);
            ButtonX = new ControllerButton(joystick, 2);
            ButtonY = new ControllerButton(joystick, 3);
            ButtonLB = new ControllerButton(joystick, 4);
            ButtonRB = new ControllerButton(joystick, 5);
            ButtonBack = new ControllerButton(joystick, 6);
            ButtonStart = new ControllerButton(joystick, 7);
            ButtonL3 = new ControllerButton(joystick, 8);
            ButtonR3 = new ControllerButton(joystick, 9);
            RightTrigger = new ControlTrigger(joystick, 2, false);
            LeftTrigger = new ControlTrigger(joystick, 2, true);
            Dpad = new DPad(joystick, 0);
        }

        public void Update()
        {
            LeftControlStick.Update();
            RightControlStick.Update();
            ButtonA.Update();
            ButtonB.Update();
            ButtonX.Update();
            ButtonY.Update();
            ButtonLB.Update();
            ButtonRB.Update();
            ButtonBack.Update();
            ButtonStart.Update();
            ButtonL3.Update();
            ButtonR3.Update();
            RightTrigger.Update();
            LeftTrigger.Update();
            Dpad.Update();
        }

        #region IDisposable Members

        public void Dispose()
        {
            Sdl.SDL_JoystickClose(joystick);
        }

        #endregion
    }
}
