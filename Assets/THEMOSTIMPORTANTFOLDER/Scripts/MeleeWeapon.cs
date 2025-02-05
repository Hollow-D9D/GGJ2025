using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : MonoBehaviour
{
    [SerializeField] private int damage;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hit: " + other.name);
        other.GetComponent<HealthBase>().TakeDamage(damage);
    }
}
