using System.Collections;
using UnityEngine;

public class AlexAttacks : Attack
{
    // Alex's light distance attack : he'll fire a joke book
    public override void NormalMD()
    {
        base.NormalMD();
        bulletSprite = playerAttack.GetComponent<BulletSpriteHandler>().MDSprites[0];
        nbBulletToFire = 1;
        playerAttack.bounce = 0;
        attackDamage = 60;
    }
    //Alex's light distance attack when combo with double down input : he'll either summon a computer to crash his target or summon a dog
    public override void DownDownMD()
    {
        base.DownDownMD();
        System.Random rnd = new System.Random();
        int ind = rnd.Next(1,3);
        if (ind == 1) {
            bulletSprite = playerAttack.GetComponent<BulletSpriteHandler>().MDSprites[ind];
            nbBulletToFire = 1;
            playerAttack.bounce =0;
            attackDamage = 70;
        } else {
            nbBulletToFire = 0;
            invocation = true;
        }
    }
    //Alex's light distance attack when combo with front key input : he'll throw a joke book, but this time it'll bounce against the wall once
    public override void SpecialMD()
    {
        base.SpecialMD();
        bulletSprite = playerAttack.GetComponent<BulletSpriteHandler>().MDSprites[0];
        nbBulletToFire = 1;
        playerAttack.bounce = 1;
        attackDamage = 60;
    }

    //Alex's heavy distance attack : he's being to funny for the ennemy's mental health
    public override void NormalLD()
    {
        base.NormalLD();
        System.Random rnd = new System.Random();
        int ind = rnd.Next(0,3);
        bulletSprite = playerAttack.GetComponent<BulletSpriteHandler>().LDSprites[ind];
        nbBulletToFire = 1;
        playerAttack.bounce =0;
        attackDamage = 70;
    }
    //Alex's heavy attack when combo with front key input : he'll summon his yugioh trap card, better not walk in it
    public override void SpecialLD()
    {
        base.SpecialLD();
        invocation = true;
        invocationToSummon = 1;
    }
    //Alex's heavy distance attack when combo with double down input : he'll hack you, beware
    public override void DownDownLD()
    {
        Debug.Log("oui");
        base.DownDownLD();
        nbBulletToFire = 1;
        bulletSprite = playerAttack.GetComponent<BulletSpriteHandler>().LDSprites[0];
        hack = true;
    }
}

