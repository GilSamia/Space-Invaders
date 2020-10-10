//*** Guy Ronen © 2008-2011 ***//
using System;
using Infrastructure.ObjectModel;
using Infrastructure.ServiceInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Infrastructure.ObjectModel.Animators.ConcreteAnimators;

namespace GameScreens.Sprites
{
    public class WalkingSquare : Sprite
    {
        private const string k_AssetName = @"Sprites\WalkingSquare";
        private const int k_NumOfFrames = 5;

        IInputManager m_InputManager = null;

        public WalkingSquare(Game i_Game)
            : base(k_AssetName, i_Game)
        {
        }

        public override void Initialize()
        {
            base.Initialize();

            m_InputManager = Game.Services.GetService(typeof(IInputManager)) as IInputManager;

            this.Animations.Add(new CelAnimator(TimeSpan.FromSeconds(0.3), k_NumOfFrames, TimeSpan.Zero));
            this.Animations.Add(new PulseAnimator("Pulse", TimeSpan.Zero, 1.05f, 1f));
            this.Animations.Enabled = true;
        }

        protected override void InitSourceRectangle()
        {
            base.InitSourceRectangle();

            this.SourceRectangle = new Rectangle(
                0,
                0,
                (int)(m_SourceRectangle.Width/k_NumOfFrames),
                (int)m_HeightBeforeScale);
        }

        protected override void InitBounds()
        {
            base.InitBounds();

            this.WidthBeforeScale /= k_NumOfFrames;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.Animations["CelAnimation"].Enabled = true;
            if (m_InputManager.KeyHeld(Keys.Left))
            {
                this.m_Velocity.X -= 5;
            }
            else if (m_InputManager.KeyHeld(Keys.Right))
            {
                this.m_Velocity.X += 5;
            }
            else
            {
                if (this.Velocity.X <= 1 && this.Velocity.X >= -1)
                {
                    this.Velocity = Vector2.Zero;
                    this.Animations["CelAnimation"].Reset();
                    this.Animations["CelAnimation"].Enabled = false;
                }
                else
                {
                    this.Velocity *= 0.95f;
                }
            }

            this.Velocity = Vector2.Clamp(Velocity, new Vector2(-400), new Vector2(400));
        }
    }
}
