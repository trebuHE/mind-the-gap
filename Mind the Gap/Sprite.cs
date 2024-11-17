using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    internal class Sprite
    {
        public Texture2D Texture { get; set; }
        public bool Visible { get => visible; set { visible = value; } }
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
        protected bool visible;

        public Sprite(Vector2 position, Vector2 size, int scale = 4, bool visible = true)
        {
            this.position = position;
            this.size = size;
            this.scale = scale;
            this.visible = visible;
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            if(visible)
                spriteBatch.Draw(Texture, DestinationRect, Color.White);
        }
    }
}
