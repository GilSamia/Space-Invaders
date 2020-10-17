using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Screens
{
    public class PauseScreen : GameScreen
    {
        public PauseScreen(Game i_Game) : base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = false;
            this.BlackTintAlpha = 0.4f;
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                ExitScreen();
            }
            if (InputManager.KeyPressed(Keys.M))
            {
                (Game as GameWithScreens).MuteSound();
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            try
            {
                SpriteBatch.Begin();
                displayPauseMessage();
            }
            finally
            {
                SpriteBatch.End();
            }
        }

        private void displayPauseMessage()
        {
            SpriteFont consolasFont = ContentManager.Load<SpriteFont>(@"Fonts\Consolas");
            string message = string.Format("The game is paused{0}{0} You can resume by pressing <R>", System.Environment.NewLine);
            Vector2 position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
            SpriteBatch.DrawString(consolasFont, message, position, Color.White);
        }
    }
}
