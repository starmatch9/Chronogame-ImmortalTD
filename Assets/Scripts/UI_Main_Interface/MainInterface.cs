using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainInterface : MonoBehaviour
{
    [Header("按钮音效")]
    public AudioSource b = null;

    [Header("翻页音效")]
    public AudioSource p = null;




    //关卡列表
    [Header("关卡列表")]
    public List<LockCheck> levels = new List<LockCheck>();


    //游戏开始默认开启第一关
    private void Start()
    {
        GlobalMusic._Button = b;
        GlobalMusic._Page = p;

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
        GlobalMusic._Button.Play();

        //重置所有静态变量
        GlobalData.ResetAllData();
        GlobalLink.ResetAllData();
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

    public void ButtonSound()
    {
        GlobalMusic._Button.Play();
    }

    public void PageSound()
    {
        GlobalMusic._Page.Play();
    }

}
