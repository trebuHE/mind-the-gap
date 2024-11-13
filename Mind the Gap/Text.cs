using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Mind_the_Gap
{
    internal class Text
    {
        public string DisplayedText { get; set; }
        public Color Color { get; set; }
        public SpriteFont Font { get; set; }
        public Vector2 Position { get; private set; }

        public Text(Vector2 position, Color color, string text)
        {
            Position = position;
            Color = color;
            DisplayedText = text;
            Font = null;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, DisplayedText, Position, Color);
        }


    }
}
