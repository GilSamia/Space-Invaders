using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceInvaders.Animations;
using SpaceInvaders.Interfaces;
using System;


namespace SpaceInvaders.Sprites.SpaceShips
{
    class LifeAndScore
    {
        private readonly SpaceShip r_SpaceShip;
        internal readonly string r_AssetName = @"Sprites\Ship01_32x32";
        private GameScreen m_GameScreen;

        private int m_CurrentScore;
        private int m_CurrentNumberOfLife;

        public LifeAndScore(GameScreen i_Game, SpaceShip i_SpaceShip)
        {
            r_SpaceShip = i_SpaceShip;
            m_GameScreen = i_Game;
        }

        public int CurrentNumberOfLife => m_CurrentNumberOfLife;

        public int CurrentScore => m_CurrentScore;

        public void ReduceLife()
        {
            int currentLifeToReduce = r_SpaceShip.PlayerInformation.CurrentLife;
            if (currentLifeToReduce <= -1)
            {
                r_SpaceShip.SpaceShipGameOver();
            }
            else
            {
                (m_GameScreen.Game as GameWithScreens).SpriteSoundEffects["LifeDie"].Play();
                //m_GameScreen.Remove(this);
                //Dispose();
                //TODO: understand that it needs to be disposed..
            }
        }
    }
}
