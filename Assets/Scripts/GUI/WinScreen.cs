using System;
using NorskaLib.GUI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinScreen : NorskaLib.GUI.Screen
{
    [SerializeField] Button nextLevel;

    private void Start()
    {
        nextLevel.onClick.AddListener(() => LoadNextLevel());
    }

    private void LoadNextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
