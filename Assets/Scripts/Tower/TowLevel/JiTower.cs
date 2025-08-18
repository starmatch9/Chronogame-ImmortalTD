using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JiTower : Tower
{
    //    * 荆棘塔 *

    //候选一：在目标敌人脚下生成荆棘（选取）
    //候选二：在塔周围直接生成荆棘

    //荆棘预制件
    [Header("荆棘预制件")]
    public GameObject thornsPrefab;

    //荆棘持续时间
    [Header("荆棘持续时间")]
    [Range(0f, 10f)] public float thornsDuration = 2f;

    //荆棘伤害时间间隔（暂定通过协程实现）
    [Header("荆棘伤害时间间隔")]
    [Range(0f, 5f)] public float thornsAttackInterval = 0.5f;

    //荆棘的一次攻击伤害
    [Header("荆棘的一次攻击伤害")]
    [Range(0f, 100f)] public float thornsAttackDamage = 10f;

    //维护一个荆棘生命周期中需要攻击的敌人列表
    List<Enemy> enemies = new List<Enemy>();

    public override void TowerAction()
    {
        //获取攻击范围内的敌人
        enemies = FindEnemyInside();
        if (enemies.Count == 0)
        {
            return; //没有敌人则不执行
        }
        //在每个敌人脚下生成荆棘
        foreach (Enemy enemy in enemies)
        {
            //跳过不需要攻击的敌人
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            //生成荆棘
            SpawnThorns(enemy);
        }
    }

    //生成荆棘
    void SpawnThorns(Enemy enemy)
    {
        //生成荆棘实例         (第四个参数为父物体位置，表示生成物作为子物体)
        GameObject thorns = Instantiate(thornsPrefab, enemy.GetGameObject().transform.position, Quaternion.identity, enemy.GetGameObject().transform);
        
        //设置荆棘的持续时间
        StartCoroutine(ThornsLifetime(thorns, enemy));
    }

    //荆棘生命周期协程
    IEnumerator ThornsLifetime(GameObject thorns, Enemy target)
    {
        //停止敌人的移动
        target.gameObject.GetComponent<Move>().StopMove();

        //大计时器记录生命时长
        //小计时器记录攻击间隔
        float bigTimer = 0f;
        float smallTimer = 0f;

        while (bigTimer < thornsDuration)
        {
            if (smallTimer >= thornsAttackInterval)
            {
                //跳过不需要攻击的敌人
                if (!target.NoMoreShotsNeeded())
                {
                    target.AcceptAttack(thornsAttackDamage);
                }
                smallTimer = 0f; //重置小计时器
            }
            yield return null;
            bigTimer += Time.deltaTime;
            smallTimer += Time.deltaTime;
        }

        Destroy(thorns);

        //恢复敌人移动
        target.gameObject.GetComponent<Move>().ContinueMove();

        //清空敌人
        enemies.Clear();
    }
}
