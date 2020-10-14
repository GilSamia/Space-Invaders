using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Interfaces;
using System;

namespace SpaceInvaders.Sprites.Bullets
{
    internal abstract class Bullet : Sprite, ICollidable2D
    {
        private const string k_AssetName = @"Sprites/Bullet";
        protected readonly IShooter r_Shooter;
        protected const float k_BulletSpeed = 140F;
        protected int m_Direction = 1;
        protected float m_BulletMaxAppearance = 0;

        public event EventHandler<EventArgs> Disposed;

        public Bullet(Game i_Game, IShooter i_Shooter) : base(k_AssetName, i_Game)
        {
            r_Shooter = i_Shooter;
        }

        public override void Initialize()
        {
            base.Initialize();
            initPosition();
        }
        public IShooter Shooter
        {
            get { return r_Shooter; }
        }

        protected void initPosition()
        {
            float deltaX = (r_Shooter.ShooterWidth / 2) - (Texture.Width / 2);
            Position = new Vector2(r_Shooter.ShooterPosition.X + deltaX, r_Shooter.ShooterPosition.Y);
        }

        public override void Update(GameTime i_GameTime)
        {
            m_Velocity.Y = k_BulletSpeed * getDirectionY();
            base.Update(i_GameTime);
            if (isBulletNeedsToRemove())
            {
                RemoveBullet();
            }
        }

        public void RemoveBullet()
        {
            r_Shooter.ReduceBulletsByOne();
            Game.Components.Remove(this);
            this.Dispose();
        }

        protected abstract bool isBulletNeedsToRemove();

        protected abstract float getDirectionY();

        public override void Collided(ICollidable i_Collidable)
        {
        }
    }
}
