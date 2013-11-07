using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Diplodocus.Textures
{
    public enum DeplacementsActeursEtat
    {
        Droite,
        Gauche,
        Saute,
        Immobile
    }

    interface IActeurSaute
    {
        DeplacementsActeursEtat Etat { get; set; }

        void DessinerImageEnCours(GameTime temps, SpriteBatch spriteBatch, Rectangle position);
    }
}
