using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnemyBehavior : MonoBehaviour
{
    Transform _player;
    [SerializeField] private float VisibilityDistance = 10f;
    private float AttackDistance = 1.5f;
    
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
    
    
    public GlobalData.TimeState EnemyType = GlobalData.TimeState.TIME_STATE_DAY;
    private EnemyState CurrentEnemyState;
    private EnemyState OldEnemyState;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        EvaluateEnemyState();
        OldEnemyState = CurrentEnemyState;
    }

    private void EvaluateEnemyState()
    {
        OldEnemyState = CurrentEnemyState;
        if (EnemyType == GlobalData.CurrentTimeState)
        {
            if (Vector3.Distance(_player.position, transform.position) < VisibilityDistance)
            {
                RaycastHit2D result = Physics2D.Raycast(transform.position, transform.right, VisibilityDistance, ~IgnoreSelfLayer);
                Debug.Log(result.collider?.name);
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
        EvaluateEnemyState();
        if (OldEnemyState != CurrentEnemyState)
        {
            EnemyStateComponents[(int)OldEnemyState].OnFinish(ref CurrentEnemyState);
            EnemyStateComponents[(int)CurrentEnemyState].OnStart(ref CurrentEnemyState);
        }
        EnemyStateComponents[(int)CurrentEnemyState].Tick(ref CurrentEnemyState);
    }
    
}
