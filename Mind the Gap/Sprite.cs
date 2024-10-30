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
        protected Vector2 position;
        protected Vector2 size;
        protected static readonly int SCALE = 4;
        private Texture2D texture;
        public Texture2D Texture { get => texture; set => texture = value; }
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


        public Sprite(Vector2 position, Vector2 size)
        {
            this.position = position;
            this.size = size;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRect, Color.White);
        }
    }
}
