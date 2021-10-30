using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
   private Animator animator;
   private void Awake()
   {
      this.animator = this.GetComponent<Animator>();
   }

   public void OnAnimatorMove()
   {
      SendMessageUpwards("OnUpDateRootMotion",(object)this.animator.deltaPosition);
   }
}
