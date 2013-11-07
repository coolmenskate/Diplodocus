using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diplodocus.Textures
{
    public enum EtatSprite
    {
        DeplacementGauche,
        DeplacementDroite, 
        Immobile,
        Saute, 
        FlipArriere
    }

    public interface ISpriteSheet
    {
        EtatSprite Etat { get; }
        Texture2D SpriteSheet { get; }
        SpriteEffects Effet { get; }
        Rectangle ObtenirImageEnCours();
    }
}
