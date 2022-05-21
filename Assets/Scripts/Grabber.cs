using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabber : MonoBehaviour
{
    UserDataManager UserDataManager => UserDataManager.Instance;
    GrabbableItemManager GrabbableItemManager => GrabbableItemManager.Instance;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<GrabbableMoney>(out var money))
        {
            UserDataManager.Money += money.BucksAmount;
            GrabbableItemManager.money += money.BucksAmount;
            Vibration.Vibrate(200, 1);
        }

        if(other.TryGetComponent<GrabbableFuel>(out var fuel))
        {
            GrabbableItemManager.fuelAmount += fuel.FuelAmount;
            UnitEvents.OnUnitGrabFuel?.Invoke(fuel);
        }
    }
}
