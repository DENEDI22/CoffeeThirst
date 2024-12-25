using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenAnimationScaler : MonoBehaviour
{
    [SerializeField] private Animator chickenAnimator;
    [SerializeField] private ChickenNavigation chickenNavigation;

    private void OnValidate()
    {
        chickenAnimator = GetComponent<Animator>();
        chickenNavigation = GetComponent<ChickenNavigation>();
    }

    private void FixedUpdate()
    {
        if (chickenNavigation.GetCurrentState != ChickenStates.Patroling)
        {
            chickenAnimator.SetFloat("AnimationSpeed", 4);
        }
        else
        {
            chickenAnimator.SetFloat("AnimationSpeed", 1);
        }
    }
}
