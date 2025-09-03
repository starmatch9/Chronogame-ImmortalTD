using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopGame : MonoBehaviour
{
    //զ��³�࣡����������
    public void TheWorld()
    {
        //����������ֹͣ���ɣ���
        if(GlobalEnemyGroupFunction.item != null)
        {
            foreach(EnemySpawn spawn in GlobalEnemyGroupFunction.item.enemySpawnGroup)
            {
                spawn.enabled = false;
            }
        }

        //���е���ֹͣ�ƶ�������
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            Move move = enemy.gameObject.GetComponent<Move>();
            move.StopMove();
        }
        
        //���л�������ͣ������
        foreach(Tower towerInitial in GlobalData.towersInitial)
        {
            towerInitial.enabled = false;
        }

        //���ж�������ͣ������
        foreach(Tower tower in GlobalData.towers)
        {
            tower.enabled = false;
        }
    }
    
    public void BackGame()
    {
        //������������������
        if (GlobalEnemyGroupFunction.item != null)
        {
            foreach (EnemySpawn spawn in GlobalEnemyGroupFunction.item.enemySpawnGroup)
            {
                spawn.enabled = true;
            }

            //GlobalEnemyGroupFunction.item.enemySpawn.enabled = true;
        }

        //���е���j�����ƶ�������
        foreach (Enemy enemy in GlobalData.globalEnemies)
        {
            if (enemy.NoMoreShotsNeeded())
            {
                continue;
            }
            Move move = enemy.gameObject.GetComponent<Move>();
            move.ContinueMove();
        }

        //���л���������������
        foreach (Tower towerInitial in GlobalData.towersInitial)
        {
            towerInitial.enabled = true;
        }

        //���ж���������������
        foreach (Tower tower in GlobalData.towers)
        {
            tower.enabled = true;
        }
    }

    //
    //��Ҫ�����������仯ʱ����̬��������Ӱ�죬����ÿ����Ҫ���ã�������������������һ�ػ������¼���ʲô�ģ�����
    //
    public void ReStartGame()
    {
        //�������о�̬����
        GlobalData.ResetAllData();
        GlobalElementPowerFunction.ResetAllData();
        GlobalEnemyGroupFunction.ResetAllData();

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    //�����Ƿ��ر��⣬�������˳�
    public void BackToTitle()
    {
        Application.Quit();
    }
}
