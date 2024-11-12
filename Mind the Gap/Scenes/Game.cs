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
        private Text levelNumberText;
        private Text walkOnText;
        private int levelNumber = 1;
        public Game(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            levels = new List<Level>
            {
                new("../../../data/levels/level_test/level_test_0.csv", "../../../data/levels/level_test/level_test_1.csv", 1f, new Vector2(3, 5), 17, contentManager)
            };
            currentLevel = levels.First();

            levelNumberText = new(new Vector2(32, (12 * 64) + 20), Color.White, "Level: " + levelNumber);
            walkOnText = new(new Vector2(4 * 64, (12 * 64) + 20), Color.White, "Walk on: ");
        }

        public void Load()
        {
            currentLevel.Load();
            player = new(currentLevel.PlayerSpawnGridPos, new Vector2(14, 14), 64, new Vector2(20, 12));

            // textures
            Texture2D texture = contentManager.Load<Texture2D>("player_sprite_sheet");
            player.Texture = texture;

            // fonts
            SpriteFont gameFont = contentManager.Load<SpriteFont>("game_font");
            levelNumberText.Font = gameFont;
            walkOnText.Font = gameFont;
        }

        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime, player);

            if(currentLevel.DrawAndUpdatePlayer)
                player.Update(gameTime);

            if(currentLevel.GameOver)
                Load();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);

            if(currentLevel.DrawAndUpdatePlayer)
                player.Draw(spriteBatch);

            levelNumberText.Draw(spriteBatch);
            walkOnText.Draw(spriteBatch);
        }
    }
}
