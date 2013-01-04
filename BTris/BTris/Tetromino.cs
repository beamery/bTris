using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class Tetromino : ICloneable
    {
        protected List<bool[,]> rotations = new List<bool[,]>();
        protected Color color;
        protected Color ghostColor;
        protected char key;
        protected int currentRotation = 0;
        protected int width;

        public int Width { get { return width; } }
        public int CurrentRotation { get { return currentRotation; } }
        public Color Color { get { return color; } set { color = value; } }
        public Color GhostColor { get { return ghostColor; } }
        public char Key { get { return key; } }
        public Point Position { get; set; }

        public void RotateRight()
        {
            currentRotation++;
            if (currentRotation == 4)
            {
                currentRotation = 0;
            }
        }

        public void RotateLeft()
        {
            currentRotation--;
            if (currentRotation < 0)
            {
                currentRotation = 3;
            }
        }

        public bool[,] GetCurrentRotation()
        {
            return rotations[currentRotation];
        }

        public bool[,] GetNextRotation()
        {
            int nextRotation = currentRotation + 1;
            if (nextRotation == 4)
            {
                nextRotation = 0;
            }
            return rotations[nextRotation];
        }

        public bool[,] GetPreviousRotation()
        {
            int previousRotation = currentRotation - 1;
            if (previousRotation < 0)
            {
                previousRotation = 3;
            }
            return rotations[previousRotation];
        }

        public static Tetromino CreateMino(int index)
        {
            switch (index)
            {
                case 0:
                    return new IMino();
                case 1:
                    return new JMino();
                case 2:
                    return new LMino();
                case 3:
                    return new OMino();
                case 4:
                    return new SMino();
                case 5:
                    return new TMino();
                case 6:
                    return new ZMino();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public object Clone()
        {
            Tetromino clone = (Tetromino)this.MemberwiseClone();
            return clone;
        }
    }
}
