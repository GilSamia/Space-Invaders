using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;


namespace SpaceInvaders.Animations
{
    internal class ShrinkAnimator : SpriteAnimator
    {
        public ShrinkAnimator(string i_Name, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
        {
        }

        protected override void DoFrame(GameTime i_GameTime)
        {
            BoundSprite.Scales -= new Vector2((1/ (float) AnimationLength.TotalSeconds) * (float)i_GameTime.ElapsedGameTime.TotalSeconds);
            if (BoundSprite.Width <= 0 || BoundSprite.Width <= 0)
            {
                this.IsFinished = true;
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Scales = m_OriginalSpriteInfo.Scales;
        }
    }
}
