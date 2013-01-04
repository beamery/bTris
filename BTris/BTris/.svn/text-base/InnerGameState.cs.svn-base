using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;
using Engine.Input;
using Tao.OpenGl;

namespace BTris
{
    class InnerGameState : IGameObject
    {
        Input input;
        StateSystem stateSystem;
        TextureManager textureManager;
        Renderer renderer = new Renderer();
        EffectsManager effectsManager;
        private static double timerMax = 60;
        double timer = timerMax;
        Level level;
        GameData gameData;

        public InnerGameState(Input input, StateSystem stateSystem, TextureManager textureManager,
            GameData gameData, EffectsManager effectsManager)
        {
            this.input = input;
            this.stateSystem = stateSystem;
            this.textureManager = textureManager;
            this.gameData = gameData;
            this.effectsManager = effectsManager;

            OnGameStart();
        }

        public void OnGameStart()
        {
            timer = timerMax;
            level = new Level(textureManager, input, gameData, effectsManager);
            gameData.GameOver = false;
        }

        public void Update(double elapsedTime)
        {
            level.Update(elapsedTime);
            timer -= elapsedTime;
            if (/*timer <= 0 || */gameData.GameOver)
            {
                 OnGameStart();
                stateSystem.ChangeState("game_over");
            }
        }

        public void Render()
        {
            Gl.glClearColor(0, 0, 0, 1);
            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT);
            level.Render(renderer);
        }
    }
}
