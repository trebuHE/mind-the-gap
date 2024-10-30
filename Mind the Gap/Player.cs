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
            IDLE_DOWN,
            IDLE_RIGHT,
            IDLE_LEFT,
            IDLE_UP,
            WALK_DOWN,
            WALK_RIGHT,
            WALK_LEFT,
            WALK_UP
        }
        private AnimationManager animationManager;
        private Vector2 velocity;
        private float speed = 150f;
        public Player(Vector2 position, Vector2 size) : base(position, size)
        {
            animationManager = new(new Dictionary<int, Animation>() {
                { (int)Animations.IDLE_DOWN, new Animation(2, 0, size) },
                { (int)Animations.IDLE_RIGHT,new Animation(2, 1, size) },
                { (int)Animations.IDLE_LEFT, new Animation(2, 2, size) },
                { (int)Animations.IDLE_UP,   new Animation(2, 3, size) },
                { (int)Animations.WALK_DOWN, new Animation(4, 4, size) },
                { (int)Animations.WALK_RIGHT,new Animation(4, 5, size) },
                { (int)Animations.WALK_LEFT, new Animation(4, 6, size) },
                { (int)Animations.WALK_UP,   new Animation(4, 7, size) },
            });

            animationManager.ActiveAnimation = (int)Animations.IDLE_DOWN;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 prevVelocity = velocity;

            if(velocity.Y == 0)
                MoveX(deltaTime);

            if(velocity.X == 0)
                MoveY(deltaTime);

            SetAnimation(prevVelocity);

            animationManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRect, animationManager.SourceRect, Color.White);
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

        private void SetAnimation(Vector2 prevVelocity)
        {

            // was going right
            if(prevVelocity.X > 0f)
            {
                //is going right
                if(velocity.X > 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.WALK_RIGHT;
                }

                //is standing to the right
                if(velocity.X == 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.IDLE_RIGHT;
                }
            }

            // was going left
            if(prevVelocity.X < 0f)
            {
                //is going left
                if(velocity.X < 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.WALK_LEFT;
                }

                //is standing to the left
                if(velocity.X == 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.IDLE_LEFT;
                }
            }

            // was going down
            if(prevVelocity.Y > 0f)
            {
                //is going down
                if(velocity.Y > 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.WALK_DOWN;
                }

                //is standing face down
                if(velocity.Y == 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.IDLE_DOWN;
                }
            }

            // was going up
            if(prevVelocity.Y < 0f)
            {
                //is going up
                if(velocity.Y < 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.WALK_UP;
                }

                //is standing face up
                if(velocity.Y == 0f)
                {
                    animationManager.ActiveAnimation = (int)Animations.IDLE_UP;
                }
            }
        }
    }
}
