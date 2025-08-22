using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GlobalData
{
    //�����б�
    public static List<Enemy> globalEnemies = new List<Enemy>();

    //·���б�
    public static List<Tilemap> globalRoads = new List<Tilemap>();

    //��Ԥ�Ƽ��б�
    public static List<GameObject> globalTowerPrefabs = new List<GameObject>();

    //�ﵽ�յ�ĵ�������
    public static int FinalEnemyNumber = 0;

    //��Ϸʧ��ʱ�������յ�ĵ�������
    public static int maxNumber = 5;

    //ö�ٱ���Ϊ��̬����
    //�ں��������У����˵Ľ��ܹ�������������˹���������Ϊ�������ﵽ����Ч��
    public enum AttackAttribute
    {
        JIN, MU, SHUI, HUO, TU
    }

    //���������maxNumber�������������Ϸ����
    public static void CheckFinalEnemy()
    {
        if( FinalEnemyNumber < maxNumber)
        {
            return;
        }
        //������Ϸ����
        //
        //
        Debug.Log("��ʧ����");

    }
}
