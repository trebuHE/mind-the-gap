using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_the_Gap
{
    internal class TileMap : Sprite
    {
        private string path = null;
        public TileMap(Texture2D texture, Vector2 position, Vector2 size, string tilesetPath) : base(texture, position, size)
        {
            path = tilesetPath;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
