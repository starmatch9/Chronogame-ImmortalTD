using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class LangTower : Tower
{
    //  -* ���� *-

    //��ǰ˼·������λ�ü�⣬�ӵ��˵�һ����ʼ����������õõ���·���ľ�ͷλ�ã��ڴ������� 

    [TextArea]
    public string Tips = "ע�⣺�����ġ����з�Χ������Ч������Ϊ���ʱ�䡱�����Ĵ�һ�ε�ʱ�䡣";

    [Header("���ɷ���ı߳�������Ź���ı߳�Ϊ3��")]
    [Range(1, 5)] public int length = 3;

    //ע�⣺·���б���Ҫ��������·��������У���������Ϊȫ�־�̬��������ʼ��ʱ���ݹ������е�·��������
    [Header("·���б�ע�⣺�����������Ҫ�ŵ�ȫ�ֹ�������ͳһ����������")]
    public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("�˴�����Ԥ�Ƽ�")]
    public GameObject waveTriggerPrefab;

    [Header("��Ԥ�Ƽ�")]
    public GameObject wavePrefab;

    //ά�����н���������Χ�ĵ����б�
    //�����б�Ϊ��ʱ�������޸�firstPoint�������б�Ϊ��ʱ�������޸�firstPoint��
    [HideInInspector]
    public List<Enemy> enemies = new List<Enemy>();

    //������Χ�ڵ����е�
    List<Vector3> points = new List<Vector3>();

    //������Ϯ�ĵ�
    //[HideInInspector]
    //public Vector3 firstPoint;

    private void Start()
    {
        //��ʼ��·���б�
        points = GetAllPointInGrid();
        //�����˴�����
        foreach (Vector3 point in points)
        {
            GameObject waveTrigger = Instantiate(waveTriggerPrefab, point, Quaternion.identity);
            waveTrigger.GetComponent<WaveTrigger>().SetTower(this);
            waveTrigger.GetComponent<WaveTrigger>().point = point;
            waveTrigger.transform.parent = transform; //����������Ϊ�����������壬�������
        }
    }

    public override void TowerAction()
    {
        if(enemies.Count == 0)
        {
            return;
        }
        //foreach (Vector3 p in points)
        //{
        //    Debug.Log(p);
        //}

        //������



        UpdateEnemies();//ˢ�µ����б�
    }

    //�����ˣ�Э�̣�
    public void SpawnWave()
    {
        if (enemies.Count == 0)
        {
            return;
        }


    }

    //ȷ����0��length-1��˳������˵��ƶ�˳��
    //firstPoint�ǵ�����Ϯ�ĵ�һ���㣬���˵��յ㣨����������Ҫ��������
    public void ResortPoint(Vector3 firstPoint)
    {
        List<Vector3> newPoints = new List<Vector3>();

        Vector3 currentPoint = firstPoint;
        
        newPoints.Add(firstPoint);

        //�ڵ�������ǰ���������
        while (newPoints.Count < points.Count)
        {
            foreach (Vector3 point in points)
            {
                if (newPoints.Contains(point))
                {
                    continue;
                }
                //���������ĸ��㿴һ��
                Vector3 upPosition = currentPoint + new Vector3(0, 1, 0); //�Ϸ�λ��
                Vector3 downPosition = currentPoint + new Vector3(0, -1, 0); //�·�λ��
                Vector3 leftPosition = currentPoint + new Vector3(-1, 0, 0); //��λ��
                Vector3 rightPosition = currentPoint + new Vector3(1, 0, 0); //�ҷ�λ��
                if (point.x == upPosition.x && point.y == upPosition.y)
                {
                    newPoints.Add(point);
                    currentPoint = point;
                }
                else if (point.x == downPosition.x && point.y == downPosition.y)
                {
                    newPoints.Add(point);
                    currentPoint = point;
                }
                else if (point.x == leftPosition.x && point.y == leftPosition.y)
                {
                    newPoints.Add(point);
                    currentPoint = point;
                }
                else if (point.x == rightPosition.x && point.y == rightPosition.y)
                {
                    newPoints.Add(point);
                    currentPoint = point;
                }
            }
        }
        //����˳��
        points = newPoints;
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
            float range = Mathf.Sqrt(Mathf.Pow(length / 2, 2) + Mathf.Pow(length, 2));
            if (distance > range)
            {
                enemies.Remove(enemy);
                continue;
            }
        }
    }

    //���������·���ϵĵ�
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
                if (tilemaps.Count != 0)
                {
                    foreach (Tilemap tilemap in tilemaps)
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
}
