using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Sprites
{
    internal class TextBlock : Sprite
    {
        private GameScreen r_Game;

        protected SpriteFont m_Font;
        private Color m_FontColor = Color.White;
        private readonly string r_AssetFontName;
        private const string k_AssetTextureName = @"Sprites/TransparentBG_1x1";

        public string Text { get; set; }
        protected Color FontColor
        {
            get { return m_FontColor; }
            set { m_FontColor = value; }
        }

        public TextBlock(string i_AssetFontName, GameScreen i_Game, string i_Text) : base(k_AssetTextureName, i_Game.Game)
        {
            r_AssetFontName = i_AssetFontName;
            Text = i_Text;
            r_Game = i_Game;
            r_Game.Add(this);
        }
        public TextBlock(string i_AssetFontName, string i_AssetTextureName, Game i_Game, string i_Text) : base(i_AssetTextureName, i_Game)
        {
            r_AssetFontName = i_AssetFontName;
            Text = i_Text;
        }

        protected override void LoadContent()
        {
            m_Font = Game.Content.Load<SpriteFont>(r_AssetFontName);
            base.LoadContent();
        }

        protected override void drawFunction()
        {
            m_SpriteBatch.DrawString(m_Font, Text, Position, FontColor, Rotation, RotationOrigin, Scales, SpriteEffects.None, LayerDepth);
        }
    }
}
