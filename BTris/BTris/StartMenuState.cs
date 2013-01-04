using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace BTris
{
    class StartMenuState : IGameObject
    {
        StateSystem stateSystem;
        TextureManager textureManager;
        Input input;
        GameData gameData;
        Renderer renderer = new Renderer();

        Font titleFont;
        Text titleText;

        Font menuFont;
        VerticalMenu menu;

        public StartMenuState(StateSystem stateSystem, TextureManager textureManager, 
            Input input, GameData gameData)
        {
            this.input = input;
            this.textureManager = textureManager;
            this.stateSystem = stateSystem;
            this.gameData = gameData;

            // Create title
            titleFont = new Font(textureManager.Get("title_font"), FontParser.Parse("titleFont.fnt"));
            titleText = new Text("bTris", titleFont);
            titleText.SetPosition(-titleText.Width / 2, 150);
            titleText.SetColor(new Color(1, 1, 1, 1));

            InitializeMenu();
        }

        private void ResetGameData()
        {
            gameData.Score = 0;
            gameData.Level = 1;
            gameData.TotalBreaks = 0;
        }

        private void InitializeMenu()
        {
            menuFont = new Font(textureManager.Get("menu_font"), FontParser.Parse("menuFont.fnt"));
            menu = new VerticalMenu(0, -100, input, new Color(1, 1, 1, 1), new Color(0, 0, 1, 1));

            // Create start, controls, exit for menu
            Button start = new Button(delegate(object sender, EventArgs e)
            {
                stateSystem.ChangeState("inner_game");
                ResetGameData();
            },
            new Text("Start", menuFont));

            Button controls = new Button(delegate(object sender, EventArgs e)
            {
                stateSystem.ChangeState("controls");
            },
            new Text("Controls", menuFont));

            Button exit = new Button(delegate(object sender, EventArgs e)
            {
                System.Windows.Forms.Application.Exit();
            },
            new Text("Exit", menuFont));

            menu.AddButton(start);
            menu.AddButton(controls);
            menu.AddButton(exit);
        }

        public void Update(double elapsedTime)
        {
            menu.HandleInput();
        }

        public void Render()
        {
            Gl.glClearColor(0, 0, 0, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            renderer.DrawText(titleText);
            menu.Render(renderer);
        }
    }
}
