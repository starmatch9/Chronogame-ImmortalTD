using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JiTower : Tower
{
    //    * ������ *

    //��ѡһ����Ŀ����˽������ɾ�����ѡȡ��
    //��ѡ����������Χֱ�����ɾ���

    //����Ԥ�Ƽ�
    [Header("����Ԥ�Ƽ�")]
    public GameObject thornsPrefab;

    //��������ʱ��
    [Header("��������ʱ��")]
    [Range(0f, 10f)] public float thornsDuration = 2f;

    //�����˺�ʱ�������ݶ�ͨ��Э��ʵ�֣�
    [Header("�����˺�ʱ����")]
    [Range(0f, 5f)] public float thornsAttackInterval = 0.5f;

    //������һ�ι����˺�
    [Header("������һ�ι����˺�")]
    [Range(0f, 100f)] public float thornsAttackDamage = 10f;

    //ά��һ������������������Ҫ�����ĵ����б�
    List<Enemy> enemies = new List<Enemy>();

    public override void TowerAction()
    {
        //��ȡ������Χ�ڵĵ���
        enemies = FindEnemyInside();
        if (enemies.Count == 0)
        {
            return; //û�е�����ִ��
        }
        //��ÿ�����˽������ɾ���
        foreach (Enemy enemy in enemies)
        {
            //��������Ҫ�����ĵ���
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            //���ɾ���
            SpawnThorns(enemy);
        }
    }

    //���ɾ���
    void SpawnThorns(Enemy enemy)
    {
        //���ɾ���ʵ��         (���ĸ�����Ϊ������λ�ã���ʾ��������Ϊ������)
        GameObject thorns = Instantiate(thornsPrefab, enemy.GetGameObject().transform.position, Quaternion.identity, enemy.GetGameObject().transform);
        
        //���þ����ĳ���ʱ��
        StartCoroutine(ThornsLifetime(thorns, enemy));
    }

    //������������Э��
    IEnumerator ThornsLifetime(GameObject thorns, Enemy target)
    {
        //ֹͣ���˵��ƶ�
        target.gameObject.GetComponent<Move>().StopMove();

        //���ʱ����¼����ʱ��
        //С��ʱ����¼�������
        float bigTimer = 0f;
        float smallTimer = 0f;

        while (bigTimer < thornsDuration)
        {
            if (smallTimer >= thornsAttackInterval)
            {
                //��������Ҫ�����ĵ���
                if (!target.NoMoreShotsNeeded())
                {
                    target.AcceptAttack(thornsAttackDamage);
                }
                smallTimer = 0f; //����С��ʱ��
            }
            yield return null;
            bigTimer += Time.deltaTime;
            smallTimer += Time.deltaTime;
        }

        Destroy(thorns);

        //�ָ������ƶ�
        target.gameObject.GetComponent<Move>().ContinueMove();

        //��յ���
        enemies.Clear();
    }
}
