using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//让字段在检查器中可见
[System.Serializable]
public class EnemyGroupItem
{
    //[Header("本波次的敌人生成器")]
    //public EnemySpawn enemySpawn;

    [Header("本波次的敌人生成器（多个版）")]
    public List<SpawnAndDuration> enemySpawnGroup;

    //[Header("两两敌人之间的释放时间差")]
    //[Range(0, 5)]public float timeDiff = 0.5f;

    [Header("本波次结束后提供元素力数量")]
    [Range(0, 99999)]public int elementPower =1000;

}
