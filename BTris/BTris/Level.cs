using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tao.OpenGl;
using Engine;
using Engine.Input;
using System.Drawing;
using System.Windows.Forms;

namespace BTris
{
    public class Level
    {
        public GridPosition[,] grid = new GridPosition[18, 22];
        List<Block> outlineBlocks = new List<Block>();
       
        public List<int> breaks = new List<int>(); 
        bool locked = false;
        bool downHeld = false;
        Tween breakAnimation = new Tween(1, 0, 5);
        
        public const int startPosX = 3;
        public const int startPosY = 1;
        public const int maxPosX = 9; // gridPositions.Length - 1
        public const int maxPosY = 21; // gridPositions[].Length - 1
        
        double defaultSpeed = 1;
        double fastSpeed = 0.02;
        double lockSpeed = 0.2;
        double timeBetweenMoves;
        double timer; // Seconds per movement
        double lockTimer; // Seconds before mino locks
        
        RectangleF leftBound;
        RectangleF rightBound;
        RectangleF lowerBound;
        RectangleF upperBound;
        
        TextureManager textureManager;
        Input input;
        GameData gameData;
        TetrominoManager minoManager;
        EffectsManager effectsManager;
        Scoreboard scoreboard;

        Random ranGen = new Random();

        public Level(TextureManager textureManager, Input input, GameData gameData, EffectsManager effectsManager)
        {
            this.textureManager = textureManager;
            this.input = input;
            this.gameData = gameData;
            this.effectsManager = effectsManager;
            scoreboard = new Scoreboard(gameData, textureManager);

            // Initialize game data
            timeBetweenMoves = defaultSpeed;
            timer = timeBetweenMoves;
            lockTimer = lockSpeed;
            
            // Initialize boundaries
            InitBoundaries();
            
            // Initialize blocks
            InitGrid();
            minoManager = new TetrominoManager(this, textureManager, gameData);
            CreateMino();
            InitializeBoundingBlocks();
        }

        private void InitBoundaries()
        {
            leftBound = new RectangleF(-(float)(Block.WIDTH * 6), (float)(Block.HEIGHT * 11),
                (float)Block.WIDTH, -(float)(Block.HEIGHT * 22));

            rightBound = new RectangleF((float)(Block.WIDTH * 5), (float)(Block.HEIGHT * 11),
                (float)Block.WIDTH, -(float)(Block.HEIGHT * 22));

            lowerBound = new RectangleF(-(float)(Block.WIDTH * 6), -(float)(Block.HEIGHT * 11),
                (float)(Block.WIDTH * 12), -(float)Block.HEIGHT);

            upperBound = new RectangleF(-(float)(Block.WIDTH * 6), (float)(Block.HEIGHT * 12),
                (float)(Block.WIDTH * 12), -(float)Block.HEIGHT);
        }

        private void CreateMino()
        {
            minoManager.GetNextMino();
            timeBetweenMoves = defaultSpeed;
        }

        private void InitGrid()
        {
            double x = leftBound.Right + Block.WIDTH / 2;
            for (int i = 0; i < 18; i++)
            {
                double y = upperBound.Bottom - Block.HEIGHT / 2;
                for (int j = 0; j < 22; j++)
                {
                    grid[i, j] = new GridPosition(x, y);
                    y -= Block.HEIGHT;
                }
                x += Block.HEIGHT;
            }
        }

        private void InitializeBoundingBlocks()
        {
            Engine.Color blockColor = new Engine.Color(0.6f, 0.6f, 0.7f, 1);

            // Initialize left bound
            double x = leftBound.Left + Block.WIDTH / 2;
            double y = leftBound.Top - Block.HEIGHT / 2 - 2 * Block.HEIGHT;

            while (y > leftBound.Bottom)
            {
                Block block = new Block(textureManager);
                block.SetPosition(new Vector(x, y, 0));
                y -= Block.HEIGHT;
                block.Color = blockColor;
                outlineBlocks.Add(block);
            }

            // Initialize right bound
            x = rightBound.Left + Block.WIDTH / 2;
            y = rightBound.Top - Block.HEIGHT / 2 - 2 * Block.HEIGHT;

            while (y > leftBound.Bottom)
            {
                Block block = new Block(textureManager);
                block.SetPosition(new Vector(x, y, 0));
                y -= Block.HEIGHT;
                block.Color = blockColor;
                outlineBlocks.Add(block);
            }

            // Initialize upper bound
            x = upperBound.Left + Block.WIDTH / 2;
            y = upperBound.Top - Block.HEIGHT / 2 - 2 * Block.HEIGHT;

            while (x < upperBound.Right)
            {
                Block block = new Block(textureManager);
                block.SetPosition(new Vector(x, y, 0));
                x += Block.WIDTH;
                block.Color = blockColor;
                outlineBlocks.Add(block);
            }

            // Initialize lower bound
            x = lowerBound.Left + Block.WIDTH / 2;
            y = lowerBound.Top - Block.HEIGHT / 2;

            while (x < lowerBound.Right)
            {
                Block block = new Block(textureManager);
                block.SetPosition(new Vector(x, y, 0));
                x += Block.WIDTH;
                block.Color = blockColor;
                outlineBlocks.Add(block);
            }
        }

