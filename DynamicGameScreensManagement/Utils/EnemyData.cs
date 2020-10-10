using Microsoft.Xna.Framework;

namespace SpaceInvaders.Utils
{
    internal class EnemyData
    {
        internal EnemyData(Color i_enemyColor, int i_EnemyScore, string i_AssetName, Vector2 i_EnemyStartPosition, int i_EnemyTextureOffset, Point i_EnemyPoint)
        {
            EnemyColor = i_enemyColor;
            EnemyScore = i_EnemyScore;
            AssetName = i_AssetName;
            EnemyStartPosition = i_EnemyStartPosition;
            EnemyTextureOffset = i_EnemyTextureOffset;
            EnemyPoint = i_EnemyPoint;
        }

        internal Point EnemyPoint { get; private set; }
        internal Color EnemyColor { get; private set; }
        internal int EnemyScore { get; private set; }
        internal string AssetName { get; private set; }
        internal Vector2 EnemyStartPosition { get; private set; }
        internal int EnemyTextureOffset { get; private set; }
    }
}
