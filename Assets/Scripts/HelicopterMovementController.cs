using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class HelicopterMovementController : MonoBehaviour
{
    GrabbableItemManager GrabbableItemManager => GrabbableItemManager.Instance;

    [SerializeField] Transform arm;

    [Header("Route points")]
    [SerializeField] Transform takeOffPoint;
    [SerializeField] NumberPlatform[] platforms;

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<NumberPlatform>(out var number))
        {
            UnitEvents.OnHelicopterEndFinish.Invoke(number);
        }
    }

    private NumberPlatform GetTragetPosition(int fuelAmount)
    {
        return platforms.FirstOrDefault(p => p.GoldMultiply == fuelAmount)
            ?? platforms.First();
    }

    private void MovePropeller()
    {
        arm.DORotate(new Vector3(0, 180, 0), 0.1f, RotateMode.LocalAxisAdd).SetLoops(-1);
    }

    public void HelicopterFly()
    {
        MovePropeller();

        StartCoroutine(FlightRoutine(GrabbableItemManager.fuelAmount));
    }

    IEnumerator FlightRoutine(int fuelAmount)
    {
        // Time for gain height
        var takeOffDuration = 1f;

        // Time for flying over 1 platform is inversly interpolated
        var flightDurationMax = 1.3f;
        var flightDurationMin = 0.9f;
        var referencePlatformsCountMin = 3;
        var referencePlatformsCountMax = 5;
        //var flightEase = Ease.OutCirc;

        // Time for rotate sideway to the camera
        var finishDuration = 1f;

        transform.DORotate(Vector3.zero, takeOffDuration);
        transform.DOMove(takeOffPoint.position, takeOffDuration);

        yield return new WaitForSeconds(takeOffDuration);

        var targetPlatform = GetTragetPosition(fuelAmount);
        var platformsCount = targetPlatform.GoldMultiply;
        var lerpFactor = Mathf.InverseLerp(referencePlatformsCountMax, referencePlatformsCountMin, platformsCount);
        var duration = Mathf.Lerp(flightDurationMin, flightDurationMax, lerpFactor) * platformsCount;

        transform.DORotate(new Vector3(15, 0, 0), finishDuration);
        transform.DOMove(targetPlatform.HelicopterTargetPont.position, duration);

        yield return new WaitForSeconds(duration);

        transform.DORotate(new Vector3(0, 90, 0), finishDuration);

        UnitEvents.OnHelicopterReachedEndPoint?.Invoke();
    }
}
