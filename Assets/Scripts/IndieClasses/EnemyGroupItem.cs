using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���ֶ��ڼ�����пɼ�
[System.Serializable]
public class EnemyGroupItem
{
    [Header("�����εĵ���������")]
    public EnemySpawn enemySpawn;

    [Header("�����ν������ṩԪ��������")]
    [Range(0, 99999)]public int elementPower;

}
