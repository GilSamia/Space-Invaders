using Microsoft.Xna.Framework;
using System;

namespace SpaceInvaders.Menus
{
    public class OperationOption : MenuItem
    {
        private RunnerDelegate m_Operation;

        public OperationOption(Game i_Game, string i_Hearder, RunnerDelegate i_Operation) : base(i_Game, i_Hearder)
        {
            this.m_Operation = i_Operation;
        }

        public void RunProgram()
        {
            this.m_Operation();
        }
    }
}
