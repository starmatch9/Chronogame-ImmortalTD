using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuoTower : Tower
{
    //—— 火塔 ——

    //子弹预制体，要炮的子弹，溅射伤害
    [Header("子弹预制件")]
    public GameObject paoBullet;

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
        GameObject bullet = Instantiate(paoBullet, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //设置攻击伤害
        bulletScript.baseAttack = bulletAttack;

        bulletScript.SetTarget(enemy);
    }
}
