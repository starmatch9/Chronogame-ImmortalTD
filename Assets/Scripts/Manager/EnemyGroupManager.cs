using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    //�����������б�
    [Header("�����б����ΰ�˳��ų����ˣ�")]
    public List<EnemyGroupItem> enemyGroupItems;

    private void Awake()
    {
        GlobalEnemyGroupFunction.enemyGroupItems = enemyGroupItems;
    }
}
