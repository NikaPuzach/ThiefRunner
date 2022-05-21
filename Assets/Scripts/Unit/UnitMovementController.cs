using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovementController : MonoBehaviour
{
    public float speedMax;
    [SerializeField] float acceleration;

    [HideInInspector]
    public float speed;

    private bool isMoving;
    public bool IsMoving => isMoving;

    [HideInInspector]
    public Vector3 desiredDirection;

    void FixedUpdate()
    {
        if(desiredDirection != Vector3.zero)
        {
            if (speed < speedMax)
                speed += acceleration * Time.fixedDeltaTime;

            transform.position += desiredDirection * speed * Time.fixedDeltaTime;

            transform.rotation = Quaternion.LookRotation(desiredDirection, Vector3.up);

            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
    }
}
