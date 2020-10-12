using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SpaceInvaders.Menus.MenuItems;

namespace SpaceInvaders.Menus
{
    public class DelegatesMenu
    {
        public static MainMenu CreateDelegatesMenu(Game i_Game)
        {
            MainMenu mainMenu = new MainMenu(
                i_Game,
                "Main Menu:",
                new List<MenuItem>
                {
                    new SubMenu(
                        i_Game,
                        "Screen Settings",
                        new List<MenuItem>
                        {
                            new OperationOption(i_Game, "Allow Window Resizing", new AllowWindowResizing().RunProgram),
                            new OperationOption(i_Game, "Full Screen Mode", new FullScreenMode().RunProgram),
                            new OperationOption(i_Game, "Mouse Visability: Visable/Invisible", new MouseVisability().RunProgram)
                        }),

                    new OperationOption(i_Game, "Players: One/Two", new NumberOfPlayers().RunProgram),

                        new SubMenu(
                        i_Game, 
                        "Sound Settings",
                        new List<MenuItem>
                        {
                            new OperationOption(i_Game, "Toggle Sound", new ToggleSound().RunProgram),
                            new OperationOption(i_Game, "Background Music Volume: 0-100", new BackgroundMusicVolume().RunProgram),
                            new OperationOption(i_Game, "Sounds Effects Volume: 0-100", new SoundEffectVolume().RunProgram)
                        })
                });

            return mainMenu;
        }
    }
}
