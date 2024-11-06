using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap
{
    internal class Level
    {
        public bool Finished { get; private set; }
        public bool DrawAndUpdatePlayer { get; private set; }
        private ContentManager contentManager;
        private TileMap pathMap;
        private string pathMapPath;
        private TileMap gameMap;
        private string gameMapPath;
        private float memorizeTimeSec;
        private float timeElapsedSec;

        public Level(string pathMapPath, string gameMapPath, float memorizeTimeSec, ContentManager contentManager)
        {
            this.contentManager = contentManager;
            this.gameMapPath = gameMapPath;
            this.pathMapPath = pathMapPath;
            this.memorizeTimeSec = memorizeTimeSec;
            Finished = false;
            pathMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            gameMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            Texture2D texture = contentManager.Load<Texture2D>("tileset");
            pathMap.Texture = texture;
            gameMap.Texture = texture;
            timeElapsedSec = 0f;
            DrawAndUpdatePlayer = false;
        }

        public void Load()
        {
            pathMap.LoadMap(pathMapPath);
            gameMap.LoadMap(gameMapPath);
        }

        public void Update(GameTime gameTime)
        {
            timeElapsedSec += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            pathMap.Draw(spriteBatch);

            if(timeElapsedSec >= memorizeTimeSec)
            {
                DrawAndUpdatePlayer = true;
                gameMap.Draw(spriteBatch);
            }

        }
    }
}
