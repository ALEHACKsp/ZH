using System;
using UnityEngine;
using System.IO;
using System.Linq;

namespace ZeroHour_Hacks
{
    
    public class Loader
    {

        public static void Load()
        {
        gameObject = new UnityEngine.GameObject();
        gameObject.AddComponent<gameObj>();
        UnityEngine.Object.DontDestroyOnLoad(gameObject);

        }
        public static void Unload()
        {
            UnityEngine.Object.Destroy(gameObject);
        }
        static GameObject gameObject;
    }
}