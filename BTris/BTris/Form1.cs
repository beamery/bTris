using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.DevIl;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Engine;
using Engine.Input;

namespace BTris
{
    public partial class Form1 : Form
    {

        bool fullscreen = false;
        const int WIDTH = 800;
        const int HEIGHT = 600;
        TextureManager textureManager;
        StateSystem stateSystem;
        EffectsManager effectsManager;
        FastLoop fastLoop;
        Input input = new Input();
        GameData gameData = new GameData();

        public Form1()
        {
            InitializeComponent();
            openGlControl.InitializeContexts();

            input.Mouse = new Mouse(this, openGlControl);
            input.Keyboard = new Keyboard(openGlControl);

            InitializeDisplay();
            InitializeTextures();
            InitializeFonts();
            InitializeEffects();
            InitializeGameStates();

            fastLoop = new FastLoop(GameLoop);
        }

        private void InitializeGameStates()
        {
            stateSystem = new StateSystem();

            // Add game states
            stateSystem.AddState("start_menu", new StartMenuState(stateSystem, textureManager, input, gameData));
            stateSystem.AddState("controls", new ControlsState(input, stateSystem, textureManager));
            stateSystem.AddState("inner_game", new InnerGameState(input, stateSystem, textureManager, gameData, effectsManager));
            stateSystem.AddState("game_over", new GameOverState(input, stateSystem, gameData, textureManager));

            // Set starting state
            stateSystem.ChangeState("start_menu");
        }

        private void InitializeEffects()
        {
            effectsManager = new EffectsManager(textureManager);
            effectsManager.Add("double", "Double!");
            effectsManager.Add("triple", "Triple!");
            effectsManager.Add("tetris", "Tetris!");
            effectsManager.Add("level_up", "Level Up!");
        }

        private void InitializeFonts()
        {
            // Load fonts
            textureManager.LoadTexture("title_font", "titleFont_0.tga");
            textureManager.LoadTexture("menu_font", "menuFont_0.tga");
            textureManager.LoadTexture("effects_font", "effectsFont_0.tga");
        }

        private void InitializeTextures()
        {
            //Initialize DevIl
            Il.ilInit();
            Ilu.iluInit();
            Ilut.ilutInit();
            Ilut.ilutRenderer(Ilut.ILUT_OPENGL);
            
            textureManager = new TextureManager();

            // Load Textures
            textureManager.LoadTexture("block", "block.png");
        }

        private void InitializeDisplay()
        {
            if (fullscreen)
            {
                FormBorderStyle = FormBorderStyle.None;
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                ClientSize = new Size(WIDTH, HEIGHT);
            }
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void Setup2DGraphics(double width, double height)
        {
            double halfWidth = width / 2;
            double halfHeight = height / 2;
            Gl.glMatrixMode(Gl.GL_PROJECTION);
            Gl.glLoadIdentity();
            Gl.glOrtho(-halfWidth, halfWidth, -halfHeight, halfHeight, -100, 100);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();
        }

        protected override void OnClientSizeChanged(EventArgs e)
        {
            base.OnClientSizeChanged(e);
            Gl.glViewport(0, 0, ClientSize.Width, ClientSize.Height);
            Setup2DGraphics(ClientSize.Width, ClientSize.Height);
        }

        private void GameLoop(double elapsedTime)
        {
            input.Update(elapsedTime);
            stateSystem.Update(elapsedTime);
            stateSystem.Render();
            openGlControl.Refresh();
        }
    }
}
