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

    [Header("提示弹窗游戏对象")]
    public GameObject tipWindow = null;

    [Header("介绍窗口游戏对象")]
    public GameObject introWindow = null;

    [Header("价格窗口游戏对象")]
    public GameObject priceWindow = null;

    [Header("Page的介绍列表")]
    public List<GameObject> list = new List<GameObject>();

    void Awake()
    {
        GlobalData.mono = this;

        GlobalData.maxNumber = maxNumber;

        //关卡结束相关
        GlobalData.sg = sg;
        GlobalData.nextName = nextName;
        GlobalData.passWindow = passWindow;
        GlobalData.noPassWindow = noPassWindow;
        GlobalData.tipWindow = tipWindow;
        GlobalData.priceWindow = priceWindow;
    }

    private void Start()
    {   
        string name = SceneManager.GetActiveScene().name;
        //Set+Save保存数据
        PlayerPrefs.SetInt(name, 1);
        PlayerPrefs.Save();

        if (list.Count == 0)
        {
            introWindow.SetActive(false);
        }
        else
        {
            pageIndex = 0;
            introWindow.SetActive(true);
            NextPage();
        }
    }

    int pageIndex = 0;
    public void NextPage()
    {
        GlobalMusic._Page.Play();

        if(pageIndex >= list.Count)
        {
            //整个禁用（因为是父物体所以就确保都禁了）
            introWindow.SetActive(false);
            return;
        }

        if(pageIndex - 1 >= 0)
        {
            list[pageIndex - 1].SetActive(false);
        }

        list[pageIndex].SetActive(true);

        pageIndex++;

    }
}
