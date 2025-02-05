using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateComponent : MonoBehaviour
{
    public Animator _animator;

    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public virtual void Tick(ref EnemyBehavior.EnemyState inEnemyState)
    {
        throw new System.NotImplementedException();
    }

    public virtual void OnStart(ref EnemyBehavior.EnemyState inEnemyState)
    {
        
    }
    
    public virtual void OnFinish(ref EnemyBehavior.EnemyState inEnemyState)
    {
        
    }
}
