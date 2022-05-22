using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberPlatform : MonoBehaviour
{
    [SerializeField] int goldMultiply;
    public int GoldMultiply => goldMultiply;

    [SerializeField] Transform helicopterTargetPont;
    public Transform HelicopterTargetPont => helicopterTargetPont;
}
