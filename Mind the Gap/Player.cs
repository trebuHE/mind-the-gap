using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_the_Gap
{
    internal class Player : Sprite
    {
        private enum Animations
        {
            IDLE_FRONT,
            IDLE_RIGHT,
            IDLE_LEFT,
            IDLE_BACK,
            WALK_FRONT,
            WALK_RIGHT,
            WALK_LEFT,
            WALK_BACK
        }
        private AnimationManager animationManager;
        private Vector2 velocity;
        private float speed = 100f;
        public Player(Texture2D texture, Vector2 position, Vector2 size) : base(texture, position, size)
        {
            animationManager = new(new Dictionary<int, Animation>() {
                { (int)Animations.IDLE_FRONT, new Animation(2, 0, size) },
                { (int)Animations.IDLE_RIGHT, new Animation(2, 1, size) },
                { (int)Animations.IDLE_LEFT,  new Animation(2, 2, size) },
                { (int)Animations.IDLE_BACK,  new Animation(2, 3, size) },
                { (int)Animations.WALK_FRONT, new Animation(4, 4, size) },
                { (int)Animations.WALK_RIGHT, new Animation(4, 5, size) },
                { (int)Animations.WALK_LEFT,  new Animation(4, 6, size) },
                { (int)Animations.WALK_BACK,  new Animation(4, 7, size) },
            });

            animationManager.ActiveAnimation = (int)Animations.IDLE_FRONT;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(velocity.Y == 0)
                MoveX(deltaTime);

            if(velocity.X == 0)
                MoveY(deltaTime);

            animationManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, DestinationRect, animationManager.SourceRect, Color.White);
        }

        private void MoveX(float deltaTime)
        {
            velocity.X = 0f;
            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                velocity.X += speed * deltaTime;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                velocity.X -= speed * deltaTime;
            }
            position.X += velocity.X;

        }
        private void MoveY(float deltaTime)
        {
            velocity.Y = 0f;
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                velocity.Y -= speed * deltaTime;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                velocity.Y += speed * deltaTime;
            }
            position.Y += velocity.Y;

        }
    }
}
