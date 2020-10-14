//*** Guy Ronen © 2008-2011 ***//
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameScreens.Screens
{
    public class GameInstructionsScreen : GameScreen
    {
        SpriteFont m_FontCalibri;
        Vector2 m_MsgPosition = new Vector2(70, 300);

        public GameInstructionsScreen(Game i_Game)
            : base(i_Game)
        {
            this.IsModal = true;
            this.IsOverlayed = true;
            this.UseGradientBackground = true;
            this.BlackTintAlpha = 0.65f;
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            SpriteBatch.Begin();
            drawInstructions();
            SpriteBatch.End();
        }

        private void drawInstructions()
        {
            SpriteBatch.DrawString(m_FontCalibri, @"
[ Instructions ]
Use the arrows to move the walking square.
Try to avoid reaching thr right of the screen..

R - Resume Game", m_MsgPosition, Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.R))
            {
                this.ExitScreen();
            }
        }

        protected override void LoadContent()
        {
            base.LoadContent();

            m_FontCalibri = ContentManager.Load<SpriteFont>(@"Fonts\Calibri");
        }
    }
}

