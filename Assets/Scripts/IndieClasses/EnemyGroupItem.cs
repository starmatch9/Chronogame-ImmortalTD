using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ֶ��ڼ�����пɼ�
[System.Serializable]
public class EnemyGroupItem
{
    //[Header("�����εĵ���������")]
    //public EnemySpawn enemySpawn;

    [Header("�����εĵ���������������棩")]
    public List<EnemySpawn> enemySpawnGroup;

    [Header("��������֮����ͷ�ʱ���")]
    [Range(0, 5)]public float timeDiff = 0.5f;

    [Header("�����ν������ṩԪ��������")]
    [Range(0, 99999)]public int elementPower =1000;

}
