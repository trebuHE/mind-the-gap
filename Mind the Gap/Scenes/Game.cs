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
        private static readonly Vector2 RESTART_BUTT_POS = new(12.5f * 64, (12 * 64) + 20);
        private static readonly Vector2 MAIN_MENU_BUTT_POS = new(16 * 64, (12 * 64) + 20);
        private static readonly Vector2 HEALTH_STATE_ICON_POS = new((10 * 64) - 32, 12 * 64);
        private static readonly Vector2 PLAYER_SIZE = new(14, 14);
        private static readonly Vector2 GRID_SIZE = new(20, 12);
        private static readonly Vector2 WALKABLE_TILE_ICON_POS = new(430, (12 * 64) + 8);
        private static readonly Vector2 WALKABLE_TILE_ICON_SIZE = new(48, 48);
        private static readonly Vector2 HEALTH_STATE_ICON_SIZE = new(16, 16);
        #endregion

        private readonly ContentManager contentManager;
        private readonly List<Level> levels;
        private readonly Text levelNumberText;
        private readonly Text walkOnText;
        private Sprite healthStateIcon;
        private int levelNumber = 1;
        private Level currentLevel;
        private Player player;
        private TextButton restartTextButton;
        private TextButton mainMenuTextButton;

        public Game(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            levels = new List<Level>
            {
                new("../../../data/levels/level_test/level_test_path.csv", "../../../data/levels/level_test/level_test_game.csv", 1f, new Vector2(3, 5), 17, contentManager),
                new("../../../data/levels/level_test2/level_test2_path.csv", "../../../data/levels/level_test2/level_test2_game.csv", 1f, new Vector2(3, 5), 17, contentManager)
            };
            currentLevel = levels.First();

            levelNumberText = new(LEVEL_NUM_TXT_POS, Color.White, "Level: " + levelNumber);
            walkOnText = new(WALK_ON_TXT_POS, Color.White, "Memorize a path!");

            restartTextButton = new("Restart", RESTART_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            restartTextButton.OnClick += RestartTextButton_OnClick;
            mainMenuTextButton = new("Main menu", MAIN_MENU_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            mainMenuTextButton.OnClick += MainMenuTextButton_OnClick;
            healthStateIcon = new(HEALTH_STATE_ICON_POS, HEALTH_STATE_ICON_SIZE);
        }

        public void Load()
        {
            currentLevel.Load();
            player = new(currentLevel.PlayerSpawnGridPos, PLAYER_SIZE, 64, GRID_SIZE);

            // textures
            Texture2D texture = contentManager.Load<Texture2D>("player_sprite_sheet");
            player.Texture = texture;
            texture = contentManager.Load<Texture2D>("player_heart_sheet");
            healthStateIcon.Texture = texture;

            // fonts
            SpriteFont gameFont = contentManager.Load<SpriteFont>("game_font");
            levelNumberText.Font = gameFont;
            walkOnText.Font = gameFont;
            restartTextButton.Font = gameFont;
            mainMenuTextButton.Font = gameFont;
        }

        public void Update(GameTime gameTime)
        {
            currentLevel.Update(gameTime, player);

            if(currentLevel.DrawAndUpdatePlayer)
                player.Update(gameTime);

            if(currentLevel.Failed)
                RestartLevel();

            if(currentLevel.Finished)
                StartNextLevel();

            restartTextButton.Update();
            levelNumberText.DisplayedText = "Level: " + levelNumber;
            mainMenuTextButton.Update();
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
                spriteBatch.Draw(healthStateIcon.Texture, healthStateIcon.DestinationRect, player.healthStateManager.SourceRect, Color.White);
            }

            walkOnText.Draw(spriteBatch);
            restartTextButton.Draw(spriteBatch);
            mainMenuTextButton.Draw(spriteBatch);
        }

        private void StartNextLevel()
        {
            levelNumber++;
            currentLevel = levels[levelNumber - 1];
            RestartLevel();
        }

        private void RestartLevel()
        {
            Load();
        }

        private void RestartTextButton_OnClick(object sender, System.EventArgs e)
        {
            RestartLevel();
        }
        private void MainMenuTextButton_OnClick(object sender, System.EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}
