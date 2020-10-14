using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using SpaceInvaders.Animations;
using SpaceInvaders.Interfaces;
using SpaceInvaders.Sprites.Bullets;
using SpaceInvaders.Utils;
using System;

namespace SpaceInvaders.Sprites.Enemies
{
    internal class Enemy : Sprite, ICollidable2D, IShooter, IDeadable, IEnemy
    {
        private GameScreen r_Game;
        private readonly EnemyData r_EnemyData;
        private readonly EnemyMatrix r_EnemyMatrix;
        private readonly int r_EnemyIndex;

        private static int s_EnemyCounter;
        private static float s_JumpYDelta;
        private static int s_JumpXDirection = 1;
        private static TimeSpan s_AnimationJumpLength = TimeSpan.FromSeconds(0.5f);

        private const float k_EnemyDistanceDelta = 1.5f;
        private const float k_AnimationRotationSpeed = 1.2f;
        private readonly TimeSpan r_AnimationKillLength = TimeSpan.FromSeconds(1.7f);
        private const int k_TextureSize = 32;
        private CompositeAnimator m_JumpAnimators;
        private JumpXAnimator m_JumpAnimator;

        private const int k_MaxTimeToShoot = 30;
        private readonly RandomTimer r_RandomTimer = new RandomTimer(k_MaxTimeToShoot);
        private static readonly Random r_Random = new Random();
        private int m_BulletCounter;
        private readonly int r_MaxNumberOfBullets = 1;

        public event EventHandler<EventArgs> Disposed;

        public Enemy(GameScreen i_Game, EnemyData i_EnemyData, EnemyMatrix i_EnemyMatrix)
            : base(i_EnemyData.AssetName, i_Game.Game)
        {
            r_Game = i_Game;
            this.r_EnemyData = i_EnemyData;
            Position = i_EnemyData.EnemyStartPosition;
            TintColor = i_EnemyData.EnemyColor;
            r_EnemyIndex = s_EnemyCounter;
            s_EnemyCounter++;
            s_JumpYDelta = 0;
            r_EnemyMatrix = i_EnemyMatrix;
            m_BulletCounter = 0;
            i_Game.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_JumpAnimators = new CompositeAnimator(this);
            TintColor = r_EnemyData.EnemyColor;
            Position = r_EnemyData.EnemyStartPosition;
            Height = k_TextureSize;
            Width = k_TextureSize;
            addKillingAnimations();
            addJumpAnimations();
            initTimer();
        }

        protected override void InitBounds()
        {
            SourceRectangle = new Rectangle(r_EnemyData.EnemyTextureOffset * (int)k_TextureSize,
                0, (int)k_TextureSize, (int)k_TextureSize);
            RotationOrigin = SourceRectangleCenter;
        }

        public override void Update(GameTime i_GameTime)
        {
            isEnemyAtTheBottom();
            updateJump(i_GameTime);
            r_RandomTimer.CheckTimer(i_GameTime);

            base.Update(i_GameTime);
        }

        private void updateJump(GameTime i_GameTime)
        {
            m_JumpAnimator.XDirection = JumpXDirection;
            jumpY();
            updateJumpSpeed();

            m_JumpAnimators.Update(i_GameTime);
        }

        private void updateJumpSpeed()
        {
            m_JumpAnimator.JumpTime = s_AnimationJumpLength;
            (m_JumpAnimators["CellAnimation"] as CellAnimator).CellTime = s_AnimationJumpLength;
        }

        private void jumpY()
        {
            Position = new Vector2(Position.X, Position.Y + JumpYDelta);
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
        }

        internal static void ChangeSpeed(float i_SpeedChangePrecente)
        {
            s_AnimationJumpLength *= i_SpeedChangePrecente;
        }

        private void initTimer()
        {
            r_RandomTimer.TimerTick += randomTimer_TimerTick;
        }

        private void randomTimer_TimerTick(object sender, EventArgs e)
        {
            shoot();
        }

        private void addJumpAnimations()
        {
            Vector2 jumpDelta = new Vector2(k_TextureSize, 0);
            m_JumpAnimator = new JumpXAnimator(s_AnimationJumpLength, jumpDelta, TimeSpan.Zero, s_JumpXDirection, this);

            m_JumpAnimators.Add(m_JumpAnimator);
            m_JumpAnimators.Add(new CellAnimator(s_AnimationJumpLength, EnemyData.EnemyTextureOffset, 2, TimeSpan.Zero));
            m_JumpAnimators.Restart();
        }

        private void addKillingAnimations()
        {
            m_Animations.Add(new ShrinkAnimator("Shrink", r_AnimationKillLength));
            m_Animations.Add(new RotationAnimator("Rotate", r_AnimationKillLength, k_AnimationRotationSpeed));
            m_Animations["Shrink"].Finished += Enemy_AnimationFinished;
        }

        private void Enemy_AnimationFinished(object sender, EventArgs e)
        {
            Game.Components.Remove(this);
        }

        public EnemyData EnemyData { get { return r_EnemyData; } }

        public Vector2 ShooterPosition => Position;

        public float ShooterWidth => Width;

        public int Score => EnemyData.EnemyScore;

        public static float JumpYDelta { get => s_JumpYDelta; set => s_JumpYDelta = value; }

        public static int JumpXDirection { get => s_JumpXDirection; set => s_JumpXDirection = value; }

        protected override void InitOrigins()
        {
            RotationOrigin = TextureCenter;
        }

        private void isEnemyAtTheBottom()
        {
            if (Position.Y + Texture.Height >= SpaceShip.PositionY)
            {
                //(Game as GameScreen).GameManager.GameOver(false); TODO: Game over!!!!
            }
        }

        private void shoot()
        {
            if (m_BulletCounter < r_MaxNumberOfBullets)
            {
                Bullet bullet = new EnemyBullet(r_Game, this);
                m_BulletCounter++;
            }
        }

        public void ReduceBulletsByOne()
        {
            m_BulletCounter--;
        }

        public void OnHit(ICollidable2D i_Collidable)
        {
            //Do Nothing.
        }

        public void OnKill(IShooter i_MyKiller)
        {
            m_JumpAnimators.Reset();
            m_Animations.Restart();
            this.Dispose();
            r_EnemyMatrix.KillEnemyAt(r_EnemyData.EnemyPoint);
        }
    }
}
