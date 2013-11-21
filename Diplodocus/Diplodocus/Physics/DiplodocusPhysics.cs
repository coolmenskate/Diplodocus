using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Diplodocus.Physics
{
    public static class DiplodocusPhysics
    {
        public static float Gravity {get;set;}

        static DiplodocusPhysics()
        {
            Gravity = 9.81f;
        }

        public static Vector2 GetSpeedWithGravity(Vector2 speed, float seconds)
        {
            speed.Y += Gravity * seconds;
            return speed;
        }
    }
}
