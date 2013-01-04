using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class Effect
    {
        public Tween Color { get; set; }
        public Tween Movement { get; set; }
        Text text;
        public Text Text 
        {
            set { text = value; }
            get { return text; }
        }

        public Effect(Text text)
        {
            this.text = text;
            Color = new Tween(1, 0, 0.5);
            Movement = new Tween(0, 25, 0.5);
        }

        public void Update(double elapsedTime)
        {
            Color.Update(elapsedTime);
            Movement.Update(elapsedTime);

            text.SetPosition(-text.Width / 2, Movement.Value);
            text.SetColor(new Color(text.GetColor().Red, text.GetColor().Green, text.GetColor().Blue, (float)Color.Value));
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawText(text);
        }
    }
}
