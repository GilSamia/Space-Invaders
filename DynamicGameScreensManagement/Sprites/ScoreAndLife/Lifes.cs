using Infrastructure.ObjectModel.Screens;
using System;

namespace SpaceInvaders.Sprites.ScoreAndLife
{
    internal class Lifes
    {
        private readonly SpaceShipLife[] r_SpaceShipLives;
        private readonly SpaceShip r_SpaceShip;
        
        private GameScreen m_GameScreen;

        public Lifes(GameScreen i_Game, SpaceShip i_SpaceShip)
        {
            r_SpaceShip = i_SpaceShip;
            r_SpaceShipLives = new SpaceShipLife[r_SpaceShip.PlayerInformation.MaxLife];
            m_GameScreen = i_Game;
            initLifes();
        }

        private void initLifes()
        {
            for (int i = 0; i < r_SpaceShip.PlayerInformation.MaxLife; i++)
            {
                r_SpaceShipLives[i] = new SpaceShipLife(r_SpaceShip.AssetName, m_GameScreen, r_SpaceShip.SpaceShipIndex, i);
            }
        }

        public void ReduceLife()
        {
            int currentLifeToReduce = r_SpaceShip.PlayerInformation.CurrentLife;
            if (currentLifeToReduce <= -1)
            {
                r_SpaceShip.SpaceShipGameOver();
            }
            else
            {
                r_SpaceShipLives[currentLifeToReduce].OnKill(null);
            }
        }

        internal void ChangeScreen(GameScreen i_GameScreen)
        {
            m_GameScreen = i_GameScreen;
            foreach (SpaceShipLife spaceShipLife in r_SpaceShipLives)
            {
                m_GameScreen.Add(spaceShipLife);
            }
        }
    }
}