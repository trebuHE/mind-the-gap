using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    internal class Sprite
    {
        protected Vector2 position;
        protected Vector2 size;
        protected static readonly int SCALE = 4;

        public Texture2D Texture { get; set; }
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
