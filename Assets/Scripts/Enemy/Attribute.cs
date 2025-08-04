using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : MonoBehaviour, Enemy
{
    public int health = 100; //ÑªÁ¿Öµ

    public int GetHealth()
    {
        return health;
    }

    public void MinusHealth(int attack)
    {
        health -= attack;
        if (health <= 0)
        {
            Destroy(gameObject); //µÐÈËËÀÍö
        }
    }
}
