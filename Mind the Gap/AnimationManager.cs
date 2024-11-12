﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Mind_the_Gap
{
    public enum Animations
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
    internal class AnimationManager
    {
        public Animations ActiveAnimation
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
        private readonly Dictionary<Animations, Animation> animations;
        public AnimationManager(Dictionary<Animations, Animation> animations)
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
