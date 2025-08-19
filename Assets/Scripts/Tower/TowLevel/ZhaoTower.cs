using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class ZhaoTower : Tower
{
    //  *--   沼泽塔   --*

    //沼泽塔在9宫格的范围内生成沼泽地面，检测到路面直接生成，没有其余动作
    [TextArea]
    public string Tips = "注意：沼泽塔的“索敌范围”和“间隔时间”不生效。";

    [Header("生成方格的边长，比如九宫格的边长为3")]
    [Range(1, 5)] public int length = 3;

    [Header("减速为原来的多少，百分比")]
    [Range(0f, 1f)] public float slowFactor = 0.3f;

    //注意：路面列表需要单独放在路面管理器中，将其设置为全局静态变量，初始化时根据管理器中的路面进行添加
    [Header("路面列表（注意：这个参数后续要放到全局管理器中统一管理！！！）")]
    public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("泥潭方格预制件")]
    public GameObject mudPrefab;


    public override void Update(){}
    public override void TowerAction() { }

    public void Start()
    {
        //生成沼泽地面
        SpawnMud();
    }

    //生成沼泽地面
    public void SpawnMud()
    {
        //获取九宫格内的所有点
        List<Vector3> points = GetAllPointInGrid();
        //遍历每个点
        foreach (Vector3 point in points)
        {
            //生成泥潭方格
            GameObject mud = Instantiate(mudPrefab, point, Quaternion.identity);
        }
    }


    //获得九宫格内的所有点
    public List<Vector3> GetAllPointInGrid()
    {
        List<Vector3> points = new List<Vector3>();

        //塔本身的位置
        float x = transform.position.x;
        float y = transform.position.y;
        //第一个点的位置
        float first_x = x - length / 2;
        float first_y = y + length / 2;

        //一共有  边长 * 边长  个点需要遍历  （如果是偶数个自动减去1）
        
        //外面决定第几行，里面决定第几列
        for (float i_y = first_y; i_y > y - length / 2; i_y -= 1f)
        {
            for (float i_x = first_x; i_x < x + length / 2; x += 1f)
            {
                //每个点的坐标
                Vector3 point = new Vector3(i_x, i_y, transform.position.z);
                //列表不为空时，判断是否在路面上
                if (tilemaps.Count != 0)
                {
                    foreach (Tilemap tilemap in tilemaps)
                    {
                        //如果在路面上
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

    //不同于基类的圆形范围，这里是一个正方形的范围
    public override void OnDrawGizmos()
    {
        float halfSize = (float)length / 2; // 边长3，半边长1.5
        Vector3 center = transform.position;

        // 定义四个角（2D XY平面）
        Vector3 topRight = center + new Vector3(halfSize, halfSize, 0);
        Vector3 topLeft = center + new Vector3(-halfSize, halfSize, 0);
        Vector3 bottomLeft = center + new Vector3(-halfSize, -halfSize, 0);
        Vector3 bottomRight = center + new Vector3(halfSize, -halfSize, 0);

        // 设置Gizmos颜色
        Gizmos.color = Color.red;

        // 绘制四条边
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
    }

}
