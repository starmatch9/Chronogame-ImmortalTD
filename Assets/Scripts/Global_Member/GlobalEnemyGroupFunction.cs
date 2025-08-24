using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEnemyGroupFunction
{
    //ά��һ��������Ŀ�б�
    public static List<EnemyGroupItem> enemyGroupItems;

    //ά�����UI�ű�����Ҫ�����伤��״̬��������ʾ��ť���
    public static GameObject nextOneBigButton;

    //ά��һ��ָ���б���Ŀ������
    static int index = 0;

    //ά����ǰ���Σ��ǵ�ǰ����ǰ��
    public static EnemyGroupItem item;

    //ά��һ���������ڣ�ȷ�����˵������ܹ�����ʱ���йط���
    public static MonoBehaviour mono;


    //��ʼ�ͷ�һ������
    public static void StartOneEnemyGroup()
    {
        if(index >= enemyGroupItems.Count)
        {
            return;
        }
        item = enemyGroupItems[index];
        //��ʼ�ͷŵ���
        //item.enemySpawn.Switch();
        mono.StartCoroutine(DispatchEnemy());

        //�رհ�ť
        CloseButton();

        ++index;
    }

    //����ʱ����ͷŵĵ���
    public static IEnumerator DispatchEnemy()
    {
        foreach(EnemySpawn spawn in item.enemySpawnGroup)
        {
            spawn.Switch();

            yield return new WaitForSeconds(item.timeDiff);  
        }
    }


    //��Ⲩ���Ƿ����
    public static void CheckEnd()
    {
        //����Ƿ�ȫ�ֵ����б��еĵ����Ƿ��ڼ���״̬
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //��һ���������ţ���return
            if (enemy.gameObject.activeInHierarchy)
            {
                return;
            }
        }

        EndEnemyGroup();
    }

    //Ϊ��������β
    public static void EndEnemyGroup() 
    { 
        if (index >= enemyGroupItems.Count)
        {
            Debug.Log("�ؿ��Ѿ������˺ܺ�ھͺù���������");
            return;
        }

        //��ȡ�������
        GlobalElementPowerFunction.AddCount(item.elementPower);

        //��յ����б�
        GlobalData.globalEnemies.Clear();

        //չ��UI��ť
        OpenButton();
    }


    //��ʾ��ť
    public static void OpenButton()
    {
        nextOneBigButton.SetActive(true);
    }
    //����ʾ��ť
    public static void CloseButton() { 
        nextOneBigButton.SetActive(false);
    }

    //�������о�̬����
    public static void ResetAllData()
    {
        //ά��һ��������Ŀ�б�
        enemyGroupItems = new List<EnemyGroupItem>();

        //ά�����UI�ű�����Ҫ�����伤��״̬��������ʾ��ť���
        nextOneBigButton = null;

        //ά��һ��ָ���б���Ŀ������
        index = 0;

        //ά����ǰ���Σ��ǵ�ǰ����ǰ��
        item = null;
    }
}
