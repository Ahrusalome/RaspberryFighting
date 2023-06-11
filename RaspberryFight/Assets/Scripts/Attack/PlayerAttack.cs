using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //The location where projectiles will be instantiate;
    public Transform[] firePoints;
    //Prefabs to spawn
    public GameObject bulletPrefab;
    public GameObject[] invocationPrefab;
    // Boolean that will alterate some attacks if true
    public bool downDown = false;
    // Int that will be passed to the "bullet" script so it knows how many times it's supposed to bounce against walls
    public int bounce = 0;
    private Animator animator;
    // The script that contains all the different attack behaviours
    private Attack attackScript;
    private string playerName;
    //Bool used to set a cooldown beetween attacks
    private bool isReady = true;
    void Start()
    {
        //Check the player's name to know whose attacks it'll have to launch
        playerName = gameObject.name;
        animator = GetComponent<Animator>();
        switch(playerName) {
            case "Alex(Clone)":
                attackScript = new AlexAttacks();
                break;
            case "Paul(Clone)":
                attackScript = new PaulAttacks();
                break;
        }
        attackScript.Start(this,animator);
    }

    //If the player do 2 times the down input and an attack input soon after, will change the behaviour of the attacks
    void OnDownDownAttack() {
        downDown = true;
        StartCoroutine("CoolDown", 0.1f);
    }

    //Reset the "down down" change if the player don't attack soon enough
    IEnumerator CoolDown(float timeToWait) {
        yield return new WaitForSeconds(timeToWait);
        downDown = false;
        isReady = true;
    }

    //Launch the light ranged attack
    void OnMDAttack() {
        if (isReady && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsHacked")) {
            attackScript.OnMDAttack();
            // if the player's light ranged attack has to instanciate a projectile, it will
            if (attackScript.nbBulletToFire > 0 && isReady) {
                for(int i = 0; i< attackScript.nbBulletToFire; i++) {
                    InstantiateProjectile(attackScript.bulletDirection[i], firePoints[i]);
                }
            }
            // if the player's light ranged attack has to instanciate an invocation, it will
            if (attackScript.invocation) {
                Invoke(attackScript.invocationToSummon);
            }
            isReady = false;
            StartCoroutine("CoolDown", attackScript.recuperationTime*1.1/gameObject.GetComponent<Stats>().dexterity);
        }
    }

    //Launch the heavy ranged attack
    void OnLDAttack() {
        if(isReady && !animator.GetCurrentAnimatorStateInfo(0).IsName("IsHacked")) {
            attackScript.OnLDAttack();
            if (attackScript.nbBulletToFire > 0 &&isReady) {
                for(int i = 0; i< attackScript.nbBulletToFire; i++) {
                    InstantiateProjectile(attackScript.bulletDirection[i], firePoints[i]);
                }
            }
            if (attackScript.invocation) {
                Invoke(attackScript.invocationToSummon);
            }
            isReady = false;
            StartCoroutine("CoolDown", attackScript.recuperationTime*1.3/gameObject.GetComponent<Stats>().dexterity);
        }
    }
    void InstantiateProjectile(Vector2 direction, Transform firePoint) {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<SpriteRenderer>().sprite = attackScript.bulletSprite;
        bullet.layer = gameObject.layer;
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.bounce = bounce;
        bulletScript.damage = attackScript.attackDamage;
        bullet.GetComponent<Stats>().attack = GetComponent<Stats>().attack;
        bulletScript.speed = GetComponent<Stats>().ShotSpeed;
        bulletScript.Launch(direction);
        bulletScript.hack = attackScript.hack;
        attackScript.hack = false;
    }
    void Invoke(int invocationIndex) {
        GameObject invocationGO = Instantiate(invocationPrefab[invocationIndex], firePoints[0].position, Quaternion.identity);
        invocationGO.layer = this.gameObject.layer;
        attackScript.invocation = false;
        attackScript.invocationToSummon = 0;
    }
}
