using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    public float speed;
    private Animator animator;
    private Rigidbody2D rb;
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    //The dog will go straight until it encounters something. If it does and explode, it will be destroyed
    void Update() {
        rb.AddForce(new Vector2(speed, 0));
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("HasExploded")) {
            Destroy(this.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        //If the dog collides with a wall or it's master, it will change direction
        if (other.gameObject.layer == 8 || other.gameObject.layer == gameObject.layer) {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            speed = -speed;
        }
        else {
            animator.SetBool("IsWalking", true);
        }
    }
}
