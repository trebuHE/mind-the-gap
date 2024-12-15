using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            // set window size
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 768 + 64;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            SceneManager.AddScene(new Scenes.MainMenu(Content));
            SoundEffects.Load(Content);
        }

        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            Timer.UpdateTimers(gameTime);
            SceneManager.CurrentScene.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // scaling method used for pixel-art

            SceneManager.CurrentScene.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
