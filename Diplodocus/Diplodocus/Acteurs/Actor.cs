using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Diplodocus.Data;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Diplodocus.Acteurs
{
    public abstract class Actor
    {
        private AnimationState previousState;
        private AnimationDescription presentAnimation;
        private int timeSinceFrameIsOn = 0;
        private int presentFrame = 0;

        protected List<AnimationDescription> animations;
        protected Texture2D SpriteSheet { get; private set; }
        protected AnimationState presentState;
        protected Rectangle frameToDraw;
        protected bool finalFrame = false;

        public Actor(ContentManager loader, string xmlFilePath)
        {
            SpriteSheetDescription spriteSheetDescription = loader.Load<SpriteSheetDescription>(xmlFilePath);
            this.SpriteSheet = loader.Load<Texture2D>(spriteSheetDescription.SpriteSheetFilePath);
            this.animations = spriteSheetDescription.Animations;
            this.frameToDraw = spriteSheetDescription.Frame;
            presentState = AnimationState.Waiting;
            previousState = presentState;
            int searchIndex = 0;
            while (searchIndex < animations.Count && animations[searchIndex].Type != presentState)
                searchIndex++;
            if (searchIndex == animations.Count)
                presentAnimation = animations[0];
            else
                presentAnimation = animations[searchIndex];
        }

        public Actor(Texture2D spriteSheet, List<AnimationDescription> animations, Rectangle frameSize, AnimationState startingState)
        {
            this.SpriteSheet = spriteSheet;
            this.animations = animations;
            this.presentState = startingState;
            this.previousState = this.presentState;
        }

        public virtual void Update(GameTime gameTime)
        {
            if (previousState != presentState )
            {
                presentFrame = 0;
                timeSinceFrameIsOn = 0;
                int searchanimationIndex = 0;
                while (searchanimationIndex < animations.Count && animations[searchanimationIndex].Type != presentState)
                    searchanimationIndex++;
                if (searchanimationIndex != animations.Count)
                    presentAnimation = animations[searchanimationIndex];
            }
            else
            {
                timeSinceFrameIsOn += gameTime.ElapsedGameTime.Milliseconds;
                if (timeSinceFrameIsOn >= presentAnimation.TimeBetweenFrames)
                {
                    timeSinceFrameIsOn -= presentAnimation.TimeBetweenFrames;
                    presentFrame++;
                    if (presentFrame == presentAnimation.nFramesTotal-1)
                        finalFrame = true;
                    else if (presentFrame >= presentAnimation.nFramesTotal)
                    {
                        finalFrame = false;
                        presentFrame = 0;
                    }
                }                
            }
            int column = presentAnimation.Start.X + (presentFrame % (SpriteSheet.Width / frameToDraw.Width));
            int row = presentAnimation.Start.Y + (presentFrame / (SpriteSheet.Width / frameToDraw.Width));
            frameToDraw.X = column * frameToDraw.Width;
            frameToDraw.Y = row * frameToDraw.Height;
            previousState = presentState;
        }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
