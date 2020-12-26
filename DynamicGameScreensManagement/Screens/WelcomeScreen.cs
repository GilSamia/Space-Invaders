//*** Guy Ronen © 2008-2011 ***//
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
using Microsoft.Xna.Framework.Audio;
using SpaceInvaders;
using SpaceInvaders.Screens;

namespace GameScreens.Screens
{
    public class WelcomeScreen : GameScreen
    {
        private readonly GameWithScreens r_Game;

        private Sprite m_WelcomeMessage;
        private Sprite m_PressEnterMsg;

        private Background m_Background;

        public WelcomeScreen(GameWithScreens i_Game)
            : base(i_Game)
        {
            r_Game = i_Game;
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
                r_Game.MenuMoveSound.Play();
                ExitScreen();
                ScreensManager.SetCurrentScreen(new PlayScreen(Game, 1)); ;
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 1));
            }

            if (InputManager.KeyPressed(Keys.Escape))
            {
                r_Game.MenuMoveSound.Play();
                this.Game.Exit();
            }

            if (InputManager.KeyPressed(Keys.M))
            {
                r_Game.MenuMoveSound.Play();
                ExitScreen();
                ScreensManager.SetCurrentScreen(new MainMenu(r_Game));
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
