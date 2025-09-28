using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MudEnemy
{
    [Header("敌人预制件")]
    public GameObject enemyPrefab;

    [Header("受沼泽影响的倍数（百分比：速度更慢填1以下，变快了填1以上）")]
    [Range(0, 2f)] public float swampEffect = 1f;
}
