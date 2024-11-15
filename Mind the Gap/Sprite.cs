using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    internal class Sprite
    {
        public Texture2D Texture { get; set; }
        public Rectangle DestinationRect
        {
            get
            {
                return new Rectangle(
                    (int)position.X,
                    (int)position.Y,
                    (int)size.X * scale,
                    (int)size.Y * scale);
            }
        }

        protected Vector2 position;
        protected Vector2 size;
        protected readonly int scale = 4;

        public Sprite(Vector2 position, Vector2 size, int scale = 4)
        {
            this.position = position;
            this.size = size;
            this.scale = scale;
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
