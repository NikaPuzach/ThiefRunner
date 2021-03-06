using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NorskaLib.Utilities;
using Random = UnityEngine.Random;
using UnityEngine.AI;
using DG.Tweening;
using TMPro;

public class NPCAI : MonoBehaviour
{
    [SerializeField] Unit unit;
    [SerializeField] float stopDistance;

    World World => World.Instance;

    float exploringTimer;
    float exploringTime = 5;

    Vector3 position;
    bool hasArrived;

    private void Update()
    {
        exploringTimer += Time.deltaTime;

        if (exploringTimer >= exploringTime)
        {
            hasArrived = false;
            position = SetRandomDestination();
            exploringTimer = 0;
        }

        if (!hasArrived)
        {
            var direction = position - unit.transform.position;
            hasArrived = direction.magnitude <= stopDistance;
            unit.MovementController.desiredDirection = hasArrived
                ? Vector3.zero
                : direction.normalized;
        }
    }

    private Vector3 SetRandomDestination()
    {
        int[] randomAngle = { 0, 90, 180, 360 };

        var r = Random.Range(0.5f, 3);
        var angle = Random.Range(0, randomAngle.Length);
        var projection = World.Project(transform.position);

        return MathUtils.PositionOnCircle3D(projection, randomAngle[angle], r);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<FieldOfView>(out var player))
        {
            var npc = this.GetComponent<Unit>();
            npc.AnimationController.AnimateAnger();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, position);
    }
}