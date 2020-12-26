using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpaceInvaders.Utils
{
    internal class PlayerData
    {
        internal PlayerData(Keys i_KeyLeft, Keys i_KeyRight, Keys i_KeyShoot, string i_AssetName)
        {
            KeyLeft = i_KeyLeft;
            KeyRight = i_KeyRight;
            KeyShoot = i_KeyShoot;
            AssetName = i_AssetName;
        }
        internal Keys KeyLeft { get; set; }
        internal Keys KeyRight { get; set; }
        internal Keys KeyShoot { get; set; }
        internal string AssetName { get; set; }
    }
}
