﻿using SpaceInvaders.Interfaces;
using System;

namespace SpaceInvaders.Menus.MenuItems
{
    class MouseVisability : IMenuOperation
    {
        public void RunProgram()
        {
            Console.WriteLine(string.Format("The date today is: {0}", DateTime.Now.ToString("dd/MM/yyyy")));
        }
    }
}
