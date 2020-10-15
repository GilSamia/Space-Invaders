//*** Guy Ronen © 2008-2011 ***//
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using SpaceInvaders.Menus;
using SpaceInvaders.Screens;

namespace GameScreens.Screens
{
    public class GameOverScreen : GameScreen
    {
        private Game r_Game;
        private Background m_Background;
        private string m_Scores;
        private int m_WinningPlayerIndex;

        public GameOverScreen(Game i_Game, string i_Scores, int i_WinningPlayerIndex)
            : base(i_Game)
        {
            r_Game = i_Game;
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 30);
            this.Add(m_Background);

            m_Scores = i_Scores;
            m_WinningPlayerIndex = i_WinningPlayerIndex;
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
                this.Game.Exit();
            }
            if (InputManager.KeyPressed(Keys.Home))
            {
                ExitScreen();
                ScreensManager.SetCurrentScreen(new DummyGameScreen(Game)); ;
                ScreensManager.SetCurrentScreen(new PlayScreen(Game, 1)); ;
                ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 1));
                //r_Game.Components.Remove(this);
                //(r_Game as GameWithScreens).setScreenStackOnGameOver((r_Game as GameWithScreens).ScreensManager);
            }
            if (InputManager.KeyPressed(Keys.M))
            {
                ExitScreen();
                ScreensManager.SetCurrentScreen(new MainMenu(r_Game as GameWithScreens));

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
            string message = string.Format("Game Over!{0}{0} You're Scores Are{0}{1}{0}{0}ESC  - To Exit{0}HOME  - Start a new game{0}M  - Main Menu", System.Environment.NewLine, m_Scores);
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            SpriteBatch.DrawString(consolasFont, message, position, Color.White);
        }
    }
}
