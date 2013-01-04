using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    class IMino : Tetromino
    {
        bool[,] oPos = new bool[5, 5];
        bool[,] rPos = new bool[5, 5];
        bool[,] twoPos = new bool[5, 5];
        bool[,] lPos = new bool[5, 5];

        public IMino()
        {
            InitializeRotations();
            color = new Color(0, 0.9f, 0.9f, 1);
            ghostColor = new Color(color.Red, color.Green, color.Blue, 0.3f);
            key = 'i';
            width = 5;
        }

        private void InitializeRotations()
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    oPos[i, j] = false;
                    rPos[i, j] = false;
                    twoPos[i, j] = false;
                    lPos[i, j] = false;
                }
            }

            oPos[1, 2] = true;
            oPos[2, 2] = true;
            oPos[3, 2] = true;
            oPos[4, 2] = true;

            rPos[2, 1] = true;
            rPos[2, 2] = true;
            rPos[2, 3] = true;
            rPos[2, 4] = true;

            twoPos[0, 2] = true;
            twoPos[1, 2] = true;
            twoPos[2, 2] = true;
            twoPos[3, 2] = true;

            lPos[2, 0] = true;
            lPos[2, 1] = true;
            lPos[2, 2] = true;
            lPos[2, 3] = true;

            rotations.Add(oPos);
            rotations.Add(rPos);
            rotations.Add(twoPos);
            rotations.Add(lPos);
        }
    }
}
