using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAttackin : EnemyStateComponent
{
   [SerializeField] private Collider2D weaponHitbox;
   [SerializeField] private float hitTime = 2f;

   private bool IsHitting;

   private void Start()
   {
      IsHitting = false;
      weaponHitbox.gameObject.SetActive(IsHitting);
   }

   public override void Tick(ref EnemyBehavior.EnemyState inEnemyState)
   {
      if (!IsHitting)
      {
         
         StartCoroutine(Attack());
      }
   }

   public override void OnFinish(ref EnemyBehavior.EnemyState inEnemyState)
   {
      StopAllCoroutines();
      IsHitting = false;
      weaponHitbox.gameObject.SetActive(IsHitting);
   }

   private IEnumerator Attack()
   {
      yield return new WaitForSeconds(hitTime);
      IsHitting = true;
      weaponHitbox.gameObject.SetActive(IsHitting);
      yield return new WaitForSeconds(hitTime);
      weaponHitbox.gameObject.SetActive(false);
      yield return new WaitForFixedUpdate();
      IsHitting = false;
   }
}
