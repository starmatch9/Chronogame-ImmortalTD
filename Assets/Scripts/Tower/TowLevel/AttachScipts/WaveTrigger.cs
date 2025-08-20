using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    //浪触发器，用来检测敌人来时的方向

    //连接浪塔
    LangTower langTower;

    //维护一个Vector3
    [HideInInspector]
    public Vector3 point;

    public void SetTower(LangTower l)
    {
        langTower = l;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //看敌人列表是否为空
            if (langTower.enemies.Count == 0)
            {
                //如果为空，设置firstPoint
                //langTower.firstPoint = point;
                //重新排序points列表，保证顺序正确
                langTower.ResortPoint(point);

            }

            if (!langTower.enemies.Contains(enemy))
            {
                langTower.enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            if (!langTower.enemies.Contains(enemy))
            {
                langTower.enemies.Add(enemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemy enemy = collision.GetComponent<Enemy>();

            //从列表中移除敌人
            if (langTower.enemies.Contains(enemy))
            {
                langTower.enemies.Remove(enemy);
            }
        }
    }
}
