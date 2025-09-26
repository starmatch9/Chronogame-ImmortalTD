using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnAndDuration
{
    [Header("生成器")]
    public EnemySpawn spawn;

    [Header("多长时间后进行运行下一个生成器（秒），最后一个一定是0")]
    [Range(0, 100)]public float duration = 5f;

    [Header("此生成器的敌人全部击败后再运行下一个生成器")]
    public bool waitAllDead = false;
}
