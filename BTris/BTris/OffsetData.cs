using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class OffsetData
    {
        #region Offset Data

        // JLSTZ offset data
        static Point[] zeroOffset = { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        static Point[] rOffset = { new Point(0, 0), new Point(1, 0), new Point(1, -1), new Point(0, 2), new Point(1, 2) };
        static Point[] twoOffset = { new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0), new Point(0, 0) };
        static Point[] lOffset = { new Point(0, 0), new Point(-1, 0), new Point(-1, -1), new Point(0, 2), new Point(-1, 2) };

        // I offset data
        static Point[] zeroOffsetI = { new Point(0, 0), new Point(-1, 0), new Point(2, 0), new Point(-1, 0), new Point(2, 0) };
        static Point[] rOffsetI = { new Point(-1, 0), new Point(0, 0), new Point(0, 0), new Point(0, 1), new Point(0, -2) };
        static Point[] twoOffsetI = { new Point(-1, 1), new Point(1, 1), new Point(-2, 1), new Point(1, 0), new Point(-2, 0) };
        static Point[] lOffsetI = { new Point(0, 1), new Point(0, 1), new Point(0, 1), new Point(0, -1), new Point(0, 2) };

        // O offset data
        static Point[] zeroOffsetO = { new Point(0, 0) };
        static Point[] rOffsetO = { new Point(0, -1) };
        static Point[] twoOffsetO = { new Point(-1, -1) };
        static Point[] lOffsetO = { new Point(-1, 0) };

        #endregion

        Point[,] offsetData;

        // Actual offset data
        Point[] zero;
        Point[] r;
        Point[] two;
        Point[] l;

        public OffsetData(char id)
        {
            if (id == 'i')
            {
                zero = zeroOffsetI;
                r = rOffsetI;
                two = twoOffsetI;
                l = lOffsetI;
            }
            else if (id == 'o')
            {
                zero = zeroOffsetO;
                r = rOffsetO;
                two = twoOffsetO;
                l = lOffsetO;
            }
            else
            {
                zero = zeroOffset;
                r = rOffset;
                two = twoOffset;
                l = lOffset;
            }
            offsetData = new Point[4, zero.Length];
            InitializeOffsetData();
        }

        private void InitializeOffsetData()
        {
            for (int i = 0; i < zero.Length; i++)
            {
                offsetData[0, i] = zero[i];
                offsetData[1, i] = r[i];
                offsetData[2, i] = two[i];
                offsetData[3, i] = l[i];
            }
        }

        public Point[] GetOffset(int from, int to)
        {
            Point[] offset = new Point[zero.Length];

            for (int i = 0; i < zero.Length; i++)
            {
                offset[i] = offsetData[from, i] - offsetData[to, i];
            }

            return offset;
        }
    }
}
