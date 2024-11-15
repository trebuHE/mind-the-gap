using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Mind_the_Gap
{
    internal class AnimationManager<T> where T : Enum
    {
        public T ActiveAnimation
        {
            set
            {
                animations.TryGetValue(value, out Animation anim);
                Animation prev = activeAnimation;
                if(anim != null && prev != anim)
                {
                    activeAnimation = anim;
                    activeAnimation.Reset();
                }
            }
        }

        /// <summary>
        /// Used as a source rectangle in a Draw() call
        /// </summary>
        public Rectangle SourceRect
        {
            get
            {
                return activeAnimation.GetFrameRect();
            }
        }

        private Animation activeAnimation;
        private readonly Dictionary<T, Animation> animations;
        public AnimationManager(Dictionary<T, Animation> animations)
        {
            this.animations = animations;
            ActiveAnimation = default;
        }

        public void Update(GameTime gameTime)
        {
            activeAnimation.Update(gameTime);
        }
    }
}
