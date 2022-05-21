using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class ArmController : MonoBehaviour
{
	 public Rig scooter;
	[SerializeField] Rig rigRight;
	[SerializeField] Rig rigLeft;
	[SerializeField] TwoBoneIKConstraint rightArm;
	[SerializeField] TwoBoneIKConstraint leftArm;
	[SerializeField] FieldOfView FieldOfView;


    private void Start()
    {
		scooter.weight = 0;
    }

    private void LateUpdate()
	{
		rightArm.data.target = FieldOfView.PrimaryTargetRight;
        leftArm.data.target = FieldOfView.PrimaryTargetLeft;

		rigRight.weight = FieldOfView.PrimaryTargetRight == null ? 0 : 1;
        leftArm.weight = FieldOfView.PrimaryTargetLeft == null ? 0 : 1;
    }
}