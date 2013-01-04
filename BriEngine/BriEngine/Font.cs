using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Font
    {
        Texture texture;
        Dictionary<char, CharacterData> characterData;

        public Font(Texture texture, Dictionary<char, CharacterData> characterData)
        {
            this.texture = texture;
            this.characterData = characterData;
        }

        public CharacterSprite CreateSprite(char c)
        {
            CharacterData charData = characterData[c];
            Sprite sprite = new Sprite();
            sprite.Texture = texture;

            // Setup UVs
            Point topLeft = new Point((float)charData.X / (float)texture.Width,
                (float)charData.Y / (float)texture.Height);
            Point bottomRight = new Point(topLeft.X + ((float)charData.Width /
                (float)texture.Width), topLeft.Y + ((float)charData.Height /
                (float)texture.Height));
            
            sprite.SetUVs(topLeft, bottomRight);
            sprite.SetWidth(charData.Width);
            sprite.SetHeight(charData.Height);
            sprite.SetColor(new Color(1, 1, 1, 1));

            return new CharacterSprite(sprite, charData);
        }

        public Vector MeasureFont(string text)
        {
            return MeasureFont(text, -1);
        }
        public Vector MeasureFont(string text, double maxWidth)
        {
            Vector dimensions = new Vector();
            foreach (char c in text)
            {
                CharacterData data = characterData[c];
                dimensions.X += data.XAdvance;
                dimensions.Y = Math.Max(dimensions.Y, data.Height + data.YOffset);
            }
            return dimensions;
        }
    }
}
