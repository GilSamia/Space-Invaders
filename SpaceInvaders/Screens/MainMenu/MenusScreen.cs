using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Screens;

namespace SpaceInvaders.Screens.MainMenu
{
    public abstract class MenusScreen : GameScreen
    {
        private readonly Game r_Game;
        private string m_MenuTitle;
        private ButtonState m_LastBottonState = ButtonState.Released;

        public MenusScreen(Game i_Game, string i_MenuTitle)
            : base(i_Game)
        {
            r_Game = i_Game;
            m_MenuTitle = i_MenuTitle;
        }

        protected abstract void InitMenuItems();

        protected void AddMenuItems(params MenusScreen[] i_MenuItems)
        {
        }

        public override void Initialize()
        {
            InitMenuItems();
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        private void activateCurrentMenuItem()
        {
        }

        private void runMenuItemMethod()
        {
        }

        private void handleKeyboard()
        {
        }

        protected void done()
        {
            GameScreen previousScreen = ScreensManager.ActiveScreen.PreviousScreen;
            ScreensManager.Remove(ScreensManager.ActiveScreen);
            ScreensManager.SetCurrentScreen(previousScreen);
        }
    }

}

