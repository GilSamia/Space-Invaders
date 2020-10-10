namespace SpaceInvaders.Interfaces
{
    internal interface IDeadable
    {
        void OnKill(IShooter i_MyKiller);
    }
}
