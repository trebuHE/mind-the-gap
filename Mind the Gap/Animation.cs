using Microsoft.Xna.Framework;

namespace Mind_the_Gap
{
    /// <summary>
    /// Single-row animation
    /// </summary>
    internal class Animation
    {
        private int activeFrame;
        private float counter;
        private readonly int frames;
        private readonly int row;
        private readonly Vector2 frameSize;
        private readonly float interval;

        /// <param name="frames">number of frames in the animation</param>
        /// <param name="row">index of a row in a sprite sheet, starting at 0</param>
        /// <param name="frameSize">size of each animation frame</param>
        /// <param name="loopTime">time in secondsafter which animation loops</param>
        public Animation(int frames, int row, float loopTime, Vector2 frameSize)
        {
            this.frames = frames;
            this.row = row;
            this.frameSize = frameSize;
            activeFrame = 0;
            interval = loopTime / frames;
        }

        public void Update(GameTime gameTime)
        {
            if(frames > 1)
            {
                counter += (float)gameTime.ElapsedGameTime.TotalSeconds;
                if(counter > interval)
                {
                    counter = 0;
                    NextFrame();
                }
            }
        }

        private void NextFrame()
        {
            activeFrame++;
            if(activeFrame >= frames)
            {
                activeFrame = 0;
            }
        }

        public Rectangle GetFrameRect()
        {
            return new Rectangle(activeFrame * (int)frameSize.X,
                row * (int)frameSize.Y,
                (int)frameSize.X,
                (int)frameSize.Y);

        }

        public void Reset()
        {
            activeFrame = 0;
        }
    }
}