        public void DrawOutline(Renderer renderer)
        {
            foreach (Block block in outlineBlocks)
            {
                block.Render(renderer);
            }
        }

        public Block PlaceBlock(Block block, int x, int y)
        {
            block.SetPosition(new Vector(grid[x, y].X,
               grid[x, y].Y, 0));
            grid[x, y].SetBlock(block);
            block.gridPosX = x;
            block.gridPosY = y;

            return block;
        }

        #region Current Block Movements

        public void MoveBlockDown(Block block)
        {
            block.gridPosY++;
            PlaceBlock(block, block.gridPosX, block.gridPosY);
            grid[block.gridPosX, block.gridPosY - 1].RemoveBlock();
        }

        public void MoveBlockLeft(Block block)
        {
            block.gridPosX--;
            PlaceBlock(block, block.gridPosX, block.gridPosY);
            grid[block.gridPosX + 1, block.gridPosY].RemoveBlock();
            
        }

        public void MoveBlockRight(Block block)
        {
            block.gridPosX++;
            PlaceBlock(block, block.gridPosX, block.gridPosY);
            grid[block.gridPosX - 1, block.gridPosY].RemoveBlock();
        }

        public void MoveBlockUp(Block block)
        {
            block.gridPosY--;
            PlaceBlock(block, block.gridPosX, block.gridPosY);
            grid[block.gridPosX, block.gridPosY + 1].RemoveBlock();
        }

        public bool CanMoveDown(Block block)
        {
             return (block.gridPosY < maxPosY &&
                !grid[block.gridPosX, block.gridPosY + 1].Filled);

        }

        public bool CanMoveLeft(Block block)
        {
            return (block.gridPosX > 0 &&
                !grid[block.gridPosX - 1, block.gridPosY].Filled);
        }

        public bool CanMoveRight(Block block)
        {
            return (block.gridPosX < maxPosX &&
                !grid[block.gridPosX + 1, block.gridPosY].Filled);
        }

        public bool CanMoveUp(Block block)
        {
            return (block.gridPosY > 0 &&
                !grid[block.gridPosX, block.gridPosY - 1].Filled);
        }

        #endregion

        public void UpdateCurrentBlockPosition(double elapsedTime)
        {
            HandleInput();

            timer -= elapsedTime;
            if (timer <= 0)
            {
                if (minoManager.CanMoveDown(minoManager.Blocks))
                {
                    minoManager.MoveMinoDown();
                    timer = timeBetweenMoves;
                    if (downHeld)
                    {
                        gameData.Score++;
                    }
                }
                else
                {
                    lockTimer -= elapsedTime;
                    if (minoManager.minoPos.Y < 2)
                    {
                        gameData.GameOver = true;
                    }
                    if (lockTimer <= 0)
                    {
                        FinalizeMino(elapsedTime);
                    }
                }
            }
        }

        private void FinalizeMino(double elapsedTime)
        {
            if (!locked)
            {
                minoManager.LockMino();
                breaks = UpdateBreaks();
                locked = true;
                breakAnimation = new Tween(1, 0, 0.5);
                gameData.TotalBreaks += breaks.Count;
                
                // Update game score
                switch (breaks.Count)
                {
                    case 1:
                        gameData.Score += gameData.Level * 40;
                        break;
                    case 2:
                        gameData.Score += gameData.Level * 100;
                        break;
                    case 3:
                        gameData.Score += gameData.Level * 300;
                        break;
                    case 4:
                        gameData.Score += gameData.Level * 1200;
                        break;
                    default:
                        break;
                }

                if (breaks.Count > 1)
                {
                    effectsManager.Play(breaks.Count, grid[0, breaks[0]].Y);
                }
            }
            if (breaks.Count > 0)
            {

                if (gameData.TotalBreaks >= 5)
                {
                    gameData.TotalBreaks = gameData.TotalBreaks % 5;
                    LevelUp();
                }

                PlayAnimation(elapsedTime, breakAnimation);

                if (breakAnimation.IsFinished())
                {
                    BreakLines(breaks);
                    CreateMino();
                    timer = timeBetweenMoves + 0.3;
                    lockTimer = lockSpeed;
                    locked = false;
                }
            }
            else
            {
                CreateMino();
                timer = timeBetweenMoves + 0.3;
                lockTimer = lockSpeed;
                locked = false;
            }
        }

