using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace BTris
{
    class GameOverState : IGameObject
    {
        Input input;
        StateSystem stateSystem;
        Renderer renderer = new Renderer();
        GameData gameData;
        Scoreboard scoreboard;
        TextureManager textureManager;

        Text gameOverText;
        Font gameOverFont;
        Text returnText;
        Font returnFont;

        public GameOverState(Input input, StateSystem stateSystem, 
            GameData gameData, TextureManager textureManager)
        {
            this.input = input;
            this.stateSystem = stateSystem;
            this.gameData = gameData;
            this.textureManager = textureManager;
            
            scoreboard = new Scoreboard(gameData, textureManager);
            scoreboard.SetPosition(new Point(-scoreboard.Width / 2, 0));

            gameOverFont = new Font(textureManager.Get("title_font"), FontParser.Parse("titleFont.fnt"));
            gameOverText = new Text("Game Over", gameOverFont);
            gameOverText.SetPosition(-gameOverText.Width / 2, 100);

            returnFont = new Font(textureManager.Get("effects_font"), FontParser.Parse("effectsFont.fnt"));
            returnText = new Text("Press Enter to return to the start menu", returnFont);
            returnText.SetPosition(-returnText.Width / 2, -200);
        }

        public void Update(double elapsedTime)
        {
            scoreboard.Update();
            scoreboard.SetPosition(new Point(-scoreboard.Width / 2, 0));
            if (input.Keyboard.IsKeyPressed(Keys.Enter))
            {
                stateSystem.ChangeState("start_menu");
            }
        }

        public void Render()
        {
            Gl.glClearColor(0, 0, 0, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            scoreboard.Render(renderer);
            renderer.DrawText(gameOverText);
            renderer.DrawText(returnText);
            renderer.Render();
        }
    }
}
