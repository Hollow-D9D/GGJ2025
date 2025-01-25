using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    private void Start()
    {
        GlobalData.CurrentEnemyCount++;
    }

    public override void TakeDamage(int amount)
    {
        if (GetComponent<EnemyBehavior>().EnemyType != GlobalData.CurrentTimeState)
        {
            Die();
        }
    }

    public override void Die()
    {
        GlobalData.CurrentEnemyCount--;
        Destroy(gameObject);
    }
}
