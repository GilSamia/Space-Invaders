using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace SpaceInvaders.Screens.MainMenu
{
    public class MenuItemManager : MenuItem
    {
        private readonly Game r_Game;
        private SpriteBatch m_SpriteBatch;
        private ScreensMananger m_ScreensManager;

        private string m_description;
        private Vector2 m_Position;
        private Texture2D m_ButtonTexture;
        private SpriteFont m_ButtonFont;
        private List<Component> m_Components;


        public MenuItemManager(Game i_Game, SpriteBatch i_SpriteBatch, ScreensMananger i_ScreensManager, string i_ItemDescription, Vector2 i_Position, Texture2D i_ButtonTexture, SpriteFont i_ButtonFont, eMainMenuItems i_MethodToRun)
        {
            r_Game = i_Game;
            m_SpriteBatch = i_SpriteBatch;
            m_ScreensManager = i_ScreensManager;

            m_description = i_ItemDescription;
            m_Position = i_Position;
            m_ButtonTexture = i_ButtonTexture;
            m_ButtonFont = i_ButtonFont;

            initMenuItem(i_MethodToRun, m_ScreensManager, i_Game, i_SpriteBatch);
        }

        public void initMenuItem(eMainMenuItems i_MethodToRun, ScreensMananger i_ScreensManager, Game i_Game, SpriteBatch i_SpriteBatch)
        {
            switch (i_MethodToRun)
            {
                case eMainMenuItems.ScreenSettings:
                    screenSettingsDelegate.Invoke(i_ScreensManager, i_Game, i_SpriteBatch);
                    break;
                    //case eMainMenuItems.PlayersNumber:
                    //    createButton(i_ButtonTexture, i_buttonFont, "Number of Players");
                    //    break;
                    //case eMainMenuItems.SoundSettings:
                    //    createButton(i_ButtonTexture, i_buttonFont, "Sound Settings");
                    //    break;
                    //case eMainMenuItems.Play:
                    //    createButton(i_ButtonTexture, i_buttonFont, "Play");
                    //    break;
                    //case eMainMenuItems.Quit:
                    //    createButton(i_ButtonTexture, i_buttonFont, "Quit");
                    //    break;
            }
        }

        public delegate void ScreenSettingsDelegate(ScreensMananger i_ScreensManager, Game i_Game, SpriteBatch i_SpriteBatch);
        ScreenSettingsDelegate screenSettingsDelegate = (i_ScreensManager, i_Game, i_SpriteBatch) => i_ScreensManager.SetCurrentScreen(new MainMenuScreen(i_Game, i_SpriteBatch));
    }
}
