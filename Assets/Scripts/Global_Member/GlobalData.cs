using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public static class GlobalData
{
    public static MonoBehaviour mono;

    //敌人列表
    public static List<Enemy> globalEnemies = new List<Enemy>();

    //塔列表
    public static List<Tower> towers = new List<Tower>();

    //基础塔列表
    //public static List<Tower> towersInitial = new List<Tower>();

    //路径列表
    public static List<Tilemap> globalRoads = new List<Tilemap>();

    //塔预制件列表
    public static List<GameObject> globalTowerPrefabs = new List<GameObject>();

    //达到终点的敌人数量
    public static int FinalEnemyNumber = 0;

    //游戏失败时，到达终点的敌人数量
    public static int maxNumber = 5;

    //游戏的鼠标是否处于可以点击塔的状态
    public static bool towerClick = true;


    //这下面是游戏结时用到的东西
    public static StopGame sg = null;

    //[Header("下一关场景名称")]
    public static string nextName = "";

    //[Header("成功弹窗游戏对象")]
    public static GameObject passWindow = null;

    //[Header("失败弹窗游戏对象")]
    public static GameObject noPassWindow = null;

    //[Header("提示弹窗游戏对象")]
    public static GameObject tipWindow = null;

    //[Header("价格窗口游戏对象")]
    public static GameObject priceWindow = null;

    //技能：炸弹
    public static Bomb bomb = null;

    //技能：连接
    public static Link link = null;


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
        //
        //宣告游戏结束
        //
        NoPass();

    }

    //重置所有静态变量
    public static void ResetAllData()
    {
        //敌人列表
        globalEnemies = new List<Enemy>();

        //塔列表
        towers = new List<Tower>();

        //基础塔列表
        //towersInitial = new List<Tower>();

        //路径列表
        globalRoads = new List<Tilemap>();

        //塔预制件列表
        globalTowerPrefabs = new List<GameObject>();

        //达到终点的敌人数量
        FinalEnemyNumber = 0;

        //游戏失败时，到达终点的敌人数量
        maxNumber = 5;

        //塔点击许可
        towerClick = true;
    }


    //成功通过的逻辑
    public static void Pass()
    {
        //解锁下一关
        //暂停->弹窗->回到主菜单或下一关
        PlayerPrefs.SetInt(nextName, 1);
        PlayerPrefs.Save();

        //咋瓦鲁多！
        sg.TheWorld();

        passWindow.SetActive(true);
    }

    //失败的逻辑
    public static void NoPass()
    {
        //暂停->弹窗->回到主菜单或重新开始

        //咋瓦鲁多！
        sg.TheWorld();

        noPassWindow.SetActive(true);

    }


    //弹出提示方法
    static Coroutine tipCoroutine = null;
    public static void JumpTip(string text)
    {
        TextMeshProUGUI textComponent = tipWindow.GetComponentInChildren<TextMeshProUGUI>();
        textComponent.text = text;

        if(tipCoroutine != null)
        {

            mono.StopCoroutine(tipCoroutine);
            tipCoroutine = null;
        }
        tipCoroutine = mono.StartCoroutine(Jump());
    }
    public static System.Collections.IEnumerator Jump()
    {
        //修改局部坐标而非世界坐标
        Vector3 original = Vector3.zero;
        tipWindow.transform.localPosition = original;
        float timer = 0;
        //一秒钟如何
        while (timer < 0.5f)
        {
            //从0到-200插值
            float y = Mathf.Lerp(0, -200, timer / 0.5f);
            Vector3 v = new Vector3 (tipWindow.transform.localPosition.x, y, tipWindow.transform.localPosition.z);
            tipWindow.transform.localPosition = v;

            timer += Time.deltaTime;
            yield return null;
        }
        tipWindow.transform.localPosition = new Vector3(tipWindow.transform.localPosition.x, -200, tipWindow.transform.localPosition.z);
        yield return new WaitForSeconds(2f);
        timer = 0;
        //一秒钟如何
        while (timer < 0.5f)
        {
            //从-200到0插值
            float y = Mathf.Lerp(-200, 0, timer / 0.5f);
            Vector3 v = new Vector3(tipWindow.transform.localPosition.x, y, tipWindow.transform.localPosition.z);
            tipWindow.transform.localPosition = v;

            timer += Time.deltaTime;
            yield return null;
        }
        tipWindow.transform.localPosition = new Vector3(tipWindow.transform.localPosition.x, 0, tipWindow.transform.localPosition.z);

        tipCoroutine = null;
    }
}
