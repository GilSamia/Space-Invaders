using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Animations
{
    internal class RotationAnimator : SpriteAnimator
    {
        private readonly float r_RotationSpeed;
        public RotationAnimator(string i_Name, TimeSpan i_AnimationLength, float i_RotationSpeed)
            : base(i_Name, i_AnimationLength)
        {
            r_RotationSpeed = i_RotationSpeed;
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.AngularVelocity = r_RotationSpeed;
        }

        protected override void RevertToOriginal()
        {
            BoundSprite.AngularVelocity = m_OriginalSpriteInfo.AngularVelocity;
            BoundSprite.Rotation = m_OriginalSpriteInfo.Rotation;
            //BoundSprite.SourceRectangle = m_OriginalSpriteInfo.SourceRectangle;
        }
    }
}
