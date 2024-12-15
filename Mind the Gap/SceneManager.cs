using System.Collections.Generic;

namespace Mind_the_Gap
{
    internal static class SceneManager
    {
        public static IScene CurrentScene
        {
            get
            {
                return scenes.Peek();
            }
            private set { }
        }

        private static Stack<IScene> scenes = new();

        public static void AddScene(IScene scene)
        {
            scenes.Push(scene);
            scene.Load();
        }

        public static void RemoveScene(IScene scene)
        {
            scenes.Pop();
            scenes.Peek().Focus();
        }
    }
}
