using System;
using UnityEngine;

[Serializable]
public class Character
{
    public GameObject prefab;

    public string name;
    public Sprite icon;
    public int score = 0;
    public HealthBar healthBar;
}
