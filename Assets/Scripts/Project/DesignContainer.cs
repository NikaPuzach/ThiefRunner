using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DesignContainer", menuName = "Custom/DesignContainer")]
public class DesignContainer : ScriptableObject
{
    [System.Serializable]
    public class ColorsContainer
    {
        [FoldoutGroup("Common"), ColorUsage(showAlpha: true)]
        public Color
            Text_Generic = Color.white,
            Text_Error = Color.red;
    }

    [SerializeField] ColorsContainer colors;
    public ColorsContainer Colors => colors;
}