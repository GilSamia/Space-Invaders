//*** Guy Ronen © 2008-2011 ***//
using System;
using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using SpaceInvaders.Sprites.Enemies;

namespace SpaceInvaders.Animations
{
    internal class JumpXAnimator : SpriteAnimator
    {
        private TimeSpan m_JumpTime;
        private TimeSpan m_TimeLeftForJump;
        private Vector2 m_JumpDelta;
        private readonly Enemy r_Enemy;
        private int m_XDirection;

        public TimeSpan JumpTime { get => m_JumpTime; set => m_JumpTime = value; }
        public int XDirection { get => m_XDirection; set => m_XDirection = value; }
        public Vector2 JumpDelta { get => m_JumpDelta; set => m_JumpDelta = value; }

        // CTORs
        public JumpXAnimator(TimeSpan i_JumpTime, Vector2 i_JumpDelta, TimeSpan i_AnimationLength, int i_Direction, Enemy i_Enemy)
            : base("Jump", i_AnimationLength)
        {
            this.m_JumpTime = i_JumpTime;
            this.m_TimeLeftForJump = i_JumpTime;
            m_JumpDelta = i_JumpDelta;
            this.m_XDirection = i_Direction;
            this.r_Enemy = i_Enemy;
        }


        protected override void RevertToOriginal()
        {
           // this.BoundSprite.Position = m_OriginalSpriteInfo.Position;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            m_TimeLeftForJump -= i_GameTime.ElapsedGameTime;
            if (m_TimeLeftForJump.TotalSeconds <= 0)
            {
                /// we have elapsed, so JUMP
                Vector2 jumpDeltaWithDirection = new Vector2(m_JumpDelta.X * XDirection, 0);

                BoundSprite.Position += jumpDeltaWithDirection;
                m_TimeLeftForJump = m_JumpTime;
            }
        }

        //private Vector2 checkBoundaries()
        //{
        //    Vector2 jumpDeltaWithDirection = new Vector2(m_JumpDelta.X * XDirection, 0);
        //    if (XDirection > 0)
        //    {
        //        if (r_Enemy.Position.X + jumpDeltaWithDirection.X > r_Enemy.Game.Window.ClientBounds.Width - r_Enemy.Width)
        //        {
        //            jumpDeltaWithDirection = new Vector2(r_Enemy.Game.Window.ClientBounds.Width - r_Enemy.Width - r_Enemy.Position.X, 0);
        //        }
        //    }
        //    else
        //    {
        //        if (r_Enemy.Position.X + jumpDeltaWithDirection.X < 0)
        //        {
        //            jumpDeltaWithDirection = new Vector2(-r_Enemy.Position.X, 0);
        //        }
        //    }
        //    return jumpDeltaWithDirection;
        //}
    }
}
