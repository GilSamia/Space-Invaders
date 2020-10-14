using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using GameScreens.Sprites;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework.Audio;

namespace SpaceInvaders.Menus
{
    class ScreenSettings : GameScreen
    {   
        private readonly GameWithScreens r_Game;
        private Background m_Background;
        private readonly List<string> r_MenuItemList;

        private bool m_AllowWindowResizing;
        private bool m_FullScreenOn;
        private bool m_MouseVisable;
        private int m_CurrentMenuItemIndex;

        public ScreenSettings(GameWithScreens i_Game) : base(i_Game)
        {
            r_Game = i_Game;
            m_Background = new Background(this, @"Sprites\BG_Space01_1024x768", 1);
            this.Add(m_Background);
            m_CurrentMenuItemIndex = 0;

            m_AllowWindowResizing = false;
            m_FullScreenOn = false;
            m_MouseVisable = false;
            r_MenuItemList = new List<string>();

            initMenuItems();
        }

        private void initMenuItems()
        {
            r_MenuItemList.Add(string.Format("Allow Window Resizing: {0}", boolToString(m_AllowWindowResizing)));
            r_MenuItemList.Add(string.Format("Full Screen Mode: {0}", boolToString(m_FullScreenOn)));
            r_MenuItemList.Add(string.Format("Mouse Visability: {0}", isMouseVisable()));
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

        private string isMouseVisable()
        {
            string mouseVisability = string.Empty;
            if (this.Game.IsMouseVisible)
            {
                mouseVisability = "Visible";
            }
            else
            {
                mouseVisability = "Invisible";

            }

            return mouseVisability;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            updateCircularMenuPosition();
            updateMenuItem();
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

        private void updateMenuItem()
        {
            if (InputManager.KeyPressed(Keys.Enter) && m_CurrentMenuItemIndex == 3)
            {
                ExitScreen();
            }

            if ((InputManager.KeyPressed(Keys.PageUp) || InputManager.KeyPressed(Keys.PageDown)))
            {

                switch (m_CurrentMenuItemIndex)
                {
                    //Allow Window Resizing
                    case 0:
                        toggleWindowResizing();
                        break;

                    //Full Screen Mode
                    case 1:
                        toggleFullScreenMode();
                        break;

                    //Mouse Visability
                    case 2:
                        toggleMouseVisability();
                        break;

                    //Done
                    case 3:
                        //ExitScreen();
                        break;
                }
            }

        }

        private void toggleMouseVisability()
        {
            r_Game.IsMouseVisible = !r_Game.IsMouseVisible;
            m_MouseVisable = !m_MouseVisable;
            r_MenuItemList[2] = string.Format("Mouse Visability: {0}", boolToString(m_MouseVisable));
        }

        private void toggleFullScreenMode()
        {
            (r_Game as GameWithScreens).GraphicsManager.ToggleFullScreen();
            //TODO: WHY THIS IS NOT WORKING?????

            m_FullScreenOn = !m_FullScreenOn;
            r_MenuItemList[1] = string.Format("Full Screen Mode: {0}", boolToString(m_FullScreenOn));
        }

        private void toggleWindowResizing()
        {
            r_Game.Window.AllowUserResizing = !r_Game.Window.AllowUserResizing;
            m_AllowWindowResizing = !m_AllowWindowResizing;
            r_MenuItemList[0] = string.Format("Allow Window Resizing: {0}", boolToString(m_AllowWindowResizing));
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
