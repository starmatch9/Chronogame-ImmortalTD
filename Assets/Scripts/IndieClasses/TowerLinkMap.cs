using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLinkMap
{
    //约定：1为相生增益，2为主克增益，3为被克失效
    public int relation = 1;

    //当前塔
    public Tower tower;

    //原来的攻速
    public float originalActionTime = 0;

    //原来的索敌范围
    public float originalAttackRange = 0;

    //原来的基础攻击力
    public float originalBulletAttack = 0;
}
