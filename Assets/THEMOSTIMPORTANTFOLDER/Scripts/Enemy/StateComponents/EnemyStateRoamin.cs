using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStateRoamin : EnemyStateComponent
{
    [SerializeField] float RotationTime;
    private float _speed;
    private const float _rotationSpeed = 80f;
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
        ROTATING,
        WALKING,
    }

    private MovementState _movementState = MovementState.IDLE;

    float GetRandomValue() => Random.value * 3;

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
        _rigidbody2D.AddForce(transform.right * 80f);
        yield return new WaitForSeconds(GetRandomValue());
        _rigidbody2D.velocity *= 0f;
        yield return new WaitForSeconds(GetRandomValue());
        _movementState = MovementState.IDLE;
    }

    private IEnumerator ResolveRotating()
    {
        while (true)
        {
            yield return new WaitForSeconds(RotationTime);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, 20f, wallLayermask);
            if (hit.collider == null)
            {
                break;
            }
        }

        _rigidbody2D.angularVelocity = 0f;
        _movementState = MovementState.WALKING;
        StartCoroutine(ResolveWalking());
    }

    private void ResolveIdle()
    {
        RotationTime = GetRandomValue();
        _rigidbody2D.angularVelocity = _rotationSpeed;
        _movementState = MovementState.ROTATING;
        StartCoroutine(ResolveRotating());
    }
}
