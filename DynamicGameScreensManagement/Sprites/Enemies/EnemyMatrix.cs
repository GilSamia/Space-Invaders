﻿using GameScreens.Screens;
using Infrastructure.ObjectModel;
using Infrastructure.ObjectModel.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SpaceInvaders.Utils;


namespace SpaceInvaders.Sprites.Enemies
{
    public class EnemyMatrix : Sprite
    {
        internal const int k_NumberOfRows = 1;
        internal const int k_NumberOfCols = 1;
        private const int k_DeadAmountToIncreaseSpeed = 5;
        private int m_NumberOfCurrentDeadEnemies;

        private const float k_PercentageOfSpeedIncrementDueToDead = 0.97F;
        private const float k_PercentageOfSpeedIncrementDueToRowDrop = 0.95F;
        private const float k_PercentageOfDistanceBetweenEnemies = 1.6F;

        private const int k_PinkEnemyScore = 300;
        private const int k_BlueEnemyScore = 200;
        private const int k_YellowEnemyScore = 70;

        private float m_EnemySize;
        private readonly GameScreen r_Game;
        private static int s_CurrentLevel;

        private readonly GameScreen r_GameScreen;

        public int NumberOfRows
        {
            get { return k_NumberOfRows; }
        }

        public int NumberOfCols
        {
            get { return (k_NumberOfCols); }
        }

        public int PinkEnemyScore
        {
            get { return k_PinkEnemyScore + ((s_CurrentLevel % 1) * 100); }
        }

        public int BlueEnemyScore
        {
            get { return k_BlueEnemyScore + ((s_CurrentLevel % 1) * 100); }
        }

        public int YellowEnemyScore
        {
            get { return k_YellowEnemyScore + ((s_CurrentLevel % 1) * 100); }
        }

        public EnemyMatrix(GameScreen i_Game, string i_AssetName, int i_Level)
            : base(i_AssetName, i_Game.Game)
        {
            r_Game = i_Game;
            r_GameScreen = i_Game;
            s_CurrentLevel = i_Level;
            i_Game.Add(this);
        }

        internal Enemy[,] m_EnemyMatrix = new Enemy[k_NumberOfRows, k_NumberOfCols + (s_CurrentLevel % 1)];

        internal void KillEnemyAt(Point i_EnemyPoint)
        {
            m_EnemyMatrix[i_EnemyPoint.X, i_EnemyPoint.Y] = null;
            m_NumberOfCurrentDeadEnemies++;
        }

        public override void Initialize()
        {
            base.Initialize();
            initializeEnemyMatrix();
        }

        private void initializeEnemyMatrix()
        {
            EnemyData currentEnemyData;
            Vector2 position;
            m_EnemySize = Texture.Height;
            float distance = k_PercentageOfDistanceBetweenEnemies * m_EnemySize;
            for (int i = 0; i < NumberOfRows; i++)
            {
                for (int j = 0; j < NumberOfCols + (s_CurrentLevel % 1); j++)
                {
                    position = new Vector2((float)j * distance, i * distance + m_EnemySize * 3);
                    Point point = new Point(i, j);

                    if (i == 0)
                    {
                        currentEnemyData = new EnemyData(Color.Pink, PinkEnemyScore, 
                            @"Sprites/AllEnemies_192x32", position, 0, point);
                    }
                    else if (i > 0 && i < 3)
                    {
                        currentEnemyData = new EnemyData(Color.LightBlue, BlueEnemyScore, @"Sprites/AllEnemies_192x32", position, 2, point);
                    }
                    else
                    {
                        currentEnemyData = new EnemyData(Color.LightYellow, YellowEnemyScore, @"Sprites/AllEnemies_192x32", position, 4, point);
                    }

                    Enemy enemy = new Enemy(r_GameScreen, currentEnemyData, this);
                    m_EnemyMatrix[i, j] = enemy;
                }
            }
        }

        public override void Update(GameTime i_GameTime)
        {
            s_CurrentLevel = (r_Game as PlayScreen).CurrentLevel;

            Enemy.JumpYDelta = 0;
            if (Enemy.JumpXDirection > 0)
            {
                setXDirectionRight();
            }
            else
            {
                setXDirectionLeft();
            }

            if (m_NumberOfCurrentDeadEnemies >= k_DeadAmountToIncreaseSpeed)
            {
                Enemy.ChangeSpeed(k_PercentageOfSpeedIncrementDueToDead);
                m_NumberOfCurrentDeadEnemies = 0;
            }

            isGameOver();
        }

        private void setXDirectionRight()
        {
            Vector2 rightestEnemyPosition;

            for (int j = NumberOfCols - 1; j >= 0; j--)
            {
                for (int i = 0; i < NumberOfRows; i++)
                {
                    if (m_EnemyMatrix[i, j] != null)
                    {
                        rightestEnemyPosition = m_EnemyMatrix[i, j].Position;
                        checkRightBoundery(rightestEnemyPosition);
                        return;
                    }
                }
            }
        }

        private void setXDirectionLeft()
        {
            Vector2 leftestEnemyPosition;

            for (int j = 0; j < NumberOfCols; j++)
            {
                for (int i = 0; i < NumberOfRows; i++)
                {
                    if (m_EnemyMatrix[i, j] != null)
                    {
                        leftestEnemyPosition = m_EnemyMatrix[i, j].Position;
                        checkLeftBoundery(leftestEnemyPosition);
                        return;
                    }
                }
            }
        }

        private void checkRightBoundery(Vector2 i_RightestEnemyPosition)
        {
            float widthViewPort = (float)GraphicsDevice.Viewport.Width;
            if (i_RightestEnemyPosition.X + m_EnemySize / 2 > widthViewPort - m_EnemySize)
            {
                float deltaX = widthViewPort - m_EnemySize - i_RightestEnemyPosition.X;
                setAllEnemiesXPosition(deltaX);
            }

            if (i_RightestEnemyPosition.X + m_EnemySize >= widthViewPort)
            {
                float deltaX = widthViewPort - m_EnemySize - i_RightestEnemyPosition.X;
                rowDropHandler();
            }
        }

        private void checkLeftBoundery(Vector2 i_LeftestEnemyPosition)
        {
            if (i_LeftestEnemyPosition.X - m_EnemySize / 2 < 0)
            {
                setAllEnemiesXPosition(-i_LeftestEnemyPosition.X);
            }
            if (i_LeftestEnemyPosition.X < 0)
            {
                rowDropHandler();
            }
        }

        private void rowDropHandler()
        {
            Enemy.JumpXDirection *= -1;
            Enemy.ChangeSpeed(k_PercentageOfSpeedIncrementDueToRowDrop);
            Enemy.JumpYDelta = m_EnemySize * 0.5F;
        }

        private void setAllEnemiesXPosition(float i_DeltaX)
        {
            foreach (Enemy enemy in m_EnemyMatrix)
            {
                if (enemy != null)
                {
                    enemy.Position += new Vector2(i_DeltaX, 0);
                }
            }
        }

        private void isGameOver()
        {
            foreach (Enemy enemy in m_EnemyMatrix)
            {
                if (enemy != null)
                {
                    return;
                }
            }
            (r_GameScreen as PlayScreen).moveLevel();
             //(r_Game as PlayScreen).OnGameOver();

        }


        public override void Draw(GameTime i_GameTime)
        {

        }
    }
}
