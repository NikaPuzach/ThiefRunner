using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NorskaLib.Utilities;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	public float viewRadius;

	[Range(0, 360)]
	public float viewAngle;

	public LayerMask targetMask;
	public LayerMask obstacleMask;

	public List<Transform> visibleTargetsRight = new List<Transform>();
	public Transform PrimaryTargetRight
		=> visibleTargetsRight.FirstOrDefault();

	public List<Transform> visibleTargetsLeft = new List<Transform>();
	public Transform PrimaryTargetLeft
		=> visibleTargetsLeft.FirstOrDefault();

	void Update()
    {
		UpdateVisibleTargets();
	}

	void UpdateVisibleTargets()
    {
		visibleTargetsRight.Clear();
		visibleTargetsLeft.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);

        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
			var target = targetsInViewRadius[i].transform;

			if (Physics.Linecast(transform.position, target.position, obstacleMask))
				continue;

			var angle = MathUtils.RelativeSignedAngleXZ(transform, target.position);

			if(Mathf.Abs(angle) > viewAngle /2 )
				return;

			var targetList = angle < 0? visibleTargetsRight : visibleTargetsLeft;

			targetList.Add(target);

			var targetDistance = Vector3.Distance(transform.position, target.position);
		}

		visibleTargetsRight = visibleTargetsRight.OrderBy(t => Vector3.Distance(transform.position, t.position)).ToList();
		visibleTargetsLeft= visibleTargetsLeft.OrderBy(t => Vector3.Distance(transform.position, t.position)).ToList();
	}

	public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
        if (!angleIsGlobal)
        {
			angleInDegrees += transform.eulerAngles.y;
        }

		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

}