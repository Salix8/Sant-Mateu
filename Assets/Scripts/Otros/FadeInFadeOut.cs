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

    public void FadeIn()
    { animator.Play("FadeIn"); }

    public void FadeOut() 
    { animator.Play("FadeOut"); }
}
