using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap.Scenes
{
    internal class OptionsMenu : IScene
    {
        #region consts
        private readonly Vector2 BACK_BUTT_POS = new(420, 600);
        #endregion
        private readonly ContentManager contentManager;
        private TextButton backButton;

        public OptionsMenu(ContentManager contentManager)
        {
            this.contentManager = contentManager;
            backButton = new("BACK", BACK_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            backButton.OnClick += BackButton_OnClick;
        }

        public void Load()
        {
            SpriteFont font = contentManager.Load<SpriteFont>("main_menu_font");
            backButton.Font = font;
        }

        public void Update(GameTime gameTime)
        {
            backButton.Update();
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            backButton.Draw(spriteBatch);
        }

        private void BackButton_OnClick(object sender, System.EventArgs e)
        {
            // save settings

            // close scene
            SceneManager.RemoveScene(this);
        }

    }
}
