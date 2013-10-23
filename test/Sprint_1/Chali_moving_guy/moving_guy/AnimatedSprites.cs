using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace moving_guy
{
    public class AnimatedSprites
    {
        //Notre atlas.
        public Texture2D Texture { get; set; }

        //Le nombre de rangees
        public int Rows { get; set; }
        //Le nombre de colonnes
        public int Columns { get; set; }
        //Le numero de l'image en cours
        private int currentFrame;
        //Le total d'images qu'on a
        private int totalFrames;

        //La position du bonhomme
        private Vector2 m_animatedGuyPosition;

        //Constructeur
        public AnimatedSprites(Texture2D texture, int rows, int columns, int windowWidth, int windowHeight)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            currentFrame = 0;
            totalFrames = Rows * Columns;
            m_animatedGuyPosition = new Vector2(windowWidth/2 - (Texture.Width/Columns)/2, windowHeight/2 - (Texture.Height/Rows));
        }

        public Vector2 getAnimatedSpritePosition()
        {
            return m_animatedGuyPosition;
        }

        public void setAnimatedSpritePosition(Vector2 nouvPos)
        {
            m_animatedGuyPosition = nouvPos;
        }

        public void Update()
        {
            currentFrame++;
            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int)((float)currentFrame / (float)Columns);
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            //spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            //spriteBatch.End();
        }
    }
}
