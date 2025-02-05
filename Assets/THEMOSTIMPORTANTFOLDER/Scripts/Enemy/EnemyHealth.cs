using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBase
{
    private Animator _animator;
    private ResistText _resistText;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _resistText = GameObject.Find("Light").GetComponent<ResistText>();
        GlobalData.CurrentEnemyCount++;
    }

    public override void TakeDamage(int amount)
    {
        if (GetComponent<EnemyBehavior>().EnemyType != GlobalData.TimeState.TIME_STATE_DAY)
        {
            health -= amount;
            if (health <= 0) Die() ;
            else _animator.SetTrigger("Hit");
        }
        else
        {
            Resist();
        }
    }

    void Resist()
    {
        _resistText.Create(transform);
    }
    
    public override void Die()
    {
        GlobalData.CurrentEnemyCount--;
        _animator.SetTrigger("Death");
        Destroy(gameObject, 1);
    }
}
