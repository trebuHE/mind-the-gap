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
        public bool GameOver { get; private set; }
        public bool DrawAndUpdatePlayer { get; private set; }
        public Vector2 PlayerSpawnGridPos { get; private set; }
        public int WalkableTile { get; private set; }
        public TileMap GameMap { get; private set; }

        private bool levelStarted;
        private ContentManager contentManager;
        private TileMap pathMap;
        private string pathMapPath;
        private string gameMapPath;
        private float memorizeTimeSec;
        private HashSet<int> forbiddenTiles;
        private int winCol;

        public Level(string pathMapPath, string gameMapPath, float memorizeTimeSec, Vector2 playerSpawnGridPos, int winCol, ContentManager contentManager)
        {
            this.contentManager = contentManager;
            this.gameMapPath = gameMapPath;
            this.pathMapPath = pathMapPath;
            this.memorizeTimeSec = memorizeTimeSec;
            PlayerSpawnGridPos = playerSpawnGridPos;
            this.winCol = winCol;

            Init();
        }

        private void Init()
        {
            Finished = false;
            pathMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            GameMap = new(Vector2.Zero, new Vector2(16, 16), 4);
            Texture2D texture = contentManager.Load<Texture2D>("tileset");
            pathMap.Texture = texture;
            GameMap.Texture = texture;
            DrawAndUpdatePlayer = false;
            levelStarted = false;
            WalkableTile = -1;
            forbiddenTiles = new();
            GameOver = false;
        }

        public void Load()
        {
            Init();

            pathMap.LoadMap(pathMapPath);
            GameMap.LoadMap(gameMapPath);

            SetWalkableAndForbiddenTiles();

            Timer.Create(memorizeTimeSec, () => StartLevel());
        }

        public void Update(GameTime gameTime, Player player)
        {
            Vector2 playerGridPos = player.GridPosition;
            int pathTile = pathMap.GetTileAtPos(playerGridPos);

            if(pathTile == WalkableTile)
            {
                GameMap.SetTileAtPos(playerGridPos, pathTile);
            }
            else if(forbiddenTiles.Contains(pathTile))
            {
                // game over
                Debug.WriteLine("GAME OVER");
                GameOver = true;
            }

            if(player.GridPosition.X >= winCol - 1)
            {
                // level finished
                Debug.WriteLine("Level finished!");
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
                GameMap.Draw(spriteBatch);
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
            pool.ExceptWith(GameMap.UsedTiles); // difference in sets

            List<int> poolList = new(pool);
            Random random = new();
            int i = random.Next(poolList.Count);
            Debug.WriteLine("Walkable tile is: " + poolList[i]);
            WalkableTile = poolList[i];

            forbiddenTiles = new(pool);
            forbiddenTiles.Remove(poolList[i]);
            Debug.WriteLine("Forbidden tiles are: " + string.Join(", ", forbiddenTiles));
        }
    }
}
