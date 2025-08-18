using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BingBullet : Bullet
{
    //    *- 冰子弹 -*

    //为了不干扰敌人的内部逻辑（因为本身就够乱了）
    //这里不采用在敌人脚本里加冻结逻辑
    //而是通过全局脚本控制敌人冻结，请移步到Freeze.cs脚本中查看

    [Header("雪花预制件")]
    public GameObject snowPrefab;

    [Header("冻结时间")]
    [Range(0f, 10f)] public float freezeTime = 3f;

    int maxCount = 0;

    //子弹死亡前累计冻结
    public override IEnumerator DieAction()
    {
        Enemy targetEnemy = target.GetComponent<Enemy>();
        //打击敌人前累计次数
        if (!Freeze.enemyHitCount.ContainsKey(targetEnemy))
        {
            //字典中没有，就加上
            Freeze.enemyHitCount.Add(targetEnemy, 1);
        }
        else
        {
            //有的话，加加
            ++Freeze.enemyHitCount[targetEnemy];

            //加完检测次数是否大于目标
            if (Freeze.enemyHitCount[targetEnemy] >= maxCount)
            {
                //放冻结逻辑


            }

        }


        yield return base.DieAction(); //调用基类的死亡逻辑
    }


    public void SetMaxCount(int count)
    {
        maxCount = count;
    }

}
