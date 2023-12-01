using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decorator : MonoBehaviour
{
    [SerializeField] private Animator DarkLayerAnimator;

    private void Awake()
    {
        DarkLayerAnimator.SetTrigger("FadeOut");
    }
}
