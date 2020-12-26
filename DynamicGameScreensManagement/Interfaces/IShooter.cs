using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;

namespace SpaceInvaders.Interfaces
{
    internal interface IShooter
    {
        Vector2 ShooterPosition { get; }

        float ShooterWidth { get; }

        void ReduceBulletsByOne();

        void OnHit(ICollidable2D i_Collidable);
    }
}
