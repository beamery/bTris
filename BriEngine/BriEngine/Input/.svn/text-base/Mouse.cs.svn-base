using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Engine.Input
{
    public class Mouse
    {
        Form parentForm;
        Control openGLControl;

        public Point Position { get; set; }

        bool leftClickDetect = false;
        bool rightClickDetect = false;
        bool middleClickDetect = false;

        public bool LeftPressed { get; private set; }
        public bool RightPressed { get; private set; }
        public bool MiddlePressed { get; private set; }

        public bool LeftHeld { get; private set; }
        public bool RightHeld { get; set; }
        public bool MiddleHeld { get; private set; }

        public Mouse(Form form, Control openGLControl)
        {
            parentForm = form;
            this.openGLControl = openGLControl;
            openGLControl.MouseClick += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    leftClickDetect = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    rightClickDetect = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    middleClickDetect = true;
                }
            };

            openGLControl.MouseDown += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = true;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = true;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = true;
                }
            };

            openGLControl.MouseUp += delegate(object obj, MouseEventArgs e)
            {
                if (e.Button == MouseButtons.Left)
                {
                    LeftHeld = false;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    RightHeld = false;
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    MiddleHeld = false;
                }
            };

            openGLControl.MouseLeave += delegate(object obj, EventArgs e)
            {
                // If you leave the window, then release all held buttons
                LeftHeld = false;
                RightHeld = false;
                MiddleHeld = false;
            };
        }

        public void Update(double elapsedTime)
        {
            UpdateMousePosition();
            UpdateMouseButtons();
        }

        private void UpdateMousePosition()
        {
            System.Drawing.Point mousePos = Cursor.Position;
            mousePos = openGLControl.PointToClient(mousePos);

            // Now use our point definition
            Engine.Point adjustedMousePoint = new Engine.Point();
            adjustedMousePoint.X = (float)mousePos.X - ((float)parentForm.ClientSize.Width / 2);
            adjustedMousePoint.Y = ((float)parentForm.ClientSize.Height / 2) - (float)mousePos.Y;

            Position = adjustedMousePoint;
        }

        private void UpdateMouseButtons()
        {
            // Reset buttons
            MiddlePressed = false;
            LeftPressed = false;
            RightPressed = false;

            if (leftClickDetect)
            {
                LeftPressed = true;
                leftClickDetect = false;
            }
            if (rightClickDetect)
            {
                RightPressed = true;
                rightClickDetect = false;
            }
            if (middleClickDetect)
            {
                MiddlePressed = true;
                middleClickDetect = false;
            }
        }
    }
}
