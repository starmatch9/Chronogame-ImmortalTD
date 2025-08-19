using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //连接熔岩塔，由熔岩塔管理敌人列表
    RongTower rongTower;

    public void SetTower(RongTower r)
    {
        rongTower = r;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!rongTower.enemies.Contains(enemy))
            {
                rongTower.enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!rongTower.enemies.Contains(enemy))
            {
                rongTower.enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //从列表中移除敌人
            if (rongTower.enemies.Contains(enemy))
            {
                rongTower.enemies.Remove(enemy);
            }
        }
    }

}
