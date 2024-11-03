using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Mind_the_Gap
{
    internal class Player : Sprite
    {
        public Vector2 GridPosition
        {
            get
            {
                return new Vector2((int)(position.X / gridCellSize), (int)(position.Y / gridCellSize));
            }
            private set
            {
                targetPos.X = (int)(value.X * gridCellSize);
                targetPos.Y = (int)(value.Y * gridCellSize);
            }
        }

        private AnimationManager animationManager;
        private Vector2 velocity;
        private Vector2 targetPos;
        private bool canMove;
        private bool takeInputD;
        private bool takeInputA;
        private bool takeInputS;
        private bool takeInputW;
        // private readonly float speed = 150f;
        private readonly int gridCellSize;
        private readonly Vector2 gridSize;
        private readonly float step = 1;
        public Player(Vector2 position, Vector2 size, int stepSize, Vector2 gridSize) : base(position, size)
        {
            animationManager = new(new Dictionary<Animations, Animation>() {
                { Animations.IDLE_DOWN, new Animation(2, 0, size) },
                { Animations.IDLE_RIGHT,new Animation(2, 1, size) },
                { Animations.IDLE_LEFT, new Animation(2, 2, size) },
                { Animations.IDLE_UP,   new Animation(2, 3, size) },
                { Animations.WALK_DOWN, new Animation(4, 4, size) },
                { Animations.WALK_RIGHT,new Animation(4, 5, size) },
                { Animations.WALK_LEFT, new Animation(4, 6, size) },
                { Animations.WALK_UP,   new Animation(4, 7, size) },
            });

            animationManager.ActiveAnimation = Animations.IDLE_DOWN;
            gridCellSize = stepSize;
            GridPosition = Vector2.Zero;
            this.gridSize = gridSize;
            canMove = true;
            takeInputD = true;
            takeInputA = true;
            takeInputS = true;
            takeInputW = true;
            targetPos = position;
        }

        public override void Update(GameTime gameTime)
        {
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Vector2 prevVelocity = velocity;

            //if(velocity.Y == 0)
            //    MoveX(deltaTime);

            //if(velocity.X == 0)
            //    MoveY(deltaTime);

            GetInput();
            Move();
            SetAnimation(prevVelocity);

            animationManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, DestinationRect, animationManager.SourceRect, Color.White);
        }

        private void GetInput()
        {
            Vector2 prevGridPos = GridPosition;
            if(canMove)
            {
                //right
                if(takeInputD && Keyboard.GetState().IsKeyDown(Keys.D) && GridPosition.X < gridSize.X - 1)
                {
                    prevGridPos.X += 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputD = false;
                }
                if(Keyboard.GetState().IsKeyUp(Keys.D))
                    takeInputD = true;

                //left
                if(takeInputA && Keyboard.GetState().IsKeyDown(Keys.A) && GridPosition.X > 0)
                {
                    prevGridPos.X -= 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputA = false;
                }
                if(!Keyboard.GetState().IsKeyDown(Keys.A))
                    takeInputA = true;

                //up
                if(takeInputW && Keyboard.GetState().IsKeyDown(Keys.W) && GridPosition.Y > 0)
                {
                    prevGridPos.Y -= 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputW = false;
                }
                if(!Keyboard.GetState().IsKeyDown(Keys.W))
                    takeInputW = true;

                //down
                if(takeInputS && Keyboard.GetState().IsKeyDown(Keys.S) && GridPosition.Y < gridSize.Y - 1)
                {
                    prevGridPos.Y += 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputS = false;
                }
                if(!Keyboard.GetState().IsKeyDown(Keys.S))
                    takeInputS = true;
            }
        }

        private void Move()
        {
            velocity = Vector2.Zero;
            if(position != targetPos)
            {
                velocity = targetPos - position;
                //right
                if(velocity.X > 0)
                {
                    position.X += step;
                }
                //left
                if(velocity.X < 0)
                {
                    position.X -= step;
                }
                //up
                if(velocity.Y > 0)
                {
                    position.Y += step;
                }
                //down
                if(velocity.Y < 0)
                {
                    position.Y -= step;
                }
            }
            else
            {
                canMove = true;
            }
        }

        private void SetAnimation(Vector2 prevVelocity)
        {

            // was going right
            if(prevVelocity.X > 0f)
            {
                //is going right
                if(velocity.X > 0f)
                {
                    animationManager.ActiveAnimation = Animations.WALK_RIGHT;
                }

                //is standing to the right
                if(velocity.X == 0f)
                {
                    animationManager.ActiveAnimation = Animations.IDLE_RIGHT;
                }
            }

            // was going left
            if(prevVelocity.X < 0f)
            {
                //is going left
                if(velocity.X < 0f)
                {
                    animationManager.ActiveAnimation = Animations.WALK_LEFT;
                }

                //is standing to the left
                if(velocity.X == 0f)
                {
                    animationManager.ActiveAnimation = Animations.IDLE_LEFT;
                }
            }

            // was going down
            if(prevVelocity.Y > 0f)
            {
                //is going down
                if(velocity.Y > 0f)
                {
                    animationManager.ActiveAnimation = Animations.WALK_DOWN;
                }

                //is standing face down
                if(velocity.Y == 0f)
                {
                    animationManager.ActiveAnimation = Animations.IDLE_DOWN;
                }
            }

            // was going up
            if(prevVelocity.Y < 0f)
            {
                //is going up
                if(velocity.Y < 0f)
                {
                    animationManager.ActiveAnimation = Animations.WALK_UP;
                }

                //is standing face up
                if(velocity.Y == 0f)
                {
                    animationManager.ActiveAnimation = Animations.IDLE_UP;
                }
            }
        }

        /*
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

        }*/
    }
}
