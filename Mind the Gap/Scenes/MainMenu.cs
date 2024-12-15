using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Mind_the_Gap.Scenes
{
    internal class MainMenu : IScene
    {
        #region consts
        private static readonly Vector2 TITLE_TXT_POS = new Vector2(200, 120);
        private static readonly Vector2 NEW_GAME_BUTT_POS = new Vector2(420, 350);
        private static readonly Vector2 OPTIONS_BUTT_POS = new Vector2(420, 450);
        private static readonly Vector2 EXIT_BUTT_POS = new Vector2(420, 550);
        private static readonly Vector2 BEST_RUN_TXT_POS = new Vector2(420, 700);
        private static readonly Vector2 HEALTH_STATE_ICON_POS = new Vector2(615, 685);
        private static readonly Vector2 HEALTH_STATE_ICON_SIZE = new(32, 32);
        private static readonly Vector2 HEART_ICON_SIZE = new(16, 16);
        #endregion
        private TextButton newGameButton;
        private TextButton optionsButton;
        private TextButton exitButton;
        private Sprite healthStateIcon;
        private AnimationManager<HealthState> healthStateManager;
        private UserSettings settings;
        private readonly ContentManager contentManager;
        private readonly Text title;
        private readonly Text bestRun;

        public MainMenu(ContentManager contentManager)
        {
            this.contentManager = contentManager;

            newGameButton = new("NEW GAME", NEW_GAME_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            newGameButton.OnClick += NewGameButton_OnClick;
            optionsButton = new("OPTIONS", OPTIONS_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            optionsButton.OnClick += OptionsButton_OnClick;
            exitButton = new("EXIT", EXIT_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            exitButton.OnClick += ExitButton_OnClick;
            title = new("Mind the Gap", TITLE_TXT_POS, Color.White);
            bestRun = new("Best run: -", BEST_RUN_TXT_POS, Color.White);

            healthStateManager = new(new Dictionary<HealthState, Animation>() {
                {HealthState.FULL,           new Animation(1, 0, HEART_ICON_SIZE) },
                {HealthState.THREE_QUARTERS, new Animation(1, 1, HEART_ICON_SIZE) },
                {HealthState.HALF,           new Animation(1, 2, HEART_ICON_SIZE) },
                {HealthState.ONE_QUARTER,    new Animation(1, 3, HEART_ICON_SIZE) },
                {HealthState.NONE,           new Animation(1, 4, HEART_ICON_SIZE) },
            });
            healthStateManager.ActiveAnimation = HealthState.NONE;
            healthStateIcon = new(HEALTH_STATE_ICON_POS, HEALTH_STATE_ICON_SIZE, scale: 2);
            Focus();
        }

        public void Load()
        {
            SpriteFont font = contentManager.Load<SpriteFont>("main_menu_font");
            newGameButton.Font = font;
            optionsButton.Font = font;
            exitButton.Font = font;
            font = contentManager.Load<SpriteFont>("title_font");
            title.Font = font;
            font = contentManager.Load<SpriteFont>("game_font");
            bestRun.Font = font;
            Texture2D texture = contentManager.Load<Texture2D>("player_heart_sheet");
            healthStateIcon.Texture = texture;
        }

        public void Focus()
        {
            settings = UserSettings.Load();
            healthStateManager.ActiveAnimation = settings.HealthState;
        }

        public void Update(GameTime gameTime)
        {
            newGameButton.Update();
            optionsButton.Update();
            exitButton.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            newGameButton.Draw(spriteBatch);
            optionsButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
            title.Draw(spriteBatch);
            bestRun.Draw(spriteBatch);
            spriteBatch.Draw(healthStateIcon.Texture, healthStateIcon.DestinationRect, healthStateManager.SourceRect, Color.White);
        }

        private void ExitButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            Timer.Create(0.15f, () => System.Environment.Exit(0));
        }

        private void OptionsButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            SceneManager.AddScene(new OptionsMenu(contentManager));
        }

        private void NewGameButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            SceneManager.AddScene(new Game(contentManager));
        }
    }
}
