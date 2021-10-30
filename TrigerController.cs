using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigerController : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        this.animator = this.GetComponent<Animator>();
    }

    public void ResertTriget(string TrigerName)
    {
        this.animator.ResetTrigger(TrigerName);
    }
}
