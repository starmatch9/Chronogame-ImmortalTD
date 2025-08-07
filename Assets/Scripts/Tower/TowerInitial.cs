using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GlobalData;

//����ű�������ʼ��һ����
//������������Ч��ͳͳ�����ӵ��ϣ����������ݶ���
public class TowerInitial : MonoBehaviour
{
    //�ӵ�Ԥ����
    public GameObject bulletPrefab;

    //������Χ�뾶
    [Range(0, 20)]public float attackRange = 2f;

    //�ӵ����ʱ��
    [Range(0, 2)]public float shootTime = 0.5f;

    List<Enemy> enemies = new List<Enemy>();

    //public Enemy oneEnemy;

    //����������ʱ��
    float timeSinceLastShot = 0f;

    void Awake()
    {
        //��������������
        Adjust();
    }

    private void Start()
    {
        enemies = globalEnemies; //��ȡȫ�ֵ����б�
    }
    void Update()
    {
        //����ʱ����.�ۼ�ʱ��
        timeSinceLastShot += Time.deltaTime;
        if (timeSinceLastShot < shootTime) { 
            return; //���ʱ����С��1�룬��ִ�й����߼�
        }
        Enemy closestEnemy = FindClosestEnemy(attackRange);

        if (closestEnemy != null)
        {
            Shoot(closestEnemy.GetGameObject());
        }

        timeSinceLastShot = 0f; //����ʱ����
    }

    void Shoot(GameObject enemy)
    {
        //ʵ�����ӵ�
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        //��Ҫê���ӵ���Ŀ�꣬��ȡ�ӵ�����Ϊ�ű�
        Action bulletScript = bullet.GetComponent<Action>();
        bulletScript.SetTarget(enemy);
    }

    //��λ�����뾶������ĵ���
    private Enemy FindClosestEnemy(float ShootingDistance)
    {
        Enemy closestEnemy = null;
        float closestDistance = Mathf.Infinity;//��ʼ�������Ϊ�����
        //�����б��е����е���
        foreach (Enemy enemy in enemies)
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
}
