using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Mind_the_Gap
{
    internal class TextButton : Text
    {
        public event EventHandler OnClick;
        private MouseState prevMouseState;
        private MouseState currentMouseState;
        private readonly Color color;
        private readonly Color hoverColor;
        private readonly Color clickColor;
        private Rectangle hitbox;

        public TextButton(string text, Vector2 position, Color color, Color hoverColor, Color clickColor) : base(position, color, text)
        {
            this.color = color;
            this.hoverColor = hoverColor;
            this.clickColor = clickColor;
        }

        public void Update()
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            Color = color;
            hitbox = new(
                (int)Position.X,
                (int)Position.Y,
                (int)Font.MeasureString(DisplayedText).X,
                (int)Font.MeasureString(DisplayedText).Y);

            Rectangle mouseHitbox = new(currentMouseState.X, currentMouseState.Y, 1, 1);

            // hovering
            if(mouseHitbox.Intersects(hitbox))
            {
                Color = hoverColor;

                if(prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    Color = clickColor;

                    if(currentMouseState.LeftButton == ButtonState.Released)
                    {
                        OnClick?.Invoke(this, new EventArgs());
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
