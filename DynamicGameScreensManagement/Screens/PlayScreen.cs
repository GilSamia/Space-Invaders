//*** Guy Ronen © 2008-2011 ***//
using GameScreens.Sprites;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpaceInvaders;
using SpaceInvaders.Screens;
using SpaceInvaders.Sprites;
using SpaceInvaders.Sprites.Enemies;
using SpaceInvaders.Sprites.SpaceShips;
using SpaceInvaders.Utils;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameScreens.Screens
{
    public class PlayScreen : GameScreen
    {
        private readonly Game r_Game;
        private Background m_Background;
        private EnemyMatrix m_EnemyMatrix;
        private MotherShip m_MotherShip;
        private Barriers m_Barriers;
        private bool m_IsInit = false;
        private readonly List<SpaceShip> r_SpaceShips = new List<SpaceShip>();

        private bool m_IsGameOver = false;

        GameInstructionsScreen m_GameInstructionsScreen;
        PauseScreen m_PauseScreen;

        SpriteFont m_FontCalibri;

        private readonly string r_BackgroundTexturePath = @"Sprites\BG_Space01_1024x768";
        private Vector2 m_BackgroundPosition;
        private readonly Color r_BackgroundTint = Color.White;

        public PlayScreen(Game i_Game)
            : base(i_Game)
        {
            m_GameInstructionsScreen = new GameInstructionsScreen(i_Game);
            m_PauseScreen = new PauseScreen(i_Game);
            r_Game = i_Game;
        }

        public override void Initialize()
        {
            initializeGameComponents();
            base.Initialize();
        }

        private void initializeGameComponents()
        {
            if (!m_IsInit)
            {
                addManagers();
                addBackground();
                addSpaceShips();
                addMotherShip();
                addBarriers();
                addEnemyMatrix();
                m_IsInit = true;
            }
        }

        public void addBarriers()
        {
            m_Barriers = new Barriers(this, @"Sprites/Barrier_44x32");
        }

        public void addEnemyMatrix()
        {
            m_EnemyMatrix = new EnemyMatrix(this, @"Sprites/AllEnemies_192x32");
        }

        private void addMotherShip()
        {
            m_MotherShip = new MotherShip(this);
        }

        private void addManagers()
        {
            //new InputManager(r_Game);
            new CollisionsManager(r_Game);
        }

        private void addBackground()
        {
            m_Background = new Background(this, @"Sprites/BG_Space01_1024x768", 1);
            this.Add(m_Background);
        }

        private void addSpaceShips()
        {
            List<PlayerData> playersData = new List<PlayerData>();

            PlayerData player1Data = (new PlayerData(Keys.I, Keys.P, Keys.D9, @"Sprites/Ship01_32x32"));
            r_SpaceShips.Add(new SpaceShipWithMouse(this, player1Data));

            if (!(Game as GameWithScreens).SinglePlayerGame)
            {
                playersData.Add(new PlayerData(Keys.W, Keys.R, Keys.D3, @"Sprites/Ship02_32x32"));
            }

            foreach (PlayerData data in playersData)
            {
                r_SpaceShips.Add(new SpaceShip(this, data));
            }
        }

        public void OnGameOver()
        {
            this.ExitScreen();
        }

        protected override void LoadContent()
        {
            m_SpriteBatch = new SpriteBatch(r_Game.GraphicsDevice);
            m_FontCalibri = ContentManager.Load<SpriteFont>(@"Fonts\Calibri");
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (InputManager.KeyPressed(Keys.I))
            {
                ScreensManager.SetCurrentScreen(m_GameInstructionsScreen);
            }

            if (InputManager.KeyPressed(Keys.P))
            {
                m_ScreensManager.SetCurrentScreen(new PauseScreen(this.Game));
            }
            if (InputManager.KeyPressed(Keys.M))
            {
                (Game as GameWithScreens).MuteSound();
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
        }
    }
}
