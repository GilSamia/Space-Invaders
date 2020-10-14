//*** Guy Ronen © 2008-2011 ***//
using System;
using GameScreens.Sprites;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameScreens.Screens
{
    public class GameOverScreen : GameScreen
    {
        Sprite m_GameOverMessage;
        Sprite m_PressEnterMsg;

        Background m_Background;
        public GameOverScreen(Game i_Game)
            : base(i_Game)
        {
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 100);
            m_Background.TintColor = Color.Red;
            this.Add(m_Background);

            m_GameOverMessage = new Sprite(@"Sprites\GameOverMessage", this.Game);
            this.Add(m_GameOverMessage);
        }

        public override void Initialize()
        {
            base.Initialize();

            m_GameOverMessage.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, 1.05f, 0.7f));
            m_GameOverMessage.Animations.Enabled = true;
            m_GameOverMessage.PositionOrigin = m_GameOverMessage.SourceRectangleCenter;
            m_GameOverMessage.RotationOrigin = m_GameOverMessage.SourceRectangleCenter;
            m_GameOverMessage.Position = CenterOfViewPort;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.Escape))
            {
                ExitScreen();
            }
        }
    }
}
