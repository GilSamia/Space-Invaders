using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Interfaces;
using System;

namespace SpaceInvaders.Menus.MenuItems
{
    class AllowWindowResizing : IMenuOperation
    {
        public void RunProgram()
        {
            Console.WriteLine(string.Format("The date today is: {0}", DateTime.Now.ToString("dd/MM/yyyy")));
        }
    }
}
