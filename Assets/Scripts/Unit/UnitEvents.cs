using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UnitEvents 
{
    public static Action<Unit, Finish> OnUnitEnteredFinish;
    public static Action<Unit, TrashConteiner> OnUnitHit;
    public static Action<Unit, NPCPoliceman> OnUnitCaught;
    public static Action<Unit, Obstacle> OnUnitSlowedDown;
    public static Action<Unit, Scooter> OnScooter;
    public static Action<Scooter> OnScooterLoose;
    public static Action<NumberPlatform> OnHelicopterEndFinish;
    public static Action<Unit, HelicopterMovementController> OnUnitEnterHelicopter;
    public static Action<GrabbableFuel> OnUnitGrabFuel;
    public static Action<GrabbableFuel> OnFuelsErupt;
    public static Action OnHelicopterReachedEndPoint;
}
