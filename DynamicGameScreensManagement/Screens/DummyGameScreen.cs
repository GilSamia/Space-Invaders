using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Screens
{
    class DummyGameScreen : GameScreen
    {
        private readonly Game r_Game;
        private Background m_Background;

        public DummyGameScreen(Game i_Game) : base(i_Game)
        {
            r_Game = i_Game;
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 100);
            this.Add(m_Background);
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
