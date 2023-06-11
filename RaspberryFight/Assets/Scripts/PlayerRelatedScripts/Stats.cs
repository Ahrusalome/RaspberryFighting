using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;
using Firebase.Extensions;

public class Stats : MonoBehaviour
{
    private FirebaseFirestore dbreference;
    public float attack;
    public float defense;
    public float speed;
    public float dexterity;
    public float health;
    public float ShotSpeed; // average = 20f
    Query userQuery;
    async public void SetStats() {
        dbreference = FirebaseFirestore.DefaultInstance;
        if (gameObject.layer == 6) {
            userQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true).WhereEqualTo("PlayerNb", 1);
        } else {
            userQuery = dbreference.Collection("Users").WhereEqualTo("isPlaying", true).WhereEqualTo("PlayerNb", 2);
        }
        QuerySnapshot userQuerySnapshot = await userQuery.GetSnapshotAsync();
        foreach (DocumentSnapshot user in userQuerySnapshot)
        {
            User _user = user.ConvertTo<User>();
            attack = _user.Attack;
            speed = _user.MoveSpeed;
            dexterity = _user.Dexterity;
            health = _user.Health;
            ShotSpeed = _user.ShotSpeed;
        }
    }
}