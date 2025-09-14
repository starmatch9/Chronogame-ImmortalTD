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
    public string Tips = "注意：沼泽塔的“索敌范围”不生效。";

    [Header("生成方格的边长，比如九宫格的边长为3")]
    [Range(1, 5)] public int length = 3;

    [Header("减速为原来的多少，百分比")]
    [Range(0f, 1f)] public float slowFactor = 0.3f;

    //这是进入受击范围的敌人列表
    [HideInInspector]
    public List<Enemy> enemyList = new List<Enemy>();

    //注意：路面列表需要单独放在路面管理器中，将其设置为全局静态变量，初始化时根据管理器中的路面进行添加
    //[Header("路面列表（注意：这个参数后续要放到全局管理器中统一管理！！！）")]
    //public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("泥潭方格预制件")]
    public GameObject mudPrefab;

    [Header("伤害")]
    [Range(0f, 50f)] public float mudAttack = 20f;

    public override void TowerAction() {

        //刷新后进行攻击方法
        UpdateEnemies();
        MudAttack();
    }

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
            mud.GetComponent<Mud>().SetTower(this);
            mud.gameObject.GetComponent<Mud>().SetSlowFactor(slowFactor);
        }
    }


    //刷新敌人列表
    private void UpdateEnemies()
    {
        foreach (Enemy enemy in enemyList)
        {
            //移除不需要攻击的敌人
            if (enemy.NoMoreShotsNeeded())
            {
                enemyList.Remove(enemy);
                continue;
            }
            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            //勾股定理
            float range = Mathf.Sqrt(Mathf.Pow(length / 2, 2) + Mathf.Pow(length, 2));
            if (distance > range)
            {
                enemyList.Remove(enemy);
                continue;
            }
        }
    }

    void MudAttack()
    {
        //列表本体总是变化，所以需要复制一份
        List<Enemy> enemiesCopy = new List<Enemy>(enemyList);

        foreach (Enemy enemy in enemiesCopy)
        {
            //跳过不需要攻击的敌人(一定不能忘了这段)
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.AcceptAttack(mudAttack);
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
        float first_x = x - (length - 1) / 2;
        float first_y = y + (length - 1) / 2;

        //一共有  边长 * 边长  个点需要遍历  （如果是偶数个自动减去1）
        int l = ((length - 1) / 2) * 2 + 1;

        //外面决定第几行，里面决定第几列
        for (int i = 0; i < l; ++i)
        {
            for (int j = 0; j <  l; ++j)
            {
                //每个点的坐标
                //现在是第i行第j列的点
                float point_x = first_x + i;
                float point_y = first_y - j; //注意：Y轴是向下的，所以要减去j

                Vector3 point = new Vector3(point_x, point_y, transform.position.z);
                //列表不为空时，判断是否在路面上
                if (GlobalData.globalRoads.Count != 0)
                {
                    foreach (Tilemap tilemap in GlobalData.globalRoads)
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

    //不同于基类的圆形范围，这里是一个正方形的范围
    public override void OnDrawGizmos()
    {
        float halfSize = (float)length / 2;
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

    ////绘制索敌范围
    //public override void DrawAttackArea()
    //{
    //    attackObject = Instantiate(GlobalTowerFunction.SquareArea, transform.position, Quaternion.identity);

    //    attackObject.transform.localScale = Vector3.one * length;
    //}

}
