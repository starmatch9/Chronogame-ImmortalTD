using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ZhaoTower : Tower
{
    //  *--   ������   --*

    //��������9����ķ�Χ������������棬��⵽·��ֱ�����ɣ�û�����ද��
    [TextArea]
    public string Tips = "ע�⣺�������ġ����з�Χ���͡����ʱ�䡱����Ч��";

    [Header("���ɷ���ı߳�������Ź���ı߳�Ϊ3")]
    [Range(1, 5)] public int length = 3;

    [Header("����Ϊԭ���Ķ��٣��ٷֱ�")]
    [Range(0f, 1f)] public float slowFactor = 0.3f;

    //ע�⣺·���б���Ҫ��������·��������У���������Ϊȫ�־�̬��������ʼ��ʱ���ݹ������е�·��������
    [Header("·���б�ע�⣺�����������Ҫ�ŵ�ȫ�ֹ�������ͳһ����������")]
    public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("��̶����Ԥ�Ƽ�")]
    public GameObject mudPrefab;


    public override void Update(){}
    public override void TowerAction() { }

    public void Start()
    {
        //�����������
        SpawnMud();
    }

    //�����������
    public void SpawnMud()
    {
        //��ȡ�Ź����ڵ����е�
        List<Vector3> points = GetAllPointInGrid();
        //����ÿ����
        foreach (Vector3 point in points)
        {
            //������̶����
            GameObject mud = Instantiate(mudPrefab, point, Quaternion.identity);
        }
    }


    //��þŹ����ڵ����е�
    public List<Vector3> GetAllPointInGrid()
    {
        List<Vector3> points = new List<Vector3>();

        //�������λ��
        float x = transform.position.x;
        float y = transform.position.y;
        //��һ�����λ��
        float first_x = x - length / 2;
        float first_y = y + length / 2;

        //һ����  �߳� * �߳�  ������Ҫ����  �������ż�����Զ���ȥ1��
        
        //��������ڼ��У���������ڼ���
        for (float i_y = first_y; i_y > y - length / 2; i_y -= 1f)
        {
            for (float i_x = first_x; i_x < x + length / 2; x += 1f)
            {
                //ÿ���������
                Vector3 point = new Vector3(i_x, i_y, transform.position.z);
                //�б�Ϊ��ʱ���ж��Ƿ���·����
                if (tilemaps.Count != 0)
                {
                    foreach (Tilemap tilemap in tilemaps)
                    {
                        //�����·����
                        if (tilemap.HasTile(tilemap.WorldToCell(point)))
                        {
                            points.Add(point);
                        }
                    }
                }
            }
        }
        return points;
    }

    //��ͬ�ڻ����Բ�η�Χ��������һ�������εķ�Χ
    public override void OnDrawGizmos()
    {
        float halfSize = (float)length / 2; // �߳�3����߳�1.5
        Vector3 center = transform.position;

        // �����ĸ��ǣ�2D XYƽ�棩
        Vector3 topRight = center + new Vector3(halfSize, halfSize, 0);
        Vector3 topLeft = center + new Vector3(-halfSize, halfSize, 0);
        Vector3 bottomLeft = center + new Vector3(-halfSize, -halfSize, 0);
        Vector3 bottomRight = center + new Vector3(halfSize, -halfSize, 0);

        // ����Gizmos��ɫ
        Gizmos.color = Color.red;

        // ����������
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
    }

}
