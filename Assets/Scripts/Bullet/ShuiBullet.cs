using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShuiBullet : Bullet
{
    float slowFactor = 0.3f;

    ShuiTower tower = null;

    public void SetFactor(float f, ShuiTower t)
    {
        slowFactor = f;

        tower = t;
    }

    //子弹临死前调用减速方法
    public override void Die(Enemy enemy)
    {
        tower.startSlow(enemy);

        //正常死亡
        base.Die(null);
    }

}
