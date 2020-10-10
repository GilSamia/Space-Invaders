using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Controls;
using System;

namespace SpaceInvaders.Screens.MainMenu
{
    public class MainMenuScreen : MenusScreen
    {
        private readonly Game r_Game;
        private int m_XPosition = 100; 
        private int m_YPosition = 100;
        private int m_YDelta = 100;

        public MainMenuScreen(Game i_Game, SpriteBatch spriteBatch) : base(i_Game, "")
        {
            r_Game = i_Game;
            
        }

        protected override void InitMenuItems()
        {
            Texture2D buttonTexture = Game.Content.Load<Texture2D>(@"Controls\Button");
            SpriteFont buttonFont = Game.Content.Load<SpriteFont>(@"Fonts\Consolas");
            createMenu(buttonTexture, buttonFont);
        }

        private void createMenu(Texture2D i_ButtonTexture, SpriteFont i_buttonFont)
        {
            Array menuItems = Enum.GetValues(typeof(eMainMenuItems));
            foreach (eMainMenuItems menuItem in menuItems)

            {
                switch (menuItem)
                {
                    case eMainMenuItems.ScreenSettings:
                        createButton(i_ButtonTexture, i_buttonFont, "Screen Settings");
                        break;
                    case eMainMenuItems.PlayersNumber:
                        createButton(i_ButtonTexture, i_buttonFont, "Number of Players");
                        break;
                    case eMainMenuItems.SoundSettings:
                        createButton(i_ButtonTexture, i_buttonFont, "Sound Settings");
                        break;
                    case eMainMenuItems.Play:
                        createButton(i_ButtonTexture, i_buttonFont, "Play");
                        break;
                    case eMainMenuItems.Quit:
                        createButton(i_ButtonTexture, i_buttonFont, "Quit");
                        break;
                }
            }
        }

        private void createButton(Texture2D i_ButtonTexture, SpriteFont i_buttonFont, string i_ButtonText)
        {
            Button newGameButton = new Button(r_Game, i_ButtonTexture, i_buttonFont)
            {
                Position = new Vector2(m_XPosition, m_YPosition),
                Text = i_ButtonText,
            };

            newGameButton.Click += NewGameButton_Click;
        }

        private void NewGameButton_Click(object sender, EventArgs e)
        {
            //r_Game.ChangeState(new GameState(_game, _graphicsDevice, _content));
        }
    }
}
