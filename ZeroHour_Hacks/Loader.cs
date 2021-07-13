using UnityEngine;

namespace ZeroHour_Hacks
{
    public class Loader
    {
        public static void Load()
        {
            gameObject = new GameObject();
            gameObject.AddComponent<gameObj>();
            Object.DontDestroyOnLoad(gameObject);
        }
        public static void Unload()
        {
            Object.Destroy(gameObject);
        }

        private static GameObject gameObject;
    }
}