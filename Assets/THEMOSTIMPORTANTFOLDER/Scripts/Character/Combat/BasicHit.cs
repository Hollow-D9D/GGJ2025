using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BasicHit : MonoBehaviour
{
    [SerializeField] private Collider2D hitBox;
    private bool canHit = true;

    private Animator _animator;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        hitBox.enabled = false;
    }

    void Cooldown()
    {
        canHit = true;
        hitBox.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && canHit)
        {
            hitBox.enabled = true;
            canHit = false;
            Invoke("Cooldown", 0.2f);
            _animator.SetTrigger("Attack" + (int)Random.Range(1, 4));
        }
    }
    
    
}
