using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mind_the_Gap
{
    internal class Level
    {
        public bool Finished { get; private set; }
        public bool DrawAndUpdatePlayer { get; private set; }

        private bool levelStarted;
        private ContentManager contentManager;
        private TileMap pathMap;
        private string pathMapPath;
        private TileMap gameMap;
        private string gameMapPath;
        private float memorizeTimeSec;
        private int walkableTile;
        private HashSet<int> forbiddenTiles;

        public Level(string pathMapPath, string gameMapPath, float memorizeTimeSec, ContentManager contentManager)
        {
            this.contentManager = contentManager;
            this.gameMapPath = gameMapPath;
            this.pathMapPath = pathMapPath;
            this.memorizeTimeSec = memorizeTimeSec;
            Finished = false;
            pathMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            gameMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            Texture2D texture = contentManager.Load<Texture2D>("tileset");
            pathMap.Texture = texture;
            gameMap.Texture = texture;
            DrawAndUpdatePlayer = false;
            levelStarted = false;
            walkableTile = -1;
            forbiddenTiles = new();
        }

        public void Load()
        {
            pathMap.LoadMap(pathMapPath);
            gameMap.LoadMap(gameMapPath);

            SetWalkableAndForbiddenTiles();

            Timer.Create(memorizeTimeSec, () => StartLevel());
        }

        public void Update(GameTime gameTime, Player player)
        {
            Vector2 playerGridPos = player.GridPosition;
            int gameTile = gameMap.GetTileAtPos(playerGridPos);
            int pathTile = pathMap.GetTileAtPos(playerGridPos);

            if(pathTile == walkableTile)
            {
                gameMap.SetTileAtPos(playerGridPos, pathTile);
            }
            else if(forbiddenTiles.Contains(pathTile))
            {
                // game over
                Debug.WriteLine("GAME OVER");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!levelStarted)
            {
                pathMap.Draw(spriteBatch);
            }
            else
            {
                gameMap.Draw(spriteBatch);
            }
        }

        private void StartLevel()
        {
            levelStarted = true;
            DrawAndUpdatePlayer = true;
        }

        private void SetWalkableAndForbiddenTiles()
        {
            HashSet<int> pool = new(pathMap.UsedTiles);
            pool.ExceptWith(gameMap.UsedTiles); // difference in sets

            List<int> poolList = new(pool);
            Random random = new();
            int i = random.Next(poolList.Count);
            Debug.WriteLine("Walkable tile is: " + poolList[i]);
            walkableTile = poolList[i];

            forbiddenTiles = new(pool);
            forbiddenTiles.Remove(poolList[i]);
            Debug.WriteLine("Forbidden tiles are: " + string.Join(", ", forbiddenTiles));
        }
    }
}
