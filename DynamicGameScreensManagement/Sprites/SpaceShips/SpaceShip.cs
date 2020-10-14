using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Animations;
using SpaceInvaders.Interfaces;
using SpaceInvaders.Sprites.Bullets;
using SpaceInvaders.Sprites.ScoreAndLife;
using SpaceInvaders.Sprites.Texts;
using SpaceInvaders.Utils;
using System;

namespace SpaceInvaders.Sprites
{
    internal class SpaceShip : Sprite, IShooter, ICollidable2D, IDeadable
    {
        private GameScreen r_Game;

        protected IInputManager m_InputManager;
        private const float k_Velocity = 140f;
        private const float k_SpaceShipDistanceDelta = 1.5f;
        private const float k_SpaceShipDistanceFromViewPortPrecente = 0.3f;

        private static int m_SpaceShipCounter;
        private readonly int r_SpaceShipIndex;
        private PlayerData m_PlayerData;
        private ScoreText m_ScoreText;
        private Lifes m_PlayerLifes;
        private int m_BulletsCounter = 0;

        private bool m_IsDying = false;
        private const int k_BulletsLimitation = 2;
        private const float k_RotationSpeed = 6f * MathHelper.TwoPi;
        private CompositeAnimator m_DistroyedAnimations;

        private readonly TimeSpan r_KillTimeAnimation = TimeSpan.FromSeconds(2.6f);
        private readonly TimeSpan r_BlinkTimeAnimation = TimeSpan.FromSeconds(1 / 8f);
        private readonly TimeSpan r_BlinkLengthAnimation = TimeSpan.FromSeconds(2);

        private static int m_TexutreSize;

        public event EventHandler<EventArgs> Disposed;

        public SpaceShip(GameScreen i_Game, PlayerData m_PlayerData)
            : base(m_PlayerData.AssetName, i_Game.Game)
        {
            r_Game = i_Game;
            this.m_PlayerData = m_PlayerData;
            r_SpaceShipIndex = m_SpaceShipCounter;
            m_SpaceShipCounter++;
            PlayerInformation = new PlayerInformation(r_SpaceShipIndex);
            i_Game.Add(this);
        }

        public PlayerInformation PlayerInformation { get; }

        public Vector2 ShooterPosition => Position;

        public float ShooterWidth => Width;

        public int SpaceShipIndex => r_SpaceShipIndex;

        public static int TexutreSize { get => m_TexutreSize; set => m_TexutreSize = value; }

        public static float PositionY { get; private set; }

        private void addScoreFont()
        {
            string text = String.Format("P{0} Score: {1}", r_SpaceShipIndex + 1, PlayerInformation.CurrentScore);
            m_ScoreText = new ScoreText(Game, PlayerInformation);
        }

        private void addDistroyedAnimation()
        {
            m_DistroyedAnimations = new CompositeAnimator(this);
            m_DistroyedAnimations.Add(new FadeOutAnimator("Fade", r_KillTimeAnimation));
            m_DistroyedAnimations.Add(new RotationAnimator("Rotation", r_KillTimeAnimation, k_RotationSpeed));
            m_DistroyedAnimations["Fade"].Finished += distroyedAnimations_Finished;
        }

        private void addAnimation()
        {
            m_Animations.Add(new BlinkAnimator("Blink", r_BlinkTimeAnimation, r_BlinkLengthAnimation));
            m_Animations["Blink"].Finished += animations_Finished;
        }

        private void distroyedAnimations_Finished(object sender, EventArgs e)
        {
            r_Game.Remove(this);
            m_IsDying = false;
            this.Dispose();
        }

        private void animations_Finished(object sender, EventArgs e)
        {
            initPosition();
            m_IsDying = false;
        }

        public void SpaceShipGameOver()
        {
            m_Animations.Pause();
            m_DistroyedAnimations.Restart();
        }

        public override void Initialize()
        {
            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;
            base.Initialize();

            initPosition();
            addDistroyedAnimation();
            addAnimation();
            addLifes();
            addScoreFont();
            TexutreSize = Texture.Width;
        }

        private void addLifes()
        {
            m_PlayerLifes = new Lifes(r_Game, this);
        }

        protected override void InitOrigins()
        {
            RotationOrigin = SourceRectangleCenter;

            base.InitOrigins();
        }

        private void initPosition()
        {
            float x = r_SpaceShipIndex * Texture.Width * k_SpaceShipDistanceDelta;
            float y = GraphicsDevice.Viewport.Height - (Texture.Height + Texture.Height * k_SpaceShipDistanceFromViewPortPrecente);

            PositionY = y;
            Position = new Vector2(x, y);
        }

        public override void Update(GameTime i_GameTime)
        {
            if (m_InputManager.KeyboardState.IsKeyDown(m_PlayerData.KeyLeft))
            {
                m_Velocity.X = k_Velocity * -1;
            }
            else if (m_InputManager.KeyboardState.IsKeyDown(m_PlayerData.KeyRight))
            {
                m_Velocity.X = k_Velocity;
            }
            else
            {
                m_Velocity.X = 0;
            }

            m_Position.X = MathHelper.Clamp(m_Position.X, 0, GraphicsDevice.Viewport.Width - Texture.Width);
            if (m_InputManager.KeyPressed(m_PlayerData.KeyShoot))
            {
                shoot();

            }

            m_DistroyedAnimations.Update(i_GameTime);
            base.Update(i_GameTime);
        }

        protected void shoot()
        {
            if (canShoot())
            {
                (Game as GameWithScreens).SpriteSoundEffects["SSGunShot"].Play();
                Bullet bullet = new SpaceShipBullet(r_Game, this);
                m_BulletsCounter++;
            }
        }

        private bool canShoot()
        {
            bool canShoot = m_BulletsCounter < k_BulletsLimitation;
            canShoot &= !m_IsDying;
            return canShoot;
        }

        public void ReduceBulletsByOne()
        {
            m_BulletsCounter--;
        }

        public void OnKill(IShooter i_MyKiller)
        {
            m_Animations.Restart();
            m_IsDying = true;
            PlayerInformation.ReduceLife();
            m_PlayerLifes.ReduceLife();
        }

        public void OnHit(ICollidable2D i_Collidable)
        {
            IEnemy hittedEnemy = i_Collidable as IEnemy;
            if (hittedEnemy != null)
            {
                PlayerInformation.UpdateScore(hittedEnemy.Score);
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
        }
    }
}