using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float maxHealth;
    public HealthBar healthBar;
    private Animator animator;
    public float curHealth;
    void Start()
    {
        animator = GetComponent<Animator>();
        maxHealth = GetComponent<Stats>().health;
    }

    public void DamagePlayer(float damage) {
            curHealth -= damage;
            healthBar.SetHealth(curHealth);
        if (curHealth <= 0) {
            Die();
        }
    }
    public void Die() {
        Debug.Log(this.name + " is dead");
        animator.SetBool("IsDead", true);
        LevelManager.instance.EndTurnPrep();
        this.enabled = false;
    }
}
