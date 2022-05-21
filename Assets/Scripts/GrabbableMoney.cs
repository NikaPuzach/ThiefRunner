using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableMoney : MonoBehaviour
{
    private int bucksAmount;
    public int BucksAmount => bucksAmount;

    private void Start()
    {
        bucksAmount = Random.Range(5, 15);
    }
}
