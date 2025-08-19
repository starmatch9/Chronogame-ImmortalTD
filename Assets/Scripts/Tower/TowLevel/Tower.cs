using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//�������Ϊ����ӵ�����⹦�ܵĶ������Ļ���
public abstract class Tower : MonoBehaviour
{
    //һ������Ӧһ����
    [HideInInspector]
    public Transform hole;

    //������Χ�뾶
    [Header("���з�Χ")]
    [Range(0, 20)] public float attackRange = 2f;

    //�ж����ʱ��
    [Header("��Ϊ���ʱ��")]
    [Range(0, 20)] public float actionTime = 0.5f;

    //ά��һ�������б� �����̳���������Է���
    //ע�⣺����ĵ����б�ͬ��TowerInitial����ֻά��������Χ�ڵĵ����б�
    //protected List<Enemy> enemies = new List<Enemy>();

    float timer = 0f; //��ʱ��
    public virtual void Update()
    {
        timer += Time.deltaTime;
        if (timer < actionTime)
        {
            return;
        }
        ExecuteAction();
        timer = 0f; //���ü�ʱ��
    }

    //��Ϊ������ÿ��һ��ʱ��ִ��һ�εĲ���
    void ExecuteAction()
    {
        TowerAction(); //ִ��������Ϊ
    }

    //������Ҫʵ�ֵ�����Ϊ
    public abstract void TowerAction();

    public List<Enemy> FindEnemyInside()
    {
        List<Enemy> enemies = new List<Enemy>();
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //��������Ҫ�����ĵ���
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            //��ȡ������Χ
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            if (distance < attackRange)
            {
                enemies.Add(enemy);
            }
        }
        return enemies;
    }

    //����Ҳ���õĵ�����λ�����뾶�����յ�����ĵ���
    public Enemy FindClosestToFinishEnemy()
    {
        Enemy closestEnemy = null;
        float longestSurvivalTime = 0;//����ʱ��Ϊ0
        //�����б��е����е���
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //��������Ҫ�����ĵ���
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            //ɸѡ������������Χ�ĵ���
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            if (distance < attackRange)
            {
                //
                //��ȡ���յ�����ĵ��ˣ�Ŀǰ˼·���������ɺ󣬴��ʱ����ľ������յ������
                //
                float survivalTime = enemy.GetComponent<Move>().survivalTime;
                if (survivalTime > longestSurvivalTime)
                {
                    longestSurvivalTime = survivalTime;
                    closestEnemy = enemy;
                }
            }
        }
        return closestEnemy;
    }

    public virtual void OnDrawGizmos()
    {
        // ����Gizmo��ɫ
        Gizmos.color = Color.red;
        // ���������ԲȦ
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
