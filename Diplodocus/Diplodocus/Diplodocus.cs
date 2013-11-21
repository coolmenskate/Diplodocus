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
using Diplodocus.Physics;


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
        Camera2D camera = new Camera2D();
        Player player;
        List<MovingActorWithSpeed> ennemies = new List<MovingActorWithSpeed>();

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
            player = new Player(Content, @"SpriteSheets\Detective_SpriteSheetXML");
            // TODO: use this.Content to load your game content here
            carte = chargeurCarte.ChargerNiveauPrecis(2);
            foreach (MapObject spawPoint in carte.GetObjectsInRegion(1, carte.Bounds))
            {
                ennemies.Add(new MovingActorWithSpeed(Content, @"SpriteSheets\Detective_SpriteSheetXML"));
                ennemies[ennemies.Count - 1].Position = spawPoint.Bounds;
                ennemies[ennemies.Count - 1].Speed = new Vector2(5, 0);
            }
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
            player.Update(gameTime);
            player.Position = CollisionCarte.PositionApresCollisions(carte, 0, player.Position, player.PreviousPosition);
            if (CollisionCarte.contactAvecLeSol(carte, 0, player.Position) == false)
            {
                player.Speed = DiplodocusPhysics.GetSpeedWithGravity(player.Speed, (float)gameTime.ElapsedGameTime.TotalSeconds);
                if (player.IsInTheAir == false)
                    player.IsInTheAir = true;
            }
            else if (player.IsInTheAir == true)
                player.IsInTheAir = false;
            if (CollisionCarte.contactAuDessus(carte, 0, player.Position) == true && player.Speed.Y < 0)
                player.Speed = new Vector2(player.Speed.X, 0);
            camera.Suivre(player.Position, player.PreviousPosition, carte, GraphicsDevice.Viewport.Bounds);
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
            player.Draw(spriteBatch);
            carte.DrawLayer(spriteBatch, 0, carte.Bounds, 1);
            carte.DrawLayer(spriteBatch, 1, carte.Bounds, 0.5f);
            foreach (MovingActorWithSpeed ennemy in ennemies)
                ennemy.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
