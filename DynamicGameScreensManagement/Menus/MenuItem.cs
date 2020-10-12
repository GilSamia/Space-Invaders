using Infrastructure.ObjectModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpaceInvaders.Menus
{
    public class MenuItem : Sprite
    {
        private readonly Game r_Game;
        private string m_MenuHeader;
        private const string k_AssetName = @"Sprites/Button";

        public MenuItem(Game i_Game, string i_MainMenuHeader) : base(k_AssetName, i_Game)
        {
            r_Game = i_Game;
            m_MenuHeader = i_MainMenuHeader;
        }

        public string MenuHeader
        {
            get
            {
                return m_MenuHeader;
            }
        }

        public override void Draw(GameTime i_GameTime)
        {
            Position = new Vector2(100, 100);
            Texture = r_Game.Content.Load<Texture2D>(@"Sprites\Button");
            base.Draw(i_GameTime);
        }

        private void createMenuItem()
        {

        }
    }
}
