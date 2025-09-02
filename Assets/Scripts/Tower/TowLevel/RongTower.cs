using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEngine.GraphicsBuffer;

public class RongTower : Tower
{
    /*  -熔岩塔 （烫脚塔）-  */

    [TextArea]
    public string Tips = "注意：熔岩塔的“索敌范围”不生效。";

    [Header("生成方格的边长，比如九宫格的边长为3。")]
    [Range(1, 5)] public int length = 3;

    //注意：路面列表需要单独放在路面管理器中，将其设置为全局静态变量，初始化时根据管理器中的路面进行添加
    //[Header("路面列表（注意：这个参数后续要放到全局管理器中统一管理！！！）")]
    //public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("熔岩方格预制件")]
    public GameObject lavaPrefab;

    [Header("熔岩存在时长")]
    [Range(0, 15f)]public float lavaDuration = 5f;

    [Header("熔岩烫脚间隔时间")]
    [Range(0, 5f)] public float lavaAttackInterval = 0.75f;

    [Header("熔岩烫脚伤害")]
    [Range(0, 100f)] public float lavaAttack = 0.75f;


    //管理所有踩在熔岩上的敌人
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

    //熔岩的攻击
    void LavaAttack()
    {
        //列表本体总是变化，所以需要复制一份
        List<Enemy> enemiesCopy = new List<Enemy>(enemies);

        foreach (Enemy enemy in enemiesCopy)
        {
            //跳过不需要攻击的敌人(一定不能忘了这段)
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.AcceptAttack(lavaAttack);
        }
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
            float range = Mathf.Sqrt(Mathf.Pow(length/2, 2) + Mathf.Pow(length, 2));
            if (distance > range)
            {
                enemies.Remove(enemy);
                continue;
            }
        }
    }

    //生成沼泽地面
    public void SpawnLava()
    {
        //获取九宫格内的所有点
        List<Vector3> points = GetAllPointInGrid();

        //遍历每个点
        foreach (Vector3 point in points)
        {
            //生成熔岩方格
            GameObject lava = Instantiate(lavaPrefab, point, Quaternion.identity);
            lava.GetComponent<Lava>().SetTower(this);

            //开始熔岩的生命周期
            StartCoroutine(LavaLifetime(lava));
        }
        //生成熔岩的同时，开始烫脚周期
        StartCoroutine(LavaAttackLife());
    }

    IEnumerator LavaAttackLife()
    {
        //大计时器记录生命时长
        //小计时器记录攻击间隔
        float bigTimer = 0f;
        float smallTimer = 0f;

        while (bigTimer < lavaDuration)
        {
            if (smallTimer >= lavaAttackInterval)
            {
                LavaAttack();

                smallTimer = 0f; //重置小计时器
            }
            yield return null;
            bigTimer += Time.deltaTime;
            smallTimer += Time.deltaTime;
        }
    }

    //熔岩的生命周期
    private IEnumerator LavaLifetime(GameObject lava)
    {
        yield return new WaitForSeconds(lavaDuration);

        //销毁熔岩
        Destroy(lava);
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
            for (int j = 0; j < l; ++j)
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

    //绘制索敌范围
    public override void DrawAttackArea()
    {
        attackObject = Instantiate(GlobalTowerFunction.SquareArea, transform.position, Quaternion.identity);

        attackObject.transform.localScale = Vector3.one * length;
    }
}
