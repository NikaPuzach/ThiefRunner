using System;
using System.Collections;
using System.Collections.Generic;
using NorskaLib.GUI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HeadUpDisplay : NorskaLib.GUI.Screen
{
    [SerializeField] IconedLabel moneyMeter;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Button settings;
    [SerializeField] Button settingsClose;
    [SerializeField] GameObject settingsPanel;
    UserDataManager UserDataManager => UserDataManager.Instance;
    SceneController SceneController => SceneController.Instance;

    int sceneIndex;

    private void Start()
    {
        settingsPanel.SetActive(false);

        moneyMeter.LabelText = UserDataManager.Money.ToString();
        UserDataManager.onMoneyChange += OnMoneyChange;

        settings.onClick.AddListener(() => SceneController.OpenSettings(settingsPanel));
        settingsClose.onClick.AddListener(() => SceneController.CloseSettings(settingsPanel));

        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        levelText.SetText($"Level {sceneIndex + 1}");
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UserDataManager.onMoneyChange -= OnMoneyChange;
    }

    private void Update()
    {
        if (SceneController.Phase == GamePhases.Gameplay ||
            SceneController.Phase == GamePhases.Lose)
            settings.interactable = false;
        else
            settings.interactable = true;
    }

    private void OnMoneyChange(int obj)
    {
        moneyMeter.LabelText = obj.ToString();
    }
}
