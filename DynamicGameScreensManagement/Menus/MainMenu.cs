using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using System;
using Microsoft.Xna.Framework.Audio;
using GameScreens.Screens;
using SpaceInvaders.Screens;

namespace SpaceInvaders.Menus
{
    class MainMenu : GameScreen
    {
        private readonly GameWithScreens r_Game;
        private Background m_Background;
        private readonly List<string> r_MenuItemList;
        private MenuOperations m_MenuOperations;
        private int m_NumberOfPlayers;
        private int m_CurrentMenuItemIndex;

        public MainMenu(GameWithScreens i_Game) : base(i_Game)
        {
            r_Game = i_Game;
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);

            m_NumberOfPlayers = 1;
            r_MenuItemList = new List<string>();
            m_CurrentMenuItemIndex = 0;
            m_MenuOperations = new MenuOperations(r_Game);
            initializeMenuItems();
        }

        private void initializeMenuItems()
        {
            r_MenuItemList.Add("Screen Settings");
            r_MenuItemList.Add(string.Format("Players: {0}", numberOfPlayersToString()));
            r_MenuItemList.Add("Sound Settings");
            r_MenuItemList.Add("Play");
            r_MenuItemList.Add("Quit");
        }

        private string numberOfPlayersToString()
        {
            string numberInStr = string.Empty;
            if (m_NumberOfPlayers == 1)
            {
                numberInStr = "One";
            }
            else
            {
                numberInStr = "Two";
            }

            return numberInStr;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateCircularMenuPosition();
            updateUserChoise();
            if (InputManager.KeyPressed(Keys.M))
            {
                r_Game.MuteSound();
            }
        }

        private void toggleNumberOfPlayers()
        {
            if (m_NumberOfPlayers == 1)
            {
                m_NumberOfPlayers = 2;
                (Game as GameWithScreens).SinglePlayerGame = false;
            }
            else
            {
                m_NumberOfPlayers = 1;
                (Game as GameWithScreens).SinglePlayerGame = true;
            }
            r_MenuItemList[1] = string.Format("Players: {0}", numberOfPlayersToString());
        }

        private void updateCircularMenuPosition()
        {

            if (InputManager.KeyPressed(Keys.Down))
            {
                r_Game.MenuMoveSound.Play();
                m_CurrentMenuItemIndex++;
                if (m_CurrentMenuItemIndex == r_MenuItemList.Count)
                {
                    m_CurrentMenuItemIndex = 0;
                }
            }

            if (InputManager.KeyPressed(Keys.Up))
            {
                r_Game.MenuMoveSound.Play();
                m_CurrentMenuItemIndex--;
                if (m_CurrentMenuItemIndex == -1)
                {
                    m_CurrentMenuItemIndex = r_MenuItemList.Count - 1;
                }
            }
        }

        private void updateUserChoise()
        {
            if (InputManager.KeyPressed(Keys.Enter))
            {
                switch (m_CurrentMenuItemIndex)
                {
                    //Screen Settings
                    case 0:
                        ScreensManager.SetCurrentScreen(new ScreenSettings(r_Game));
                        break;

                    //User wants to toggle the number of players, but they have to use Page up/ down.
                    case 1:
                        break;

                    //Sound Settings screen.
                    case 2:
                        ScreensManager.SetCurrentScreen(new SoundSettings(r_Game));
                        break;

                    //Play Game
                    case 3:
                        ExitScreen();
                        ScreensManager.SetCurrentScreen(new PlayScreen(Game)); ;
                        ScreensManager.SetCurrentScreen(new LevelTransitionScreen(Game, 1));
                        break;

                    //Quit Game (Exit)
                    case 4:
                        Game.Exit();
                        break;
                }
            }

            if (InputManager.KeyPressed(Keys.PageUp) || InputManager.KeyPressed(Keys.PageUp))
            {
                if (m_CurrentMenuItemIndex == 1)
                {
                    toggleNumberOfPlayers();
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
            try
            {
                SpriteBatch.Begin();
                drawMenuItems();
            }
            finally {
                SpriteBatch.End();
            } 
        }

        private void drawMenuItems()
        {
            SpriteFont fontCalibri = ContentManager.Load<SpriteFont>(@"Fonts\Calibri");

            Color activeMenuItemColor = Color.Pink;
            Color nonactiveMenuItem = Color.White;
            Color currentColor = Color.White;

            int offset = 35;
            int currentIndex = 0;

            foreach (string menuItem in r_MenuItemList)
            {
                Vector2 position = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2 + (offset * currentIndex));

                if (currentIndex == m_CurrentMenuItemIndex)
                {
                    currentColor = activeMenuItemColor;
                }
                else
                {
                    currentColor = nonactiveMenuItem;
                }

                SpriteBatch.DrawString(fontCalibri, $"{r_MenuItemList[currentIndex]}", position, currentColor);
                currentIndex++;
            }
        }
    }
}
