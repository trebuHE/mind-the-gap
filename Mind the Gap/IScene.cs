using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    internal interface IScene
    {
        public void Load();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
