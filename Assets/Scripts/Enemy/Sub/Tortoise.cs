using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tortoise : Enemy
{
    // ---超级乌龟---


    [Header("盾的最大受击次数")]
    public int maxNumber = 0;

    [Header("盾游戏对象")]
    public GameObject Shield = null;

    [Header("盾存在时，魔法防御减伤百分比")]
    [Range(0, 1f)] public float shieldMagicDefense = 0f;

    [Header("金伤减伤百分比")]
    public float metalHurtReduce = 0.4f;

    [Header("土伤增伤百分比")]
    public float earthHurtIncrease = 0.4f;

    [Header("木伤一次几层")]
    [Range(1, 5)] public int num = 2;

    //记录受击次数
    int count = 0;
    float originalMagicDefense = 0f;

    public override void Awake()
    {
        originalMagicDefense = magicDefense;

        base.Awake();
    }

    public override void ActionBeforeAttack()
    {
        count++;

        if (count >= maxNumber)
        {
            //盾破掉之后，魔抗返回原值
            magicDefense = originalMagicDefense;
            Shield.SetActive(false);
        }
    }

    //元素机制
    public override void ElementFunction(GlobalData.ElementAttribute elementAttribute)
    {
        switch (elementAttribute)
        {
            //木：破多层盾
            case GlobalData.ElementAttribute.MU:
                //原来加1，现在加2
                count += num - 1;

                if (count >= maxNumber)
                {
                    //盾破掉之后，魔抗返回原值
                    magicDefense = originalMagicDefense;
                    Shield.SetActive(false);
                }
                break;
        }
    }
    //增减伤害
    public override float ElementExtraHurt(GlobalData.ElementAttribute elementAttribute, float attack)
    {
        switch (elementAttribute)
        {
            //金：减少伤害
            case GlobalData.ElementAttribute.JIN:
                attack *= (1 - metalHurtReduce);
                break;

            //土：增加伤害
            case GlobalData.ElementAttribute.TU:
                attack *= (1 + earthHurtIncrease);
                break;
        }
        return attack;
    }

    public override void OtherSpawn()
    {
        magicDefense = shieldMagicDefense;
    }
    public override void OtherReset()
    {
        magicDefense = originalMagicDefense;
        count = 0;
    }
}
