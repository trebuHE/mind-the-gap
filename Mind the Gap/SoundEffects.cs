using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Mind_the_Gap
{
    internal static class SoundEffects
    {
        public enum Effects
        {
            CLICK,
            EXPLOSION
        }

        private static SoundEffect click;
        private static SoundEffectInstance clickInstance;
        private static SoundEffect explosion;
        private static SoundEffectInstance explosionInstance;

        public static void Load(ContentManager contentManager)
        {
            click = contentManager.Load<SoundEffect>("click");
            clickInstance = click.CreateInstance();
            clickInstance.Volume = 0.4f;
            explosion = contentManager.Load<SoundEffect>("explosionSFX");
            explosionInstance = explosion.CreateInstance();
            explosionInstance.Volume = 0.1f;
        }

        public static void Play(Effects effect)
        {
            switch(effect)
            {
                case Effects.CLICK:
                    clickInstance.Play();
                    break;
                case Effects.EXPLOSION:
                    explosionInstance.Play();
                    break;
                default:
                    break;
            }
        }
    }
}
