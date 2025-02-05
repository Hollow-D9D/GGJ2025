using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStateRoamin : EnemyStateComponent
{
    [SerializeField] float RotationTime;
    private float _speed = 5f;
    private Rigidbody2D _rigidbody2D;
    
    [SerializeField] private LayerMask wallLayermask;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _rigidbody2D.gravityScale = 0;
    }

    enum MovementState
    {
        IDLE,
        WALKING
    }

    private MovementState _movementState = MovementState.IDLE;

    float GetRandomValue() => (Random.value * 6) - 3;

    public override void OnFinish(ref EnemyBehavior.EnemyState inEnemyState)
    {
        _rigidbody2D.angularVelocity = 0;
        _rigidbody2D.velocity *= 0;
        _movementState = MovementState.IDLE;
        StopAllCoroutines();
    }

    public override void Tick(ref EnemyBehavior.EnemyState inEnemyState)
    {
        // Debug.Log("Roamin");
        switch (_movementState)
        {
            case MovementState.IDLE:
                ResolveIdle();
                break;
        }
    }

    private IEnumerator ResolveWalking()
    {
        _rigidbody2D.velocity *= 0f;
        yield return new WaitForSeconds(RotationTime);
        _movementState = MovementState.IDLE;
    }

    private IEnumerator DetermineDirection()
    {
        _movementState = MovementState.WALKING;
        while (true)
        {
            Vector2 dir = new Vector2(GetRandomValue(), GetRandomValue()).normalized * _speed;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, 20f, wallLayermask);
            _rigidbody2D.velocity = dir;
            
            transform.eulerAngles = new Vector3(0,  dir.x < 0 ? 180f : 0, 0);

            if (!hit.collider)
            {
                break;
            }
            yield return new WaitForSeconds(RotationTime);
        }

        StartCoroutine(ResolveWalking());
    }

    private void ResolveIdle()
    {
        StartCoroutine(DetermineDirection());
    }
}
