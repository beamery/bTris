using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class Scoreboard
    {
        GameData gameData;

        Text level;
        Text score;
        Font font;
        Point position;
        public float Width { get; set; }

        public Scoreboard(GameData gameData, TextureManager textureManager)
        {
            this.gameData = gameData;
            font = new Font(textureManager.Get("effects_font"), FontParser.Parse("effectsFont.fnt"));
            level = new Text("Level: " + gameData.Level.ToString(), font);
            score = new Text("Score : " + gameData.Score.ToString(), font);
            position = new Point((float)Block.WIDTH * 7, -30);
            UpdateWidth();
        }

        public void SetPosition(Point position)
        {
            this.position = position;
            level.SetPosition(position.X, position.Y);
            score.SetPosition(position.X, position.Y - 40);
        }

        private void UpdateWidth()
        {
            Width = (float)Math.Max(level.Width, score.Width);
        }
        public void Update()
        {
            level = new Text("Level: " + gameData.Level.ToString(), font);
            score = new Text("Score : " + gameData.Score.ToString(), font);
            level.SetPosition(position.X, position.Y);
            score.SetPosition(position.X, position.Y - 30);
            UpdateWidth();
        }

        public void Render(Renderer renderer)
        {
            renderer.DrawText(level);
            renderer.DrawText(score);
        }
    }
}
