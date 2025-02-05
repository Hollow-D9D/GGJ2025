using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : Bubblable
{
    Transform _player;
    [SerializeField] private float VisibilityDistance = 10f;
    private float AttackDistance = 1.5f;
    private Animator _animator;
    private Rigidbody2D _rb;
    public LayerMask IgnoreSelfLayer;
    
    public enum EnemyState
    {
        ENEMY_STATE_INVALID = -1,
        ENEMY_STATE_CHILLIN,
        ENEMY_STATE_ROAMIN,
        ENEMY_STATE_AGGRESSIVE,
        ENEMY_STATE_ATTACKIN,
        ENEMY_STATE_COUNT
    }

    [SerializeField] private EnemyStateComponent[] EnemyStateComponents = new EnemyStateComponent[(int)EnemyState.ENEMY_STATE_COUNT]; 
    
    public GlobalData.TimeState EnemyType;
    private EnemyState CurrentEnemyState;
    private EnemyState OldEnemyState;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        EnemyType = GlobalData.TimeState.TIME_STATE_DAY;
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        EvaluateEnemyState();
        OldEnemyState = CurrentEnemyState;
    }

    private void EvaluateEnemyState()
    {
        OldEnemyState = CurrentEnemyState;
        if (EnemyType == GlobalData.TimeState.TIME_STATE_DAY)
        {
            if (Vector3.Distance(_player.position, transform.position) < VisibilityDistance)
            {
                RaycastHit2D result = Physics2D.CircleCast(transform.position, 5f, transform.right, VisibilityDistance, ~IgnoreSelfLayer);
                if (result.collider && result.collider.CompareTag("Player"))
                {
                    if (Vector3.Distance(_player.position, transform.position) < AttackDistance)
                    {
                        CurrentEnemyState = EnemyState.ENEMY_STATE_ATTACKIN;
                    }
                    else
                    {
                        CurrentEnemyState = EnemyState.ENEMY_STATE_AGGRESSIVE;
                    }
                }
                else
                {
                    CurrentEnemyState = EnemyState.ENEMY_STATE_ROAMIN;
                }
            }
            else
            {
                CurrentEnemyState = EnemyState.ENEMY_STATE_ROAMIN;
            }
        }
        else
        {
            CurrentEnemyState = EnemyState.ENEMY_STATE_CHILLIN;
        }
    }
    
    private void Update()
    {
        _animator.SetBool("Run", _rb.velocity.magnitude > 0.2f);
        EvaluateEnemyState();
        if (OldEnemyState != CurrentEnemyState)
        {
            EnemyStateComponents[(int)OldEnemyState].OnFinish(ref CurrentEnemyState);
            EnemyStateComponents[(int)CurrentEnemyState].OnStart(ref CurrentEnemyState);
        }
        EnemyStateComponents[(int)CurrentEnemyState].Tick(ref CurrentEnemyState);
    }

    void MakeUnimpotent()
    {
        EnemyType = GlobalData.TimeState.TIME_STATE_DAY;
    }

    public override void Bubbled(float debuffDuration)
    {
        MakeImpotent(debuffDuration);
    }
    
    public void MakeImpotent(float debuffDuration)
    {
        Debug.Log(transform.name + ": Impotent");
        EnemyType = GlobalData.TimeState.TIME_STATE_NIGHT;
        Invoke("MakeUnimpotent", debuffDuration);
    }
}
