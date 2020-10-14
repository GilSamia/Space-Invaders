//*** Guy Ronen � 2008-2011 ***//
using System;
using GameScreens.Sprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Microsoft.Xna.Framework.Graphics;
using Infrastructure.Managers;
using SpaceInvaders.Menus;

namespace GameScreens.Screens
{
    public class WelcomeScreen : GameScreen
    {
        private readonly Game r_Game;

        private Sprite m_WelcomeMessage;
        private Sprite m_PressEnterMsg;

        private Background m_Background;

        public WelcomeScreen(Game i_Game)
            : base (i_Game)
        {
            r_Game = i_Game;
            //m_Background = new Background(i_Game, @"Sprites\BG_Space01_1024x768", 1);
            //this.Add(m_Background);
        }

        public override void Initialize()
        {
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);
            base.Initialize();         
        }

        public override void Update(GameTime i_GameTime)
        {
            base.Update(i_GameTime);

            if (InputManager.KeyPressed(Keys.Enter))
            {
                ExitScreen();
            }

            if (InputManager.KeyPressed(Keys.Escape))
            {
                this.Game.Exit();
            }

            if (InputManager.KeyPressed(Keys.M))
            {
                ScreensManager.SetCurrentScreen(new MainMenu(r_Game));
                ExitScreen();
            }
        }
        public override void Draw(GameTime i_GameTime)
        {
            try
            {
                base.Draw(i_GameTime);

                SpriteBatch.Begin();
                displayInstructions();
            }
            finally
            {
                SpriteBatch.End();
            }
        }

        private void displayInstructions()
        {
            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");
            Vector2 messagePosition = new Vector2(100, GraphicsDevice.Viewport.Height / 2 - 35);
            SpriteBatch.DrawString(consolasFont, @"
Welcome to Space Invaders game.
Please choose your next move from the options below:


Enter    -    Start Game (Level 1)
Esc      -    Exit
M        -    Main Menu", messagePosition, Color.White);
        }
}
}