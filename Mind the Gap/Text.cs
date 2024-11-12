using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace Mind_the_Gap
{
    internal class Text
    {
        public string DisplayedText { get; set; }
        public Color Color { get; set; }
        public SpriteFont Font { get; set; }

        private readonly Vector2 position;

        public Text(Vector2 position, Color color, string text)
        {
            this.position = position;
            Color = color;
            DisplayedText = text;
            Font = null;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Font, DisplayedText, position, Color);
        }


    }
}
