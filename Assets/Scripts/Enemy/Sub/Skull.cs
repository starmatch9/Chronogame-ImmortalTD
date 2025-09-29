using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skull : Enemy
{
    //--普通属性怪：骷髅

    [Header("骨架的血量，掉钱以及存在时间去骨架预制件中调节")]
    [Header("当前复活次数")]
    public int count = 0;

    [Header("骨架预制件")]
    public GameObject bonePrefab;

    [Header("每次复活保留生命值百分比")]
    [Range(0f, 1f)] public float healthRetainRate = 0.8f;

    [HideInInspector]
    public bool attach = false;

    //死的时候调用生成逻辑
    public override void Die()
    {
        SpawnOne();
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

        SkullSkeleton enemyScript = o.GetComponent<SkullSkeleton> ();
        //骷髅生成骨架时，count继承
        enemyScript.attach = true;
        enemyScript.count = count;

        enemyScript.GameObjectSpawn();
        if (!GlobalData.globalEnemies.Contains(enemyScript))
        {
            //添加到全局敌人列表
            GlobalData.globalEnemies.Add(enemyScript);
        }

        move.ContinueMove();

        move.StopMove();
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
    }
    public override void OtherReset()
    {
        count = 0;
        attach = false;

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
}
