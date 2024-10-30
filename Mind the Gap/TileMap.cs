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
        public string FilePath { get; set; }
        public TileMap(Vector2 position, Vector2 size) : base(position, size) { }

        public override void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
