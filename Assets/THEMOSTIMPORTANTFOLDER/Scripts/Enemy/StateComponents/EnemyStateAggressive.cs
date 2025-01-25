using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateAggressive : EnemyStateComponent
{
    [SerializeField] private float Speed = 1f;

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
        transform.rotation = Quaternion.Euler(0,0, (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg ));
    }
}
