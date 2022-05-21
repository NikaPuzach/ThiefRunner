using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private void CashEssentials()
        {

        }

        void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
                Debug.LogWarning($"Removing double 'GameManager'");
                return;
            }

            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            DontDestroyOnLoad(this.gameObject);
            CashEssentials();

            Instance = this;
        }

        [Button]
        private void ClearPrefs()
        {
            PlayerPrefs.DeleteAll();
            Debug.Log($"Player prefs are cleared");
        }
    }
}
