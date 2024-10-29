using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mind_the_Gap
{
    internal class AnimationManager
    {
        public int ActiveAnimation
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

        private Dictionary<int, Animation> animations;
        private Animation activeAnimation;
        public AnimationManager(Dictionary<int, Animation> animations)
        {
            this.animations = animations;
            ActiveAnimation = 0;
            activeAnimation = this.animations[0];
        }

        public void Update(GameTime gameTime)
        {
            activeAnimation.Update(gameTime);
        }
    }
}
