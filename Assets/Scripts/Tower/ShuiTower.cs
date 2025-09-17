using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuiTower : Tower
{
    //  __水塔


    //子弹预制体，要炮的子弹，溅射伤害
    [Header("子弹预制件")]
    public GameObject shuiBullet;

    [Header("减速为原来的多少，百分比")]
    [Range(0f, 1f)] public float slowFactor = 0.3f;

    [Header("一次减速时长")]
    [Range(0f, 20f)] public float slowTime = 4f;

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
        GameObject bullet = Instantiate(shuiBullet, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        ShuiBullet bulletScript = bullet.GetComponent<ShuiBullet>();

        //设置攻击伤害
        bulletScript.baseAttack = bulletAttack;

        bulletScript.SetFactor(slowFactor, this);
        bulletScript.SetTarget(enemy);
    }

    public void startSlow(Enemy enemy)
    {
        if (enemy != null)
        {
            /*重要*/
            //新知识
            //
            //防止物体销毁后协程无辜暂停的方法：
            //————将协程挂载到始终激活的 “管理器类物体”！！！
            //
            GlobalEnemyGroupFunction.mono.StartCoroutine(slowEnemy(enemy));
        }
    }

    IEnumerator slowEnemy(Enemy enemy)
    {

        Move move = enemy.gameObject.GetComponent<Move>();
        //减速
        move.ChangeSpeed(slowFactor);

        float timer = 0;
        while (timer < slowTime)
        {

            if (enemy == null)
            {
                //if (isDead)
                //{
                //    break;
                //}
                yield break;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        //yield return new WaitForSeconds(slowTime);

        //重置速度
        move.ResetSpeed();
    }
}
