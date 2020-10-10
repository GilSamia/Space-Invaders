using Microsoft.Xna.Framework;
using System;

namespace SpaceInvaders.Utils
{
    internal class RandomTimer
    {
        private readonly Random r_Random = new Random();
        private readonly int r_MaxTimeToWait;
        public event EventHandler TimerTick;

        private int m_TimeToTick;
        private float currentTime = 0f;

        public RandomTimer(int i_MaxTimeToWait)
        {
            r_MaxTimeToWait = i_MaxTimeToWait;
            Restart();
        }

        public void Restart()
        {
            currentTime -= m_TimeToTick;
            m_TimeToTick = r_Random.Next(1, r_MaxTimeToWait);
        }

        public void CheckTimer(GameTime i_GameTime)
        {
            currentTime += (float)i_GameTime.ElapsedGameTime.TotalSeconds;

            if (currentTime >= m_TimeToTick)
            {
                TimerTick?.Invoke(this, null);
                Restart();
            }
        }
    }
}
