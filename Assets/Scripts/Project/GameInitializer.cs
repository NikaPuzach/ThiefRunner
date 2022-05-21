using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class GameInitializer : MonoBehaviour
    {
        public static bool isInitialized;

        void Awake()
        {
            if (isInitialized)
            {
                Debug.LogWarning("Game is already initialized!");
                return;
            }

            Run();
        }

        private void Run()
        {
            InitUserSettings();

            //CreateInstance<Localizer>();
        }

        private void InitUserSettings()
        {
            UserSettings.Initialize();
        }

        private T CreateInstance<T>()
        {
            return (T) Activator.CreateInstance(typeof(T));
        }
    }
}
