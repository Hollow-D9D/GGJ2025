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
    private Animator _animator;
    public override void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
        else _animator.SetTrigger("Hit");

        HealthImage.fillAmount = (float)health / (float)maxHealth;
    }

    void Restart()
    {
        SceneManager.LoadScene(0);
    }
    
    public override void Die()
    {
        Invoke("Restart", 3);
        _animator.SetTrigger("Death");
    }

    private void Start()
    {
        maxHealth = health;
        HealthImage.fillAmount = (float)health / (float)maxHealth;
        _animator = GetComponent<Animator>();
    }
}
