using Infrastructure.ObjectModel.Animators;
using Microsoft.Xna.Framework;
using System;

namespace SpaceInvaders.Animations
{
    internal class FadeOutAnimator : SpriteAnimator
    {
        public FadeOutAnimator(string i_Name, TimeSpan i_AnimationLength) : base(i_Name, i_AnimationLength)
        {
        }
        protected override void DoFrame(GameTime i_GameTime)
        {
            if (this.BoundSprite.Opacity > 0)
            {
                this.BoundSprite.Opacity -=
                    m_OriginalSpriteInfo.Opacity
                    * (float)i_GameTime.ElapsedGameTime.TotalSeconds
                    * (float)(1 / AnimationLength.TotalSeconds);
            }
        }

        protected override void RevertToOriginal()
        {
            this.BoundSprite.Opacity = m_OriginalSpriteInfo.Opacity;
        }
    }
}
