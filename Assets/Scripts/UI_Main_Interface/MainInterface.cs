using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    //关卡列表
    [Header("关卡列表")]
    public List<LockCheck> levels = new List<LockCheck>();


    //游戏开始默认开启第一关
    private void Start()
    {
        //Set+Save保存数据
        PlayerPrefs.SetInt("Lv1", 1);
        PlayerPrefs.Save();
    }


    //退出游戏
    public void Exit_Game()
    {
        //清除保存的数据
        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();

        Application.Quit();
    }

    //关卡选择按钮(通过场景名称寻找)
    public void Select_Level(string name)
    {
        //重置所有静态变量
        GlobalData.ResetAllData();
        GlobalElementPowerFunction.ResetAllData();
        GlobalEnemyGroupFunction.ResetAllData();

        SceneManager.LoadScene(name);
    }

    public void CheckLocks()
    {
        foreach (var lockCheck in levels)
        {
            lockCheck.check();

        }
    }


}
