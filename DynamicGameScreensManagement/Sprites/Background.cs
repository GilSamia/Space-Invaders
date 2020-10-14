using Microsoft.Xna.Framework;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;

namespace GameScreens.Sprites
{
    public class Background : Sprite
    {
        public Background(GameScreen i_Game, string i_AssetName, int i_Opacity)
            : base(i_AssetName, i_Game.Game)
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

        public override void Draw(GameTime i_GameTime)
        {
            base.Draw(i_GameTime);
        }
    }
}
