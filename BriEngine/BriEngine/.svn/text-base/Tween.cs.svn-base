using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Engine
{
    public class Tween
    {
        double original;
        double distance;
        double current;
        double totalTimePassed = 0;
        double totalDuration = 5;
        bool finished = false;
        TweenFunction tweenF = null;
        public Double Value { get { return current; } }

        public delegate double TweenFunction(double timePassed, double start, double distance, double duration);

        public bool IsFinished()
        {
            return finished;
        }

        #region Static Tween Functions

        public static double Linear(double timePassed, double start, double distance, double duration)
        {
            return distance * (timePassed / duration) + start;
        }

        public static double EaseOutExpo(double timePassed, double start, double distance, double duration)
        {
            if (timePassed == duration)
            {
                return start + distance;
            }
            return distance * (-Math.Pow(2, -10 * timePassed / duration) + 1) + start;
        }

        public static double EaseInExpo(double timePassed, double start, double distance, double duration)
        {
            if (timePassed == 0)
            {
                return start;
            }
            return distance * Math.Pow(2, 10 * (timePassed / duration - 1)) + start;
        }

        public static double EaseOutCirc(double timePassed, double start, double distance, double duration)
        {
            return distance * Math.Sqrt(1 - (timePassed = timePassed / duration - 1));
        }

        public static double EaseInCirc(double timePassed, double start, double distance, double duration)
        {
            return -distance * (Math.Sqrt(1 - (timePassed /= duration) * timePassed) - 1) + start;
        }

        #endregion

        public Tween(double start, double end, double time, TweenFunction tweenF)
        {
            distance = end - start;
            original = start;
            current = start;
            totalDuration = time;
            this.tweenF = tweenF;
        }

        public Tween(double start, double end, double time) : this(start, end, time, Tween.Linear) { }

        public void Update(double elapsedTime)
        {
            totalTimePassed += elapsedTime;
            current = tweenF(totalTimePassed, original, distance, totalDuration);

            if (totalTimePassed > totalDuration)
            {
                current = original + distance;
                finished = true;
            }
        }
    }
}
