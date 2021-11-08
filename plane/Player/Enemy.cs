using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Chacter
{
   public int deathEnergeBouns = 3;

   public override void Die()
   {
      PlayerEnery.instance.Obtain(deathEnergeBouns);
      base.Die();
      
   }
}
