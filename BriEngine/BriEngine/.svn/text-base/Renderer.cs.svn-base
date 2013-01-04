using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Engine
{
    public class Renderer
    {
        Batch batch = new Batch();
        public Renderer()
        {
            Gl.glEnable(Gl.GL_TEXTURE_2D);
            Gl.glEnable(Gl.GL_BLEND);
            Gl.glBlendFunc(Gl.GL_SRC_ALPHA, Gl.GL_ONE_MINUS_SRC_ALPHA);
        }

        public void DrawImmediateModeVertex(Vector position, Color color, Point uvs)
        {
            Gl.glColor4f(color.Red, color.Green, color.Blue, color.Alpha);
            Gl.glTexCoord2f(uvs.X, uvs.Y);
            Gl.glVertex3d(position.X, position.Y, position.Z);
        }

        public void DrawBoundingBox(Sprite sprite)
        {
            RectangleF rect = sprite.GetBoundingBox();
            Gl.glBegin(Gl.GL_LINE_LOOP);
            {
                Gl.glVertex2f(rect.Left, rect.Top);
                Gl.glVertex2f(rect.Right, rect.Top);
                Gl.glVertex2f(rect.Right, rect.Bottom);
                Gl.glVertex2f(rect.Left, rect.Bottom);
            }
            Gl.glEnd();
        }

        public void DrawSprite(Sprite sprite)
        {
            batch.AddSprite(sprite);
        }
        public void Render()
        {
            batch.Draw();
        }

        public void DrawText(Text text)
        {
            foreach (CharacterSprite c in text.CharacterSprites)
            {
                DrawSprite(c.Sprite);
            }
        }
    }
}
