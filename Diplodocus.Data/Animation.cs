using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Diplodocus.Data
{
    public enum AnimationState
    {
        Left,
        Right, 
        Up,
        Down, 
        Jump, 
        Crunch,
        Shoot,
        Backflip, 
        Teleportation, 
        RunLeft, 
        RunRight, 
        RunUp,
        RunDown, 
        Death, 
        Waiting
    }


    public class AnimationDescription
    {
        public Point Start { get; set; }
        public Point End { get; set; }
        public AnimationState Type { get; set; }
        public int TimeBetweenFrames { get; set; }
        public int nFramesTotal { get; set; }

        public AnimationDescription() { }

        public AnimationDescription(Point start, Point end, AnimationState type, int nFramesTotal, int timeBetweenFrames = 50)
        {
            this.Start = start;
            this.End = end;
            this.Type = type;
            this.TimeBetweenFrames = 50;
            this.nFramesTotal = nFramesTotal;
        }
    }
}
