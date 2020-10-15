using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace SpaceInvaders.Screens
{
    public class LevelTransitionScreen : GameScreen
    {
        private Background m_Background;
        private int m_Level;
        private readonly int r_CountDownToStartPlaying = 3;
        private string m_CoundDownNumber;
        private TimeSpan m_SecondsShow;

        public LevelTransitionScreen(Game i_Game, int i_Level) : base(i_Game)
        {
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            m_Level = i_Level;
            m_SecondsShow = TimeSpan.FromSeconds(2.5);

            this.Add(m_Background);
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale,
                Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }

        public override void Initialize()
        {
            base.Initialize();
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);
            calculateTime(i_GameTime);
        }

        private void calculateTime(GameTime i_GameTime)
        {
            m_SecondsShow -= i_GameTime.ElapsedGameTime;
            string countDownToStartPlaying = string.Empty;

            if (m_SecondsShow.TotalSeconds >= r_CountDownToStartPlaying - 1)
            {
                m_CoundDownNumber = "3";
            }
            else if (m_SecondsShow.TotalSeconds >= r_CountDownToStartPlaying - 2 && m_SecondsShow.TotalSeconds < r_CountDownToStartPlaying - 1)
            {
                m_CoundDownNumber = "2";
            }
            else if (m_SecondsShow.TotalSeconds >= 0 && m_SecondsShow.TotalSeconds < r_CountDownToStartPlaying - 2)
            {
                m_CoundDownNumber = "1";
            }
            else
            {
                ExitScreen();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            try
            {
                SpriteBatch.Begin();
                displayCountDown();
            }
            finally
            {
                SpriteBatch.End();
            }
        }
        private void displayCountDown()
        {
            SpriteFont fontCalibri = ContentManager.Load<SpriteFont>(@"Fonts\Calibri");
            string message = string.Format("You're in level {0}{2}{2}!Starting in {1}", m_Level, m_CoundDownNumber, Environment.NewLine);
            Vector2 position = new Vector2(100, GraphicsDevice.Viewport.Height / 2);
            SpriteBatch.DrawString(fontCalibri, message, position, Color.White);
        }
    }
}
