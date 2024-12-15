using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;

namespace Mind_the_Gap
{
    internal class Player : Sprite
    {
        private enum WalkAnimations
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

        private enum DeathAnimations
        {
            EXPLODE
        }

        private struct MovementMap
        {
            public MovementMap(Keys up, Keys down, Keys left, Keys right)
            {
                this.up = up;
                this.down = down;
                this.left = left;
                this.right = right;
            }

            public Keys up;
            public Keys down;
            public Keys left;
            public Keys right;
        }

        public Sprite Explosion { get; set; }

        public Vector2 GridPosition
        {
            get
            {
                return new Vector2((int)((position.X + (size.X * scale / 2)) / gridCellSize), (int)((position.Y + (size.Y * scale / 2)) / gridCellSize));
            }
            private set
            {
                targetPos.X = (int)(value.X * gridCellSize);
                targetPos.Y = (int)(value.Y * gridCellSize);
            }
        }
        public int Health { get; private set; }
        public bool Alive { get; private set; }
        public bool Dying { get; private set; }

        public const float dieTime = 1.5f;
        private Vector2 velocity;
        private Vector2 targetPos;
        private Vector2 spawnPos;
        private bool canMove;
        private bool takeInputRight;
        private bool takeInputLeft;
        private bool takeInputDown;
        private bool takeInputUp;
        private readonly AnimationManager<WalkAnimations> walkAnimationManager;
        private readonly AnimationManager<DeathAnimations> deathAnimationManager;
        private readonly int gridCellSize;
        private readonly Vector2 gridSize;
        private readonly float step = 1;
        private readonly int maxHealth = 5;
        private readonly UserSettings settings;
        private readonly MovementMap movementMap;

        public Player(Vector2 position, Vector2 size, int stepSize, Vector2 gridSize) : base(position * stepSize, size)
        {
            walkAnimationManager = new(new Dictionary<WalkAnimations, Animation>() {
                { WalkAnimations.IDLE_DOWN, new Animation(2, 0, 0.9f, size) },
                { WalkAnimations.IDLE_RIGHT,new Animation(2, 1, 0.9f, size) },
                { WalkAnimations.IDLE_LEFT, new Animation(2, 2, 0.9f, size) },
                { WalkAnimations.IDLE_UP,   new Animation(2, 3, 0.9f, size) },
                { WalkAnimations.WALK_DOWN, new Animation(4, 4, 0.9f, size) },
                { WalkAnimations.WALK_RIGHT,new Animation(4, 5, 0.9f, size) },
                { WalkAnimations.WALK_LEFT, new Animation(4, 6, 0.9f, size) },
                { WalkAnimations.WALK_UP,   new Animation(4, 7, 0.9f, size) },
            });
            walkAnimationManager.ActiveAnimation = WalkAnimations.IDLE_DOWN;

            Explosion = new(position * stepSize, new Vector2(48, 48), visible: false);

            deathAnimationManager = new(new Dictionary<DeathAnimations, Animation>() {
                { DeathAnimations.EXPLODE, new Animation(7, 0, dieTime, new Vector2(48, 48)) }
            });

            settings = new();
            settings = UserSettings.Load();

            if(settings.ActiveControlScheme == UserSettings.ControlScheme.WASD)
            {
                movementMap = new(Keys.W, Keys.S, Keys.A, Keys.D);
            }
            else if(settings.ActiveControlScheme == UserSettings.ControlScheme.ARROWS)
            {
                movementMap = new(Keys.Up, Keys.Down, Keys.Left, Keys.Right);
            }


            gridCellSize = stepSize;
            GridPosition = position;
            this.gridSize = gridSize;
            canMove = true;
            takeInputRight = true;
            takeInputLeft = true;
            takeInputDown = true;
            takeInputUp = true;
            spawnPos = position * stepSize;
            targetPos = spawnPos;
            Health = maxHealth;
            Alive = true;
            Dying = false;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 prevVelocity = velocity;
            GetInput();
            Move();
            SetAnimation(prevVelocity);

            walkAnimationManager.Update(gameTime);

            if(Dying)
                deathAnimationManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(Visible)
                spriteBatch.Draw(Texture, DestinationRect, walkAnimationManager.SourceRect, Color.White);

            if(Explosion.Visible)
                spriteBatch.Draw(Explosion.Texture, DestinationRect, deathAnimationManager.SourceRect, Color.White);
        }

        public void Restart()
        {
            visible = true;
            Explosion.Visible = false;
            Dying = false;
            position = spawnPos;
            targetPos = spawnPos;
            walkAnimationManager.ActiveAnimation = WalkAnimations.IDLE_DOWN;
        }

        public void DecrementHealth()
        {
            Health--;
            if(Health <= 0)
            {
                // DIE
                Debug.WriteLine("PLAYER IS DEAD");
                Alive = false;
            }
        }

        public void Explode()
        {
            if(!Dying)
            {
                SoundEffects.Play(SoundEffects.Effects.EXPLOSION);
                visible = false;
                deathAnimationManager.ActiveAnimation = DeathAnimations.EXPLODE;
                Explosion.Visible = true;
                Dying = true;
            }
        }

        private void GetInput()
        {
            Vector2 prevGridPos = GridPosition;
            if(canMove)
            {
                //right
                if(takeInputRight && Keyboard.GetState().IsKeyDown(movementMap.right) && GridPosition.X < gridSize.X - 1)
                {
                    prevGridPos.X += 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputRight = false;
                }
                if(Keyboard.GetState().IsKeyUp(movementMap.right))
                    takeInputRight = true;

                //left
                if(takeInputLeft && Keyboard.GetState().IsKeyDown(movementMap.left) && GridPosition.X > 0)
                {
                    prevGridPos.X -= 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputLeft = false;
                }
                if(Keyboard.GetState().IsKeyUp(movementMap.left))
                    takeInputLeft = true;

                //up
                if(takeInputUp && Keyboard.GetState().IsKeyDown(movementMap.up) && GridPosition.Y > 0)
                {
                    prevGridPos.Y -= 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputUp = false;
                }
                if(Keyboard.GetState().IsKeyUp(movementMap.up))
                    takeInputUp = true;

                //down
                if(takeInputDown && Keyboard.GetState().IsKeyDown(movementMap.down) && GridPosition.Y < gridSize.Y - 1)
                {
                    prevGridPos.Y += 1;
                    GridPosition = prevGridPos;
                    canMove = false;
                    takeInputDown = false;
                }
                if(Keyboard.GetState().IsKeyUp(movementMap.down))
                    takeInputDown = true;
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
                    walkAnimationManager.ActiveAnimation = WalkAnimations.WALK_RIGHT;
                }

                //is standing to the right
                if(velocity.X == 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.IDLE_RIGHT;
                }
            }

            // was going left
            if(prevVelocity.X < 0f)
            {
                //is going left
                if(velocity.X < 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.WALK_LEFT;
                }

                //is standing to the left
                if(velocity.X == 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.IDLE_LEFT;
                }
            }

            // was going down
            if(prevVelocity.Y > 0f)
            {
                //is going down
                if(velocity.Y > 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.WALK_DOWN;
                }

                //is standing face down
                if(velocity.Y == 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.IDLE_DOWN;
                }
            }

            // was going up
            if(prevVelocity.Y < 0f)
            {
                //is going up
                if(velocity.Y < 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.WALK_UP;
                }

                //is standing face up
                if(velocity.Y == 0f)
                {
                    walkAnimationManager.ActiveAnimation = WalkAnimations.IDLE_UP;
                }
            }
        }
    }
}
