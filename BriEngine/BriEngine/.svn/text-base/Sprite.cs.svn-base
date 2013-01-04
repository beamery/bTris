using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Engine
{
    public class Sprite
    {
        internal const int VertexAmount = 6;
        Vector[] vertexPositions = new Vector[VertexAmount];
        Color[] vertexColors = new Color[VertexAmount];
        Point[] vertexUVs = new Point[VertexAmount];
        Texture texture = new Texture();

        double scaleX = 1;
        public double ScaleX { get { return scaleX; } }
        double scaleY = 1;
        public double ScaleY { get { return scaleY; } }
        double rotation = 0;
        double positionX = 0;
        double positionY = 0;

        public Sprite()
        {
            InitVertexPositions(new Vector(0, 0, 0), 1, 1);
            SetColor(new Color(1, 1, 1, 1));
            SetUVs(new Point(0, 0), new Point(1, 1));
        }

        public Texture Texture
        {
            get { return texture; }
            set
            {
                texture = value;
                // By default the width and height is set
                // to that of the texture
                InitVertexPositions(GetCenter(), texture.Width, texture.Height);
            }
        }

        public Vector[] VertexPositions
        {
            get { return vertexPositions; }
        }

        public Color[] VertexColors
        {
            get { return vertexColors; }
        }

        public Point[] VertexUVs
        {
            get { return vertexUVs; }
        }

        private Vector GetCenter()
        {
            double halfWidth = GetWidth() / 2;
            double halfHeight = GetHeight() / 2;

            return new Vector(
                vertexPositions[0].X + halfWidth,
                vertexPositions[0].Y - halfHeight,
                vertexPositions[0].Z);
        }

        private void InitVertexPositions(Vector position, double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            // Clockwise creation of two triangles to make a quad

            // TopLeft, TopRight, BottomLeft
            vertexPositions[0] = new Vector(position.X - halfWidth, position.Y +
                halfHeight, position.Z);
            vertexPositions[1] = new Vector(position.X + halfWidth, position.Y +
                halfHeight, position.Z);
            vertexPositions[2] = new Vector(position.X - halfWidth, position.Y -
                halfHeight, position.Z);

            // TopRight, BottomRight, BottomLeft
            vertexPositions[3] = new Vector(position.X + halfWidth, position.Y +
                halfHeight, position.Z);
            vertexPositions[4] = new Vector(position.X + halfWidth, position.Y -
                halfHeight, position.Z);
            vertexPositions[5] = new Vector(position.X - halfWidth, position.Y -
                halfHeight, position.Z);
        }

        public double GetWidth()
        {
            // topright - topleft
            return vertexPositions[1].X - vertexPositions[0].X;
        }

        public double GetHeight()
        {
            // topleft - bottomleft
            return vertexPositions[0].Y - vertexPositions[2].Y;
        }

        public Vector GetPosition()
        {
            return GetCenter();
        }

        public RectangleF GetBoundingBox()
        {
            return new RectangleF((float)(GetPosition().X - GetWidth() / 2),
                (float)(GetPosition().Y + GetHeight() / 2), (float)GetWidth(), -(float)GetHeight());
        }

        public void SetWidth(float width)
        {
            InitVertexPositions(GetCenter(), width, GetHeight());
        }

        public void SetHeight(float height)
        {
            InitVertexPositions(GetCenter(), GetWidth(), height);
        }

        public void SetPosition(double x, double y)
        {
            SetPosition(new Vector(x, y, 0));
        }

        public void SetPosition(Vector position)
        {
            Matrix m = new Matrix();
            m.SetTranslation(new Vector(positionX, positionY, 0));
            ApplyMatrix(m.Inverse());
            m.SetTranslation(position);
            ApplyMatrix(m);
            positionX = position.X;
            positionY = position.Y;
        }

        public void SetScale(double x, double y)
        {
            double oldX = positionX;
            double oldY = positionY;
            SetPosition(0, 0);
            Matrix mScale = new Matrix();
            mScale.SetScale(new Vector(scaleX, scaleY, 1));
            mScale = mScale.Inverse();
            ApplyMatrix(mScale);
            mScale = new Matrix();
            mScale.SetScale(new Vector(x, y, 1));
            ApplyMatrix(mScale);
            SetPosition(oldX, oldY);
            scaleX = x;
            scaleY = y;
        }

        public void SetRotation(double rotation)
        {
            double oldX = positionX;
            double oldY = positionY;
            SetPosition(0, 0);
            Matrix mRot = new Matrix();
            mRot.SetRotate(new Vector(0, 0, 1), this.rotation);
            ApplyMatrix(mRot.Inverse());
            mRot = new Matrix();
            mRot.SetRotate(new Vector(0, 0, 1), rotation);
            ApplyMatrix(mRot);
            SetPosition(oldX, oldY);
            this.rotation = rotation;
        }

        public void SetColor(Color color)
        {
            for (int i = 0; i < Sprite.VertexAmount; i++)
            {
                vertexColors[i] = color;
            }
        }

        public void SetUVs(Point topLeft, Point bottomRight)
        {
            // TopLeft, TopRight, BottomLeft
            vertexUVs[0] = topLeft;
            vertexUVs[1] = new Point(bottomRight.X, topLeft.Y);
            vertexUVs[2] = new Point(topLeft.X, bottomRight.Y);

            // TopRight, BottomRight, BottomLeft
            vertexUVs[3] = new Point(bottomRight.X, topLeft.Y);
            vertexUVs[4] = bottomRight;
            vertexUVs[5] = new Point(topLeft.X, bottomRight.Y);
        }

        public void ApplyMatrix(Matrix m)
        {
            for (int i = 0; i < VertexPositions.Length; i++)
            {
                VertexPositions[i] *= m;
            }
        }


    }
}
