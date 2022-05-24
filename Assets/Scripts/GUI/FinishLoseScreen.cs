using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishLoseScreen : NorskaLib.GUI.Screen
{
    SceneController SceneController => SceneController.Instance;
    [SerializeField] Button restartButton;

    private void Start()
    {
        restartButton.onClick.AddListener(() => SceneController.RestartGame());
    }
}
