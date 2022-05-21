using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MovementController = UnitMovementController;

public class Unit : MonoBehaviour
{
	[SerializeField] MovementController movementController;
    public MovementController MovementController => movementController;
	[SerializeField] UnitAnimationController animationController;
    public UnitAnimationController AnimationController => animationController;

    SceneController SceneController => SceneController.Instance;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Obstacle>(out var obstacle))
        {
            UnitEvents.OnUnitSlowedDown?.Invoke(this, obstacle);
        }

        if (other.TryGetComponent<Finish>(out var finish))
        {
            UnitEvents.OnUnitEnteredFinish?.Invoke(this, finish);
        }

        if(other.TryGetComponent<TrashConteiner>(out var trash))
        {
            if (SceneController.hasHit)
                return;

            UnitEvents.OnUnitHit?.Invoke(this, trash);
        }

        if(other.TryGetComponent<NPCPoliceman>(out var policeman))
        {
            UnitEvents.OnUnitCaught?.Invoke(this, policeman);
        }

        if(other.TryGetComponent<Scooter>(out var scooter))
        {
            UnitEvents.OnScooter?.Invoke(this, scooter);
        }

        if(other.TryGetComponent<HelicopterMovementController>(out var helicopter))
        {
            UnitEvents.OnUnitEnterHelicopter?.Invoke(this, helicopter);
        }
    }
}