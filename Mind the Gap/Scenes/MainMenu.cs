using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap.Scenes
{
    internal class MainMenu : IScene
    {
        #region consts
        private readonly Vector2 NEW_GAME_BUTT_POS = new Vector2(420, 350);
        private readonly Vector2 OPTIONS_BUTT_POS = new Vector2(420, 450);
        private readonly Vector2 EXIT_BUTT_POS = new Vector2(420, 550);
        #endregion
        private TextButton newGameButton;
        private TextButton optionsButton;
        private TextButton exitButton;
        private readonly ContentManager contentManager;

        public MainMenu(ContentManager contentManager)
        {
            this.contentManager = contentManager;

            newGameButton = new("NEW GAME", NEW_GAME_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            newGameButton.OnClick += NewGameButton_OnClick;
            optionsButton = new("OPTIONS", OPTIONS_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            optionsButton.OnClick += OptionsButton_OnClick;
            exitButton = new("EXIT", EXIT_BUTT_POS, Color.White, Color.LightGray, Color.Gray);
            exitButton.OnClick += ExitButton_OnClick;
        }

        public void Load()
        {
            SpriteFont font = contentManager.Load<SpriteFont>("main_menu_font");
            newGameButton.Font = font;
            optionsButton.Font = font;
            exitButton.Font = font;
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
        }

        private void ExitButton_OnClick(object sender, System.EventArgs e)
        {
            System.Environment.Exit(0);
        }

        private void OptionsButton_OnClick(object sender, System.EventArgs e)
        {
            SceneManager.AddScene(new OptionsMenu(contentManager));
        }

        private void NewGameButton_OnClick(object sender, System.EventArgs e)
        {
            SceneManager.AddScene(new Game(contentManager));
        }
    }
}
