using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Diplodocus.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace Diplodocus.Acteurs
{
    public class Player: MovingActorWithSpeed
    {
        private SpriteEffects effects;
        private bool animationLeftIsThere = false;
        private const int SPEED_DIRECTIONKEY_PRESSED = 5;
        private const int VERTICAL_IMPULSION = -10;
        protected bool IsJumpingThisTime = false;

        public bool IsInTheAir { get; set; }
        public bool IsShooting { get; set; }

        public Player(ContentManager loader, string xmlFilePath)
            : base(loader, xmlFilePath)
        {
            int searchIndex = 0;
            while (searchIndex < animations.Count && animationLeftIsThere == false)
            {
                if (animations[searchIndex].Type == AnimationState.Left)
                    animationLeftIsThere = true;
                searchIndex++;
            }
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardNow = Keyboard.GetState();
            effects = SpriteEffects.None;

            int directionDesiredX = 0;

            if (keyboardNow.IsKeyDown(Keys.D))
                directionDesiredX += SPEED_DIRECTIONKEY_PRESSED;
            if (keyboardNow.IsKeyDown(Keys.A))
                directionDesiredX -= SPEED_DIRECTIONKEY_PRESSED;
            if (keyboardNow.IsKeyDown(Keys.Space) && IsInTheAir == false)
            {
                IsInTheAir = true;
                IsJumpingThisTime = true;
            }
            if (keyboardNow.IsKeyDown(Keys.L) || presentState == AnimationState.Shoot)
            {
                if (finalFrame == false)
                    presentState = AnimationState.Shoot;
                else
                    presentState = AnimationState.Waiting;
            }
            else if (IsInTheAir == true)
                presentState = AnimationState.Jump;
            else if (directionDesiredX != 0 && animationLeftIsThere == false)
                presentState = AnimationState.Right;
            else if (directionDesiredX == 0)
                presentState = AnimationState.Waiting;
            else
                presentState = AnimationState.Left;
            if (directionDesiredX < 0)
                effects = SpriteEffects.FlipHorizontally;
            float speedYAxis;
            if(IsJumpingThisTime == true)
                speedYAxis = VERTICAL_IMPULSION;
            else if(IsInTheAir == true)
                speedYAxis = Speed.Y;
            else
                speedYAxis = 0;
            Speed = new Vector2(directionDesiredX,speedYAxis);
            IsJumpingThisTime = false;
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, frameToDraw, Color.White, 0, Vector2.Zero, effects, 0);
        }
    }
}
