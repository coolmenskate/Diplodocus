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
using FuncWorks.XNA.XTiled;
using Diplodocus.Carte;
using Diplodocus.Camera;


namespace Diplodocus
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Diplodocus : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Map carte;
        ChargeurCarte chargeurCarte;
        Rectangle point = new Rectangle(10, 10, 5, 5);
        Rectangle positionAvant; 
        Texture2D f;
        Camera2D camera = new Camera2D();

        public Diplodocus()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            chargeurCarte = new ChargeurCarte(this);
            graphics.IsFullScreen = false;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            IsMouseVisible = true;
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
            carte = chargeurCarte.ChargerNiveauSuivant();
            f = Content.Load<Texture2D>(@"df");
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
            positionAvant  = point;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.D))
                point.Offset(5,0);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
                point.Offset(-5, 0);
            if (Keyboard.GetState().IsKeyDown(Keys.W))
                point.Offset(0, -5);
            if (Keyboard.GetState().IsKeyDown(Keys.S))
                point.Offset(0, 5);

            // TODO: Add your update logic here
            point = CollisionCarte.PositionApresCollisions(carte, 0, point, positionAvant);
            camera.Suivre(point, positionAvant, carte, GraphicsDevice.Viewport.Bounds);
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, null, null, null, null, null, camera.MatriceDeLaCamera);
            carte.Draw(spriteBatch, carte.Bounds);
            spriteBatch.Draw(f, point, Color.White);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
