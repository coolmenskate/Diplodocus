using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diplodocus.Textures
{
    public class TextureDetective: IActeurSaute
    {
        private Texture2D texture;
        private int rangeeActuelle;
        private int nImageLigne;
        private int imageActuelle = 0;
        public int largeur { get; private set; }
        public int hauteur { get; private set; }
        private int delai = 50;
        private int tempsDepuis = 0;

        public TextureDetective(Texture2D texture)
        {
            this.texture = texture;
            largeur = texture.Width / 10;
            hauteur = texture.Height / 24;
            Etat = DeplacementsActeursEtat.Immobile;
        }

        private DeplacementsActeursEtat etat;
        public DeplacementsActeursEtat  Etat
        {
            get
            {
                return etat;
            }
            set
            {
                switch (value)
                {
                    case DeplacementsActeursEtat.Immobile:
                        rangeeActuelle = 12;
                        nImageLigne = 10;
                        break;
                    case DeplacementsActeursEtat.Droite:
                    case DeplacementsActeursEtat.Gauche:
                        rangeeActuelle = 17;
                        nImageLigne = 10;
                        break;
                    case DeplacementsActeursEtat.Saute:
                        rangeeActuelle = 5;
                        nImageLigne = 3;
                        break;
                }
                etat = value;
            }
        }

        public void DessinerImageEnCours(GameTime temps, SpriteBatch spriteBatch, Rectangle position)
        {
            Rectangle imageEnCours = new Rectangle(imageActuelle * largeur, rangeeActuelle * hauteur, largeur, hauteur);   
            if(etat == DeplacementsActeursEtat.Gauche)
                spriteBatch.Draw(texture, new Rectangle(position.X, position.Y + 10, position.Width, position.Height), imageEnCours, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
            else
                spriteBatch.Draw(texture, new Rectangle(position.X, position.Y + 10, position.Width, position.Height), imageEnCours, Color.White);
            tempsDepuis += temps.ElapsedGameTime.Milliseconds;
            //Si le délai pour l'affichage est expiré, on change l'image affichée.
            if (tempsDepuis > delai)
            {
                tempsDepuis -= delai;
                imageActuelle++;
                //Si c'est la dernière image de la séquence, on se repositionne à la première
                switch(etat)
                {
                    case DeplacementsActeursEtat.Immobile:
                        if (imageActuelle == nImageLigne)
                        {
                            if (rangeeActuelle == 12)
                            {
                                rangeeActuelle++;
                                nImageLigne = 6;
                            }
                            else
                            {
                                rangeeActuelle--;
                                nImageLigne = 10;
                            }
                            imageActuelle = 0;
                        }
                        break;
                    case DeplacementsActeursEtat.Droite:
                    case DeplacementsActeursEtat.Gauche:
                        if (imageActuelle >= 9)
                        {
                            imageActuelle = 5 ;
                        }
                        break;
                    case DeplacementsActeursEtat.Saute:
                        if (imageActuelle >= 2)
                            imageActuelle = 0;
                        break;
                }
            }
        }
    }
}
