using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Sprites.Bullets;
using SpaceInvaders.Sprites.Enemies;
using System;

namespace SpaceInvaders.Sprites.SpaceShips
{

    public class Barrier : Sprite, ICollidable2D
    {
        private const float k_BarrierSpeed = 35f;
        private const float k_BoundDistance = 0.5f;
        private const float k_BulletDamage = 0.35f;
        private readonly Vector2 r_StartPosition;
        private readonly ICollisionsManager r_CollisionsManager;

        private int m_XDirection = 1;
        private float m_RightBound;
        private float m_LeftBound;
        private Color[] m_Pixels;

        public event EventHandler<EventArgs> Disposed;

        public Barrier(Game i_Game, string i_AssetName, Vector2 i_Position, Color[] i_Pixels)
            : base(i_AssetName, i_Game)
        {
            m_Pixels = i_Pixels;
            r_StartPosition = i_Position;
            r_CollisionsManager = Game.Services.GetService(typeof(ICollisionsManager)) as ICollisionsManager;
        }

        public Texture2D BarrierTexture => Texture;

        public override void Collided(ICollidable i_Collidable)
        {
            if (i_Collidable is Bullet)
            {
                Bullet bullet = i_Collidable as Bullet;
                int direction = bullet is EnemyBullet ? 1 : -1;
                Vector2 hitPosition = calcBulletHitPosition(bullet, direction);
                if (getPixel(hitPosition) != Color.Transparent)
                {
                    hasHit(bullet, hitPosition, direction);
                    bullet.RemoveBullet();
                }
            }

            if (i_Collidable is Enemy)
            {
                Enemy enemy = i_Collidable as Enemy;
                eraseBarrier(enemy);
            }
        }

        private void eraseBarrier(Enemy i_Enemy)
        {
            int directionX = Enemy.JumpXDirection;

            if (directionX < 0)
            {
                eraseRight(i_Enemy);
            }
            else
            {
                eraseLeft(i_Enemy);
            }

            Texture.SetData(m_Pixels);
        }

        private void eraseLeft(Enemy i_Enemy)
        {
            Vector2 position = i_Enemy.Position + new Vector2(i_Enemy.Width, i_Enemy.Height) - Position;

            for (int i = 0; i < position.X; i++)
            {
                for (int j = 0; j < position.Y; j++)
                {
                    Vector2 newHit = checkPixelBounderies(new Vector2(i, j));
                    m_Pixels[(int)newHit.Y * (int)Width + (int)newHit.X] = Color.Transparent;
                }
            }
        }

        private void eraseRight(Enemy i_Enemy)
        {
            float positionY = (i_Enemy.Position + new Vector2(i_Enemy.Width, i_Enemy.Height) - Position).Y;

            for (int i = (int)Texture.Width; i > Position.X - i_Enemy.Position.X + (int)Texture.Width; i--)
            {
                for (int j = 0; j < positionY; j++)
                {
                    Vector2 newHit = checkPixelBounderies(new Vector2(i, j));
                    m_Pixels[(int)newHit.Y * (int)Width + (int)newHit.X] = Color.Transparent;
                }
            }
        }

        private float calcEnemyPositionY(Enemy i_Enemy)
        {
            float positionY = i_Enemy.Position.Y + i_Enemy.Height - Position.Y;
            return positionY;
        }

        private void hasHit(Bullet i_Bullet, Vector2 i_HitPosition, int i_Direction)
        {
            int barrierDamageHeight = (int)(i_Bullet.Height * k_BulletDamage);
            Vector2 currentPixel = i_HitPosition;

            for (int i = 0; i < barrierDamageHeight; i++)
            {
                setAlphaPixelsToZero(currentPixel, i_Bullet, barrierDamageHeight);
                currentPixel.Y += i_Direction;
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        private Color getPixel(Vector2 i_HitPosition)
        {
            i_HitPosition = checkPixelBounderies(i_HitPosition);
            return m_Pixels[(int)i_HitPosition.Y * (int)Width + (int)i_HitPosition.X];
        }

        private Vector2 checkPixelBounderies(Vector2 i_HitPosition)
        {
            int x = MathHelper.Clamp((int)i_HitPosition.X, 0, Texture.Width - 1);
            int y = MathHelper.Clamp((int)i_HitPosition.Y, 0, Texture.Height - 1);
            return new Vector2(x, y);
        }

        private void setAlphaPixelsToZero(Vector2 i_HitPosition, Bullet i_Bullet, int i_Damage)
        {
            i_HitPosition = checkPixelBounderies(i_HitPosition);
            int middle = (int)i_Bullet.Width / 2;
            for (int i = -middle; i < middle; i++)
            {
                Vector2 newHit = new Vector2(i_HitPosition.X + i, i_HitPosition.Y);
                newHit = checkPixelBounderies(newHit);
                m_Pixels[(int)newHit.Y * (int)Width + (int)newHit.X] = Color.Transparent;

                int middleY = i_Damage / 2;
                for (int j = -middleY; j < middleY; j++)
                {
                    Vector2 newHit2 = new Vector2(newHit.X, newHit.Y + j);
                    newHit2 = checkPixelBounderies(newHit2);
                    m_Pixels[(int)newHit2.Y * (int)Width + (int)newHit2.X] = Color.Transparent;
                }
            }

            Texture.SetData(m_Pixels);
        }

        private Vector2 calcBulletHitPosition(Bullet i_Bullet, int i_Direction)
        {
            //Center point of the bullet.
            Vector2 hit = i_Bullet.TextureCenter + i_Bullet.Position;

            hit.Y += i_Bullet.Height * 0.5f * i_Direction;

            //Relative X and Y to barrier
            hit -= this.Position;

            return hit;
        }

        public override void Initialize()
        {
            base.Initialize();
            initPosition();
        }

        private void initPosition()
        {
            Position = r_StartPosition;
            m_RightBound = Position.X + (float)Texture.Width * (1 + k_BoundDistance);
            m_LeftBound = Position.X - (float)Texture.Width * k_BoundDistance;
        }

        public override void Update(GameTime i_GameTime)
        {
            Velocity = new Vector2(k_BarrierSpeed * m_XDirection, 0);

            base.Update(i_GameTime);

            if (m_RightBound < Position.X || m_LeftBound > Position.X)
            {
                m_XDirection *= -1;
            }
        }
    }
}
