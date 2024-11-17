using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Mind_the_Gap.Scenes
{
    public enum HealthState
    {
        FULL = 4,
        THREE_QUARTERS = 3,
        HALF = 2,
        ONE_QUARTER = 1,
        NONE = 0
    }

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
        private static readonly Vector2 HEALTH_STATE_ICON_SIZE = new(32, 32);
        private static readonly Vector2 HEART_ICON_SIZE = new(16, 16);
        #endregion

        private readonly ContentManager contentManager;
        private readonly List<Level> levels;
        private readonly Text levelNumberText;
        private readonly Text walkOnText;
        private Sprite healthStateIcon;
        private AnimationManager<HealthState> healthStateManager;
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

            healthStateManager = new(new Dictionary<HealthState, Animation>() {
                {HealthState.FULL,           new Animation(1, 0, HEART_ICON_SIZE) },
                {HealthState.THREE_QUARTERS, new Animation(1, 1, HEART_ICON_SIZE) },
                {HealthState.HALF,           new Animation(1, 2, HEART_ICON_SIZE) },
                {HealthState.ONE_QUARTER,    new Animation(1, 3, HEART_ICON_SIZE) },
                {HealthState.NONE,           new Animation(1, 4, HEART_ICON_SIZE) },
            });
            healthStateManager.ActiveAnimation = HealthState.FULL;

            levelNumberText = new("Level: " + levelNumber, LEVEL_NUM_TXT_POS, Color.White);
            walkOnText = new("Memorize a path!", WALK_ON_TXT_POS, Color.White);

            restartTextButton = new("Restart", RESTART_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            restartTextButton.OnClick += RestartTextButton_OnClick;
            mainMenuTextButton = new("Main menu", MAIN_MENU_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            mainMenuTextButton.OnClick += MainMenuTextButton_OnClick;
            healthStateIcon = new(HEALTH_STATE_ICON_POS, HEALTH_STATE_ICON_SIZE, 2);
        }

        public void Load()
        {
            currentLevel.Load();
            player = new(currentLevel.PlayerSpawnGridPos, PLAYER_SIZE, 64, GRID_SIZE);

            // textures
            Texture2D texture = contentManager.Load<Texture2D>("player_sprite_sheet");
            player.Texture = texture;
            texture = contentManager.Load<Texture2D>("explosion");
            player.Explosion.Texture = texture;
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
            {
                walkOnText.DisplayedText = "Wrong tile!";

                Timer.Create(Player.dieTime, () => { player.DecrementHealth(); RestartLevel(); }, name: "restart");
            }

            if(!player.Alive)
                SceneManager.RemoveScene(this);

            if(currentLevel.Finished)
                StartNextLevel();

            restartTextButton.Update();
            levelNumberText.DisplayedText = "Level: " + levelNumber;
            mainMenuTextButton.Update();
            healthStateManager.ActiveAnimation = (HealthState)player.Health - 1;
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
                spriteBatch.Draw(healthStateIcon.Texture, healthStateIcon.DestinationRect, healthStateManager.SourceRect, Color.White);

                if(!player.Dying)
                {
                    walkOnText.DisplayedText = "Walk on: ";
                    currentLevel.GameMap.Draw(spriteBatch, currentLevel.WalkableTile, WALKABLE_TILE_ICON_POS, WALKABLE_TILE_ICON_SIZE);
                }
                else
                {
                    walkOnText.DisplayedText = "Wrong tile!";
                }
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
            currentLevel.Load();
            player.Restart();
        }

        private void RestartTextButton_OnClick(object sender, System.EventArgs e)
        {
            player.DecrementHealth();
            RestartLevel();
        }
        private void MainMenuTextButton_OnClick(object sender, System.EventArgs e)
        {
            SceneManager.RemoveScene(this);
        }
    }
}
