using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Screens;
using GameScreens.Sprites;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaders.Menus
{
    class SoundSettings : GameScreen
    {
        private readonly GameWithScreens r_Game;
        private readonly Background r_Background;

        private readonly List<string> r_MenuItemList;
        private int m_CurrentMenuItemIndex;

        private bool m_SoundOn;
        private int m_BackgroundMusicVolume;
        private int m_SoundEffectVolume;

        public SoundSettings(GameWithScreens i_Game) : base(i_Game)
        {
            r_Game = i_Game;
            r_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(r_Background);

            r_MenuItemList = new List<string>();
            m_CurrentMenuItemIndex = 0;

            m_SoundOn = true;
            m_BackgroundMusicVolume = (int)(i_Game.BackgroundSound.Volume * 100);
            m_SoundEffectVolume = (int)(i_Game.SoundEffectVolume * 100);

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
            if (InputManager.KeyPressed(Keys.M))
            {
                r_Game.MuteSound();
            }
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
            if (InputManager.KeyPressed(Keys.Enter) && m_CurrentMenuItemIndex == 3)
            {
                ExitScreen();
            }

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
            if (InputManager.KeyPressed(Keys.Enter) && m_CurrentMenuItemIndex == 3)
            {
                ExitScreen();
            }

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
                        r_Game.BackgroundSound.Volume += 0.1f;
                        m_BackgroundMusicVolume += 10;
                    }
                    else if (!i_increase && m_BackgroundMusicVolume > 0)
                    {
                        r_Game.BackgroundSound.Volume -= 0.1f;
                        m_BackgroundMusicVolume -= 10;
                    }

                    r_MenuItemList[1] = string.Format("Background Music Volume: {0}", m_BackgroundMusicVolume);
                    break;

                //Change sound effect volume
                case 2:
                    if (i_increase && m_SoundEffectVolume < 100)
                    {
                        (Game as GameWithScreens).SoundEffectVolume += 0.1f;
                        m_SoundEffectVolume += 10;
                    }
                    else if (!i_increase && m_SoundEffectVolume > 0)
                    {
                        (Game as GameWithScreens).SoundEffectVolume -= 0.1f;
                        m_SoundEffectVolume -= 10;
                    }

                    r_MenuItemList[2] = string.Format("Sounds Effects Volume: {0}", m_SoundEffectVolume);
                    break;

                //Done
                case 3:
                    break;
            }
        }

        private void toggleSound()
        {
            r_Game.MuteSound();
            m_SoundOn = !m_SoundOn;
            r_MenuItemList[0] = string.Format("Toggle Sound: {0}", boolToString(m_SoundOn));
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
