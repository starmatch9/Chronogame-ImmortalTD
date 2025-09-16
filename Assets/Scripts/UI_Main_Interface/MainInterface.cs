using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    //关卡列表
    [Header("关卡列表")]
    public List<LockCheck> levels = new List<LockCheck>();

    //退出游戏
    public void Exit_Game()
    {
        Application.Quit();
    }

    //关卡选择按钮(通过场景名称寻找)
    public void Select_Level(string name)
    {
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
