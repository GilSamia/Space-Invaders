using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace SpaceInvaders.Menus
{
    public class SubMenu : MenuItem
    {
        private readonly List<MenuItem> r_MenuItems;

        public SubMenu(Game i_Game, string i_Header, List<MenuItem> i_MenuItems) : base(i_Game, i_Header)
        {
            this.r_MenuItems = i_MenuItems;
        }

        public List<MenuItem> MenuItems
        {
            get
            {
                return this.r_MenuItems;
            }
        }
    }
}
