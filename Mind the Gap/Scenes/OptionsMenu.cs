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
        private readonly Vector2 RESET_BEST_RUN_BUTT_POS = new(150, 400);
        private readonly Vector2 SOUND_ON_BUTT_POS = new(300, 300);
        private readonly Vector2 SOUND_OFF_BUTT_POS = new(370, 300);
        private readonly Vector2 OPTIONS_TXT_POS = new(150, 100);
        private readonly Vector2 CTRL_SCHEME_TXT_POS = new(150, 250);
        private readonly Vector2 SOUND_TXT_POS = new(150, 300);
        #endregion

        private readonly ContentManager contentManager;
        private UserSettings settings;
        private TextButton backButton;
        private TextButton WASDButton;
        private TextButton arrowsButton;
        private TextButton resetBestRunButton;
        private TextButton soundOnButton;
        private TextButton soundOffButton;
        private Text controlSchemeText;
        private Text soundText;
        private Text optionsText;

        public OptionsMenu(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            settings = new();
            settings = UserSettings.Load();

            optionsText = new("OPTIONS", OPTIONS_TXT_POS, Color.White);

            backButton = new("BACK", BACK_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            backButton.OnClick += BackButton_OnClick;

            if(settings.ActiveControlScheme == UserSettings.ControlScheme.WASD)
            {
                WASDButton = new("WASD", WASD_BUTT_POS, Color.White, Color.LightGray, Color.Gray, active: false);
                arrowsButton = new("Arrows", ARROWS_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            }
            else
            {
                WASDButton = new("WASD", WASD_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
                arrowsButton = new("Arrows", ARROWS_BUTT_POS, Color.White, Color.LightGray, Color.Gray, active: false);
            }
            WASDButton.OnClick += WASDButton_OnClick;
            arrowsButton.OnClick += ArrowsButton_OnClick;

            soundText = new("Sound: ", SOUND_TXT_POS, Color.White);

            if(settings.Sound)
            {
                soundOnButton = new("On", SOUND_ON_BUTT_POS, Color.White, Color.LightGray, Color.Gray, active: false);
                soundOffButton = new("Off", SOUND_OFF_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            }
            else
            {
                soundOnButton = new("On", SOUND_ON_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
                soundOffButton = new("Off", SOUND_OFF_BUTT_POS, Color.White, Color.LightGray, Color.Gray, active: false);
            }
            soundOnButton.OnClick += SoundOnButton_OnClick;
            soundOffButton.OnClick += SoundOffButton_OnClick;

            controlSchemeText = new("Control scheme: ", CTRL_SCHEME_TXT_POS, Color.White);

            resetBestRunButton = new("RESET BEST RUN", RESET_BEST_RUN_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            resetBestRunButton.OnClick += ResetBestRunButton_OnClick;
        }


        public void Load()
        {
            SpriteFont font = contentManager.Load<SpriteFont>("main_menu_font");
            backButton.Font = font;
            optionsText.Font = font;

            font = contentManager.Load<SpriteFont>("game_font");
            controlSchemeText.Font = font;
            WASDButton.Font = font;
            arrowsButton.Font = font;
            resetBestRunButton.Font = font;
            soundOnButton.Font = font;
            soundOffButton.Font = font;
            soundText.Font = font;
        }

        public void Focus() { }

        public void Update(GameTime gameTime)
        {
            backButton.Update();
            WASDButton.Update();
            arrowsButton.Update();
            resetBestRunButton.Update();
            soundOnButton.Update();
            soundOffButton.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            backButton.Draw(spriteBatch);
            controlSchemeText.Draw(spriteBatch);
            WASDButton.Draw(spriteBatch);
            arrowsButton.Draw(spriteBatch);
            resetBestRunButton.Draw(spriteBatch);
            soundOnButton.Draw(spriteBatch);
            soundOffButton.Draw(spriteBatch);
            soundText.Draw(spriteBatch);
            optionsText.Draw(spriteBatch);
        }
        private void SoundOffButton_OnClick(object sender, System.EventArgs e)
        {
            soundOffButton.Active = false;
            soundOnButton.Active = true;
            settings.Sound = false;
            settings.Save();
        }

        private void SoundOnButton_OnClick(object sender, System.EventArgs e)
        {
            settings.Sound = true;
            settings.Save();
            SoundEffects.Play(SoundEffects.Effects.CLICK);
            soundOnButton.Active = false;
            soundOffButton.Active = true;
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
