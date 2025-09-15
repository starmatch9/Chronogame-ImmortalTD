using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopGame : MonoBehaviour
{
    //咋瓦鲁多！！！！！！
    public void TheWorld()
    {
        //所有生成器停止生成！！
        if(GlobalEnemyGroupFunction.item != null)
        {
            foreach(EnemySpawn spawn in GlobalEnemyGroupFunction.item.enemySpawnGroup)
            {
                spawn.enabled = false;
            }
        }

        //所有敌人停止移动！！！
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            Move move = enemy.gameObject.GetComponent<Move>();
            move.StopMove();
        }

        //所有敌人停止行为！！！
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.StopAction();
        }

        //所有基础塔暂停！！！
        foreach (Tower towerInitial in GlobalData.towersInitial)
        {
            towerInitial.enabled = false;
        }

        //所有二级塔暂停！！！
        foreach(Tower tower in GlobalData.towers)
        {
            tower.enabled = false;
        }
    }
    
    public void BackGame()
    {
        //所有生成器启动！！
        if (GlobalEnemyGroupFunction.item != null)
        {
            foreach (EnemySpawn spawn in GlobalEnemyGroupFunction.item.enemySpawnGroup)
            {
                spawn.enabled = true;
            }

            //GlobalEnemyGroupFunction.item.enemySpawn.enabled = true;
        }

        //所有敌人j继续移动！！！
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            Move move = enemy.gameObject.GetComponent<Move>();
            move.ContinueMove();
        }

        //所有敌人继续行为！！！
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            enemy.ContinueAction();
        }

        //所有基础塔启动！！！
        foreach (Tower towerInitial in GlobalData.towersInitial)
        {
            towerInitial.enabled = true;
        }

        //所有二级塔启动！！！
        foreach (Tower tower in GlobalData.towers)
        {
            tower.enabled = true;
        }
    }

    //
    //重要：场景发生变化时，静态变量不受影响，所以每次需要重置！！！！！！不管是下一关还是重新加载什么的！！！
    //
    public void ReStartGame()
    {
        //重置所有静态变量
        GlobalData.ResetAllData();
        GlobalElementPowerFunction.ResetAllData();
        GlobalEnemyGroupFunction.ResetAllData();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //这里是返回标题，这里先退出
    public void BackToTitle()
    {
        Application.Quit();
    }
}
