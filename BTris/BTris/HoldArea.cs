using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class HoldArea
    {
        Tetromino mino;
        Point position = new Point();
        GridPosition[,] grid;
        List<Block> blocks = new List<Block>();
        Level level;
        TetrominoManager minoManager;
        TextureManager textureManager;

        public HoldArea(Level level, TetrominoManager minoManager, TextureManager textureManager)
        {
            this.level = level;
            this.minoManager = minoManager;
            this.textureManager = textureManager;
            position.X = (float)(level.grid[0, 0].X - Block.WIDTH);
            position.Y = (float)(level.grid[0, 0].Y);
            grid = new GridPosition[5, 5];
            InitGrid();
        }

        private void InitGrid() 
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    grid[i, j] = new GridPosition(position.X + Block.WIDTH * i, 
                        position.Y + Block.HEIGHT * j);
                }
            }
        }

        public Tetromino Swap(Tetromino otherMino)
        {
            Tetromino temp;
            if (mino != null)
            {
                temp = (Tetromino)mino.Clone();
            }
            else
            {
                temp = null;
            }
            mino = (Tetromino)otherMino.Clone();
            return temp;
        }

        public void Update()
        {
            blocks.Clear();
            bool hasBlocks = false;
            if (mino != null)
            {
                // Add in the new block positions
                int y = 0;
                for (int j = 0; j < Math.Min(mino.Width, 5); j++)
                {
                    for (int i = 0; i < mino.Width; i++)
                    {
                        if (mino.GetCurrentRotation()[i, j] == true)
                        {
                            blocks.Add(new Block(textureManager, mino.Color, i, y));
                            hasBlocks = true;
                        }
                    }
                    if (hasBlocks)
                    {
                        y++;
                    }
                }
            }
        }

        public void Render(Renderer renderer)
        {
            foreach (Block block in blocks)
            {
                block.SetPosition(new Vector(grid[block.gridPosX, block.gridPosY].X, 
                    grid[block.gridPosX, block.gridPosY].Y, 0));

                block.Render(renderer);
            }
        }
    }
}
