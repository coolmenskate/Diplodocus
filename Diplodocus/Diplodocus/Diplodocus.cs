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
using Diplodocus.Acteurs;
using Diplodocus.Textures;


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
        Detective s;
        Texture2D f;
        Camera2D camera = new Camera2D();
        TextureDetective texture;

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
            carte = chargeurCarte.ChargerNiveauPrecis(2);
            f = Content.Load<Texture2D>(@"df");
            s = new Detective(new TextureDetective(Content.Load<Texture2D>(@"SpriteSheets\Detective_SpriteSheet")), new Vector2(10, 10));
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
            s.Update(gameTime);
            // TODO: Add your update logic here
            s.PositionActuelle = CollisionCarte.PositionApresCollisions(carte, 0, s.PositionActuelle, s.PositionPrecedente);
            if (CollisionCarte.contactAvecLeSol(carte, 0, s.PositionActuelle) == true)
                s.ToucheAuSol = true;
            else if (s.ToucheAuSol == true)
                s.ToucheAuSol = false;
            if (CollisionCarte.contactAuDessus(carte, 0, s.PositionActuelle) && s.vitesse.Y < 0)
                s.ChuteLibre = true;
            camera.Suivre(s.PositionActuelle, s.PositionPrecedente, carte, GraphicsDevice.Viewport.Bounds);
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
            s.Draw(gameTime, spriteBatch);
            carte.DrawLayer(spriteBatch, 0, carte.Bounds, 1);
            carte.DrawLayer(spriteBatch, 1, carte.Bounds, 0.5f);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
