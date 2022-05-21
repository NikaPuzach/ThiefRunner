using System;
using System.Collections;
using System.Collections.Generic;
using NorskaLib.GUI;
using UnityEngine;
using UnityEngine.UI;
using NorskaLib.GUI;

public class InitialScreen : NorskaLib.GUI.Screen
{
    SceneController SceneController => SceneController.Instance;
    ScreenManager ScreenManager => ScreenManager.Instance;

    public Button startButton;
    [SerializeField] GameObject initial;
    [SerializeField] GameObject skinSettings;
    [SerializeField] Button returnButton;
    [SerializeField] Button skinSettingsButton;
    [SerializeField] Button rightButtton;
    [SerializeField] Button leftButton;

    private void Start()
    {
        skinSettings.SetActive(false);

        startButton.onClick.AddListener(() => SceneController.StartGameplay());
        skinSettingsButton.onClick.AddListener(() => OpenSkinSettings());
        returnButton.onClick.AddListener(() => CloseSkinSettings());
    }

    private void CloseSkinSettings()
    {
        ScreenManager.ShowScreen<HeadUpDisplay>();

        initial.SetActive(true);
        skinSettings.SetActive(false);
    }

    private void OpenSkinSettings()
    {
        ScreenManager.HideScreen<HeadUpDisplay>();

        initial.SetActive(false);
        skinSettings.SetActive(true);
    }
}
