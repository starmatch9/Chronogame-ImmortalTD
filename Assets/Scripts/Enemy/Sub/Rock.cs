using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock :Enemy
{
    // ---石头怪---

    [Header("木伤次数后移速降低")]
    public int wood = 4;

    [Header("木伤机制下的移动速度")]
    public float woodSpeed = 20f;

    [Header("金伤额外伤害")]
    public float ironHurt = 5f;

    int woodCount = 0;

    float oriSpeed;
    public override void Awake()
    {
        oriSpeed = gameObject.GetComponent<Move>().speed;
        base.Awake();
    }

    public override void ElementFunction(GlobalData.ElementAttribute elementAttribute)
    {
        switch (elementAttribute)
        {
            //木：改变速度
            case GlobalData.ElementAttribute.MU:
                woodCount++;

                if (woodCount >= wood)
                {
                    Move move = gameObject.GetComponent<Move>();
                    if (move.speed != woodSpeed)
                    {
                        move.StopMove();
                        move.speed = woodSpeed;
                        move.ContinueMove();
                    }
                }
                break;

        }
    }

    public override float ElementExtraHurt(GlobalData.ElementAttribute elementAttribute, float attack)
    {
        switch (elementAttribute)
        {
            //金：额外伤害
            case GlobalData.ElementAttribute.JIN:
                attack += ironHurt;
                break;
        }
        return attack;
    }

    public override void OtherReset()
    {
        woodCount = 0;

        Move move = gameObject.GetComponent<Move>();
        move.speed = oriSpeed;
    }
}
