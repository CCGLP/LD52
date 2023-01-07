using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening; 

public class TiltingText : MonoBehaviour
{
    [SerializeField]
    protected float animateTime = 0.4f, waitTime = 0.2f;
    protected TextMeshProUGUI text;
    protected float alpha = 1; 
    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
        AnimationRoutine(); 
        
    }

    private void AnimationRoutine()
    {
        alpha = alpha > 0.1f ? 0 : 1;
        text.DOFade(alpha, animateTime).OnComplete(() =>
        {
            if (alpha > 0.1f)
            {
                DOVirtual.DelayedCall(waitTime, () =>
                {
                    AnimationRoutine();
                });

            }
            else
            {
                AnimationRoutine(); 
            }
        });
    }

    void Update()
    {
        
    }
}
