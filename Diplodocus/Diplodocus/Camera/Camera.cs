using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using FuncWorks.XNA.XTiled;

namespace Diplodocus.Camera
{
    public class Camera2D
    {
        public Matrix MatriceDeLaCamera {get; private set;}

        public Camera2D() 
        {
            MatriceDeLaCamera = Matrix.Identity;
        }

        public void Suivre(Rectangle positionActuelle, Rectangle positionPrecedente, Map carte, Rectangle tailleEcran)
        {
            if (positionActuelle.Center.X + tailleEcran.Width / 2.0f <= carte.Bounds.Right && positionActuelle.Center.X - tailleEcran.Width / 2.0f >= 0)
                MatriceDeLaCamera *= Matrix.CreateTranslation(new Vector3(-(positionActuelle.X - positionPrecedente.X), 0, 0));
            if (positionActuelle.Center.Y + tailleEcran.Height / 2.0f <= carte.Bounds.Bottom && positionActuelle.Center.Y - tailleEcran.Height / 2.0f >= 0)
                MatriceDeLaCamera *= Matrix.CreateTranslation(new Vector3(0, -(positionActuelle.Y - positionPrecedente.Y), 0));
        }
    }
}
