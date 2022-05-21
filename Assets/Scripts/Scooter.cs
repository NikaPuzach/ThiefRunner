using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scooter : MonoBehaviour
{
    public Canvas canvas;
    public Transform standPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Obstacle>(out var obstacle) ||
           other.TryGetComponent<Finish>(out var finish) ||
           other.TryGetComponent<TrashConteiner>(out var trashConteiner) ||
           other.TryGetComponent<NPCPoliceman>(out var nPCPoliceman) ||
           other.TryGetComponent<HelicopterMovementController>(out var helicopter))
        {
            UnitEvents.OnScooterLoose?.Invoke(this);
        }
    }
}
