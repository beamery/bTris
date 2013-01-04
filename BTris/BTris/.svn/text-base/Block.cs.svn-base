using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Tao.OpenGl;
using Engine;

namespace BTris
{
    public class Block : ICloneable
    {
        // size of block texture
        public static double WIDTH = 20;
        public static double HEIGHT = 20;

        public int gridPosX { get; set; }
        public int gridPosY { get; set; }

        Sprite sprite;
        double rotation = 0;
        public Engine.Color Color
        {
            get 
            {
                return Color;
            }
            set
            {
                sprite.SetColor(value);
            }
        }

        public Block(TextureManager textureManager, Engine.Color color, int gridPosX, int gridPosY)
        {
            sprite = new Sprite();
            sprite.Texture = textureManager.Get("block");
            sprite.SetScale(0.625, 0.625);
            sprite.SetColor(color);
            this.gridPosX = gridPosX;
            this.gridPosY = gridPosY;
        }

        public Block(TextureManager textureManager, Engine.Color color) :
            this(textureManager, color, 0, 0) { }

        public Block(TextureManager textureManager) : 
            this(textureManager, new Engine.Color(1, 0, 0, 1), 0, 0) { }

       
        public RectangleF GetBoundingRectangle()
        {
            return sprite.GetBoundingBox();
        }

        public Vector GetPosition()
        {
            return sprite.GetPosition();
        }

        public void SetPosition(Vector position)
        {
            sprite.SetPosition(position);
        }

        public void Rotate(double amount)
        {
            rotation += amount;
            sprite.SetRotation(rotation);
        }

        public void Update(double elapsedTime) 
        {
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawSprite(sprite);
            //renderer.DrawBoundingBox(sprite);
        }

        public object Clone()
        {
            Block clone = (Block)this.MemberwiseClone();

            return clone;
        }
    }
}
