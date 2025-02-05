using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAggressive : EnemyStateComponent
{
    private float Speed = 5f;

    private Transform _player;
    private Rigidbody2D _rigidbody2D;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public override void OnFinish(ref EnemyBehavior.EnemyState inEnemyState)
    {
        _rigidbody2D.velocity *= 0f;
        
    }

    public override void Tick(ref EnemyBehavior.EnemyState inEnemyState)
    {
//        Debug.Log("Hellomoto");
        Vector3 dir = (_player.position - transform.position).normalized;
        _rigidbody2D.velocity = dir * Speed;
    }
}
