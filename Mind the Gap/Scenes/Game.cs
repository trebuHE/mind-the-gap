﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Mind_the_Gap.Scenes
{
    internal class Game : IScene
    {
        private ContentManager contentManager;
        private Player player;
        private TileMap tileMap;
        public Game(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public void Load()
        {
            tileMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            tileMap.LoadMap("../../../data/levels/level_test/level_test_1.csv");
            Vector2 playerStartPos = new(tileMap.DestinationRect.Width / 2, tileMap.DestinationRect.Height / 2);
            player = new(Vector2.Zero, new Vector2(14, 14), tileMap.DestinationRect.Width, new Vector2(20, 12));
            Texture2D texture = contentManager.Load<Texture2D>("player_sprite_sheet");
            player.Texture = texture;
            texture = contentManager.Load<Texture2D>("tileset");
            tileMap.Texture = texture;
        }

        public void Update(GameTime gameTime)
        {
            player.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            tileMap.Draw(spriteBatch);
            player.Draw(spriteBatch);
        }
    }
}
