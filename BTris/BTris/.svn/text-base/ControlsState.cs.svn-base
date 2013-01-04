using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Engine.Input;
using Engine;

namespace BTris
{
    class ControlsState : IGameObject
    {
        Input input;
        StateSystem stateSystem;
        TextureManager textureManager;
        Renderer renderer = new Renderer();

        private static double timerMax = 2;
        double timer = timerMax;

        Font font;
        Text moveLeft;
        Text moveRight;
        Text softDrop;
        Text hardDrop;
        Text rotateClockwise;
        Text rotateCounterClockwise;
        Text holdBlock;
        Text returnText;

        public ControlsState(Input input, StateSystem stateSystem, 
            TextureManager textureManager)
        {
            this.input = input;
            this.stateSystem = stateSystem;
            this.textureManager = textureManager;
            font = new Font(textureManager.Get("effects_font"), FontParser.Parse("effectsFont.fnt"));
            InitText();
        }

        private void InitText()
        {   
            moveLeft = new Text("Move Left - Left Arrow", font);
            moveLeft.SetPosition(-moveLeft.Width / 2, 220);
            moveRight = new Text("Move Right - Right Arrow", font);
            moveRight.SetPosition(-moveRight.Width / 2, 160);
            softDrop = new Text("Soft Drop - Down Arrow", font);
            softDrop.SetPosition(-softDrop.Width / 2, 100);
            hardDrop = new Text("Hard Drop - Spacebar", font);
            hardDrop.SetPosition(-moveLeft.Width / 2, 40);
            rotateClockwise = new Text("Rotate Clockwise - Up Arrow, X", font);
            rotateClockwise.SetPosition(-rotateClockwise.Width / 2, -20);
            rotateCounterClockwise = new Text("Rotate Counterclockwise - Z", font);
            rotateCounterClockwise.SetPosition(-rotateCounterClockwise.Width / 2, -80);
            holdBlock = new Text("Hold Block - C", font);
            holdBlock.SetPosition(-holdBlock.Width / 2, -140);
            returnText = new Text("Press Enter to return to the start menu", font);
            returnText.SetPosition(-returnText.Width / 2, -200);
        }

        public void Update(double elapsedTime)
        {
            Gl.glBindTexture(Gl.GL_TEXTURE_2D, textureManager.Get("effects_font").Id);
            if (input.Keyboard.IsKeyPressed(Keys.Enter))
            {
                stateSystem.ChangeState("start_menu");
            }
        }

        public void Render()
        {
            Gl.glClearColor(0, 0, 0, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);

            renderer.DrawText(moveLeft);
            renderer.DrawText(moveRight);
            renderer.DrawText(softDrop);
            renderer.DrawText(hardDrop);
            renderer.DrawText(rotateClockwise);
            renderer.DrawText(rotateCounterClockwise);
            renderer.DrawText(holdBlock);
            renderer.DrawText(returnText);

            renderer.Render();
        }
    }
}
