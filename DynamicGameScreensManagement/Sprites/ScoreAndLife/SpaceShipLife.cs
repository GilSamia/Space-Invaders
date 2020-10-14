using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using SpaceInvaders.Animations;
using SpaceInvaders.Interfaces;
using System;

namespace SpaceInvaders.Sprites.ScoreAndLife
{
    internal class SpaceShipLife : Sprite, IDeadable
    {
        private GameScreen r_Game;

        private readonly int r_LifeIndex;
        private readonly int r_PlayerIndex;
        private readonly TimeSpan r_BlinkTimeAnimation = TimeSpan.FromSeconds(1 / 8f);
        private readonly TimeSpan r_BlinkLengthAnimation = TimeSpan.FromSeconds(2);

        public SpaceShipLife(string i_AssetName, GameScreen i_Game, int i_PlayerIndex, int i_LifeIndex) : base(i_AssetName, i_Game.Game)
        {
            r_LifeIndex = i_LifeIndex;
            r_PlayerIndex = i_PlayerIndex;
            r_Game = i_Game;
            r_Game.Add(this);
        }

        public override void Initialize()
        {
            base.Initialize();
            setPosition();
            setScale();
            setOpacity();
            //addAnimation();
        }

        private void addAnimation()
        {
            m_Animations.Add(new BlinkAnimator("Blink", r_BlinkTimeAnimation, r_BlinkLengthAnimation));

            m_Animations["Blink"].Finished += animations_Finished;
        }

        private void animations_Finished(object sender, EventArgs e)
        {
            r_Game.Remove(this);
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
            (Game as GameWithScreens).SpriteSoundEffects["LifeDie"].Play();
            //r_Game.Remove(this);
            //m_Animations.Restart();
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
        }
    }
}
