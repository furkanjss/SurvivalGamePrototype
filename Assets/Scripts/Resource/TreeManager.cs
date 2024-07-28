using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TreeManager : ResourceManager
{
   public override void TakeDamage(float damage)
   {
      base.TakeDamage(damage);
      transform.DOScale(1.1f, .1f).OnComplete(() =>
      {
         transform.DOScale(1, .1f);
      });
   }
}
