using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HeroHealth : HealthBase
{
    [SerializeField] private Image HealthImage;
    private int maxHealth;
    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }

        HealthImage.fillAmount = (float)health / (float)maxHealth;
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public override void Die()
    {
        Invoke("Restart", 3);
    }

    private void Start()
    {
        maxHealth = health;
        HealthImage.fillAmount = (float)health / (float)maxHealth;
    }
}
