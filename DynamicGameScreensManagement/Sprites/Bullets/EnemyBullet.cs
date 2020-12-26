using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Interfaces;
using System;

namespace SpaceInvaders.Sprites.Bullets
{
    internal class EnemyBullet : Bullet
    {
        private readonly Random r_Random = new Random();
        private readonly int r_DisappearOnHitProbability = 7;

        public EnemyBullet(GameScreen i_Game, IShooter i_Shooter) : base(i_Game, i_Shooter)
        {
            TintColor = Color.Blue;
            m_Direction = 1;
            m_BulletMaxAppearance = i_Game.Game.Window.ClientBounds.Height;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is SpaceShip)
            {
                SpaceShip spaceShip = i_Collidable as SpaceShip;

                RemoveBullet();
                r_Shooter.OnHit(spaceShip);
                spaceShip.OnKill(this as IShooter);
            }

            if (i_Collidable is SpaceShipBullet)
            {
                int probability = r_Random.Next(0, 10);
                SpaceShipBullet spaceShipBullet = i_Collidable as SpaceShipBullet;
                spaceShipBullet.RemoveBullet();

                if (probability > r_DisappearOnHitProbability)
                {
                    this.RemoveBullet();
                }
            }
            base.Collided(i_Collidable);
        }

        protected override float getDirectionY()
        {
            return m_Direction;
        }

        protected override bool isBulletNeedsToRemove()
        {
            bool isNeedToRemove = m_Position.Y > m_BulletMaxAppearance;
            return isNeedToRemove;
        }
    }
}
