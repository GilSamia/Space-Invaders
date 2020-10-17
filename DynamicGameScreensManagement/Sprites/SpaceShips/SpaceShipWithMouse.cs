using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites.SpaceShips;
using SpaceInvaders.Utils;
using System.Collections.Generic;

namespace SpaceInvaders.Sprites
{
    internal class SpaceShipWithMouse : SpaceShip
    {
        public SpaceShipWithMouse(GameScreen i_Game, PlayerData m_PlayerData) : base(i_Game, m_PlayerData)
        {
        }
        public SpaceShipWithMouse(GameScreen i_Game, PlayerData m_PlayerData, PlayerInformation i_PlayerInformation) : base(i_Game, m_PlayerData, i_PlayerInformation)
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
