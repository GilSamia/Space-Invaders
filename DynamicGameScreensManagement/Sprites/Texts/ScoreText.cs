using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Sprites.Texts
{
    internal class ScoreText : TextBlock
    {
        private readonly PlayerInformation r_PlayerInfo;
        private const string k_ConsolasFont = @"Fonts/Consolas";

        private GameScreen m_GameScreen;

        public ScoreText(GameScreen i_Game, PlayerInformation i_PlayerInfo) : base(k_ConsolasFont, i_Game, String.Empty)
        {
            m_GameScreen = i_Game;

            r_PlayerInfo = i_PlayerInfo;
            initColor();
            setText();
            initPosition();
        }

        public ScoreText(string i_AssetTextureName, Game i_Game, PlayerInformation i_PlayerInfo) : base(k_ConsolasFont, i_AssetTextureName, i_Game, String.Empty)
        {
            r_PlayerInfo = i_PlayerInfo;
        }

        public override void Initialize()
        {
            base.Initialize();
            //initColor();
            //setText();
            //initPosition();
        }

        private void setText()
        {
            Text = String.Format("P{0} Score: {1}", r_PlayerInfo.PlayerIndex + 1, r_PlayerInfo.CurrentScore);
        }

        private void initColor()
        {
            FontColor = r_PlayerInfo.Color;
        }

        private void initPosition()
        {
            //Vector2 size = m_Font.MeasureString(Text);
            //Position = new Vector2(0, r_PlayerInfo.PlayerIndex * size.Y);

            //Vector2 size = m_Font.MeasureString(Text);
            Position = new Vector2(0, r_PlayerInfo.PlayerIndex * 20);
        }

        public override void Update(GameTime gameTime)
        {
            setText();
            base.Update(gameTime);
        }

        internal void ChangeScreen(GameScreen i_GameScreen)
        {
            m_GameScreen = i_GameScreen;
            m_GameScreen.Add(this);
        }
    }
}
