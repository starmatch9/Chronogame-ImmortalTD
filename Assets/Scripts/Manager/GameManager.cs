using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("游戏失败的条件：到达终点的最大敌人数量")]
    [Range(1,10)]public int maxNumber = 5;

    [Header("停止游戏脚本")]
    public StopGame sg = null;

    [Header("下一关场景名称")]
     public string nextName = "";

    [Header("成功弹窗游戏对象")]
    public GameObject passWindow = null;

    [Header("失败弹窗游戏对象")]
    public GameObject noPassWindow = null;

    void Awake()
    {
        GlobalData.maxNumber = maxNumber;

        //关卡结束相关
        GlobalData.sg = sg;
        GlobalData.nextName = nextName;
        GlobalData.passWindow = passWindow;
        GlobalData.noPassWindow = noPassWindow;
    }

    private void Start()
    {   
        string name = SceneManager.GetActiveScene().name;
        //Set+Save保存数据
        PlayerPrefs.SetInt(name, 1);
        PlayerPrefs.Save();


    }
}
