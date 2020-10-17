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
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace GameScreens.Screens
{
    public class PlayScreen : GameScreen
    {
        internal const int k_NumberOfCols = 9;
        private const float k_BarrierSpeedIncrement = 1.06f;

        private static int s_Level = 1;
        private int m_CurrentLevel;
        private readonly Game r_Game;
        private Background m_Background;

        private EnemyMatrix m_EnemyMatrix;
        private MotherShip m_MotherShip;
        private Barriers m_Barriers;
        private readonly List<SpaceShip> r_SpaceShips = new List<SpaceShip>();

        private bool m_IsInit = false;
        private bool m_FirstGamingRound;

        private PauseScreen m_PauseScreen;
        private LifeAndScore m_LifeAndScore;
        private readonly List<PlayerInformation> r_PlayerInformation;

        SpriteFont m_FontCalibri;

        private readonly string r_BackgroundTexturePath = @"Sprites\BG_Space01_1024x768";
        private Vector2 m_BackgroundPosition;
        private readonly Color r_BackgroundTint = Color.White;

        public PlayScreen(Game i_Game, int i_Level)
            : base(i_Game)
        {
            m_PauseScreen = new PauseScreen(i_Game);
            r_PlayerInformation = r_PlayerInformation = new List<PlayerInformation>();
            r_Game = i_Game;
            m_CurrentLevel = i_Level;
            m_FirstGamingRound = true;
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        private PlayScreen(Game i_Game, int i_Level, List<PlayerInformation> i_PlayerInformation) : base(i_Game)
        {
            m_PauseScreen = new PauseScreen(i_Game);
            r_Game = i_Game;
            
            m_CurrentLevel = i_Level;
//            r_SpaceShips = i_SpaceShips;
            r_PlayerInformation = i_PlayerInformation;
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
        }

        public int CurrentLevel
        {
            get { return m_CurrentLevel; }
        }

        public override void Initialize()
        {
            initializeGameComponents();
            base.Initialize();
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale, Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }

        private void initializeGameComponents()
        {
            if (!m_IsInit)
            {
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
            m_Barriers = new Barriers(this, @"Sprites/Barrier_44x32", m_CurrentLevel);
        }

        public void addEnemyMatrix()
        {
            m_EnemyMatrix = new EnemyMatrix(this, @"Sprites/AllEnemies_192x32", m_CurrentLevel, ((CurrentLevel - 1) % 4) + k_NumberOfCols);
        }

        private void addMotherShip()
        {
            m_MotherShip = new MotherShip(this);
        }

        private void addBackground()
        {
            Game.Window.ClientSizeChanged += Window_ClientSizeChanged;
            m_Background = new Background(this, @"Sprites/BG_Space01_1024x768", 1);
            this.Add(m_Background);
        }

        private void addSpaceShips()
        {
            PlayerData player1Data = (new PlayerData(Keys.I, Keys.P, Keys.D9, @"Sprites/Ship01_32x32"));
            if (m_FirstGamingRound)
            {
                r_SpaceShips.Add(new SpaceShipWithMouse(this, player1Data));
            }
            else
            {
                r_SpaceShips.Add(new SpaceShipWithMouse(this, player1Data, r_PlayerInformation[0]));
            }

            if (!(Game as GameWithScreens).SinglePlayerGame)
            {
                PlayerData player2Data = (new PlayerData(Keys.W, Keys.R, Keys.D3, @"Sprites/Ship02_32x32"));
                if (m_FirstGamingRound)
                {
                    r_SpaceShips.Add(new SpaceShip(this, player2Data));
                }
                else
                {
                    r_SpaceShips.Add(new SpaceShip(this, player2Data, r_PlayerInformation[1]));
                }
            }
        }

        public void OnGameOver()
        {
            string massage = buildMessage();
            m_FirstGamingRound = true;
            if (!(Game as GameWithScreens).SinglePlayerGame)
            {
                this.r_SpaceShips.Remove(r_SpaceShips[0]);
            }
            r_Game.Components.Remove(this);
            Game.Components.Remove(this);
            ExitScreen();

            ScreensManager.SetCurrentScreen(new GameOverScreen(Game, massage));
        }

        private string buildMessage()
        {
            string playersInformationScore = string.Empty;
            int scoreOfWinningPlayer = 0;
            SpaceShip winningSpaceShip = null;

            foreach (SpaceShip spaceShip in r_SpaceShips)
            {
                if (scoreOfWinningPlayer <= spaceShip.CurrentPlayerInformation.CurrentScore)
                {
                    winningSpaceShip = spaceShip;
                    scoreOfWinningPlayer = spaceShip.CurrentPlayerInformation.CurrentScore;
                }

                playersInformationScore += string.Format("Player {0}, Your Score is: {1}{2}", spaceShip.CurrentPlayerInformation.PlayerIndex + 1,
                    spaceShip.CurrentPlayerInformation.CurrentScore, Environment.NewLine);
            }

            string winningMessage = string.Format("Player {0} you won!{1}{1}", winningSpaceShip.CurrentPlayerInformation.PlayerIndex + 1,
                Environment.NewLine);
            winningMessage += playersInformationScore;

            return winningMessage;
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

            if (InputManager.KeyPressed(Keys.P))
            {
                m_ScreensManager.SetCurrentScreen(new PauseScreen(this.Game));
            }
            if (InputManager.KeyPressed(Keys.M))
            {
                (Game as GameWithScreens).MuteSound();
            }
        }

        public void moveLevel()
        {
            m_FirstGamingRound = false;
            s_Level++;
            ExitScreen();
            if (r_PlayerInformation.Count > 0) 
            {
                r_PlayerInformation.Clear(); 
            }
            foreach (SpaceShip spaceShip in r_SpaceShips)
            {
                
                r_PlayerInformation.Add(spaceShip.CurrentPlayerInformation);
            }

            (Game as GameWithScreens).SpriteSoundEffects["LevelWin"].Play();
            (base.m_ScreensManager as ScreensMananger).Push(new PlayScreen(this.Game, s_Level, r_PlayerInformation));
            base.m_ScreensManager.SetCurrentScreen(new LevelTransitionScreen(this.Game, s_Level));
        }

        private void Window_ClientSizeChanged(object sender, EventArgs e)
        {
            m_Background.Scales = new Vector2(Game.Window.ClientBounds.Width / m_Background.WidthBeforeScale,
                Game.Window.ClientBounds.Height / m_Background.HeightBeforeScale);
        }

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
            try
            {
                m_SpriteBatch.Begin();
                DrawCurrentLives(m_SpriteBatch);
                DrawScore(m_SpriteBatch);
            }
            finally
            {
                m_SpriteBatch.End();
            }
        }

        public void DrawCurrentLives(SpriteBatch i_SpriteBatch)
        {
            int spaceShipCounter = 1;
            foreach (SpaceShip spaceship in r_SpaceShips)
            {
                int numberOfLifeOfCurrentSpaceShip = spaceship.CurrentPlayerInformation.CurrentLife;
                string assetName = spaceship.AssetName;
                Color color = Color.White;
                Point scale = new Point(spaceship.Texture.Width / 2, spaceship.Texture.Height / 2);

                for (int i = 0; i <= numberOfLifeOfCurrentSpaceShip; i++) {
                    Point position = 
                        new Point(i_SpriteBatch.GraphicsDevice.Viewport.Width - (spaceship.Texture.Width / 2 * i), spaceship.Texture.Height / 2 * (spaceShipCounter - 1));
                    Rectangle rectangle = new Rectangle(position, scale);

                    i_SpriteBatch.Draw(spaceship.Texture, rectangle, color);
                }

            spaceShipCounter++;
            }
        }

        public void DrawScore(SpriteBatch i_SpriteBatch)
        {
            int spaceShipCounter = 1;
            List<Color> playersColor = new List<Color>() { Color.Blue, Color.Green };
            foreach (SpaceShip spaceship in r_SpaceShips)
            {
                int numberOfLifeOfCurrentSpaceShip = spaceship.CurrentPlayerInformation.CurrentScore;
                string text = String.Format("P{0} Score: {1}", spaceShipCounter, spaceship.CurrentPlayerInformation.CurrentScore);
                i_SpriteBatch.DrawString(m_FontCalibri, text, new Vector2(0, (spaceShipCounter - 1) * 20), playersColor[spaceShipCounter-1]);

                spaceShipCounter++;
            }
        }
    }
}
