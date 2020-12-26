using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Interfaces;
using SpaceInvaders.Sprites.Enemies;

namespace SpaceInvaders.Sprites.Bullets
{
    internal class SpaceShipBullet : Bullet
    {
        public SpaceShipBullet(GameScreen i_Game, IShooter i_Shooter) : base(i_Game, i_Shooter)
        {
            TintColor = Color.Red;
            m_Direction = -1;
            m_BulletMaxAppearance = -Texture.Height;
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Enemy)
            {
                Enemy enemy = i_Collidable as Enemy;

                r_Shooter.OnHit(enemy);
                enemy.OnKill(this as IShooter);
                RemoveBullet();
            }

            base.Collided(i_Collidable);
        }

        protected override float getDirectionY()
        {
            return m_Direction;
        }

        protected override bool isBulletNeedsToRemove()
        {
            bool isNeedToRemove = m_Position.Y < m_BulletMaxAppearance;

            return isNeedToRemove;
        }
    }
}
