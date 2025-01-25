using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour
{
    public int health = 100;

    public virtual void TakeDamage(int amount)
    {
        
    }

    public virtual void Die()
    {
        
    }
}
