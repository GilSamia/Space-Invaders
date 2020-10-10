using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Controls
{
    class Button : GameScreen
    {
        private readonly Game r_Game;

        private MouseState _currentMouse;
        private SpriteFont m_font;
        private bool _isHovering;
        private MouseState _previousMouse;
        private Texture2D m_texture;



        public event EventHandler Click;

        public bool Clicked { get; private set; }

        public Color PenColour { get; set; }

        public Vector2 Position { get; set; }

        public Rectangle Rectangle
        {
            get
            {
                return new Rectangle((int)Position.X, (int)Position.Y, m_texture.Width, m_texture.Height);
            }
        }

        public string Text { get; set; }

        public Button(Game i_Game, Texture2D texture, SpriteFont font) : base(i_Game)
        {
            r_Game = i_Game;

            m_texture = texture;
            m_font = font;
            PenColour = Color.Black;
        }

        public override void Draw(GameTime gameTime)
        {
            var colour = Color.White;

            if (_isHovering)
                colour = Color.Gray;

            SpriteBatch.Draw(m_texture, Rectangle, colour);

            if (!string.IsNullOrEmpty(Text))
            {
                var x = (Rectangle.X + (Rectangle.Width / 2)) - (m_font.MeasureString(Text).X / 2);
                var y = (Rectangle.Y + (Rectangle.Height / 2)) - (m_font.MeasureString(Text).Y / 2);

                SpriteBatch.DrawString(m_font, Text, new Vector2(x, y), PenColour);
            }
        }

        public override void Update(GameTime gameTime)
        {
            _previousMouse = _currentMouse;
            _currentMouse = Mouse.GetState();

            var mouseRectangle = new Rectangle(_currentMouse.X, _currentMouse.Y, 1, 1);

            _isHovering = false;

            if (mouseRectangle.Intersects(Rectangle))
            {
                _isHovering = true;

                if (_currentMouse.LeftButton == ButtonState.Released && _previousMouse.LeftButton == ButtonState.Pressed)
                {
                    Click?.Invoke(this, new EventArgs());
                }
            }
        }
    }
}