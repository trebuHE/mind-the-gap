using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Mind_the_Gap
{
    internal class TextButton : Text
    {
        public bool Active { get; set; }

        public event EventHandler OnClick;

        private MouseState prevMouseState;
        private MouseState currentMouseState;
        private Rectangle hitbox;
        private readonly Color color;
        private readonly Color hoverColor;
        private readonly Color clickColor;

        public TextButton(string text, Vector2 position, Color color, Color hoverColor, Color clickColor, bool active = true) : base(text, position, color)
        {
            this.color = color;
            this.hoverColor = hoverColor;
            this.clickColor = clickColor;
            Active = active;
            SetColorOnActive();
        }

        public void Update()
        {
            prevMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            hitbox = new(
                (int)Position.X,
                (int)Position.Y,
                (int)Font.MeasureString(DisplayedText).X,
                (int)Font.MeasureString(DisplayedText).Y);

            Rectangle mouseHitbox = new(currentMouseState.X, currentMouseState.Y, 1, 1);

            SetColorOnActive();

            // hovering
            if(Active && mouseHitbox.Intersects(hitbox))
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

        private void SetColorOnActive()
        {
            if(Active)
                Color = color;
            else
                Color = clickColor;
        }
    }
}
