using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HelicopterMovementController : MonoBehaviour
{
    GrabbableItemManager GrabbableItemManager => GrabbableItemManager.Instance;

    [SerializeField] Transform arm;

    private void MovePropeller()
    {
        arm.DORotate(new Vector3(0, 360, 0), 0.1f).SetLoops(-1);
    }

    public void HelicopterFly()
    {
        MovePropeller();
        transform.DORotate(Vector3.zero, 1);
        transform.DOLocalMove(new Vector3
            (transform.position.x,
            transform.position.y + 1,
            transform.position.z + GrabbableItemManager.fuelAmount * 3),
            GrabbableItemManager.fuelAmount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<NumberPlatform>(out var number))
        {
            UnitEvents.OnHelicopterEndFinish.Invoke(number);
        }
    }
}
