//*** Guy Ronen © 2008-2011 ***//
using GameScreens.Screens;
using Infrastructure.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Menus;
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

        private float m_BackgroundSoundVolume;
        private float m_SoundEffectVolume;

        private bool m_Muted = false;

        private bool m_SinglePlayerGame = true;

        private readonly Dictionary<string, SoundEffectInstance> r_SpriteSoundEffects;
        private List<string> m_SoundsEffects;

        public GameWithScreens()
        {
            m_GraphicsManager = new GraphicsDeviceManager(this);

            m_GraphicsManager.PreferredBackBufferWidth = 1500;
            m_GraphicsManager.PreferredBackBufferHeight = 1500;
            m_GraphicsManager.ApplyChanges();

            r_SpriteSoundEffects = new Dictionary<string, SoundEffectInstance>();
            m_SpriteBatch = new SpriteBatch(this.GraphicsDevice);
            new CollisionsManager(this);
            this.Content.RootDirectory = "Content";
            m_inputManager = new InputManager(this);

            m_ScreensManager = new ScreensMananger(this);
            setScreenStack(m_ScreensManager);
        }
        
        public ScreensMananger ScreensManager => m_ScreensManager;
        public GraphicsDeviceManager GraphicsManager => m_GraphicsManager;

        public SoundEffectInstance BackgroundSound => m_BackgroundSound;
        
        public SoundEffectInstance MenuMoveSound => m_MenuMoveSound;
        
        public SoundEffectInstance CurrentSound => m_CurrentSound;
        
        public List<string> SoundsEffects => m_SoundsEffects;

        public float SoundEffectVolume {
            get { return m_SoundEffectVolume; }
            set { m_SoundEffectVolume = value;
                setSoundEffectVolume(m_SoundEffectVolume);
            }
        }

        public bool SinglePlayerGame
        {
            get { return m_SinglePlayerGame; }
            set { m_SinglePlayerGame = value;}
        }

        public Dictionary<string, SoundEffectInstance> SpriteSoundEffects => r_SpriteSoundEffects;

        private void setScreenStack(ScreensMananger i_ScreensManager)
        {
            //i_ScreensManager.Push(new GameOverScreen(this));
            //i_ScreensManager.Push(new PlayScreen(this));
            //i_ScreensManager.Push(new LevelTransitionScreen(this, 1));
            i_ScreensManager.Push(new DummyGameScreen(this));
            i_ScreensManager.SetCurrentScreen(new WelcomeScreen(this));
        }

        protected override void Initialize()
        {
            base.Initialize();
            initializeSounds();
        }

        protected void initializeSounds()
        {
            m_BackgroundSound = this.Content.Load<SoundEffect>("Sounds/BGMusic").CreateInstance();
            m_BackgroundSound.Volume = 0.5f;
            m_BackgroundSound.IsLooped = true;
            m_BackgroundSound.Play();

            m_MenuMoveSound = this.Content.Load<SoundEffect>("Sounds/MenuMove").CreateInstance();
            m_MenuMoveSound.Volume = 0.5f;

            m_SoundEffectVolume = 0.5f;
            m_SoundsEffects = new List<string>(new string[] 
            {"EnemyGunShot", "EnemyKill", "MotherShipKill", "BarrierHit", "GameOver", "LevelWin", "LifeDie", "SSGunShot"}
            );
            foreach (string soundEffect in m_SoundsEffects)
            {
                m_CurrentSound = this.Content.Load<SoundEffect>($"Sounds/{soundEffect}").CreateInstance();
                m_CurrentSound.Volume = 0.5f;
                r_SpriteSoundEffects.Add(soundEffect, m_CurrentSound);
            }
        }

        public void MuteSound()
        {
            if (!m_Muted)
            {
                m_BackgroundSoundVolume = BackgroundSound.Volume;
                m_SoundEffectVolume = MenuMoveSound.Volume;

                m_BackgroundSound.Volume = 0.0f;
                m_MenuMoveSound.Volume = 0.0f;
                setSoundEffectVolume(0.0f);
                m_Muted = true;
            }
            else
            {
                m_BackgroundSound.Volume = m_BackgroundSoundVolume;
                m_MenuMoveSound.Volume = m_SoundEffectVolume;
                setSoundEffectVolume(m_SoundEffectVolume);
                m_Muted = false;
            }
        }

        private void setSoundEffectVolume(float i_Volume)
        {
            if (i_Volume <= 1.0f && i_Volume >= 0.0f)
            {
                foreach (string soundEffect in m_SoundsEffects)
                {
                    r_SpriteSoundEffects[$"{soundEffect}"].Volume = i_Volume;
                }
            }
        }

        protected override void Draw(GameTime i_GameTime)
        {
            m_GraphicsManager.GraphicsDevice.Clear(Color.White);

            base.Draw(i_GameTime);
        }
    }
}
