using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BTris
{
    public class GridPosition
    {
        double xPosition;
        double yPosition;
        bool isFilled = false;
        public double X { get { return xPosition; } }
        public double Y { get { return yPosition; } }
        Block block;
        public bool Filled 
        {
            get { return isFilled; }
            set { isFilled = value; }
        }

        public GridPosition(double x, double y)
        {
            xPosition = x;
            yPosition = y;
        }

        public void SetBlock(Block block)
        {
            this.block = block;
            isFilled = true;
        }

        public Block GetBlock()
        {
            return block;
        }

        public void RemoveBlock()
        {
            isFilled = false;
            block = null;
        }
    }
}
