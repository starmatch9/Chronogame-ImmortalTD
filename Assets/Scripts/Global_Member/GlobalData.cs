using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GlobalData
{
    //敌人列表
    public static List<Enemy> globalEnemies = new List<Enemy>();

    //路径列表
    public static List<Tilemap> globalRoads = new List<Tilemap>();

    //塔预制件列表
    public static List<GameObject> globalTowerPrefabs = new List<GameObject>();


    //枚举本身即为静态类型
    //在后续开发中，敌人的接受攻击函数，传入此攻击属性作为参数，达到抗性效果
    public enum AttackAttribute
    {
        JIN, MU, SHUI, HUO, TU
    }
}
