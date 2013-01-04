using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    class SMino : Tetromino
    {
        bool[,] oPos = new bool[3, 3];
        bool[,] rPos = new bool[3, 3];
        bool[,] twoPos = new bool[3, 3];
        bool[,] lPos = new bool[3, 3];

        public SMino()
        {
            InitializeRotations();
            color = new Color(0, 0.8f, 0, 1);
            ghostColor = new Color(color.Red, color.Green, color.Blue, 0.3f);
            key = 's';
            width = 3;
        }

        private void InitializeRotations()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    oPos[i, j] = false;
                    rPos[i, j] = false;
                    twoPos[i, j] = false;
                    lPos[i, j] = false;
                }
            }

            oPos[1, 0] = true;
            oPos[2, 0] = true;
            oPos[0, 1] = true;
            oPos[1, 1] = true;

            rPos[1, 0] = true;
            rPos[1, 1] = true;
            rPos[2, 1] = true;
            rPos[2, 2] = true;

            twoPos[0, 2] = true;
            twoPos[1, 1] = true;
            twoPos[1, 2] = true;
            twoPos[2, 1] = true;

            lPos[0, 0] = true;
            lPos[0, 1] = true;
            lPos[1, 1] = true;
            lPos[1, 2] = true;

            rotations.Add(oPos);
            rotations.Add(rPos);
            rotations.Add(twoPos);
            rotations.Add(lPos);
        }
    }
}
