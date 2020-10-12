using Infrastructure.Managers;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Controls;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Screens.MainMenu
{
    public class MainMenuScreen : GameScreen
    {
        private readonly Game r_Game;
        private SpriteBatch m_SpriteBatch;
        private ScreensMananger m_ScreensManager;
        private int m_XPosition = 100; 
        private int m_YPosition = 100;
        private int m_YDelta = 100;

        public MainMenuScreen(Game i_Game, SpriteBatch i_SpriteBatch) : base(i_Game)
        {
            r_Game = i_Game;
            m_SpriteBatch = i_SpriteBatch;
            m_ScreensManager = (ScreensMananger)this.ScreensManager;

            InitMenuItems();
        }

        private void InitMenuItems()
        {
            Texture2D buttonTexture = r_Game.Content.Load<Texture2D>(@"Sprites\Button");
            SpriteFont buttonFont = r_Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
            createMenu(buttonTexture, buttonFont);
        }

        private void createMenu(Texture2D i_ButtonTexture, SpriteFont i_ButtonFont)
        {
            MenuItemManager screenSettings = new MenuItemManager(r_Game, m_SpriteBatch, m_ScreensManager, "Screen Settings", new Vector2(100, 100), i_ButtonTexture, i_ButtonFont, eMainMenuItems.ScreenSettings);

            //Array menuItems = Enum.GetValues(typeof(eMainMenuItems));

            //foreach (eMainMenuItems menuItem in menuItems)

            //{
            //    switch (menuItem)
            //    {
            //        case eMainMenuItems.ScreenSettings:
            //            createButton(i_ButtonTexture, i_buttonFont, "Screen Settings");
            //            System.Console.WriteLine("LALALALALALA");
            //            break;
            //        case eMainMenuItems.PlayersNumber:
            //            createButton(i_ButtonTexture, i_buttonFont, "Number of Players");
            //            break;
            //        case eMainMenuItems.SoundSettings:
            //            createButton(i_ButtonTexture, i_buttonFont, "Sound Settings");
            //            break;
            //        case eMainMenuItems.Play:
            //            createButton(i_ButtonTexture, i_buttonFont, "Play");
            //            break;
            //        case eMainMenuItems.Quit:
            //            createButton(i_ButtonTexture, i_buttonFont, "Quit");
            //            break;
            //    }
            //}
        }

        //private void createButton(Texture2D i_ButtonTexture, SpriteFont i_buttonFont, string i_ButtonText)
        //{
        //    Button newGameButton = new Button(r_Game, i_ButtonTexture, i_buttonFont)
        //    {
        //        Position = new Vector2(m_XPosition, m_YPosition),
        //        Text = i_ButtonText,
        //    };

        //    newGameButton.Click += NewGameButton_Click;
        //}

        //private void NewGameButton_Click(object sender, EventArgs e)
        //{
        //    //r_Game.Cha);
        //}
    }
}
