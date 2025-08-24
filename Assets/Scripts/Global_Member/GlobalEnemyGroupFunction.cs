using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalEnemyGroupFunction
{
    //维护一个波次条目列表
    public static List<EnemyGroupItem> enemyGroupItems;

    //维护相关UI脚本，需要控制其激活状态，用于显示按钮与否
    public static GameObject nextOneBigButton;

    //维护一个指向列表条目的索引
    static int index = 0;

    //维护当前波次，是当前，当前！
    public static EnemyGroupItem item;

    //维护一个生命周期，确保敌人的生成能够设置时长有关方法
    public static MonoBehaviour mono;


    //开始释放一波敌人
    public static void StartOneEnemyGroup()
    {
        if(index >= enemyGroupItems.Count)
        {
            return;
        }
        item = enemyGroupItems[index];
        //开始释放敌人
        //item.enemySpawn.Switch();
        mono.StartCoroutine(DispatchEnemy());

        //关闭按钮
        CloseButton();

        ++index;
    }

    //加入时间差释放的敌人
    public static IEnumerator DispatchEnemy()
    {
        foreach(EnemySpawn spawn in item.enemySpawnGroup)
        {
            spawn.Switch();

            yield return new WaitForSeconds(item.timeDiff);  
        }
    }


    //检测波次是否结束
    public static void CheckEnd()
    {
        //检查是否全局敌人列表中的敌人是否处于激活状态
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            //有一个还激活着，就return
            if (enemy.gameObject.activeInHierarchy)
            {
                return;
            }
        }

        EndEnemyGroup();
    }

    //为本波次收尾
    public static void EndEnemyGroup() 
    { 
        if (index >= enemyGroupItems.Count)
        {
            Debug.Log("关卡已经结束了很后悔就好哈哈哈哈哈");
            return;
        }

        //获取本波金额
        GlobalElementPowerFunction.AddCount(item.elementPower);

        //清空敌人列表
        GlobalData.globalEnemies.Clear();

        //展开UI按钮
        OpenButton();
    }


    //显示按钮
    public static void OpenButton()
    {
        nextOneBigButton.SetActive(true);
    }
    //不显示按钮
    public static void CloseButton() { 
        nextOneBigButton.SetActive(false);
    }

    //重置所有静态变量
    public static void ResetAllData()
    {
        //维护一个波次条目列表
        enemyGroupItems = new List<EnemyGroupItem>();

        //维护相关UI脚本，需要控制其激活状态，用于显示按钮与否
        nextOneBigButton = null;

        //维护一个指向列表条目的索引
        index = 0;

        //维护当前波次，是当前，当前！
        item = null;
    }
}
