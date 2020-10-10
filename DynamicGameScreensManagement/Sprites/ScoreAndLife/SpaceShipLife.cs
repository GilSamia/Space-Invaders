using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using SpaceInvaders.Animations;
using SpaceInvaders.Interfaces;
using System;

namespace SpaceInvaders.Sprites.ScoreAndLife
{
    internal class SpaceShipLife : Sprite, IDeadable
    {
        private readonly int r_LifeIndex;
        private readonly int r_PlayerIndex;
        private readonly TimeSpan r_BlinkTimeAnimation = TimeSpan.FromSeconds(1 / 8f);
        private readonly TimeSpan r_BlinkLengthAnimation = TimeSpan.FromSeconds(2);

        public SpaceShipLife(string i_AssetName, Game i_Game, int i_PlayerIndex, int i_LifeIndex) : base(i_AssetName, i_Game)
        {
            r_LifeIndex = i_LifeIndex;
            r_PlayerIndex = i_PlayerIndex;
        }

        public override void Initialize()
        {
            base.Initialize();
            setPosition();
            setScale();
            setOpacity();
            addAnimation();
        }

        private void addAnimation()
        {
            m_Animations.Add(new BlinkAnimator("Blink", r_BlinkTimeAnimation, r_BlinkLengthAnimation));

            m_Animations["Blink"].Finished += animations_Finished;
        }

        private void animations_Finished(object sender, EventArgs e)
        {
            Game.Components.Remove(this);
            Dispose();
        }

        private void setScale()
        {
            Scales = new Vector2(0.5f, 0.5f);
        }

        private void setOpacity()
        {
            Opacity = 0.5f;
        }

        private void setPosition()
        {
            float x = Game.Window.ClientBounds.Width - (r_LifeIndex * Width) - Width;
            float y = (r_PlayerIndex * Texture.Height);

            Position = new Vector2(x, y);
        }

        public void OnKill(IShooter i_MyKiller)
        {
            m_Animations.Restart();
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
        }
    }
}
