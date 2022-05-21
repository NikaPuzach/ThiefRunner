using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPoliceman : MonoBehaviour
{
    SceneController SceneController => SceneController.Instance;
    [SerializeField] UnitMovementController unit;

    private void FixedUpdate()
    {
        if (SceneController.Phase == GamePhases.Gameplay)
            unit.desiredDirection = transform.forward.normalized;
        else
            unit.desiredDirection = Vector3.zero;
    }
}
