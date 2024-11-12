using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Mind_the_Gap.Scenes
{
    internal class Game : IScene
    {
        #region consts
        private static readonly Vector2 LEVEL_NUM_TXT_POS = new(32, (12 * 64) + 20);
        private static readonly Vector2 WALK_ON_TXT_POS = new(4 * 64, (12 * 64) + 20);
        private static readonly Vector2 PLAYER_SIZE = new(14, 14);
        private static readonly Vector2 GRID_SIZE = new(20, 12);
        private static readonly Vector2 WALKABLE_TILE_ICON_POS = new(430, (12 * 64) + 8);
        private static readonly Vector2 WALKABLE_TILE_ICON_SIZE = new(48, 48);
        #endregion

        private readonly ContentManager contentManager;
        private readonly Level currentLevel;
        private readonly List<Level> levels;
        private readonly Text levelNumberText;
        private readonly Text walkOnText;
        private readonly int levelNumber = 1;
        private Player player;

        public Game(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            levels = new List<Level>
            {
                new("../../../data/levels/level_test/level_test_0.csv", "../../../data/levels/level_test/level_test_1.csv", 1f, new Vector2(3, 5), 17, contentManager)
            };
            currentLevel = levels.First();

            levelNumberText = new(LEVEL_NUM_TXT_POS, Color.White, "Level: " + levelNumber);
            walkOnText = new(WALK_ON_TXT_POS, Color.White, "Memorize a path!");
        }

        public void Load()
        {
            currentLevel.Load();
            player = new(currentLevel.PlayerSpawnGridPos, PLAYER_SIZE, 64, GRID_SIZE);

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
            DrawGame(spriteBatch);
            DrawGUI(spriteBatch);
        }
        private void DrawGame(SpriteBatch spriteBatch)
        {
            currentLevel.Draw(spriteBatch);

            if(currentLevel.DrawAndUpdatePlayer)
                player.Draw(spriteBatch);
        }
        private void DrawGUI(SpriteBatch spriteBatch)
        {
            levelNumberText.Draw(spriteBatch);
            walkOnText.DisplayedText = "Memorize a path!";

            if(currentLevel.DrawAndUpdatePlayer)
            {
                walkOnText.DisplayedText = "Walk on: ";
                currentLevel.GameMap.Draw(spriteBatch, currentLevel.WalkableTile, WALKABLE_TILE_ICON_POS, WALKABLE_TILE_ICON_SIZE);
            }

            walkOnText.Draw(spriteBatch);
        }
    }
}
