﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Screens;
using GameScreens.Sprites;

namespace SpaceInvaders.Menus
{
    class SoundSettings : GameScreen
    {
        private readonly Game r_Game;
        private readonly Background r_Background;

        private readonly List<string> r_MenuItemList;
        private int m_CurrentMenuItemIndex;

        private bool m_SoundOn;
        private int m_BackgroundMusicVolume;
        private int m_SoundEffectVolume;

        public SoundSettings(Game i_Game) : base(i_Game)
        {
            r_Game = i_Game;
            r_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(r_Background);

            r_MenuItemList = new List<string>();
            m_CurrentMenuItemIndex = 0;

            m_SoundOn = true;
            m_BackgroundMusicVolume = 50;
            m_SoundEffectVolume = 50;

            initializeMenuItems();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        private void initializeMenuItems()
        {
            r_MenuItemList.Add(string.Format("Toggle Sound: {0}", boolToString(m_SoundOn)));
            r_MenuItemList.Add(string.Format("Background Music Volume: {0}", m_BackgroundMusicVolume));
            r_MenuItemList.Add(string.Format("Sounds Effects Volume: {0}", m_SoundEffectVolume));
            r_MenuItemList.Add("Done");
        }

        private string boolToString(bool i_BoolValue)
        {
            string ans = string.Empty;
            if (i_BoolValue)
            {
                ans = "On";
            }
            else
            {
                ans = "Off";
            }

            return ans;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateCircularMenuPosition();
            updateUserChoise();
            UpdateSettings();
        }

        private void updateCircularMenuPosition()
        {
            if (InputManager.KeyPressed(Keys.Down))
            {
                m_CurrentMenuItemIndex++;
                if (m_CurrentMenuItemIndex == r_MenuItemList.Count)
                {
                    m_CurrentMenuItemIndex = 0;
                }
            }

            if (InputManager.KeyPressed(Keys.Up))
            {
                m_CurrentMenuItemIndex--;
                if (m_CurrentMenuItemIndex == -1)
                {
                    m_CurrentMenuItemIndex = r_MenuItemList.Count - 1;
                }
            }
        }

        private void updateUserChoise()
        {
            if (InputManager.KeyPressed(Keys.PageUp) || InputManager.KeyPressed(Keys.PageDown))
            {
                if (InputManager.KeyPressed(Keys.PageUp))
                {
                    updateMenuItem(true);
                }
                else
                {
                    updateMenuItem(false);
                }
            }
        }

        private void updateMenuItem(bool i_increase)
        {
            switch (m_CurrentMenuItemIndex)
            {
                //Toggle Sound
                case 0:
                    toggleSound();
                    break;

                //Change backgroung volume
                case 1:
                    if(i_increase && m_BackgroundMusicVolume < 100)
                    {
                        m_BackgroundMusicVolume += 10;
                    }
                    else if (!i_increase && m_BackgroundMusicVolume > 0)
                    {
                        m_BackgroundMusicVolume -= 10;
                    }

                    r_MenuItemList[1] = string.Format("Background Music Volume: {0}", m_BackgroundMusicVolume);
                    break;

                //Change sound effect volume
                case 2:
                    if (i_increase && m_SoundEffectVolume < 100)
                    {
                        m_SoundEffectVolume += 10;
                    }
                    else if (!i_increase && m_SoundEffectVolume > 0)
                    {
                        m_SoundEffectVolume -= 10;
                    }

                    r_MenuItemList[1] = string.Format("Sounds Effects Volume: {0}", m_SoundEffectVolume);
                    break;

                //Done
                case 3:
                    ExitScreen();
                    break;
            }
        }

        private void toggleSound()
        {
            m_SoundOn = !m_SoundOn;
            r_MenuItemList[0] = string.Format("Toggle Sound: {0}", boolToString(m_SoundOn));
        }

        private void UpdateSettings()
        {
            //TODO: Update actual sound settings....
        }

        public override void Draw(GameTime gameTime)
        {
            try
            {
                base.Draw(gameTime);
                SpriteBatch.Begin();
                drawMenuItems();
            }
            finally
            {
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