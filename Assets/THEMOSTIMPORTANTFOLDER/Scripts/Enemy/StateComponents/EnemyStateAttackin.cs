using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStateAttackin : EnemyStateComponent
{
   [SerializeField] private Collider2D weaponHitbox;
   [SerializeField] private float hitTime = .5f;

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
      yield return new WaitForSeconds(0.3f);
      _animator.SetTrigger("Attack" + Random.Range(1,2));
      IsHitting = true;
      weaponHitbox.gameObject.SetActive(IsHitting);
      yield return new WaitForSeconds(hitTime);
      weaponHitbox.gameObject.SetActive(false);
      IsHitting = false;
   }
}
