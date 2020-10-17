using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Utils
{
    internal class PlayerInformation
    {
        private static readonly List<Color> r_PlayersColor = new List<Color>() { Color.Blue, Color.Green };
        private const int k_MaxLife = 3;
        private readonly int r_PlayerIndex;
        private int m_PlayerIndex;
        private int m_CurrentScore;
        private int m_CurrentLife;
        private const int k_NumberOfPointToReduceDueToDying = -600;

        public PlayerInformation(int i_PlayerIndex)
        {
            r_PlayerIndex = i_PlayerIndex;
            m_CurrentScore = 0;
            m_CurrentLife = k_MaxLife;
        }

        public PlayerInformation(int i_PlayerIndex, int i_CurrentLife, int i_CurrentScore)
        {
            r_PlayerIndex = i_PlayerIndex;
            m_CurrentLife = i_CurrentLife;
            m_CurrentScore = i_CurrentScore;
        }

        public List<Color> PlayersColor;

        public Color Color
        {
            get
            {
                return r_PlayersColor[r_PlayerIndex];
            }
        }

        internal int CurrentLife
        {
            get
            {
                return m_CurrentLife;
            }
        }

        internal int CurrentScore
        {
            get
            {
                return m_CurrentScore;
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

        internal void UpdateScore(int i_Score)
        {
            m_CurrentScore = Math.Max(0, m_CurrentScore + i_Score);
        }

        internal void ReduceLife(SpaceShip i_SpaceShip, Game i_Game)
        {
            m_CurrentLife--;

            if (m_CurrentLife <= -1)
            {
                i_SpaceShip.SpaceShipGameOver();
                (i_Game as GameWithScreens).SpriteSoundEffects["GameOver"].Play();
            }
            else
            {
                (i_Game as GameWithScreens).SpriteSoundEffects["LifeDie"].Play();
            }

            UpdateScore(k_NumberOfPointToReduceDueToDying);
        }
    }
}
