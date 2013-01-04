using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class MinoQueue
    {
        Queue<Tetromino> minoQueue = new Queue<Tetromino>();
        List<Block> blocks = new List<Block>();
        Tetromino[] renderArray = new Tetromino[3];
        Point position;
        Point blockPointer;

        TetrominoManager minoManager;
        Level level;
        TextureManager textureManager;

        List<int> minoList = new List<int>();
        Random ranGen = new Random();

        int leftEdge = 12;
        int topEdge = 1;

        public MinoQueue(TetrominoManager minoManager, Level level, TextureManager textureManager)
        {
            this.minoManager = minoManager;
            this.level = level;
            this.textureManager = textureManager;

            CreateNewMinoList();
            AddMino();
            AddMino();
            AddMino();

            position = new Point(leftEdge, topEdge);
            blockPointer = position;
        }

        public Tetromino GetMino()
        {
            Tetromino temp = minoQueue.Dequeue();
            AddMino();
            blocks.Clear();

            return temp;
        }

        public void AddMino()
        {
            int id = minoList[0];
            minoList.RemoveAt(0);
            if (minoList.Count == 0)
            {
                CreateNewMinoList();
            }
            minoQueue.Enqueue(Tetromino.CreateMino(id));
        }

        private void CreateNewMinoList()
        {
            int id;
            for (int i = 0; i < 7; i++)
            {
                do
                {
                    id = ranGen.Next(7);
                } while (minoList.Contains(id));

                minoList.Add(id);
            }
        }

        public void Update()
        {
            int yOffset = 0;
            renderArray = minoQueue.ToArray();

            for (int i = 0; i < renderArray.Length; i++)
            {
                renderArray[i].Position = new Point(leftEdge, topEdge + yOffset);
                yOffset += renderArray[i].Width;
            }

            foreach (Tetromino mino in renderArray)
            {
                for (int j = 0; j < mino.Width; j++)
                {
                    for (int i = 0; i < mino.Width; i++)
                    {
                        if (mino.GetCurrentRotation()[i, j] == true)
                        {
                            blocks.Add(new Block(textureManager, mino.Color,
                                (int)mino.Position.X + i, (int)mino.Position.Y + j));

                        }
                    }

                }
            }
        }

        public void Render(Renderer renderer)
        {
            foreach (Block block in blocks)
            {
                block.SetPosition(new Vector(level.grid[block.gridPosX, block.gridPosY].X, 
                    level.grid[block.gridPosX, block.gridPosY].Y, 0));
                block.Render(renderer);
            }
        }
    }
}
