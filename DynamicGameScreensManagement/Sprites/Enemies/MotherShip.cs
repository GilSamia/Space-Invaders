using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceInvaders.Animations;
using SpaceInvaders.Interfaces;
using SpaceInvaders.Sprites.Bullets;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Sprites.Enemies
{
    internal class MotherShip : Sprite, IDeadable, ICollidable2D, IEnemy
    {
        private const float k_MotherShipSpeed = 95F;
        private const string k_AssteName = @"Sprites\MotherShip_32x120";
        private readonly TimeSpan r_AnimationKillingLength = TimeSpan.FromSeconds(3);
        private readonly TimeSpan r_AnimationBlinkLength = TimeSpan.FromSeconds(0.5);
        private const int k_MaxTimeToWait = 10;
        private readonly RandomTimer r_RandomTimer = new RandomTimer(k_MaxTimeToWait);
        private const int k_ScoreOfKillingMotherShip = 600;
        private bool m_IsDying = false;

        public event EventHandler<EventArgs> Disposed;

        public int Score => k_ScoreOfKillingMotherShip;

        public MotherShip(GameScreen i_Game)
           : base(k_AssteName, i_Game.Game, i_Game)
        {
            TintColor = Color.Red;
        }

        public override void Initialize()
        {
            base.Initialize();

            initVelocity();
            initPosition();
            initTimer();
            initAnimations();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        private void initVelocity()
        {
            Velocity = new Vector2(k_MotherShipSpeed, 0);
        }

        private void initAnimations()
        {
            m_Animations.Add(new FadeOutAnimator("Fade", r_AnimationKillingLength));
            m_Animations.Add(new BlinkAnimator("Blink", r_AnimationBlinkLength, r_AnimationKillingLength));
            m_Animations.Add(new ShrinkAnimator("Shrik", r_AnimationKillingLength));

            m_Animations["Fade"].Finished += animations_Finished;
        }

        private void animations_Finished(object sender, EventArgs e)
        {
            Position = new Vector2(Game.Window.ClientBounds.Width, Position.Y);
            initVelocity();
            m_IsDying = false;
        }

        private void initTimer()
        {
            r_RandomTimer.TimerTick += randomTimer_TimerTick;
        }

        private void randomTimer_TimerTick(object sender, EventArgs e)
        {
            bool isOutOfViewPort = Position.X > Game.Window.ClientBounds.Width;

            if (isOutOfViewPort)
            {
                initPosition();
            }
        }

        private void initPosition()
        {
            Position = new Vector2(-Texture.Width, Texture.Height);
        }

        public void OnKill(IShooter i_MyKiller)
        {
            (Game as GameWithScreens).SpriteSoundEffects["MotherShipKill"].Play();
            m_IsDying = true;
            Velocity = new Vector2(0);
            m_Animations.Restart();
            i_MyKiller.OnHit(this);
        }

        public override void Update(GameTime i_GameTime)
        {
            r_RandomTimer.CheckTimer(i_GameTime);
            base.Update(i_GameTime);
        }

        public override void Collided(ICollidable i_Collidable)
        {
            if (!m_IsDying)
            {
                if (i_Collidable is SpaceShipBullet)
                {
                    SpaceShipBullet bullet = i_Collidable as SpaceShipBullet;

                    bullet.RemoveBullet();
                    OnKill(bullet.Shooter);
                }
            }
        }

        protected override void InitOrigins()
        {
            RotationOrigin = TextureCenter;
        }
    }
}
