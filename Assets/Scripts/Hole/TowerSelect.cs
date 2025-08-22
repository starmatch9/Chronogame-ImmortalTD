using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSelect : MonoBehaviour
{
    [Header("五大基础塔：金-木-水-火-土")]
    public GameObject towerJin;
    public GameObject towerMu;
    public GameObject towerShui;
    public GameObject towerHuo;
    public GameObject towerTu;

    private void Awake()
    {
        //调整物体位置
        Adjust();
    }

    //一个公用方法，在当前位置放置塔
    void PlaceTower(GameObject tower)
    {
        //找售价
        int price = GlobalElementPowerFunction.towerSale[tower];

        if (!GlobalElementPowerFunction.CanMinus(price))
        {
            //输出“元素力数量不够”的字样
            return;
        }
        GlobalElementPowerFunction.MinusCount(price);

        //初始化游戏对象
        GameObject newTower = Instantiate(tower, transform.position, Quaternion.identity);
        newTower.GetComponent<TowerInitial>().SetHole(transform); //设置对应的坑

        //找到子物体，然后先禁用它
        Transform child = transform.Find("FiveOptionsCanva");
        child.gameObject.SetActive(false);
        //然后在禁用坑
        gameObject.SetActive(false);
    }

    //如果选择金塔
    public void JinSelect()
    {
        PlaceTower(towerJin);
    }

    //如果选择木塔
    public void MuSelect()
    {
        PlaceTower(towerMu);
    }

    //如果选择水塔
    public void ShuiSelect()
    {
        PlaceTower(towerShui);
    }
    
    //如果选择火塔
    public void HuoSelect()
    {
        PlaceTower(towerHuo);
    }

    //如果选择土塔
    public void TuSelect()
    {
        PlaceTower(towerTu);
    }


    public void Adjust()
    {
        //所有点的公式
        float widthStart = -7.5f;
        float widthEnd = 7.5f;
        float heightStart = -3.5f;
        float heightEnd = 4.5f;
        //调整物体的位置
        for (float x = widthStart; x <= widthEnd; x += 1.0f)
        {
            for (float y = heightStart; y <= heightEnd; y += 1.0f)
            {
                if (Mathf.Abs(transform.position.x - x) <= 0.5f && Mathf.Abs(transform.position.y - y) <= 0.5f)
                {
                    transform.position = new Vector3(x, y, 0f);
                    break;
                }
            }
        }
    }
}
