using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Diplodocus.Textures;

namespace Diplodocus.Acteurs
{
    public class Detective
    {
        public Rectangle PositionActuelle {get; set;}
        public Rectangle PositionPrecedente { get; private set; }
        private TextureDetective texture;
        public Vector2 vitesse = Vector2.Zero;
        private bool toucheAuSol = false;
        public bool ToucheAuSol
        {
            get
            {
                return toucheAuSol;
            }
            set
            {
                toucheAuSol = value;
                if (value == true)
                {
                    vitesse.Y = 0;
                    chuteLibre = false;
                }
            }
        }
        private bool chuteLibre;
        public bool ChuteLibre 
        {
            get
            {
                return chuteLibre;
            }
            set
            {
                chuteLibre = value;
                vitesse.Y = 0;
            }
        }
        private const int IMPULSION_VERTICALE = -15;
        private const int DEPLACEMENT_LATERAL = 5;
        private const int ACCELERATION = 15;

        public Detective(TextureDetective texture, Vector2 positionDepart)
        {
            this.texture = texture;
            PositionActuelle = new Rectangle((int)positionDepart.X, (int)positionDepart.Y, texture.largeur, texture.hauteur);
            PositionPrecedente = PositionActuelle;
            ChuteLibre = true;
        }

        public void Update(GameTime tempsDeJeu)
        {
            vitesse.X = 0;
            PositionPrecedente = PositionActuelle;
            if (vitesse.X == 0)
                texture.Etat = DeplacementsActeursEtat.Immobile;
            if (toucheAuSol == false)
                vitesse.Y += (float)(tempsDeJeu.ElapsedGameTime.TotalSeconds * ACCELERATION);
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {                
                vitesse.X -= DEPLACEMENT_LATERAL;
                texture.Etat = DeplacementsActeursEtat.Gauche;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                vitesse.X += DEPLACEMENT_LATERAL;
                texture.Etat = DeplacementsActeursEtat.Droite;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Space) && toucheAuSol == true)
            {
                vitesse.Y += IMPULSION_VERTICALE;
                toucheAuSol = false;
            }

            if(toucheAuSol == false)
                texture.Etat = DeplacementsActeursEtat.Saute;

            PositionActuelle = new Rectangle(PositionActuelle.X + (int)vitesse.X, PositionActuelle.Y + (int)vitesse.Y, PositionActuelle.Width, PositionActuelle.Height);
        }

        public void Draw(GameTime jeu, SpriteBatch spriteBatch)
        {
            texture.DessinerImageEnCours(jeu, spriteBatch, PositionActuelle);
        }

    }
}
