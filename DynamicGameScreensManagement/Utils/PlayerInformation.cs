using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Utils
{
    internal class PlayerInformation
    {
        private static readonly List<Color> r_PlayresColor = new List<Color>() { Color.Blue, Color.Green };
        private const int k_MaxLife = 3;
        private readonly int r_PlayerIndex;
        private int m_CurrentScore;
        private int m_CurrentLife = k_MaxLife;
        private const int k_NumberOfPointToReduceDueToDying = -600;

        public PlayerInformation(int i_PlayerIndex)
        {
            r_PlayerIndex = i_PlayerIndex;
        }

        public Color Color
        {
            get
            {
                return r_PlayresColor[PlayerIndex];
            }
        }

        internal int CurrentLife
        {
            get
            {
                return m_CurrentLife;
            }
        }
        internal int PlayerIndex
        {
            get
            {
                return r_PlayerIndex;
            }
        }
        internal int MaxLife
        {
            get
            {
                return k_MaxLife;
            }
        }

        internal int CurrentScore
        {
            get
            {
                return m_CurrentScore;
            }
        }

        internal void UpdateScore(int i_Score)
        {
            m_CurrentScore = Math.Max(0, m_CurrentScore + i_Score);
        }

        internal void ReduceLife()
        {
            m_CurrentLife--;
            UpdateScore(k_NumberOfPointToReduceDueToDying);
        }
    }
}
