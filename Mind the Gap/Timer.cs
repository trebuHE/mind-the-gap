﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Mind_the_Gap
{
    internal class Timer
    {
        private static readonly List<Timer> timers = new();

        private readonly float time;
        private readonly Action callbackAction;
        private readonly bool looping;
        private readonly string name;
        private float elapsedTime;
        private bool destroy;
        private bool stop;

        private Timer(float time, Action callbackAction, bool looping = false, string name = null)
        {
            this.time = time;
            this.callbackAction = callbackAction;
            this.looping = looping;
            this.name = name;
            destroy = false;
            elapsedTime = 0f;
            stop = false;
        }

        private void Update(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(elapsedTime > time)
            {
                elapsedTime = 0f;

                if(!stop)
                    callbackAction();

                if(!looping)
                    destroy = true;
            }
        }

        public void Stop()
        {
            stop = true;
            destroy = true;
        }

        public static bool Stop(string name)
        {
            bool succes = false;

            for(int i = timers.Count - 1; i >= 0; i--)
            {
                if(timers[i].name == name)
                {
                    timers[i].Stop();
                    succes = true;
                }
            }

            return succes;
        }

        /// <summary>
        /// Creates a Timer and returns it.
        /// </summary>
        /// <remarks>There can only be one Timer with a given <paramref name="name"/>!</remarks>
        /// <param name="time">in seconds</param>
        /// <param name="callbackAction">function to be called when the time is up</param>
        /// <param name="looping">should the timer loop?</param>
        /// <param name="name">to reference a specific Timer. Note: there can only be one Timer with a given <paramref name="name"/>! </param>
        public static Timer Create(float time, Action callbackAction, bool looping = false, string name = null)
        {
            Timer timer = new(time, callbackAction, looping, name);

            foreach(var t in timers)
            {
                if(t.name == name && name != null)
                    return null;
            }

            timers.Add(timer);
            return timer;
        }

        public static void UpdateTimers(GameTime gameTime)
        {
            if(timers.Count > 0)
            {
                for(int i = timers.Count - 1; i >= 0; i--)
                {
                    timers[i].Update(gameTime);

                    if(timers[i].destroy)
                    {
                        timers.RemoveAt(i);
                    }
                }
            }
        }
    }
}
