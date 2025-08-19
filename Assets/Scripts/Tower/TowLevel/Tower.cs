using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//这个塔作为所有拥有特殊功能的二级塔的基类
public abstract class Tower : MonoBehaviour
{
    //一个塔对应一个坑
    [HideInInspector]
    public Transform hole;

    //攻击范围半径
    [Header("索敌范围")]
    [Range(0, 20)] public float attackRange = 2f;

    //行动间隔时间
    [Header("行为间隔时间")]
    [Range(0, 20)] public float actionTime = 0.5f;

    //维护一个敌人列表 保护继承让子类可以访问
    //注意：这里的敌人列表不同于TowerInitial，此只维护攻击范围内的敌人列表
    //protected List<Enemy> enemies = new List<Enemy>();

    float timer = 0f; //计时器
    public virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer < actionTime)
        {
            return;
        }
        ExecuteAction();
        timer = 0f; //重置计时器
    }

    //行为，用于每隔一段时间执行一次的操作
    void ExecuteAction()
    {
        TowerAction(); //执行塔的行为
    }

    //子类需要实现的塔行为
    public abstract void TowerAction();

    public List<Enemy> FindEnemyInside()
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //跳过不需要攻击的敌人
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            //获取攻击范围
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            if (distance < attackRange)
            {
                enemies.Add(enemy);
            }
        }
        return enemies;
    }

    //子类也许用的到：定位攻击半径内离终点最近的敌人
    public Enemy FindClosestToFinishEnemy()
    {
        Enemy closestEnemy = null;
        float longestSurvivalTime = 0;//生存时间为0
        //遍历列表中的所有敌人
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //跳过不需要攻击的敌人
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            //筛选掉超过攻击范围的敌人
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            if (distance < attackRange)
            {
                //
                //获取离终点最近的敌人，目前思路：敌人生成后，存活时间最长的就是离终点最近的
                //
                float survivalTime = enemy.GetComponent<Move>().survivalTime;
                if (survivalTime > longestSurvivalTime)
                {
                    longestSurvivalTime = survivalTime;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    public virtual void OnDrawGizmos()
    {
        // 设置Gizmo颜色
        Gizmos.color = Color.red;
        // 绘制无填充圆圈
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
