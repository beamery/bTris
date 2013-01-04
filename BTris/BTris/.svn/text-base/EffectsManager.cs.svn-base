using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Engine;

namespace BTris
{
    public class EffectsManager
    {
        List<Effect> activeEffects = new List<Effect>();
        Dictionary<string, Effect> effectsLibrary = new Dictionary<string, Effect>();
        TextureManager textureManager;

        public EffectsManager(TextureManager textureManager)
        {
            this.textureManager = textureManager;
        }

        public void Play(int numBreaks, double yPos)
        {
            switch (numBreaks)
            {
                case 2:
                    effectsLibrary["double"].Text.SetPosition(-effectsLibrary["double"].Text.Width / 2, yPos);
                    effectsLibrary["double"].Text.SetColor(new Color(0, 1, 1, 1));
                    effectsLibrary["double"].Movement = new Tween(yPos, yPos + 50, 4, Tween.EaseOutExpo);
                    effectsLibrary["double"].Color = new Tween(1, 0, 4, Tween.EaseOutExpo);
                    activeEffects.Add(effectsLibrary["double"]);
                    break;
                case 3:
                    effectsLibrary["triple"].Text.SetPosition(-effectsLibrary["triple"].Text.Width / 2, yPos);
                    effectsLibrary["triple"].Text.SetColor(new Color(1, 1, 0, 1));
                    effectsLibrary["triple"].Movement = new Tween(yPos, yPos + 50, 4, Tween.EaseOutExpo);
                    effectsLibrary["triple"].Color = new Tween(1, 0, 4, Tween.EaseOutExpo);
                    activeEffects.Add(effectsLibrary["triple"]);
                    break;
                case 4:
                    effectsLibrary["tetris"].Text.SetPosition(-effectsLibrary["tetris"].Text.Width / 2, yPos);
                    effectsLibrary["tetris"].Text.SetColor(new Color(0, 1, 0, 1));
                    effectsLibrary["tetris"].Movement = new Tween(yPos, yPos + 50, 4, Tween.EaseOutExpo);
                    effectsLibrary["tetris"].Color = new Tween(1, 0, 4, Tween.EaseOutExpo);
                    activeEffects.Add(effectsLibrary["tetris"]);
                    break;
                case 5:
                    effectsLibrary["level_up"].Text.SetPosition(-effectsLibrary["level_up"].Text.Width / 2, yPos);
                    effectsLibrary["level_up"].Text.SetColor(new Color(1, 0, 1, 1));
                    effectsLibrary["level_up"].Movement = new Tween(yPos, yPos + 50, 4);
                    effectsLibrary["level_up"].Color = new Tween(1, 0, 4);
                    activeEffects.Add(effectsLibrary["level_up"]);
                    break;
                default:
                    break;
            }
        }

        public void Add(string key, string value)
        {
            Text text = new Text(value, new Font(textureManager.Get("effects_font"), FontParser.Parse("effectsFont.fnt")));
            Effect effect = new Effect(text);
            effectsLibrary.Add(key, effect);
        }

        public void Update(double elapsedTime)
        {
            if (activeEffects.Count > 0)
            {
                foreach (Effect effect in activeEffects)
                {
                    effect.Update(elapsedTime);
                }
                for (int i = activeEffects.Count - 1; i >= 0; i--)
                {
                    if (activeEffects[i].Movement.IsFinished() || activeEffects[i].Color.IsFinished())
                    {
                        activeEffects.RemoveAt(i);
                    }
                }
            }
            else
            {
                activeEffects.Clear();
            }
        }

        public void Render(Renderer renderer)
        {
            if (activeEffects.Count > 0)
            {
                foreach (Effect effect in activeEffects)
                {
                    effect.Render(renderer);
                }
            }
        }
    }
}
