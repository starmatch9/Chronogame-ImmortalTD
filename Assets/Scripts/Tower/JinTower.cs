using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class JinTower : Tower
{
    //    *- 炮 -*

    //这个塔需要子弹预制体
    [Header("子弹预制件")]
    public GameObject jinBullet;

    [Header("最多穿透几个敌人")]
    [Range(0, 10)]public int maxPenetrate = 0;

    [TextArea]
    public string Tips = "注意：子弹的伤害参数记得要去子弹预制件里面调。";

    private void Start()
    {
        jinBullet.GetComponent<JinBullet>().SetPenetrateCount(maxPenetrate);

    }

    //重写每隔一段时间执行的行为
    public override void TowerAction()
    {
        if (FindClosestToFinishEnemy() == null)
        {
            return;
        }
        GameObject target = FindClosestToFinishEnemy().gameObject;

        Shoot(target);
    }

    //发射子弹，及生成子弹实例
    void Shoot(GameObject enemy)
    {
        // 偏移 ：子弹在塔上方1.5米的位置发射
        Vector3 offset = new Vector3(0, 1f, 0);

        //实例化子弹
        GameObject bullet = Instantiate(jinBullet, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        JinBullet bulletScript = bullet.GetComponent<JinBullet>();
        bulletScript.SetTarget(enemy);
        bulletScript.SetPenetrateCount(maxPenetrate);
    }
}
