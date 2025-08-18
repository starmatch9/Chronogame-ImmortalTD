using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingTower : Tower
{

    //    *- 冰 -*

    //除了子弹，其他没有什么特别之处

    //这个塔需要子弹预制体
    [Header("子弹预制件")]
    public GameObject bingBullet;

    [Header("打击几次后冻结")]
    public int freezeCount = 4;

    [TextArea]
    public string Tips = "注意：子弹的伤害参数记得要去子弹预制件里面调。";

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
        GameObject bullet = Instantiate(bingBullet, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //设置冻结所需次数
        bullet.GetComponent<BingBullet>().SetMaxCount(freezeCount);

        bulletScript.SetTarget(enemy);
    }
}
