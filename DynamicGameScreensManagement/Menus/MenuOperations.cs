using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Menus
{
    class MenuOperations: GameScreen
    {
        private readonly Game r_Game;


        public MenuOperations(Game i_Game) : base(i_Game)
        {
            r_Game = i_Game;
        }

        internal void updateCircularMenuPosition(int i_CurrentMenuItemIndex, List<string> i_MenuItemList)
        {
            if (InputManager.KeyPressed(Keys.Down))
            {
                i_CurrentMenuItemIndex++;
                if (i_CurrentMenuItemIndex == i_MenuItemList.Count)
                {
                    i_CurrentMenuItemIndex = 0;
                }
            }

            if (InputManager.KeyPressed(Keys.Up))
            {
                i_CurrentMenuItemIndex--;
                if (i_CurrentMenuItemIndex == -1)
                {
                    i_CurrentMenuItemIndex = i_MenuItemList.Count - 1;
                }
            }
        }

    }
}
