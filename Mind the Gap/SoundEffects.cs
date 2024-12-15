using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Mind_the_Gap
{
    internal static class SoundEffects
    {
        public enum Effects
        {
            CLICK,
            EXPLOSION,
            WIN,
            WALK
        }

        private static SoundEffect click;
        private static SoundEffectInstance clickInstance;
        private static SoundEffect explosion;
        private static SoundEffectInstance explosionInstance;
        private static SoundEffect win;
        private static SoundEffectInstance winInstance;
        private static SoundEffect walk1;
        private static SoundEffectInstance walk1Instance;
        private static SoundEffect walk2;
        private static SoundEffectInstance walk2Instance;

        private static UserSettings settings;

        public static void Load(ContentManager contentManager)
        {
            click = contentManager.Load<SoundEffect>("click");
            clickInstance = click.CreateInstance();
            clickInstance.Volume = 0.4f;
            explosion = contentManager.Load<SoundEffect>("explosionSFX");
            explosionInstance = explosion.CreateInstance();
            explosionInstance.Volume = 0.1f;
            win = contentManager.Load<SoundEffect>("win");
            winInstance = win.CreateInstance();
            winInstance.Volume = 0.8f;
            walk1 = contentManager.Load<SoundEffect>("walk_1");
            walk1Instance = walk1.CreateInstance();
            walk1Instance.Volume = 0.05f;
            walk2 = contentManager.Load<SoundEffect>("walk_2");
            walk2Instance = walk2.CreateInstance();
            walk2Instance.Volume = 0.05f;
        }

        public static void Play(Effects effect)
        {
            settings = UserSettings.Load();

            if(settings.Sound)
            {
                switch(effect)
                {
                    case Effects.CLICK:
                        clickInstance.Play();
                        break;
                    case Effects.EXPLOSION:
                        explosionInstance.Play();
                        break;
                    case Effects.WIN:
                        winInstance.Play();
                        break;
                    case Effects.WALK:
                        Timer.Create(0.2f, () => walk1Instance.Play());
                        Timer.Create(0.6f, () => walk2Instance.Play());
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
