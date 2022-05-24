using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UIAnimation : MonoBehaviour
{
    [SerializeField] float scale;

    void Start()
    {
        transform.DOScale(scale, 1).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
    }
}
