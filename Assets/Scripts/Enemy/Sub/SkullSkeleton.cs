using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullSkeleton : Enemy
{
    //--依附于骷髅而生：骨架

    [Header("当前复活次数")]
    public int count = 0;

    [Header("存在时长")]
    public float existDuration = 10f;

    [Header("骷髅预制件")]
    public GameObject bonePrefab;

    [Header("每次复活保留生命值百分比")]
    [Range(0f, 1f)]public float healthRetainRate = 0.8f;

    [HideInInspector]
    public bool attach = false;

    //存在时间协程
    Coroutine waitCoroutine = null;

    bool isStop = false;
    public IEnumerator ExistTime()
    {
        float timer = 0f;
        while (timer < existDuration)
        {
            while (isStop)
            {
                yield return null;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        SpawnOne();

        //时间到，赐死
        AcceptAttack(99999f, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);
    }


    public override void Die()
    {
        //死的时候终止协程
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
    }

    //同小人参一个逻辑
    public void SpawnOne()
    {
        //生成一个骨架
        GameObject o = Instantiate(bonePrefab);
        o.transform.position = transform.position;

        Move move = o.GetComponent<Move>();

        move.Re_Move();
        move.SetBoolMoving(true);

        move.StopMove();
        move.survivalTime = GetComponent<Move>().survivalTime;
        move.roadTilemap = GetComponent<Move>().roadTilemap;
        move.direction = GetComponent<Move>().direction;

        Skull enemyScript = o.GetComponent<Skull>();
        //当由骨架生成行走骷髅时，+count
        enemyScript.attach = true;
        enemyScript.count = count + 1;

        enemyScript.GameObjectSpawn();
        if (!GlobalData.globalEnemies.Contains(enemyScript))
        {
            //添加到全局敌人列表
            GlobalData.globalEnemies.Add(enemyScript);
        }

        move.ContinueMove();
    }


    public override void OtherSpawn()
    {
        float hurt = maxHealth;
        for (int i = 0; i < count; i++)
        {
            hurt *= healthRetainRate;
        }
        hurt = maxHealth - hurt;
        //生成就得扣20%的血(真伤)
        AcceptAttack(hurt, GlobalData.AttackAttribute.None, GlobalData.ElementAttribute.NONE);

        //然后开始存在时间协程
        if (waitCoroutine != null)
        {
            StopCoroutine(waitCoroutine);
            waitCoroutine = null;
        }
        waitCoroutine = StartCoroutine(ExistTime());
    }
    public override void OtherReset()
    {
        count = 0;

        if (!attach)
        {
            return;
        }

        if (GlobalData.globalEnemies.Contains(this))
        {
            GlobalData.globalEnemies.Remove(this);
        }

        Destroy(gameObject);
    }

    public override void StopAction()
    {
        isStop = true;
    }
    public override void ContinueAction()
    {
        isStop = false;
    }
}
