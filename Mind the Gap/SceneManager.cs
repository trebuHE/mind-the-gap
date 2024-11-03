using System.Collections.Generic;

namespace Mind_the_Gap
{
    internal class SceneManager
    {
        public IScene CurrentScene
        {
            get
            {
                return scenes.Peek();
            }
            private set { }
        }
        private Stack<IScene> scenes;
        public SceneManager()
        {
            scenes = new();
        }

        public void AddScene(IScene scene)
        {
            scenes.Push(scene);
            scene.Load();
        }

        public void RemoveScene(IScene scene)
        {
            scenes.Pop();
        }
    }
}
