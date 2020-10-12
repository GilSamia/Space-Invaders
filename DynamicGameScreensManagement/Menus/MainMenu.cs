using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Menus
{
    public class MainMenu : MenuItem
    {
        private const int k_MinNumberForUserSelection = 1;
        private const int k_BackorExitIndex = 0;
        private const string k_BackOption = "Done";
        private const string k_ExitOption = "Quit";
        private readonly Stack<SubMenu> m_MainMenuFlowStack;

        public MainMenu(Game i_Game, string i_Header, List<MenuItem> i_MainOptions) : base(i_Game, i_Header)
        {
            SubMenu mainMenu = new SubMenu(i_Game, i_Header, i_MainOptions);
            this.m_MainMenuFlowStack = new Stack<SubMenu>();
            this.m_MainMenuFlowStack.Push(mainMenu);
        }

        public void InitializeMainMenu()
        {
            bool wantsToStay = true;
            bool exitOptionExists = true;
            string lastOption = string.Empty;

            do
            {
                SubMenu subMenu = m_MainMenuFlowStack.Peek();
                exitOptionExists = this.m_MainMenuFlowStack.Count == 1;
                lastOption = string.Empty;

                if (exitOptionExists)
                {
                    lastOption = k_ExitOption;
                }
                else
                {
                    lastOption = k_BackOption;
                }

                createMenu(subMenu, lastOption);

                int userChoice = subMenu.MenuItems.Count;

                if (userChoice == 0)
                {
                    if (exitOptionExists)
                    {
                        wantsToStay = false;
                    }
                    else
                    {
                        this.m_MainMenuFlowStack.Pop();
                    }
                }
                else
                {
                    MenuItem chosenMenuItem = subMenu.MenuItems[userChoice - 1];

                    if (chosenMenuItem is SubMenu)
                    {
                        this.m_MainMenuFlowStack.Push(chosenMenuItem as SubMenu);
                    }
                    else if (chosenMenuItem is OperationOption)
                    {
                        GraphicsManager.GraphicsDevice.Clear(Color.Black);
                        (chosenMenuItem as OperationOption).RunProgram();
                        Console.ReadLine();
                    }
                }

                Console.Clear();
            }
            while (wantsToStay);
        }

        private string createMenu(SubMenu i_Menu, string i_LastOption)
        {
            StringBuilder menuItemForPrint = new StringBuilder(string.Format("{0}{1}", i_Menu.MenuHeader, System.Environment.NewLine));
            menuItemForPrint.Append('-', i_Menu.MenuHeader.Length);
            menuItemForPrint.AppendLine(System.Environment.NewLine);
            int operationsIndex = i_Menu.MenuItems.Count;

            for (int i = 1; i <= operationsIndex; i++)
            {
                menuItemForPrint.AppendLine(string.Format(@"{0}. {1}", i, i_Menu.MenuItems[i - 1].MenuHeader));
            }

            string menuItemForPrintWithExitOrBackOption = menuItemForPrint.AppendLine(string.Format("{0}. {1}", k_BackorExitIndex, i_LastOption)).ToString();
            string requestFromUser = string.Format("Please enter your choice: ({0} - {1} or 0 for {2}):", k_MinNumberForUserSelection, operationsIndex, i_LastOption.ToLower());
            return string.Format(@"{0}{1}{2}", menuItemForPrintWithExitOrBackOption, Environment.NewLine, requestFromUser);
        }
    }
}
