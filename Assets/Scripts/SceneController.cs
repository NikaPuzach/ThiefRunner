using System;
using System.Collections;
using System.Collections.Generic;
using NorskaLib.GUI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Cinemachine;
using System.Linq;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }

    CinemachineShake CinemachineShake => CinemachineShake.Instance;
    public static ScreenManager ScreenManager => ScreenManager.Instance;
    UserDataManager UserDataManager => UserDataManager.Instance;
    GrabbableItemManager GrabbableItemManager => GrabbableItemManager.Instance;

    [SerializeField] CinemachineVirtualCamera cinemachine;

    [SerializeField] Unit player;
    public Action<GamePhases> OnPhaseHasChanged;

    public bool hasHit = false;

    private GamePhases phase;
    public GamePhases Phase { get => phase;
        private set
        {
            phase = value;
            OnPhaseHasChanged?.Invoke(phase);
        }
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ScreenManager.ShowScreen<InitialScreen>();
        ScreenManager.ShowScreen<HeadUpDisplay>();
        UnitEvents.OnUnitEnteredFinish += OnUnitEnteredFinish;
        UnitEvents.OnUnitHit += OnUnitHit;
        UnitEvents.OnUnitCaught += OnUnitCaught;
        UnitEvents.OnUnitSlowedDown += OnUnitSlowedDown;
        UnitEvents.OnScooter += OnScooter;
        UnitEvents.OnScooterLoose += OnScooterLose;
        UnitEvents.OnHelicopterEndFinish += OnHelicopterEndFinish;
        UnitEvents.OnUnitEnterHelicopter += OnUnitEnterHelicopter;
        UnitEvents.OnHelicopterReachedEndPoint += OnHelicopterReachedEndPoint;
        phase = GamePhases.Initial;
    }

    private void OnDestroy()
    {
        UnitEvents.OnUnitEnteredFinish -= OnUnitEnteredFinish;
        UnitEvents.OnUnitHit -= OnUnitHit;
        UnitEvents.OnUnitCaught -= OnUnitCaught;
        UnitEvents.OnUnitSlowedDown -= OnUnitSlowedDown;
        UnitEvents.OnScooter -= OnScooter;
        UnitEvents.OnScooterLoose -= OnScooterLose;
        UnitEvents.OnUnitEnterHelicopter -= OnUnitEnterHelicopter;
        UnitEvents.OnHelicopterEndFinish -= OnHelicopterEndFinish;
        UnitEvents.OnHelicopterReachedEndPoint -= OnHelicopterReachedEndPoint;
    }

    private void OnHelicopterReachedEndPoint()
    {
        ScreenManager.ShowScreen<WinScreen>();
    }

    private void OnUnitEnterHelicopter(Unit unit, HelicopterMovementController helicopter)
    {
        StartCoroutine(EnterHelicopterRoutine(unit));
        cinemachine.Follow = helicopter.transform;
        cinemachine.LookAt = helicopter.transform;

        unit.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        helicopter.HelicopterFly();
    }

    private void OnHelicopterEndFinish(NumberPlatform numberPlatform)
    {
        List<NumberPlatform> numberPlatforms = new List<NumberPlatform>();
        numberPlatforms.Add(numberPlatform);

        var number = numberPlatforms.LastOrDefault();
        var money = GrabbableItemManager.money;

        GrabbableItemManager.money *= number.GoldMultiply;
        UserDataManager.Money += GrabbableItemManager.money - money;

        Phase = GamePhases.Finish;
    }

    private void OnScooter(Unit unit, Scooter scooter)
    {
        if(unit == player)
        {
            unit.transform.position = scooter.standPoint.position;
            unit.MovementController.speedMax = 10;
            scooter.transform.SetParent(unit.transform);
            var arm = unit.GetComponent<ArmController>();
            arm.scooter.weight = 0.9f;
            scooter.canvas.enabled = false;
        }
    }

    private void OnScooterLose(Scooter scooter)
    {
        player.MovementController.speedMax = 6;
        var arm = player.GetComponent<ArmController>();
        arm.scooter.weight = 0;
        scooter.transform.SetParent(null);
        scooter.gameObject.SetActive(false);
    }

    private void OnUnitSlowedDown(Unit unit, Obstacle obstacle)
    {
        if(unit == player)
        {
            unit.MovementController.speed = 1;
        }
    }

    private void OnUnitCaught(Unit unit, NPCPoliceman policeman)
    {
        if (unit == player)
        {
            unit.AnimationController.AnimateFall();
            var police = policeman.GetComponent<Unit>();

            StartCoroutine(LoseFinishRoutine());
        }
    }

    private void OnUnitHit(Unit unit, TrashConteiner trashConteiner)
    {
        if(unit == player)
        {
            unit.AnimationController.AnimateFall();
            CinemachineShake.ShakeCamera(5f, .1f);

            StartCoroutine(LoseFinishRoutine());
            StartCoroutine(EruptionRoutine());
        }
    }

    private void OnUnitEnteredFinish(Unit unit, Finish finish)
    {
        if(unit == player)
        {
            unit.transform.rotation = new Quaternion(0, 180, 0, 0);
            unit.AnimationController.AnimateDance();

            Phase = GamePhases.Finish;
        }
    }

    public void RestartGame()
    {
        Phase = GamePhases.Initial;
        ScreenManager.ShowScreen<InitialScreen>();
        ScreenManager.HideScreen<GameplayScreen>();
        ScreenManager.HideScreen<FinishLoseScreen>();

        SceneManager.LoadScene(0);
    }

    public void StartGameplay()
    {
        Phase = GamePhases.Gameplay;
        ScreenManager.HideScreen<InitialScreen>();
        ScreenManager.ShowScreen<GameplayScreen>();
    }

    public void OpenSettings(GameObject settingPanel)
    {
        settingPanel.SetActive(true);
        ScreenManager.HideScreen<InitialScreen>();
    }

    public void CloseSettings(GameObject settingPanel)
    {
        settingPanel.SetActive(false);
        ScreenManager.ShowScreen<InitialScreen>();
    }

    IEnumerator LoseFinishRoutine()
    {
        hasHit = true;

        Phase = GamePhases.Lose;

        yield return new WaitForSeconds(1);
        ScreenManager.ShowScreen<FinishLoseScreen>();
        Debug.Log(UserDataManager.Money);
        UserDataManager.Money -= GrabbableItemManager.money;
        Debug.Log(UserDataManager.Money);
    }

    IEnumerator EnterHelicopterRoutine(Unit unit)
    {
        unit.MovementController.speed = 1;
        unit.AnimationController.AnimateJump();

        yield return new WaitForSeconds(5);
    }

    IEnumerator EruptionRoutine()
    {
        for (int i = 0; i < GrabbableItemManager.grabbableFuels.Count; i++)
        {
            var position = new Vector3(UnityEngine.Random.Range(-4, 4), UnityEngine.Random.Range(4, 6), 0);
            GrabbableItemManager.grabbableFuels[i].transform.DOLocalMove(position, 1f);
            GrabbableItemManager.grabbableFuels[i].transform.DOScale(0, 1.5f);
        }

        yield return new WaitForSeconds(2);
        GrabbableItemManager.grabbableFuels.Clear();
    }
}

public enum GamePhases
{
    Initial,
    Gameplay,
    Lose,
    Finish
}
