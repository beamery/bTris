using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.Sdl;

namespace Engine.Input
{
    public class Input
    {
        public Point MousePosition { get; set; }
        bool usingController = false;
        public XboxController Controller { get; set; }
        public Mouse Mouse { get; set; }
        public Keyboard Keyboard { get; set; }

        public Input()
        {
            Sdl.SDL_InitSubSystem(Sdl.SDL_INIT_JOYSTICK);
            if (Sdl.SDL_NumJoysticks() > 0)
            {
                Controller = new XboxController(0);
                usingController = true;
            }
        }

        public void Update(double elapsedTime)
        {
            if (usingController)
            {
                Sdl.SDL_JoystickUpdate();
                Controller.Update();
            }
            Mouse.Update(elapsedTime);
            Keyboard.Process();
        }
    }
}
