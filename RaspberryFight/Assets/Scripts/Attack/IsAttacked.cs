using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsAttacked : MonoBehaviour
{
    public LayerMask mask;
    private Animator animator;
    private HealthManager health;
    private int combo;

    void Start() {
        // Set the hurtbox's layer the same as the character's layer
        gameObject.layer = gameObject.transform.parent.gameObject.layer;
        // Set the hitbox's layer the same as the character's layer
        gameObject.transform.parent.gameObject.transform.Find("hitbox").gameObject.layer = gameObject.transform.parent.gameObject.layer;
        // Set mask as the ennemy's layer
        if (gameObject.layer == 6) {
            mask = LayerMask.GetMask("Player2");
        } else {
            mask = LayerMask.GetMask("Player1");
        }
        animator = GetComponentInParent<Animator>();
        health = this.GetComponentInParent<HealthManager>();
    }
    void Update() {
        // Reset the combo's counter if the character has is not being hurt
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName("IsAttacked")) {
            combo = 0;
        }
    }

    //Take damage when an hitbox with ennemy's layer trigger the hurtbox
    void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & mask) != 0 && (other.name == "hitbox" || other.name == "Bullet(Clone)" || other.name == "Dog(Clone)") && !gameObject.GetComponentInParent<PlayerController>().isGuarding) {
            health.DamagePlayer(DamageToTake(other));
            animator.SetTrigger("IsAttacked");
            combo++;
        }
    }

    //Function to calcul the damage intake
    float DamageToTake(Collider2D other) {
        float baseDamage = 0;
        AnimatorStateInfo ennemiAnimatorState = other.gameObject.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0);
        if(ennemiAnimatorState.IsName("SDAttack 1")) {
            baseDamage = 50;
        } else if(ennemiAnimatorState.IsName("SDAttack 2")) {
            baseDamage = 55;
        } else if(ennemiAnimatorState.IsName("SDAttack 3") || ennemiAnimatorState.IsName("MDAttack")) {
            baseDamage = 60;
        } else if(ennemiAnimatorState.IsName("LDAttack") || other.gameObject.name == "Dog(Clone)") {
            baseDamage = 70;
        } else if (other.gameObject.name == "Bullet(Clone)") {
            baseDamage = other.gameObject.GetComponent<Bullet>().damage;
            if (other.gameObject.GetComponent<Bullet>().hack) {
                animator.SetTrigger("IsHacked");
            }
        }
        float damage = ((baseDamage * (1+other.GetComponentInParent<Stats>().attack/10f) ) * (1 + combo/10f)) / (1+GetComponentInParent<Stats>().defense/10f);
        return damage;
    }
}
