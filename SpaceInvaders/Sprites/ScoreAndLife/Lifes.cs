using Microsoft.Xna.Framework;

namespace SpaceInvaders.Sprites.ScoreAndLife
{
    internal class Lifes
    {
        private readonly SpaceShipLife[] r_SpaceShipLives;
        private readonly Game r_Game;
        private readonly SpaceShip r_SpaceShip;

        public Lifes(Game i_Game, SpaceShip i_SpaceShip)
        {
            r_SpaceShip = i_SpaceShip;
            r_SpaceShipLives = new SpaceShipLife[r_SpaceShip.PlayerInformation.MaxLife];
            r_Game = i_Game;
            initLifes();
        }

        private void initLifes()
        {
            for (int i = 0; i < r_SpaceShip.PlayerInformation.MaxLife; i++)
            {
                r_SpaceShipLives[i] = new SpaceShipLife(r_SpaceShip.AssetName, r_Game, r_SpaceShip.SpaceShipIndex, i);
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
    }
}