using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FreezeEnemy
{
    [Header("敌人预制件")]
    public GameObject enemyPrefab = null;

    [Header("冻结持续时间变化")]
    [Range(-5f, 5f)] public float attachTime = 0f;
}
