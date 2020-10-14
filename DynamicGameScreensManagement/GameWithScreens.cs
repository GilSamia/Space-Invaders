//*** Guy Ronen © 2008-2011 ***//
using GameScreens.Screens;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Screens;

namespace SpaceInvaders
{
    public class GameWithScreens : Game
    {
        GraphicsDeviceManager m_GraphicsManager;
        InputManager m_inputManager;
        SpriteBatch m_SpriteBatch;
        SoundEffectInstance m_BackgroundSounds;
        private ScreensMananger m_ScreensManager;

        public GameWithScreens()
        {
            m_GraphicsManager = new GraphicsDeviceManager(this);

            m_GraphicsManager.PreferredBackBufferWidth = 1500;
            m_GraphicsManager.PreferredBackBufferHeight = 1500;
            m_GraphicsManager.ApplyChanges();

            m_SpriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.Content.RootDirectory = "Content";

            m_inputManager = new InputManager(this);

            ScreensMananger screensManager = new ScreensMananger(this);
            setScreenStack(screensManager);
        }

        private void setScreenStack(ScreensMananger i_ScreensManager)
        {
            //i_ScreensManager.Push(new GameOverScreen(this));
            i_ScreensManager.Push(new PlayScreen(this));
            i_ScreensManager.Push(new LevelTransitionScreen(this, 1));
            i_ScreensManager.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
            addSound();
        }

        public void addSound()
        {
            m_BackgroundSounds = this.Content.Load<SoundEffect>("Sounds/BGMusic").CreateInstance();
            m_BackgroundSounds.IsLooped = true;
            m_BackgroundSounds.Play();
        }

        protected override void Draw(GameTime i_GameTime)
        {
            m_GraphicsManager.GraphicsDevice.Clear(Color.White);

            base.Draw(i_GameTime);
        }
    }
}
