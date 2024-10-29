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
        public Vector2 size;
        private static readonly int SCALE = 4;

        public Rectangle DestinationRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    (int)size.X * SCALE,
                    (int)size.Y * SCALE);
            }
        }

        public Sprite(Texture2D texture, Vector2 position, Vector2 size)
        {
            this.texture = texture;
            this.position = position;
            this.size = size;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRect, Color.White);
        }
    }
}
