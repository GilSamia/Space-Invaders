//*** Guy Ronen © 2008-2011 ***//
using System;
using GameScreens.Sprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using SpaceInvaders.Menus;

namespace GameScreens.Screens
{
    public class GameOverScreen : GameScreen
    {
        private Game r_Game;

        Background m_Background;
        public GameOverScreen(Game i_Game)
            : base(i_Game)
        {
            r_Game = i_Game;
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 100);
            m_Background.TintColor = Color.Red;
            this.Add(m_Background);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Escape))
            {
                ExitScreen();
            }
            if (InputManager.KeyPressed(Keys.Home))
            {
                //r_Game.Components.Remove(this);
                //(r_Game as GameWithScreens).setScreenStackOnGameOver((r_Game as GameWithScreens).ScreensManager);
            }
            if (InputManager.KeyPressed(Keys.M))
            {
                //r_Game.Components.Remove(this);
                //(r_Game as GameWithScreens).setMainMenuStackOnGameOver((r_Game as GameWithScreens).ScreensManager);
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            try
            {
                SpriteBatch.Begin();
                displayGameOverMessage();
            }
            finally
            {
                SpriteBatch.End();
            }
        }

        private void displayGameOverMessage()
        {
            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");
            string message = string.Format("Game Over!{0}{0} You're Scores Are{0}ADD SCORES{0}{0}ESC  - To Exit{0}HOME  - Start a new game{0}M  - Main Menu", System.Environment.NewLine);
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            SpriteBatch.DrawString(consolasFont, message, position, Color.White);
        }
    }
}
