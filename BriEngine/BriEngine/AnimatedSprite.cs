using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class AnimatedSprite : Sprite
    {
        int framesX;
        int framesY;
        int currentFrame = 0;
        double currentTimeFrame = 0.03;
        public double Speed { get; set; } // seconds per frame

        public bool Looping { get; set; }
        public bool Finished { get; set; }

        public AnimatedSprite()
        {
            Looping = false;
            Finished = false;
            Speed = 0.03; // ~30 frames per second
            currentTimeFrame = 0.03;
        }

        public System.Drawing.Point GetIndexFromFrame(int frame)
        {
            System.Drawing.Point point = new System.Drawing.Point();
            point.Y = frame / framesX;
            point.X = frame - (point.Y * framesY);
            return point;
        }

        private void UpdateUVs()
        {
            System.Drawing.Point index = GetIndexFromFrame(currentFrame);
            float frameWidth = 1.0f / framesX;
            float frameHeight = 1.0f / framesY;
            SetUVs(new Point(index.X * frameWidth, index.Y * frameHeight),
                new Point((index.X + 1) * frameWidth, (index.Y + 1) * frameHeight));
        }

        public void SetAnimation(int framesX, int framesY)
        {
            this.framesX = framesX;
            this.framesY = framesY;
            UpdateUVs();
        }

        private int GetFrameCount()
        {
            return framesX * framesY;
        }

        public void AdvanceFrame()
        {
            int numberOfFrames = GetFrameCount();
            currentFrame = (currentFrame + 1) % numberOfFrames;
        }

        public int GetCurrentFrame()
        {
            return currentFrame;
        }

        public void Update(double elapsedTime)
        {
            if (currentFrame == GetFrameCount() - 1 && !Looping)
            {
                Finished = true;
                return;
            }

            currentTimeFrame -= elapsedTime;
            if (currentTimeFrame < 0)
            {
                AdvanceFrame();
                currentTimeFrame = Speed;
                UpdateUVs();
            }
        }
    }

}
