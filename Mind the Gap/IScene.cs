using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    internal interface IScene
    {
        public void Load();
        /// <summary>
        /// Called whenever a scene atop this scene was popped from a stack
        /// </summary>
        public void Focus();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
    }
}
