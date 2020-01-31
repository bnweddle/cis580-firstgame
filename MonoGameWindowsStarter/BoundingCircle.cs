using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MonoGameWindowsStarter
{
    public struct BoundingCircle
    {
        public float X;

        public float Y;

        public float Radius;

        //This is a getter
        public Vector2 Center => new Vector2(X, Y);


    }
}
