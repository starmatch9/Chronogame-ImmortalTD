using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;

//����ű�������ʼ��һ����
//������������Ч��ͳͳ�����ӵ��ϣ����������ݶ���
public class TowerInitial : MonoBehaviour
{
    //һ������Ӧһ����
    [HideInInspector]
    public Transform hole;

    //�ӵ�Ԥ����
    [Header("�ӵ�Ԥ�Ƽ�")]
    public GameObject bulletPrefab;

    //������Χ�뾶
    [Header("���з�Χ")]
    [Range(0, 20)]public float attackRange = 2f;

    //�ӵ����ʱ��
    [Header("�������ʱ��")]
    [Range(0, 2)]public float shootTime = 0.5f;

    //List<Enemy> enemies = new List<Enemy>();

    //public Enemy oneEnemy;

    //����������ʱ��
    float timeSinceLastShot = 0f;

    void Awake()
    {
        //��������������
        Adjust();
    }
    void Update()
    {
        //����ʱ����.�ۼ�ʱ��
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot < shootTime) { 
            return; //���ʱ����С��1�룬��ִ�й����߼�
        }

        //Enemy closestEnemy = FindClosestEnemy(attackRange);
        Enemy closestEnemy = FindClosestToFinishEnemy(attackRange);

        if (closestEnemy != null)
        {
            Shoot(closestEnemy.GetGameObject());
        }

        timeSinceLastShot = 0f; //����ʱ����
    }

    void Shoot(GameObject enemy)
    {
        //�ӵ������Ϸ�1.5�׵�λ�÷���
        Vector3 offset = new Vector3(0, 1f, 0);
        //ʵ�����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, transform.position + offset, Quaternion.identity);
        //��Ҫê���ӵ���Ŀ�꣬��ȡ�ӵ�����Ϊ�ű�
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.SetTarget(enemy);
    }

    //��λ�����뾶������ĵ���
    private Enemy FindClosestEnemy(float ShootingDistance)
    {
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;//��ʼ�������Ϊ�����
        //�����б��е����е���
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //��������Ҫ�����ĵ���
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);

            //ɸѡ������������Χ�ĵ��ˣ����¼�������ĵ���
            if (distance < closestDistance && distance < ShootingDistance)
            {
                closestDistance = distance;
                closestEnemy = enemy;
            }
        }
        return closestEnemy;
    }

    //��λ�����뾶�����յ�����ĵ���
    Enemy FindClosestToFinishEnemy(float ShootingDistance)
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
            if (distance < ShootingDistance)
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

    //���ö�Ӧ��
    public void SetHole(Transform hole)
    {
        this.hole = hole;
    }

    //�ڱ༭ģʽ����ʾGizmo
    void OnDrawGizmos()
    {
        // ����Gizmo��ɫ
        Gizmos.color = Color.red;
        // ���������ԲȦ
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    public void Adjust()
    {
        //���е�Ĺ�ʽ
        float widthStart = -7.5f;
        float widthEnd = 7.5f;
        float heightStart = -3.5f;
        float heightEnd = 4.5f;
        //���������λ��
        for (float x = widthStart; x <= widthEnd; x += 1.0f)
        {
            for (float y = heightStart; y <= heightEnd; y += 1.0f)
            {
                if (Mathf.Abs(transform.position.x - x) <= 0.5f && Mathf.Abs(transform.position.y - y) <= 0.5f)
                {
                    transform.position = new Vector3(x, y, 0f);
                    break;
                }
            }
        }
    }

    public void Remove()
    {
        //�������ӻ��ҵĻ��շ���

        //
        Destroy(gameObject);
    }
}
