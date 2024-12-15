using System.IO;
using System.Text.Json;

namespace Mind_the_Gap
{
    internal class UserSettings
    {
        public enum ControlScheme
        {
            WASD,
            ARROWS
        }

        public UserSettings()
        {
            if(!File.Exists(PATH))
                Save();
        }

        public ControlScheme ActiveControlScheme { get; set; }
        public Scenes.HealthState HealthState { get; set; }

        private const string PATH = "settings.json";

        public void Save()
        {
            string serializedData = JsonSerializer.Serialize(this);
            File.WriteAllText(PATH, serializedData);
        }

        public static UserSettings Load()
        {
            if(!File.Exists(PATH))
                return null;

            string data = File.ReadAllText(PATH);
            return JsonSerializer.Deserialize<UserSettings>(data);
        }
    }
}
