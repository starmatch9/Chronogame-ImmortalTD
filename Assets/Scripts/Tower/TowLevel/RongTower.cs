using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class RongTower : Tower
{
    /*  -������ ���̽�����-  */

    [TextArea]
    public string Tips = "ע�⣺�������ġ����з�Χ������Ч��";

    [Header("���ɷ���ı߳�������Ź���ı߳�Ϊ3��")]
    [Range(1, 5)] public int length = 3;

    //ע�⣺·���б���Ҫ��������·��������У���������Ϊȫ�־�̬��������ʼ��ʱ���ݹ������е�·��������
    //[Header("·���б�ע�⣺�����������Ҫ�ŵ�ȫ�ֹ�������ͳһ����������")]
    //public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("���ҷ���Ԥ�Ƽ�")]
    public GameObject lavaPrefab;

    [Header("���Ҵ���ʱ��")]
    [Range(0, 15f)]public float lavaDuration = 5f;

    [Header("�����̽ż��ʱ��")]
    [Range(0, 5f)] public float lavaAttackInterval = 0.75f;

    [Header("�����̽��˺�")]
    [Range(0, 100f)] public float lavaAttack = 0.75f;


    //�������в��������ϵĵ���
    [HideInInspector]
    public List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        TowerAction();
    }

    public override void TowerAction()
    {
        SpawnLava();
        UpdateEnemies();
    }

    //���ҵĹ���
    void LavaAttack()
    {
        //�б������Ǳ仯��������Ҫ����һ��
        List<Enemy> enemiesCopy = new List<Enemy>(enemies);

        foreach (Enemy enemy in enemiesCopy)
        {
            //��������Ҫ�����ĵ���(һ�������������)
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.AcceptAttack(lavaAttack);
        }
    }

    //ˢ�µ����б�
    private void UpdateEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            //�Ƴ�����Ҫ�����ĵ���
            if (enemy.NoMoreShotsNeeded())
            {

            }
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            //���ɶ���
            float range = Mathf.Sqrt(Mathf.Pow(length/2, 2) + Mathf.Pow(length, 2));
            if (distance > range)
            {
                enemies.Remove(enemy);
                continue;
            }
        }
    }

    //�����������
    public void SpawnLava()
    {
        //��ȡ�Ź����ڵ����е�
        List<Vector3> points = GetAllPointInGrid();

        //����ÿ����
        foreach (Vector3 point in points)
        {
            //�������ҷ���
            GameObject lava = Instantiate(lavaPrefab, point, Quaternion.identity);
            lava.GetComponent<Lava>().SetTower(this);

            //��ʼ���ҵ���������
            StartCoroutine(LavaLifetime(lava));
        }
        //�������ҵ�ͬʱ����ʼ�̽�����
        StartCoroutine(LavaAttackLife());
    }

    IEnumerator LavaAttackLife()
    {
        //���ʱ����¼����ʱ��
        //С��ʱ����¼�������
        float bigTimer = 0f;
        float smallTimer = 0f;

        while (bigTimer < lavaDuration)
        {
            if (smallTimer >= lavaAttackInterval)
            {
                LavaAttack();

                smallTimer = 0f; //����С��ʱ��
            }
            yield return null;
            bigTimer += Time.deltaTime;
            smallTimer += Time.deltaTime;
        }
    }

    //���ҵ���������
    private IEnumerator LavaLifetime(GameObject lava)
    {
        yield return new WaitForSeconds(lavaDuration);

        //��������
        Destroy(lava);
    }

    //��þŹ����ڵ����е�
    public List<Vector3> GetAllPointInGrid()
    {
        List<Vector3> points = new List<Vector3>();

        //�������λ��
        float x = transform.position.x;
        float y = transform.position.y;
        //��һ�����λ��
        float first_x = x - (length - 1) / 2;
        float first_y = y + (length - 1) / 2;
        //һ����  �߳� * �߳�  ������Ҫ����  �������ż�����Զ���ȥ1��
        int l = ((length - 1) / 2) * 2 + 1;

        //��������ڼ��У���������ڼ���
        for (int i = 0; i < l; ++i)
        {
            for (int j = 0; j < l; ++j)
            {
                //ÿ���������
                //�����ǵ�i�е�j�еĵ�
                float point_x = first_x + i;
                float point_y = first_y - j; //ע�⣺Y�������µģ�����Ҫ��ȥj

                Vector3 point = new Vector3(point_x, point_y, transform.position.z);
                //�б�Ϊ��ʱ���ж��Ƿ���·����
                if (GlobalData.globalRoads.Count != 0)
                {
                    foreach (Tilemap tilemap in GlobalData.globalRoads)
                    {
                        //�����·����
                        if (tilemap.HasTile(tilemap.WorldToCell(point)) && !points.Contains(point))
                        {
                            points.Add(point);
                        }
                    }
                }
            }
        }
        return points;
    }

    public override void OnDrawGizmos()
    {
        float halfSize = (float)length / 2; //�߳�3����߳�1.5
        Vector3 center = transform.position;

        //�����ĸ���
        Vector3 topRight = center + new Vector3(halfSize, halfSize, 0);
        Vector3 topLeft = center + new Vector3(-halfSize, halfSize, 0);
        Vector3 bottomLeft = center + new Vector3(-halfSize, -halfSize, 0);
        Vector3 bottomRight = center + new Vector3(halfSize, -halfSize, 0);

        //����Gizmos��ɫ
        Gizmos.color = Color.red;

        //����������
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
    }

    //�������з�Χ
    public override void DrawAttackArea()
    {
        attackObject = Instantiate(GlobalTowerFunction.SquareArea, transform.position, Quaternion.identity);

        attackObject.transform.localScale = Vector3.one * length;
    }
}
