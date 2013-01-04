using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class TetrominoManager
    {
        Level level;
        TextureManager textureManager;
        Tetromino mino;
        MinoQueue minoQueue;
        HoldArea holdArea;
        GameData gameData;

        public List<Block> Blocks = new List<Block>();
        List<Block> ghostBlocks = new List<Block>();
        int maxPosX;
        int maxPosY;
        bool positionChanged;
        public Point minoPos = new Point();
        char current;

        OffsetData iOffset = new OffsetData('i');
        OffsetData oOffset = new OffsetData('o');
        OffsetData normOffset = new OffsetData('j');
        OffsetData currentOffset;

        public TetrominoManager(Level level, TextureManager textureManager, GameData gameData)
        {
            this.level = level;
            this.textureManager = textureManager;
            this.gameData = gameData;

            holdArea = new HoldArea(level, this, textureManager);
            positionChanged = false;
            minoQueue = new MinoQueue(this, level, textureManager);
        }

        public void GetNextMino()
        {
            mino = minoQueue.GetMino();
            current = mino.Key;
            if (current == 'i')
            {
                currentOffset = iOffset;
            }
            else if (current == 'o')
            {
                currentOffset = oOffset;
            }
            else
            {
                currentOffset = normOffset;
            }

            maxPosX = 10 - mino.Width;
            maxPosY = 20 - mino.Width;
            minoPos.X = Level.startPosX;
            minoPos.Y = Level.startPosY;

            positionChanged = true;
            UpdateMinoPosition();
        }

        public void LockMino()
        {
            foreach (Block block in Blocks)
            {
                level.PlaceBlock((Block)block.Clone(), 
                    block.gridPosX, block.gridPosY);
            }
            foreach (Block block in ghostBlocks)
            {
                block.Color = new Color(0, 0, 0, 0);
            }
        }

        #region Mino Movements

        public bool CanMoveRight(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                if (!level.CanMoveRight(block))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMoveLeft(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                if (!level.CanMoveLeft(block))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMoveUp(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                if (!level.CanMoveUp(block))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CanMoveDown(List<Block> blocks)
        {
            foreach (Block block in blocks)
            {
                if (!level.CanMoveDown(block))
                {
                    return false;
                }
            }
            return true;
        }

        public void MoveMinoUp()
        {
            minoPos.Y--;
            positionChanged = true;
        }

        public void MoveMinoDown()
        {
            minoPos.Y++;
            positionChanged = true;
        }

        public void MoveMinoLeft()
        {
            minoPos.X--;
            positionChanged = true;
        }

        public void MoveMinoRight()
        {
            minoPos.X++;
            positionChanged = true;
        }

        public void MoveToBottom()
        {
            while (CanMoveDown(Blocks))
            {
                MoveMinoDown();
                UpdateMinoPosition();
                gameData.Score += 2;
            }
        }

        public void MoveGhostToBottom()
        {
            while (CanMoveDown(ghostBlocks))
            {
                foreach (Block block in ghostBlocks)
                {
                    block.gridPosY++;
                }
            }
        }

        #endregion

        #region Mino Rotations

        public void RotateRight()
        {
            if (CanPlace(mino.GetNextRotation()))
            {
                mino.RotateRight();
                positionChanged = true;
            }
            else
            {
                TryWallKick('r', mino.GetNextRotation());
            }
        }

        public void RotateLeft()
        {
            if (CanPlace(mino.GetPreviousRotation()))
            {
                mino.RotateLeft();
                positionChanged = true;
            }
            else
            {
                TryWallKick('l', mino.GetPreviousRotation());
            }
        }

        private bool CanPlace(bool[,] minoGrid, float xPos, float yPos)
        {
            for (int i = 0; i < mino.Width; i++)
            {
                for (int j = 0; j < mino.Width; j++)
                {
                    if (minoGrid[i, j] == true)
                    {
                        if (xPos + i < 0 || xPos + i > Level.maxPosX ||
                            yPos + j < 0 || yPos + j > Level.maxPosY ||
                            level.grid[(int)xPos + i, (int)yPos + j].Filled)
                        {
                            return false;
                        }
                    }
                }

            }
            return true;
        }

        private bool CanPlace(bool[,] minoGrid)
        {
            return CanPlace(minoGrid, minoPos.X, minoPos.Y);
        }

        private void TryWallKick(char rotationDir, bool[,] nextRotation)
        {
            Point[] offsetValues;
            if (rotationDir == 'r')
            {
                offsetValues = currentOffset.GetOffset(mino.CurrentRotation, (mino.CurrentRotation + 1) % 4);
            }
            else
            {
                offsetValues = currentOffset.GetOffset(mino.CurrentRotation, (mino.CurrentRotation + 3) % 4);
            }
            float xOffset;
            float yOffset;
            int offsetIndex = 0;
            do {
                xOffset = offsetValues[offsetIndex].X;
                yOffset = offsetValues[offsetIndex].Y;
                offsetIndex++;

            } while (offsetIndex < offsetValues.Length &&
                !CanPlace(nextRotation, minoPos.X + xOffset, minoPos.Y + yOffset));

            if (CanPlace(nextRotation, minoPos.X + xOffset, minoPos.Y + yOffset))
            {
                minoPos = new Point(minoPos.X + xOffset, minoPos.Y + yOffset);
                if (rotationDir == 'r')
                {
                    RotateRight();
                }
                else
                {
                    RotateLeft();
                }
            }
        }

        #endregion

        public void SwapMino()
        {
            mino = holdArea.Swap(mino);
            
            if (minoPos.X <= 0)
            {
                minoPos.X = 0;
            }
            else if (minoPos.X >= maxPosX)
            {
                minoPos.X = maxPosX;
            }
            if (mino == null)
            {
                mino = minoQueue.GetMino();
                current = mino.Key;
                if (current == 'i')
                {
                    currentOffset = iOffset;
                }
                else if (current == 'o')
                {
                    currentOffset = oOffset;
                }
                else
                {
                    currentOffset = normOffset;
                }
            }
            positionChanged = true;
            UpdateMinoPosition();
        }

        private void UpdateMinoPosition()
        {
            if (positionChanged)
            {
                // Reset the list
                Blocks.Clear();
                ghostBlocks.Clear();
                // Add in the new block positions
                for (int i = 0; i < mino.Width; i++)
                {
                    for (int j = 0; j < mino.Width; j++)
                    {
                        if (mino.GetCurrentRotation()[i, j] == true)
                        {
                            Blocks.Add(new Block(textureManager, mino.Color,
                                (int)minoPos.X + i, (int)minoPos.Y + j));

                            ghostBlocks.Add(new Block(textureManager, mino.GhostColor,
                                (int)minoPos.X + i, (int)minoPos.Y + j));
                        }
                    }
                }
            }
            MoveGhostToBottom();
            positionChanged = false;
        }

        public void Update(double elapsedTime)
        {
            UpdateMinoPosition();
            minoQueue.Update();
            holdArea.Update();
        }

        public void Render(Renderer renderer)
        {
            foreach (Block block in Blocks)
            {
                if (block.gridPosY > 1)
                {
                    block.SetPosition(new Vector(level.grid[block.gridPosX, block.gridPosY].X,
                        level.grid[block.gridPosX, block.gridPosY].Y, 0));

                    block.Render(renderer);
                }
            }
            foreach (Block block in ghostBlocks)
            {
                if (block.gridPosY > 1)
                {
                    block.SetPosition(new Vector(level.grid[block.gridPosX, block.gridPosY].X,
                        level.grid[block.gridPosX, block.gridPosY].Y, 0));

                    block.Render(renderer);
                }
            }
            minoQueue.Render(renderer);
            holdArea.Render(renderer);
        }

    }
}
