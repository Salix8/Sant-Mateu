using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransiciónMenus : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {

        animator = GetComponent<Animator>();

    }

    public void MenuIn()
    { animator.Play("MenuIn"); }

    public void MenuOut()
    { animator.Play("MenuOut"); }
}