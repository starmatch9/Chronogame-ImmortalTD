using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : MonoBehaviour
{
    //需要获取沼泽塔的敌人列表
    ZhaoTower t;

    //速度减为原来的百分之多少
    float slowFactor = 0.3f;

    [HideInInspector]
    public List<MudEnemy> missEnemies = new List<MudEnemy>();

    public void SetSlowFactor(float slowFactor)
    {
        this.slowFactor = slowFactor;
    }

    public void SetTower(ZhaoTower tower)
    {
        t = tower;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //只要是敌人就减速
        if (collision.CompareTag("Enemy"))
        {
            //加入敌人列表
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!t.enemyList.Contains(enemy))
            {
                t.enemyList.Add(enemy);
            }

            //处理受影响敌人  lambda表达式提供临时变量
            float effectForEnemy = 1f;
            MudEnemy foundEnemy = missEnemies.Find(missEnemy =>
                missEnemy != null &&
                enemy.gameObject.name.Contains(missEnemy.enemyPrefab.name));
            if (foundEnemy != null)
            {
                effectForEnemy = foundEnemy.swampEffect;
            }

            Move move = collision.GetComponent<Move>();
            if (move != null)
            {
                move.ChangeSpeed(slowFactor * effectForEnemy);

            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //只要是敌人，检测一下是否需要保持减速状态
        if (collision.CompareTag("Enemy"))
        {
            //加入敌人列表
            Enemy enemy = collision.GetComponent<Enemy>();
            if (!t.enemyList.Contains(enemy))
            {
                t.enemyList.Add(enemy);
            }

            //处理受影响敌人  lambda表达式提供临时变量
            float effectForEnemy = 1f;
            MudEnemy foundEnemy = missEnemies.Find(missEnemy =>
                missEnemy != null &&
                enemy.gameObject.name.Contains(missEnemy.enemyPrefab.name));
            if (foundEnemy != null)
            {
                effectForEnemy = foundEnemy.swampEffect;
            }

            Move move = collision.GetComponent<Move>();
            //如果恢复了原速，继续减速
            if(move.GetSpeed() == move.speed)
            {
                move.ChangeSpeed(slowFactor * effectForEnemy);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //只要是敌人就回到原来速度
        if (collision.CompareTag("Enemy"))
        {
            //加入敌人列表
            Enemy enemy = collision.GetComponent<Enemy>();
            if (t.enemyList.Contains(enemy))
            {
                t.enemyList.Remove(enemy);
            }


            Move move = collision.GetComponent<Move>();
            if (move != null)
            {
                move.ResetSpeed();
            }
        }
    }
}
