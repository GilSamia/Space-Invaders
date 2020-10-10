using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace SpaceInvaders.Sprites.SpaceShips
{
    internal class Barriers : Sprite
    {

        private const int k_NumberOfBarriers = 4;
        private readonly List<Barrier> r_Barriers;

        private const int k_TextureSize = 44;
        private const float k_DistancePrecentage = 1.3f;

        public Barriers(GameScreen i_Game, string i_AssetName)
            : base(i_AssetName, i_Game.Game)
        {
            r_Barriers = new List<Barrier>();
        }

        public override void Initialize()
        {
            base.Initialize();
            float y = Game.Window.ClientBounds.Height - (SpaceShip.TexutreSize * 2 + Texture.Height);
            float barriersWidth = Texture.Width * k_NumberOfBarriers + Texture.Width * k_DistancePrecentage * (k_NumberOfBarriers - 1);
            float leftestBarrier = (Game.Window.ClientBounds.Width - barriersWidth) / 2;
            float currentX = leftestBarrier;

            for (int i = 0; i < k_NumberOfBarriers; i++)
            {
                Vector2 position = new Vector2(currentX, y);
                Color[] pixels = new Color[Texture.Width * Texture.Height];
                Texture.GetData<Color>(pixels);
                Color[] clonePixels = pixels.Clone() as Color[];

                r_Barriers.Add(new Barrier(Game, AssetName + i, position, clonePixels));
                currentX += (Texture.Width + Texture.Width * k_DistancePrecentage);
            }
        }

        public override void Update(GameTime i_GameTime)
        {
        }

        public override void Draw(GameTime i_GameTime)
        {

        }
    }
}
