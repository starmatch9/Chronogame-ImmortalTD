using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drill : MonoBehaviour
{
    float attack = 30f; //钻头移动速度

    public void SetAttack(float attack)
    {
        this.attack = attack;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //只要是敌人就开干
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().AcceptAttack(attack);
        }
    }
}
