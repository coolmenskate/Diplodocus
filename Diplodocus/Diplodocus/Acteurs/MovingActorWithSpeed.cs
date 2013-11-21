using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Diplodocus.Data;
using Microsoft.Xna.Framework.Content;

namespace Diplodocus.Acteurs
{
    public class MovingActorWithSpeed:Actor
    {
        public Vector2 Speed { get; set;}
        public Rectangle Position { get; set;}
        public Rectangle PreviousPosition { get; private set; }

        public MovingActorWithSpeed(ContentManager loader, string xmlFilePath)
            : base(loader, xmlFilePath)
        {
            Position = new Rectangle(0,0, frameToDraw.Width, frameToDraw.Height);
            Speed = Vector2.Zero;
        }

        public override void Update(GameTime gameTime)
        {
            PreviousPosition = Position;
            Position = new Rectangle((int)(Position.X + Speed.X), (int)(Position.Y + Speed.Y), Position.Width, Position.Height);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteSheet, Position, frameToDraw, Color.White);
        }
    }
}
