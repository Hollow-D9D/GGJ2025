using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStateChillin : EnemyStateComponent
{
    public override void OnStart(ref EnemyBehavior.EnemyState inEnemyState)
    {
        _animator.SetBool("Chill", true); 
    }

    public override void OnFinish(ref EnemyBehavior.EnemyState inEnemyState)
    {
        _animator.SetBool("Chill", false);
    }
    public override void Tick(ref EnemyBehavior.EnemyState inEnemyState)
    {
    }

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

}
