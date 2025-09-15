using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GlobalData
{
    //敌人列表
    public static List<Enemy> globalEnemies = new List<Enemy>();

    //塔列表
    public static List<Tower> towers = new List<Tower>();

    //基础塔列表
    public static List<Tower> towersInitial = new List<Tower>();

    //路径列表
    public static List<Tilemap> globalRoads = new List<Tilemap>();

    //塔预制件列表
    public static List<GameObject> globalTowerPrefabs = new List<GameObject>();

    //达到终点的敌人数量
    public static int FinalEnemyNumber = 0;

    //游戏失败时，到达终点的敌人数量
    public static int maxNumber = 5;

    //枚举本身即为静态类型
    //在后续开发中，敌人的接受攻击函数，传入此攻击属性作为参数，达到抗性效果
    
    //五大元素属性
    public enum ElementAttribute
    {
        JIN, MU, SHUI, HUO, TU, NONE
    }

    //魔法攻击，物理攻击，真伤（None）
    public enum AttackAttribute
    {
        Magic, Physics, None
    }

    //最大数量是maxNumber，超过这个数游戏结束
    public static void CheckFinalEnemy()
    {
        if( FinalEnemyNumber < maxNumber)
        {
            return;
        }
        //宣告游戏结束
        //
        //
        Debug.Log("你失败了");

    }

    //重置所有静态变量
    public static void ResetAllData()
    {
        //敌人列表
        globalEnemies = new List<Enemy>();

        //塔列表
        towers = new List<Tower>();

        //基础塔列表
        towersInitial = new List<Tower>();

        //路径列表
        globalRoads = new List<Tilemap>();

        //塔预制件列表
        globalTowerPrefabs = new List<GameObject>();

        //达到终点的敌人数量
        FinalEnemyNumber = 0;

        //游戏失败时，到达终点的敌人数量
        maxNumber = 5;
    }
}
