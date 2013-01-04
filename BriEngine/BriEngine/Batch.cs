using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;

namespace Engine
{
    public class Batch
    {
        const int MaxVertexNumber = 1000;
        const int VertexDimensions = 3;
        const int ColorDimensions = 4;
        const int UVDimensions = 2;
        Vector[] vertexPositions = new Vector[MaxVertexNumber];
        Color[] vertexColors = new Color[MaxVertexNumber];
        Point[] vertexUVs = new Point[MaxVertexNumber];
        int batchSize = 0;
        Sprite current = new Sprite();

        public void AddSprite(Sprite sprite)
        {
            // If new texture is different from previous texture, draw and 
            // change to new texture.
            if (sprite.Texture.Id != current.Texture.Id)
            {
                Draw();
                Gl.glBindTexture(Gl.GL_TEXTURE_2D, sprite.Texture.Id);
            }
            current = sprite;
            
            //If the batch is full, draw it, empty and start again.
            if (sprite.VertexPositions.Length + batchSize > MaxVertexNumber)
            {
                Draw();
            }
            // Add the current sprite vertices to the batch.
            for (int i = 0; i < sprite.VertexPositions.Length; i++)
            {
                vertexPositions[batchSize + i] = sprite.VertexPositions[i];
                vertexColors[batchSize + i] = sprite.VertexColors[i];
                vertexUVs[batchSize + i] = sprite.VertexUVs[i];
            }
            batchSize += sprite.VertexPositions.Length;
        }

        void SetupPointers()
        {
            Gl.glEnableClientState(Gl.GL_COLOR_ARRAY);
            Gl.glEnableClientState(Gl.GL_VERTEX_ARRAY);
            Gl.glEnableClientState(Gl.GL_TEXTURE_COORD_ARRAY);
            Gl.glVertexPointer(VertexDimensions, Gl.GL_DOUBLE, 0,
                vertexPositions);
            Gl.glColorPointer(ColorDimensions, Gl.GL_FLOAT, 0, vertexColors);
            Gl.glTexCoordPointer(UVDimensions, Gl.GL_FLOAT, 0, vertexUVs);
        }

        public void Draw()
        {
            if (batchSize == 0)
            {
                return;
            }
            SetupPointers();
            Gl.glDrawArrays(Gl.GL_TRIANGLES, 0, batchSize);
            batchSize = 0;
        }
      
    }
}
