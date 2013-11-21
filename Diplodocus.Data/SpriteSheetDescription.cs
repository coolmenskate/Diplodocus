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

namespace Diplodocus.Data
{
    public class SpriteSheetDescription
    {
        public SpriteSheetDescription(){}
        public string SpriteSheetFilePath { get; set; }
        public Rectangle Frame { get; set; }
        public List<AnimationDescription> Animations { get; set; }        
    }
}
