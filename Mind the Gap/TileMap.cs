using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;

namespace Mind_the_Gap
{
    internal class TileMap : Sprite
    {
        public HashSet<int> UsedTiles { get; private set; }
        private Dictionary<Vector2, int> map;
        private int tilesPerRow;
        public TileMap(Vector2 position, Vector2 tileSize, int tilesPerRow) : base(position, tileSize)
        {
            this.tilesPerRow = tilesPerRow;
            UsedTiles = new();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach(var tile in map)
            {
                position = tile.Key * size * SCALE;
                Rectangle destRect = new(
                    (int)position.X,
                    (int)position.Y,
                    (int)size.X * SCALE,
                    (int)size.Y * SCALE);

                int x = tile.Value % tilesPerRow;
                int y = tile.Value / tilesPerRow;

                Rectangle srcRect = new(
                    x * (int)size.X,
                    y * (int)size.Y,
                    (int)size.X,
                    (int)size.Y);

                spriteBatch.Draw(Texture, destRect, srcRect, Color.White);
            }
        }

        public void Draw(SpriteBatch spriteBatch, int index, Vector2 position, Vector2 screenSize)
        {
            Rectangle destRect = new(
                    (int)position.X,
                    (int)position.Y,
                    (int)screenSize.X,
                    (int)screenSize.Y);

            int x = index % tilesPerRow;
            int y = index / tilesPerRow;

            Rectangle srcRect = new(
                x * (int)size.X,
                y * (int)size.Y,
                (int)size.X,
                (int)size.Y);

            spriteBatch.Draw(Texture, destRect, srcRect, Color.White);
        }

        public void LoadMap(string filePath)
        {
            map = new Dictionary<Vector2, int>();

            StreamReader reader = new(filePath);
            int y = 0;
            string line;

            while((line = reader.ReadLine()) != null)
            {
                string[] tiles = line.Split(",");

                for(int x = 0; x < tiles.Length; x++)
                {
                    if(int.TryParse(tiles[x], out int value) && value > -1)
                    {
                        map[new Vector2(x, y)] = value;
                        UsedTiles.Add(value);
                    }
                }
                y++;
            }
        }

        public int GetTileAtPos(Vector2 tilePos)
        {
            return map[tilePos];
        }

        public void SetTileAtPos(Vector2 tilePos, int tile)
        {
            map[tilePos] = tile;
        }
    }
}
