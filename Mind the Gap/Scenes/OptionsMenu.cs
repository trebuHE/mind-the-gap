using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap.Scenes
{
    internal class OptionsMenu : IScene
    {
        #region consts
        private readonly Vector2 BACK_BUTT_POS = new(600, 600);
        private readonly Vector2 WASD_BUTT_POS = new(500, 250);
        private readonly Vector2 ARROWS_BUTT_POS = new(640, 250);
        private readonly Vector2 RESET_BEST_RUN_BUTT_POS = new(150, 350);
        private readonly Vector2 CTRL_SCHEME_TXT_POS = new(150, 250);
        #endregion

        private readonly ContentManager contentManager;
        private UserSettings settings;
        private TextButton backButton;
        private TextButton WASDButton;
        private TextButton arrowsButton;
        private TextButton resetBestRunButton;
        private Text controlSchemeText;

        public OptionsMenu(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            settings = new();
            settings = UserSettings.Load();
            backButton = new("BACK", BACK_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            backButton.OnClick += BackButton_OnClick;

            if(settings.ActiveControlScheme == UserSettings.ControlScheme.WASD)
            {
                WASDButton = new("WASD", WASD_BUTT_POS, Color.White, Color.LightGray, Color.Gray, active: false);
                WASDButton.OnClick += WASDButton_OnClick;

                arrowsButton = new("Arrows", ARROWS_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
                arrowsButton.OnClick += ArrowsButton_OnClick;

            }
            else
            {
                WASDButton = new("WASD", WASD_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
                WASDButton.OnClick += WASDButton_OnClick;

                arrowsButton = new("Arrows", ARROWS_BUTT_POS, Color.White, Color.LightGray, Color.Gray, active: false);
                arrowsButton.OnClick += ArrowsButton_OnClick;
            }

            controlSchemeText = new("Control scheme: ", CTRL_SCHEME_TXT_POS, Color.White);

            resetBestRunButton = new("RESET BEST RUN", RESET_BEST_RUN_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            resetBestRunButton.OnClick += ResetBestRunButton_OnClick;
        }


        public void Load()
        {
            SpriteFont font = contentManager.Load<SpriteFont>("main_menu_font");
            backButton.Font = font;

            font = contentManager.Load<SpriteFont>("game_font");
            controlSchemeText.Font = font;
            WASDButton.Font = font;
            arrowsButton.Font = font;
            resetBestRunButton.Font = font;
        }

        public void Focus() { }

        public void Update(GameTime gameTime)
        {
            backButton.Update();
            WASDButton.Update();
            arrowsButton.Update();
            resetBestRunButton.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            backButton.Draw(spriteBatch);
            controlSchemeText.Draw(spriteBatch);
            WASDButton.Draw(spriteBatch);
            arrowsButton.Draw(spriteBatch);
            resetBestRunButton.Draw(spriteBatch);
        }
        private void ResetBestRunButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            settings.HealthState = HealthState.NONE;
        }

        private void BackButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            // save settings
            settings.Save();

            // close scene
            SceneManager.RemoveScene(this);
        }
        private void ArrowsButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            arrowsButton.Active = false;
            WASDButton.Active = true;
            settings.ActiveControlScheme = UserSettings.ControlScheme.ARROWS;
        }

        private void WASDButton_OnClick(object sender, System.EventArgs e)
        {
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            WASDButton.Active = false;
            arrowsButton.Active = true;
            settings.ActiveControlScheme = UserSettings.ControlScheme.WASD;
        }

    }
}
