using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class LangTower : Tower
{
    //  -* 浪塔 *-

    //当前思路：加入位置检测，从敌人第一方格开始，反向推理得得到浪路径的尽头位置，在此生成浪 

    [TextArea]
    public string Tips = "注意：“行为间隔时间”是子弹生成的时间。";

    [Header("生成方格的边长，比如九宫格的边长为3。")]
    [Range(1, 5)] public int length = 3;

    [Header("子弹预制件")]
    public GameObject bulletPref;

    //注意：路面列表需要单独放在路面管理器中，将其设置为全局静态变量，初始化时根据管理器中的路面进行添加
    //[Header("路面列表（注意：这个参数后续要放到全局管理器中统一管理！！！）")]
    //public List<Tilemap> tilemaps = new List<Tilemap>();

    [Header("浪触发器预制件")]
    public GameObject waveTriggerPrefab;

    [Header("浪生成间隔")]
    [Range(0, 20f)] public float waveDuration;

    [Header("浪预制件")]
    public GameObject wavePrefab;

    [Header("浪伤害")]
    [Range(0f, 100f)]public int waveAttack = 15;

    [Header("回退速度")]
    [Range(0, 5f)] public float backSpeed = 1f;

    [Header("回退时间")]
    [Range(0, 2f)] public float backTime = 0.4f;

    [Header("浪的攻击属性")]
    public GlobalData.AttackAttribute attackAttribute = GlobalData.AttackAttribute.None;

    [Header("浪的元素属性")]
    public GlobalData.ElementAttribute elementAttribute = GlobalData.ElementAttribute.NONE;

    //维护所有进入浪塔范围的敌人列表
    //敌人列表不为空时，不能修改firstPoint。敌人列表为空时，可以修改firstPoint。
    [HideInInspector]
    public List<Enemy> enemies = new List<Enemy>();

    //浪塔范围内的所有点（按TileMap区分）
    Dictionary<Tilemap, List<Vector3>> Tilemap_Points = new Dictionary<Tilemap, List<Vector3>>();

    //List<Vector3> points = new List<Vector3>();

    //敌人来袭的点
    //[HideInInspector]
    //public Vector3 firstPoint;

    private void Start()
    {
        //初始化路面列表
        Tilemap_Points = GetAllPointInGrid();
        //生成浪触发器
        foreach(Tilemap t in Tilemap_Points.Keys)
        {
            //在每个地图组中的点组
            foreach (Vector3 point in Tilemap_Points[t])
            {
                GameObject waveTrigger = Instantiate(waveTriggerPrefab, point, Quaternion.identity);
                waveTrigger.GetComponent<WaveTrigger>().SetTower(this);
                waveTrigger.GetComponent<WaveTrigger>().point = point;
                waveTrigger.GetComponent<WaveTrigger>().road = t;
                waveTrigger.transform.parent = transform; //将触发器设为浪塔的子物体，方便管理
            }
        }
    }

    float timer0 = 0f; //计时器
    public override void Update()
    {
        //timer记录浪生成间隔
        timer += Time.deltaTime;
        if (timer >= waveDuration)
        {
            ExecuteAction();
            timer = 0f; //重置计时器
        }
        //timer记录子弹间隔
        timer0 += Time.deltaTime;
        if (timer0 >= actionTime)
        {
            if (FindClosestToFinishEnemy() == null)
            {
                return;
            }
            GameObject target = FindClosestToFinishEnemy().gameObject;
            Shoot(target);
            timer0 = 0f; //重置计时器
        }
    }
    //发射子弹，及生成子弹实例
    void Shoot(GameObject enemy)
    {
        // 偏移 ：子弹在塔上方1.5米的位置发射
        Vector3 offset = new Vector3(0, 1f, 0);

        //实例化子弹
        GameObject bullet = Instantiate(bulletPref, transform.position + offset, Quaternion.identity);

        //需要锚定子弹的目标，获取子弹的行为脚本
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        //设置攻击伤害
        bulletScript.baseAttack = bulletAttack;

        bulletScript.SetTarget(enemy);
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
        SpawnWave();

        //推
        PushEnemies();

        UpdateEnemies();//刷新敌人列表
    }

    public void PushEnemies()
    {
        //防止发生变化得复制一份（new是复制！！不然就是引用了！！）
        List<Enemy> enemiesCopy  = new List<Enemy>(enemies);

        foreach (Enemy enemy in enemiesCopy)
        {
            if (enemy == null)
            {
                continue;
            }

            //敌人接受伤害
            enemy.AcceptAttack(waveAttack, attackAttribute, elementAttribute);

            Move move = enemy.gameObject.GetComponent<Move>();
            if (!move.isStopMove)
            {
                GlobalEnemyGroupFunction.mono.StartCoroutine(EnemyBack(enemy));
            }
        }
    }

    IEnumerator EnemyBack(Enemy enemy) {

        GlobalMusic.PlayOnce(GlobalMusic._Wave);

        //及时止损
        Move move = enemy.gameObject.GetComponent<Move>();
        if (move.isStopMove)
        {
            yield break;
        }
        //减速因子
        float slowFactor = -backSpeed;
        move.ChangeSpeed(slowFactor);

        float timer = 0f;
        while (timer < backTime) {

            if (enemy == null)
            {
                yield break;
            }

            if (move.isStopMove)
            {
                move.SetSpeedFactor(1f);
                yield break;
            }

            //推的过程中发现推出路面了
            if (move.noBackRoad())
            {
                move.ChangeSpeed(0f);
            }
        
            yield return null;
            timer += Time.deltaTime;
        } 

        move.ResetSpeed();
    }

    //生成浪（协程）actionTime的时间从point[Count-1]到point[0]
    public void SpawnWave()
    {
        foreach (Tilemap t in Tilemap_Points.Keys)
        {
            if (BeIncluded(t))
            {
                continue;
            }
            GlobalEnemyGroupFunction.mono.StartCoroutine(SpawnOneWave(t));

        }
    }

    public IEnumerator SpawnOneWave(Tilemap t)
    {
        //先把预制件放出了
        List<Vector3> points = Tilemap_Points[t];
        GameObject wave = Instantiate(wavePrefab, points[points.Count - 1], Quaternion.identity);

        //points的数量代表point-1的线段，每个线段的用时为actionTime / point - 1
        float partTime = actionTime / (points.Count - 1);
        int index = points.Count - 1;
        while (index > 0)
        {
            float timer = 0;
            while (timer < partTime)
            {
                wave.transform.position = Vector3.Lerp(points[index], points[index - 1], timer / partTime);

                timer += Time.deltaTime;
                yield return null;
            }
            --index;
        }

        Destroy(wave);
    }

    //确保从0到length-1的顺序就是浪的移动顺序
    //firstPoint是敌人来袭的第一个点，是浪的终点（所以浪生成要反着来）
    public void ResortPoint(Vector3 firstPoint, Tilemap t)
    {
        List<Vector3> newPoints = new List<Vector3>();

        Vector3 currentPoint = firstPoint;
        
        newPoints.Add(firstPoint);

        //在到达数量前，往里面加
        while (newPoints.Count < Tilemap_Points[t].Count)
        {
            foreach (Vector3 point in Tilemap_Points[t])
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
        Tilemap_Points[t] = newPoints;
    }

    //刷新敌人列表
    private void UpdateEnemies()
    {
        //避免“遍历时修改数组”的两种解决办法
        //1、倒叙遍历。因为报错的本质是数组的索引发生了变化
        //2、复制数组。

        for (int i = enemies.Count - 1; i >= 0; i--)
        {
            Enemy enemy = enemies[i];

            // 移除不需要攻击的敌人
            if (enemy == null || enemy.NoMoreShotsNeeded())
            {
                enemies.RemoveAt(i);
                continue;
            }

            float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
            float range = Mathf.Sqrt(Mathf.Pow(length / 2, 2) + Mathf.Pow(length, 2));

            if (distance > range)
            {
                enemies.RemoveAt(i);
                continue;
            }
        }
        //List<Enemy> enemiesCopy = new List<Enemy>(enemies);
        //foreach (Enemy enemy in enemiesCopy)
        //{
        //    //移除不需要攻击的敌人
        //    if (enemy == null || enemy.NoMoreShotsNeeded())
        //    {
        //        enemiesCopy.Remove(enemy);
        //        continue;
        //    }
        //    float distance = Vector2.Distance(transform.position, enemy.GetGameObject().transform.position);
        //    //勾股定理
        //    float range = Mathf.Sqrt(Mathf.Pow(length / 2, 2) + Mathf.Pow(length, 2));
        //    if (distance > range)
        //    {
        //        enemiesCopy.Remove(enemy);
        //        continue;
        //    }
        //}
        //enemies = new List<Enemy>(enemiesCopy);
    }

    //获得所有在路面上的点
    public Dictionary<Tilemap, List<Vector3>> GetAllPointInGrid()
    {
        Dictionary<Tilemap, List<Vector3>> points = new Dictionary<Tilemap, List<Vector3>>();

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
                        if (tilemap.HasTile(tilemap.WorldToCell(point)))
                        {
                            //确保有这个路面的键
                            if (!points.ContainsKey(tilemap))
                            {
                                points[tilemap] = new List<Vector3>();
                            }
                            //然后再加
                            if (!points[tilemap].Contains(point))
                            {
                                points[tilemap].Add(point);
                            }
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
        Gizmos.color = UnityEngine.Color.red;

        //绘制四条边
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);

        base.OnDrawGizmos();
    }

    public bool BeIncluded(Tilemap currentTilemap)
    {
        foreach (Tilemap t in Tilemap_Points.Keys)
        {
            //跳过一样的
            if (t != currentTilemap)
            {
                //防止修改，采用复制
                List<Vector3> pointsA = new List<Vector3>(Tilemap_Points[currentTilemap]);
                List<Vector3> pointsB = new List<Vector3>(Tilemap_Points[t]);

                List<Vector3> pointsHave = new List<Vector3>();
                foreach (Vector3 pA in pointsA)
                {
                    //如果每个点都存在，那就被包含了
                    foreach(Vector3 pB in pointsB)
                    {
                        if(pA == pB)
                        {
                            pointsHave.Add(pA);
                            break;
                        }

                    }
                }
                if(pointsA.Count == pointsHave.Count)
                {
                    return true;
                }
            }
        }
        return false;
    }


    ////绘制索敌范围
    //public override void DrawAttackArea()
    //{
    //    attackObject = Instantiate(GlobalTowerFunction.SquareArea, transform.position, Quaternion.identity);

    //    attackObject.transform.localScale = Vector3.one * length;

    //    //base.DrawAttackArea();
    //}
}
