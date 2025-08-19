using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
    //����������������������������б�
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

            //���б����Ƴ�����
            if (rongTower.enemies.Contains(enemy))
            {
                rongTower.enemies.Remove(enemy);
            }
        }
    }

}
