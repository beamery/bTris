using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Engine.Input;
using System.Windows.Forms;

namespace Engine
{
    public class VerticalMenu
    {
        Vector position = new Vector();
        Input.Input input;
        List<Button> buttons = new List<Button>();
        public double Spacing { get; set; }

        bool inDown = false;
        bool inUp = false;
        int currentFocus = 0;

        public VerticalMenu(double x, double y, Input.Input input, Color unfocused, Color focused)
        {
            this.input = input;
            position = new Vector(x, y, 0);
            Spacing = 50;
            SetUnfocusedColor(unfocused);
            SetFocusColor(focused);
        }

        public VerticalMenu(double x, double y, Input.Input input) :
            this(x, y, input, Button.unfocusedColor, Button.focusedColor) { }

        public void AddButton(Button button)
        {
            double currentY = position.Y;

            if (buttons.Count != 0)
            {
                currentY = buttons.Last().Position.Y;
                currentY -= Spacing;
            }
            else
            {
                button.OnGainFocus();
            }

            button.Position = new Vector(position.X, currentY, 0);
            buttons.Add(button);
        }

        public void HandleInput()
        {
            if (input.Controller != null)
            {
                HandleControllerInput();
            }
            else
            {
                HandleKeyboardInput();
            }
        }

        private void HandleKeyboardInput()
        {
            if (input.Keyboard.IsKeyPressed(Keys.Down))
            {
                OnDown();
            }
            if (input.Keyboard.IsKeyPressed(Keys.Up))
            {
                OnUp();
            }
            if (input.Keyboard.IsKeyPressed(Keys.Enter))
            {
                OnButtonPress();
            }
        }
       
        private void HandleControllerInput()
        {
            bool controlPadUp = false;
            bool controlPadDown = false;

            float invertY = input.Controller.LeftControlStick.Y * -1;

            if (invertY < -0.2 || input.Controller.Dpad.DownHeld)
            {
                // The control stick is pulled down
                if (inDown == false)
                {
                    controlPadDown = true;
                    inDown = true;
                }
                else
                {
                    inDown = false;
                }
                
            }
            if (invertY > 0.2 || input.Controller.Dpad.UpHeld)
            {
                // the control stick is pushed up
                if (inUp == false)
                {
                    controlPadUp = true;
                    inUp = true;
                }
                else
                {
                    inUp = false;
                }
            }

            if (input.Keyboard.IsKeyPressed(Keys.Down) ||
                controlPadDown)
            {
                OnDown();

            }
            else if (input.Keyboard.IsKeyPressed(Keys.Up) ||
                controlPadUp)
            {
                OnUp();
            }
            else if (input.Keyboard.IsKeyPressed(Keys.Enter) ||
                input.Controller.ButtonA.Pressed)
            {
                OnButtonPress();
            }
        }

        public void SetUnfocusedColor(Color color)
        {
            Button.unfocusedColor = color;
        }

        public void SetFocusColor(Color color)
        {
            Button.focusedColor = color;
        }

        private void OnButtonPress()
        {
            buttons[currentFocus].OnPress();
        }

        private void OnDown()
        {
            int oldFocus = currentFocus;
            currentFocus++;
            if (currentFocus == buttons.Count)
            {
                currentFocus = 0;
            }
            ChangeFocus(oldFocus, currentFocus);
        }

        private void OnUp()
        {
            int oldFocus = currentFocus;
            currentFocus--;
            if (currentFocus < 0)
            {
                currentFocus = buttons.Count - 1;
            }
            ChangeFocus(oldFocus, currentFocus);
        }

        private void ChangeFocus(int from, int to)
        {
            if (from != to)
            {
                buttons[from].OnLoseFocus();
                buttons[to].OnGainFocus();
            }
        }

        public void Render(Renderer renderer)
        {
            buttons.ForEach(x => x.Render(renderer));
        }
    }
}
