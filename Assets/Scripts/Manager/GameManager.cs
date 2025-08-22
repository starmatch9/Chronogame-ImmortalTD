using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("游戏失败的条件：到达终点的最大敌人数量")]
    [Range(1,10)]public int maxNumber = 5;

    void Awake()
    {
        GlobalData.maxNumber = maxNumber;
    }
}
