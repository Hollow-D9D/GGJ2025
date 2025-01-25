using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicHit : MonoBehaviour
{
    [SerializeField] private Collider2D hitBox;
    private bool canHit = true;
    private void Start()
    {
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
            Debug.Log("uau");
            hitBox.enabled = true;
            canHit = false;
            Invoke("Cooldown", 3);
        }
    }
    
    
}
