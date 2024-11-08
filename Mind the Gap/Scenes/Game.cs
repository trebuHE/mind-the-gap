using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Mind_the_Gap.Scenes
{
    internal class Game : IScene
    {
        private ContentManager contentManager;
        private Player player;
        private Level currentLevel;
        private List<Level> levels;
        public Game(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            levels = new List<Level>
            {
                new("../../../data/levels/level_test/level_test_0.csv", "../../../data/levels/level_test/level_test_1.csv", 5f, contentManager)
            };
            currentLevel = levels.First();
        }

        public void Load()
        {
            currentLevel.Load();
            player = new(new Vector2(0, 6 * 64), new Vector2(14, 14), 64, new Vector2(20, 12));
            Texture2D texture = contentManager.Load<Texture2D>("player_sprite_sheet");
            player.Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime, player);

            if(currentLevel.DrawAndUpdatePlayer)
                player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);

            if(currentLevel.DrawAndUpdatePlayer)
                player.Draw(spriteBatch);
        }
    }
}
