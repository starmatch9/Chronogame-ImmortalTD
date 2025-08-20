using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class LangTower : Tower
{
    //  -* 浪塔 *-

    //当前思路：加入位置检测，从敌人第一方格开始，反向推理得得到浪路径的尽头位置，在此生成浪 

    [TextArea]
    public string Tips = "注意：浪塔的“索敌范围”不生效。“行为间隔时间”是浪拍打一次的时间。";

    [Header("生成方格的边长，比如九宫格的边长为3。")]
    [Range(1, 5)] public int length = 3;

    //注意：路面列表需要单独放在路面管理器中，将其设置为全局静态变量，初始化时根据管理器中的路面进行添加
    [Header("路面列表（注意：这个参数后续要放到全局管理器中统一管理！！！）")]
    public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("浪触发器预制件")]
    public GameObject waveTriggerPrefab;

    [Header("浪预制件")]
    public GameObject wavePrefab;

    //维护所有进入浪塔范围的敌人列表
    //敌人列表不为空时，不能修改firstPoint。敌人列表为空时，可以修改firstPoint。
    [HideInInspector]
    public List<Enemy> enemies = new List<Enemy>();

    //浪塔范围内的所有点
    List<Vector3> points = new List<Vector3>();

    //敌人来袭的点
    //[HideInInspector]
    //public Vector3 firstPoint;

    private void Start()
    {
        //初始化路面列表
        points = GetAllPointInGrid();
        //生成浪触发器
        foreach (Vector3 point in points)
        {
            GameObject waveTrigger = Instantiate(waveTriggerPrefab, point, Quaternion.identity);
            waveTrigger.GetComponent<WaveTrigger>().SetTower(this);
            waveTrigger.GetComponent<WaveTrigger>().point = point;
            waveTrigger.transform.parent = transform; //将触发器设为浪塔的子物体，方便管理
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

        //生成浪



        UpdateEnemies();//刷新敌人列表
    }

    //生成浪（协程）
    public void SpawnWave()
    {
        if (enemies.Count == 0)
        {
            return;
        }


    }

    //确保从0到length-1的顺序就是浪的移动顺序
    //firstPoint是敌人来袭的第一个点，是浪的终点（所以浪生成要反着来）
    public void ResortPoint(Vector3 firstPoint)
    {
        List<Vector3> newPoints = new List<Vector3>();

        Vector3 currentPoint = firstPoint;
        
        newPoints.Add(firstPoint);

        //在到达数量前，往里面加
        while (newPoints.Count < points.Count)
        {
            foreach (Vector3 point in points)
            {
                if (newPoints.Contains(point))
                {
                    continue;
                }
                //上下左右四个点看一遍
                Vector3 upPosition = currentPoint + new Vector3(0, 1, 0); //上方位置
                Vector3 downPosition = currentPoint + new Vector3(0, -1, 0); //下方位置
                Vector3 leftPosition = currentPoint + new Vector3(-1, 0, 0); //左方位置
                Vector3 rightPosition = currentPoint + new Vector3(1, 0, 0); //右方位置
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
        //更新顺序
        points = newPoints;
    }

    //刷新敌人列表
    private void UpdateEnemies()
    {
        foreach (Enemy enemy in enemies)
        {
            //移除不需要攻击的敌人
            if (enemy.NoMoreShotsNeeded())
            {

            }
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            //勾股定理
            float range = Mathf.Sqrt(Mathf.Pow(length / 2, 2) + Mathf.Pow(length, 2));
            if (distance > range)
            {
                enemies.Remove(enemy);
                continue;
            }
        }
    }

    //获得所有在路面上的点
    public List<Vector3> GetAllPointInGrid()
    {
        List<Vector3> points = new List<Vector3>();

        //塔本身的位置
        float x = transform.position.x;
        float y = transform.position.y;
        //第一个点的位置
        float first_x = x - (length - 1) / 2;
        float first_y = y + (length - 1) / 2;
        //一共有  边长 * 边长  个点需要遍历  （如果是偶数个自动减去1）
        int l = ((length - 1) / 2) * 2 + 1;

        //外面决定第几行，里面决定第几列
        for (int i = 0; i < l; ++i)
        {
            for (int j = 0; j < l; ++j)
            {
                //每个点的坐标
                //现在是第i行第j列的点
                float point_x = first_x + i;
                float point_y = first_y - j; //注意：Y轴是向下的，所以要减去j

                Vector3 point = new Vector3(point_x, point_y, transform.position.z);
                //列表不为空时，判断是否在路面上
                if (tilemaps.Count != 0)
                {
                    foreach (Tilemap tilemap in tilemaps)
                    {
                        //如果在路面上
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
        float halfSize = (float)length / 2; //边长3，半边长1.5
        Vector3 center = transform.position;

        //定义四个角
        Vector3 topRight = center + new Vector3(halfSize, halfSize, 0);
        Vector3 topLeft = center + new Vector3(-halfSize, halfSize, 0);
        Vector3 bottomLeft = center + new Vector3(-halfSize, -halfSize, 0);
        Vector3 bottomRight = center + new Vector3(halfSize, -halfSize, 0);

        //设置Gizmos颜色
        Gizmos.color = Color.red;

        //绘制四条边
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
    }
}
