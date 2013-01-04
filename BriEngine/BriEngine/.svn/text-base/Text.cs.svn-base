using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Text
    {
        Font font;
        List<CharacterSprite> bitmapText = new List<CharacterSprite>();
        string text;
        Color color;
        Vector dimensions;
        int maxWidth = -1;
        public double Width
        {
            get { return dimensions.X; }
        }
        public double Height
        {
            get { return dimensions.Y; }
        }

        public List<CharacterSprite> CharacterSprites
        {
            get { return bitmapText; }
        }

        public Text(string text, Font font) : this(text, font, -1) { }
        public Text(string text, Font font, int maxWidth)
        {
            this.text = text;
            this.font = font;
            color = new Color(1, 1, 1, 1);
            this.maxWidth = maxWidth;
            CreateText(0, 0);
        }

        public void SetPosition(double x, double y)
        {
            CreateText(x, y);
        }

        public void SetColor(Color color)
        {
            this.color = color;
            foreach (CharacterSprite s in bitmapText)
            {
                s.Sprite.SetColor(color);
            }
        }

        public void SetColor()
        {
            foreach (CharacterSprite s in bitmapText)
            {
                s.Sprite.SetColor(color);
            }
        }

        public Color GetColor()
        {
            return color;
        }

        private void CreateText(double x, double y)
        {
            CreateText(x, y, maxWidth);
        }
        private void CreateText(double x, double y, double maxWidth)
        {
            bitmapText.Clear();
            double currentX = 0;
            double currentY = 0;
            string[] words = text.Split(' ');

            foreach (string word in words)
            {
                Vector nextWordLength = font.MeasureFont(word);
                if (maxWidth != -1 && (currentX + nextWordLength.X) > maxWidth)
                {
                    currentX = 0;
                    currentY += nextWordLength.Y;
                }

                string wordWithSpace = word + " ";

                foreach (char c in wordWithSpace)
                {
                    CharacterSprite sprite = font.CreateSprite(c);
                    float xOffset = ((float)sprite.Data.XOffset) / 2;
                    float yOffset = ((float)sprite.Data.Height) * 0.5f +
                        ((float)sprite.Data.YOffset) ;
                    sprite.Sprite.SetPosition(x + currentX + xOffset, y - currentY - 
                        yOffset);

                    currentX += sprite.Data.XAdvance;
                    bitmapText.Add(sprite);
                }
                dimensions = font.MeasureFont(text, maxWidth);
                dimensions.Y = currentY;
                SetColor(color);
            }
        }
    }
}
