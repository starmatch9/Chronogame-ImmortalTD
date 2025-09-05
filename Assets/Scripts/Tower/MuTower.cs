using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuTower : Tower
{
    //后面的子弹要单独制作出来
    [Header("子弹预制件")]
    public GameObject bulletPref = null;

    public override void TowerAction()
    {
        if (FindClosestToFinishEnemy() == null)
        {
            return;
        }
        //射击
        GameObject target = FindClosestToFinishEnemy().gameObject;
        Shoot(target);
    }

    //发射子弹，及生成子弹实例
    void Shoot(GameObject enemy)
    {
        // 偏移 ：子弹在塔上方1.5米的位置发射
        Vector3 offset = new Vector3(0, 1f, 0);

        //实例化子弹
        GameObject bullet = Instantiate(bulletPref, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(enemy);
    }
}
