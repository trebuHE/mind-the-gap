using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_the_Gap
{
    internal class Sprite
    {
        public Texture2D texture;
        public Vector2 position;

        public Sprite(Texture2D texture, Vector2 position) 
        {
            this.texture = texture;
            this.position = position;
        }
    }
}
