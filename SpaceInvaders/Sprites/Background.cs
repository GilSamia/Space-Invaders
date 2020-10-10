using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;

namespace GameScreens.Sprites
{
    public class Background : Sprite
    {
        public Background(Game i_Game, string i_AssetName, int i_Opacity)
            : base(i_AssetName, i_Game)
        {
            this.Opacity = i_Opacity;
        }

        protected override void InitBounds()
        {
            base.InitBounds();

            this.DrawOrder = int.MinValue;
        }

        public override void Initialize()
        {
            base.Initialize();
        }
    }
}
