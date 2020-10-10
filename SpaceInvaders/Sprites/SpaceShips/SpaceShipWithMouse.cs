using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Utils;

namespace SpaceInvaders.Sprites
{
    internal class SpaceShipWithMouse : SpaceShip
    {
        public SpaceShipWithMouse(GameScreen i_Game, PlayerData m_PlayerData) : base(i_Game, m_PlayerData)
        {
        }

        public override void Update(GameTime gameTime)
        {
            PlayerMouseUpdate();
            if (m_InputManager.ButtonPressed(eInputButtons.Left))
            {
                shoot();
            }

            base.Update(gameTime);
        }

        private void PlayerMouseUpdate()
        {
            m_Position.X += m_InputManager.MousePositionDelta.X;
        }
    }
}
