using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Button
    {
        EventHandler onPressEvent;
        Text label;
        Vector position = new Vector();

        public static Color focusedColor = new Color(1, 0, 0, 1);
        public static Color unfocusedColor = new Color(0, 0, 0, 1);

        public Vector Position
        {
            get { return position; }
            set
            {
                position = value;
                UpdatePosition();
            }
        }

        public Button(EventHandler onPressEvent, Text label)
        {
            this.onPressEvent = onPressEvent;
            this.label = label;
            this.label.SetColor(unfocusedColor);
            UpdatePosition();
        }

        public void UpdatePosition()
        {
            // Center label text on position
            label.SetPosition(position.X - label.Width / 2,
                position.Y + label.Height / 2);
        }

        public void OnGainFocus()
        {
            label.SetColor(focusedColor);
        }

        public void OnLoseFocus()
        {
            label.SetColor(unfocusedColor);
        }

        public void OnPress()
        {
            onPressEvent(this, EventArgs.Empty);
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawText(label);
        }

    }
}
