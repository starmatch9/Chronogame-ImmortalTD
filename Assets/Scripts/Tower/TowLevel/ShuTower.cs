using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuTower : Tower
{
    //  树塔

    //暂定单纯生成元素力

    //树塔每次生成元素力的数量
    [Header("每次生成元素力数量")]
    [Range(1, 1000)]public int elementAmount = 100;

    //Start函数中获取元素力管理器（元素力数据交由全局管理）

    public override void TowerAction()
    {
        //生成元素力
        GlobalElementPowerFunction.AddCount(elementAmount);
    }
}
