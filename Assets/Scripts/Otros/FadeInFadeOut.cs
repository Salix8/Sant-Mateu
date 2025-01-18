using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInFadeOut : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        
    }

    private void FadeIn()
    { animator.Play("FadeIn"); }

    private void FadeOut() 
    { animator.Play("FadeOut"); }
}
