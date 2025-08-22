using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    //敌人生成器列表
    [Header("波次列表（波次按顺序放出敌人）")]
    public List<EnemyGroupItem> enemyGroupItems;

    private void Awake()
    {
        GlobalEnemyGroupFunction.enemyGroupItems = enemyGroupItems;
    }
}
