//*** Guy Ronen © 2008-2011 ***//
using GameScreens.Screens;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Screens;
using System.Collections.Generic;

namespace SpaceInvaders
{
    public class GameWithScreens : Game
    {
        private GraphicsDeviceManager m_GraphicsManager;
        private InputManager m_inputManager;
        private SpriteBatch m_SpriteBatch;
        private SoundEffectInstance m_BackgroundSounds;
        private SoundEffectInstance m_MenuSound;
        private ScreensMananger m_ScreensManager;
        private SoundEffectInstance m_BackgroundSound;
        private SoundEffectInstance m_MenuMoveSound;
        private SoundEffectInstance m_CurrentSound;
        private readonly Dictionary<string, SoundEffectInstance> r_SpriteSoundEffects;

        public GameWithScreens()
        {
            m_GraphicsManager = new GraphicsDeviceManager(this);

            m_GraphicsManager.PreferredBackBufferWidth = 1500;
            m_GraphicsManager.PreferredBackBufferHeight = 1500;
            m_GraphicsManager.ApplyChanges();

            r_SpriteSoundEffects = new Dictionary<string, SoundEffectInstance>();

            m_SpriteBatch = new SpriteBatch(this.GraphicsDevice);

            this.Content.RootDirectory = "Content";
            initializeSounds();

            m_inputManager = new InputManager(this);

            ScreensMananger screensManager = new ScreensMananger(this);
            setScreenStack(screensManager);
        }
        public SoundEffectInstance BackgroundSound => m_BackgroundSound;
        public SoundEffectInstance MenuMoveSound => m_MenuMoveSound;
        public SoundEffectInstance CurrentSound => m_CurrentSound;
        public Dictionary<string, SoundEffectInstance> SpriteSoundEffects => r_SpriteSoundEffects;

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

        protected void initializeSounds()
        {
            m_BackgroundSound = this.Content.Load<SoundEffect>("Sounds/BGMusic").CreateInstance();
            m_BackgroundSound.IsLooped = true;
            m_BackgroundSound.Play();

            m_MenuMoveSound = this.Content.Load<SoundEffect>("Sounds/MenuMove").CreateInstance();

            List<string> soundsEffects = new List<string>(new string[] 
            {"EnemyGunShot", "EnemyKill", "MotherShipKill", "BarrierHit", "GameOver", "LevelWin", "LifeDie", "SSGunShot"}
            );
            foreach (string soundEffect in soundsEffects)
            {
                m_CurrentSound = this.Content.Load<SoundEffect>($"Sounds/{soundEffect}").CreateInstance();
                r_SpriteSoundEffects.Add(soundEffect, m_CurrentSound);
            }
        }

        protected override void Draw(GameTime i_GameTime)
        {
            m_GraphicsManager.GraphicsDevice.Clear(Color.White);

            base.Draw(i_GameTime);
        }
    }
}
