using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace moving_guy
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Tout ce qui a trait au bonhomme
        private Texture2D moving_guyBase;
        private AnimatedSprites movingSprite;
        private Vector2 moving_guyPos;
        private Vector2 moving_guyOldPos;

        //Dimensions de l'ecran, limites du jeu pour l'instant
        private int m_screenWidth;
        private int m_screenHeight;

        //Recuperation de l'etat du clavier
        private KeyboardState m_etatClavier;
        private KeyboardState m_oldKeyboardState;

        //Pour bouger le bonhomme avec la souris aussi
        private MouseState m_etatSouris;
        private MouseState m_OldMouseState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            //On prend la dimension de l'ecran
            m_screenHeight = Window.ClientBounds.Height;
            m_screenWidth = Window.ClientBounds.Width;

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            // TODO: use this.Content to load your game content here
            moving_guyBase = Content.Load<Texture2D>("Images/SmileyWalk");
            movingSprite = new AnimatedSprites(moving_guyBase, 4, 4, m_screenWidth, m_screenHeight);
            moving_guyPos = movingSprite.getAnimatedSpritePosition();//new Vector2(m_screenWidth / 2 - moving_guy.Width / 2, m_screenHeight / 2 - moving_guy.Height / 2);  //Besoin que l'image soit deja instanciee
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            m_etatClavier = Keyboard.GetState();    //Recuperation de l'etat clavier
            m_etatSouris = Mouse.GetState();
            //movingSprite.Update();

            if (m_etatSouris.X == m_OldMouseState.X &&
                m_etatSouris.Y == m_OldMouseState.Y)
            {
                //Mouvements avec le clavier
                if (m_etatClavier.IsKeyDown(Keys.Up))
                {
                    moving_guyPos.Y -= 4;
                }
                else if (m_etatClavier.IsKeyDown(Keys.Down))
                {
                    moving_guyPos.Y += 4;
                }
                else if (m_etatClavier.IsKeyDown(Keys.Right))
                {
                    moving_guyPos.X++;
                }
                else if (m_etatClavier.IsKeyDown(Keys.Left))
                {
                    moving_guyPos.X--;
                }
            }
            else
            {
                //Mouvements avec la souris
                moving_guyPos.Y = m_etatSouris.Y;
                moving_guyPos.X = m_etatSouris.X;
            }

            m_OldMouseState = m_etatSouris;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            movingSprite.Draw(spriteBatch, moving_guyPos);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
