using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TextField = TMPro.TextMeshProUGUI;

namespace NorskaLib.GUI
{
    public class IconedLabel : MonoBehaviour
    {
        [SerializeField] Image icon;
        public Sprite IconSprite
        {
            set => icon.sprite = value;
        }
        public Color IconColor
        {
            set => icon.color = value;
        }

        [SerializeField] TextField label;
        public string LabelText
        {
            set => label.text = value;
        }
        public Color LabelColor
        {
            set => label.color = value;
        }

        Coroutine countAnimatioRoutine;

        public void AnimateCount(int a, int b, float duration)
        {
            if (countAnimatioRoutine != null)
                StopCoroutine(countAnimatioRoutine);

            countAnimatioRoutine = StartCoroutine(CountAnimatioRoutine(a, b, duration));
        }
        IEnumerator CountAnimatioRoutine(int a, int b, float duration)
        {
            var t = 0f;
        
            while (t < duration)
            {
                LabelText = Mathf.RoundToInt(Mathf.Lerp(a, b, t / duration)).ToString();
                yield return null;
                t += Time.deltaTime;
            }
            LabelText = b.ToString();
            countAnimatioRoutine = null;
        }
    }
}