        private void LevelUp()
        {
            gameData.Level++;
            effectsManager.Play(5, 0);
            if (defaultSpeed > 0.5)
            {
                defaultSpeed -= 0.25;
            }
            else if (defaultSpeed > 0.1)
            {
                defaultSpeed -= 0.04;
            }
            else
            {
                defaultSpeed -= 0.005;
            }
        }

        private void PlayAnimation(double elapsedTime, Tween breakAnimation)
        {
            breakAnimation.Update(elapsedTime);
            foreach (int line in breaks)
            {
                for (int i = 0; i <= maxPosX; i++)
                {
                    grid[i, line].GetBlock().Color = new Engine.Color((float)breakAnimation.Value,
                        (float)breakAnimation.Value, (float)breakAnimation.Value, 1);
                }
            }
        }

        private void HandleInput()
        {
            if (input.Keyboard.IsKeyPressed(Keys.Space))
            {
                minoManager.MoveToBottom();
                lockTimer = 0;
                timer = Math.Min(timer, 0.1);
            }
            if (input.Keyboard.IsKeyPressed(Keys.Down))
            {
                timer = 0;
                downHeld = false;
                gameData.Score++;
            }
            else if (input.Keyboard.IsKeyHeld(Keys.Down))
            {
                timer = Math.Min(timer, 0.15);
                timeBetweenMoves = fastSpeed;
                downHeld = true;
            }
            else
            {
                timeBetweenMoves = defaultSpeed;
                downHeld = false;

            }
            if (input.Keyboard.IsKeyPressed(Keys.Up))
            {
                minoManager.RotateRight();
                lockTimer = lockSpeed;
            }

            if (input.Keyboard.IsKeyPressed(Keys.C))
            {
                minoManager.SwapMino();
            }
            if (input.Keyboard.IsKeyPressed(Keys.Z))
            {
                minoManager.RotateLeft();
                lockTimer = lockSpeed;
            }
            if (input.Keyboard.IsKeyPressed(Keys.X))
            {
                minoManager.RotateRight();
                lockTimer = lockSpeed;
            }
            if (input.Keyboard.IsKeyPressed(Keys.Left))
            {
                if (minoManager.CanMoveLeft(minoManager.Blocks))
                {
                    minoManager.MoveMinoLeft();
                    timer = Math.Max(timer, 0.1);
                }
            }
            if (input.Keyboard.IsKeyPressed(Keys.Right))
            {
                if (minoManager.CanMoveRight(minoManager.Blocks))
                {
                    minoManager.MoveMinoRight();
                    timer = Math.Max(timer, 0.1);
                }
            }
        }

        private List<int> UpdateBreaks()
        {
            List<int> breaks = new List<int>();
            for (int i = 0; i <= maxPosY; i++)
            {
                bool lineFull = true;
                for (int j = 0; j <= maxPosX; j++)
                {
                    if (!grid[j, i].Filled)
                    {
                        lineFull = false;
                        break;
                    }
                }
                if (lineFull)
                {
                    breaks.Add(i);
                }
            }
            return breaks;
        }

        private void BreakLines(List<int> lines)
        {
            foreach (int line in lines)
            {
                for (int i = (line - 1); i >= 0; i--)
                {
                    for (int j = 0; j <= maxPosX; j++)
                    {
                        grid[j, i + 1].RemoveBlock();
                        if (grid[j, i].Filled)
                        {
                            MoveBlockDown(grid[j, i].GetBlock());
                        }
                        else
                        {
                            grid[j, i + 1].Filled = false;
                        }
                        if (i == maxPosY)
                        {
                            grid[j, i].RemoveBlock();
                        }
                    }
                }
            }
        }

        public void Update(double elapsedTime)
        {
            UpdateCurrentBlockPosition(elapsedTime);
            minoManager.Update(elapsedTime);
            effectsManager.Update(elapsedTime);
            scoreboard.Update();
        }

        public void RenderGrid(Renderer renderer)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 2; j < 22; j++)
                {
                    if (grid[i, j].Filled)
                    {
                        grid[i, j].GetBlock().Render(renderer);
                    }
                }
            }
        }

        public void Render(Renderer renderer)
        {
            // Draw level outline
            DrawOutline(renderer);

            RenderGrid(renderer);
            minoManager.Render(renderer);
            effectsManager.Render(renderer);
            scoreboard.Render(renderer);
            renderer.Render();
        }
    }
}
