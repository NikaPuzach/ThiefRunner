using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class GrabbableItemManager : MonoBehaviour
{
    public static GrabbableItemManager Instance { get; private set; }
    [SerializeField] Transform player;
    public List<GrabbableFuel> grabbableFuels = new List<GrabbableFuel>();

    public int money;
    public int fuelAmount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        money = 0;
        fuelAmount = 0;
        UnitEvents.OnUnitGrabFuel += OnUnitGrabFuel;
    }

    private void OnDestroy()
    {
        UnitEvents.OnUnitGrabFuel -= OnUnitGrabFuel;
    }

    private void OnUnitGrabFuel(GrabbableFuel fuel)
    {
        grabbableFuels.Add(fuel);

        grabbableFuels.Last().transform.SetParent(player.transform);
        grabbableFuels.Last().transform.position = Vector3.zero;
        grabbableFuels.Last().transform.rotation = Quaternion.Euler(90, 0, 90);

        if (grabbableFuels.Count == 1)
        {
            grabbableFuels.Last().transform.position = new Vector3(
                            player.position.x,
                            player.position.y + 1,
                            player.position.z + 0.5f);
        }
        else
        {
            var previous = grabbableFuels.FindIndex(i => i == grabbableFuels.Last()) - 1;
            grabbableFuels.Last().transform.rotation = grabbableFuels[previous].transform.rotation;
            grabbableFuels.Last().transform.position = new Vector3(
                grabbableFuels[previous].transform.position.x,
                grabbableFuels[previous].transform.position.y+ 0.2f,
                grabbableFuels[previous].transform.position.z
                );
        }

    }
}
