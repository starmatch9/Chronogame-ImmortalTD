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

    [Header("冻结后敌人伤害倍数")]
    [Range(1f, 2f)] public float mul = 1.2f;

    int maxCount = 0;

    //子弹死亡前累计冻结
    public override IEnumerator DieAction()
    {
        if (target== null)
        {
            yield return base.DieAction();
            yield break;
        }

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

            //加完检测次数是否等于目标   （大于都不行！！不然会重复执行）
            if (Freeze.enemyHitCount[targetEnemy] == maxCount)
            {
                

                //禁用渲染器与碰撞器
                transform.Find("Renderer").GetComponent<Renderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;

                //放冻结逻辑
                yield return StartCoroutine(SpawnSnow(targetEnemy));

                //重置次数
                if (Freeze.enemyHitCount.ContainsKey(targetEnemy))
                {
                    Freeze.enemyHitCount[targetEnemy] = 0;
                }
            }
        }

        yield return base.DieAction(); //调用基类的死亡逻辑
    }

    //生成雪花
    IEnumerator SpawnSnow(Enemy enemy)
    {
        //如果已经除以处于停止移动的状态则无法选中
        if (enemy.gameObject.GetComponent<Move>().isStopMove)
        {
            yield break;
        }

        //生成雪花实例         (第四个参数为父物体位置，表示生成物作为子物体)
        GameObject snow = Instantiate(snowPrefab, enemy.GetGameObject().transform.position, Quaternion.identity, enemy.GetGameObject().transform);
        //设置雪花的持续时间
        yield return StartCoroutine(SnowLifetime(snow, enemy));
    }

    //雪花生命周期协程
    IEnumerator SnowLifetime(GameObject snow, Enemy enemy)
    {
        //如果已经除以处于停止移动的状态则无法选中
        if (enemy.gameObject.GetComponent<Move>().isStopMove)
        {
            yield break;
        }

        enemy.gameObject.GetComponent<Move>().StopMove();
        //加伤
        //enemy.SetDefense(1f - mul);
        enemy.SetHurtRate(mul);

        float timer = 0;
        while (timer < freezeTime)
        {

            if (enemy == null)
            {
                Destroy(snow);
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(freezeTime); //等待冻结时间

        Destroy(snow);

        //时间到，解除冻结
        //enemy.ResetDefense();
        enemy.ResetHurtRate();

        enemy.gameObject.GetComponent<Move>().ContinueMove();
    }


    public void SetMaxCount(int count)
    {
        maxCount = count;
    }

}
