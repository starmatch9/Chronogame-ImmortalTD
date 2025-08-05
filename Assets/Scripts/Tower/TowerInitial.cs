using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInitial : MonoBehaviour
{
    //攻击范围半径
    [Range(0, 20)]public float attackRange = 2f;

    //子弹预制体
    public GameObject bulletPrefab;

    //敌人列表(后期从敌人生成点类获取，生产一个加一个)
    public List<Enemy> enemies = new List<Enemy>();

    public Enemy oneEnemy;

    //用来计算间隔时间
    float timeSinceLastShot = 0f;

    void Awake()
    {
        //调整物体至中央
        Adjust();
    }

    private void Start()
    {
        //初始化敌人列表
        enemies.Add(oneEnemy);
    }
    void Update()
    {
        //计算时间间隔.累加时间
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot < 0.5f) { 
            return; //如果时间间隔小于1秒，则不执行攻击逻辑
        }

        Enemy closestEnemy = FindClosestEnemy(attackRange);

        if (closestEnemy != null)
        {
            Shoot(closestEnemy.GetGameObject());
        }

        timeSinceLastShot = 0f; //重置时间间隔


    }

    void Shoot(GameObject enemy)
    {
        //实例化子弹
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //需要锚定子弹的目标，获取子弹的行为脚本
        Action bulletScript = bullet.GetComponent<Action>();
        bulletScript.SetTarget(enemy);
    }

    //定位攻击半径内最近的敌人
    private Enemy FindClosestEnemy(float ShootingDistance)
    {
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;//初始最近距离为无穷大
        //遍历列表中的所有敌人
        foreach (Enemy enemy in enemies)
        {
            //跳过不需要攻击的敌人
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);

            //筛选掉超过攻击范围的敌人，重新计算最近的敌人
            if (distance < closestDistance && distance < ShootingDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    //在编辑模式下显示Gizmo
    void OnDrawGizmos()
    {
        // 设置Gizmo颜色
        Gizmos.color = Color.red;
        // 绘制无填充圆圈
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void Adjust()
    {
        //所有点的公式
        float widthStart = -7.5f;
        float widthEnd = 7.5f;
        float heightStart = -3.5f;
        float heightEnd = 4.5f;
        //调整物体的位置
        for (float x = widthStart; x <= widthEnd; x += 1.0f)
        {
            for (float y = heightStart; y <= heightEnd; y += 1.0f)
            {
                if (Mathf.Abs(transform.position.x - x) <= 0.5f && Mathf.Abs(transform.position.y - y) <= 0.5f)
                {
                    transform.position = new Vector3(x, y, 0f);
                    break;
                }
            }
        }
    }
}
