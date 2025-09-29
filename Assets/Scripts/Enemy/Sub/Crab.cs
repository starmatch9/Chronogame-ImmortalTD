using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crab : Enemy
{
    //————螃蟹————

    //特殊机制：次数盾

    [Header("盾的最大受击次数")]
    public int maxNumber = 0;

    [Header("盾游戏对象")]
    public GameObject Shield = null;

    //记录受击次数
    int count = 0;

    public override void ActionBeforeAttack()
    {
        count++;

        if(count >= maxNumber)
        {
            //取消无敌状态
            unbeatable = false;
            Shield.SetActive(false);
        }
    }
    public override void OtherReset()
    {
        count = 0;
    }

}
