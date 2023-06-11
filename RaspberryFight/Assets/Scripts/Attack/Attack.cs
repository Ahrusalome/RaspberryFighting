using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The Attack class is an abstract class that defines the basic properties and methods for player
attacks in a game. */
public abstract class Attack
{
    //Attack script attached to the GameObject
    public PlayerAttack playerAttack;
    public Vector3[] bulletDirection = new Vector3[] {new Vector3(0,0,0)};
    protected Animator animator;
    public Sprite bulletSprite;
    public int invocationToSummon = 0;
    public float cooldown;
    public int nbBulletToFire = 0;
    public bool invocation = false;
    public int attackDamage;
    //Time to wait between 2 attacks (melee attacks not included)
    public int recuperationTime = 10;
    //Alex's hacking capacity
    public bool hack = false;
    //Retrieve the attack script and animator from the gameobject
    public void Start(PlayerAttack _playerAttack, Animator _animator) {
        playerAttack = _playerAttack;
        animator = _animator;
    }
    /*When the player uses the MD input, it's script will launch this method.
    This method will then redirect to which type of MD attack the character will proceed*/
    public void OnMDAttack() {
        if (playerAttack.GetComponent<PlayerController>().frontSpecialAttack) {
            SpecialMD();
        } else if (playerAttack.downDown) {
            DownDownMD();
        } else{
            NormalMD();
        }
    }
    public virtual void SpecialMD() {
        animator.SetTrigger("SpecialMD");
    }
    public virtual void DownDownMD() {
        animator.SetTrigger("DownDownMD");
    }
    public virtual void NormalMD() {
        animator.SetTrigger("MDAttacking");
    }
    /*When the player uses the LD input, it's script will launch this method.
    This method will then redirect to which type of LD attack the character will proceed*/
    public void OnLDAttack() {
        if (playerAttack.GetComponent<PlayerController>().frontSpecialAttack) {
            SpecialLD();
        } else if (playerAttack.downDown) {
            DownDownLD();
        } else{
            NormalLD();
        }
    }
    public virtual void SpecialLD() {
        animator.SetTrigger("SpecialLD");
    }
    public virtual void DownDownLD() {
        animator.SetTrigger("DownDownLD");
    }
    public virtual void NormalLD() {
        animator.SetTrigger("LDAttacking");
    }
}
