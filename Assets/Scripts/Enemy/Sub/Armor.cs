using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Armor : Enemy
{
    // ————精英怪———— 盔甲 ———— 金

    [Header("水伤次数后减速")]
    public int water = 3;

    [Header("新移速值")]
    public float newSpeed = 5f;

    [Header("火伤次数后减防")]
    public int fire = 3;

    [Header("新物理防御减伤百分比")]
    [Range(0, 1f)] public float newPhysicsDefense = 0f;

    [Header("新魔法防御减伤百分比")]
    [Range(0, 1f)] public float newMagicDefense = 0f;

    int waterCount = 0;
    int fireCount = 0;

    //特殊元素机制
    public override void ElementFunction(GlobalData.ElementAttribute elementAttribute)
    {
        switch (elementAttribute)
        {
            case GlobalData.ElementAttribute.SHUI:
                waterCount++;
                if (waterCount >= water)
                {
                    Move move = gameObject.GetComponent<Move>();
                    move.StopMove();
                    move.speed = newSpeed;
                    move.ContinueMove();
                }
                break;

            case GlobalData.ElementAttribute.HUO:
                fireCount++;
                if (fireCount >= fire)
                {
                    physicsDefense = newPhysicsDefense;
                    magicDefense = newMagicDefense;
                }
                break;
        }
    }

    public override void OtherReset()
    {
        waterCount = 0;
        fireCount = 0;
    }
}
