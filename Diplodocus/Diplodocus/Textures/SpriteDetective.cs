using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diplodocus.Textures
{
    public class SpriteDetective: ISpriteSheet
    {
        private int rangeeActuelle;
        private EtatSprite etat;
        public EtatSprite Etat
        {
            get
            {
                return etat;
            }
            set
            {
                switch (value)
                {
                    case EtatSprite.DeplacementGauche:
                        effet = SpriteEffects.FlipHorizontally;
                        rangeeActuelle = 17;
                        break;
                    case EtatSprite.DeplacementDroite:
                        effet = SpriteEffects.None;

                        break;
                }
            }
        }

        private Texture2D spriteSheet;
        public Texture2D SpriteSheet
        {
            get { return spriteSheet; }
        }

        private SpriteEffects effet;
        public SpriteEffects Effet { get { return effet; } }            

        public Rectangle ObtenirImageEnCours()
        {
            throw new NotImplementedException();
        }
    }
}
