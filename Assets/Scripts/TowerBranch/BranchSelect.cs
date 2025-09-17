using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BranchSelect : MonoBehaviour
{
    [Header("两个分支")]
    public GameObject branch_1;
    public GameObject branch_2;

    //与TowerSelect类似
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
        GameObject newTower = Instantiate(tower, transform.parent.position, Quaternion.identity);
        Transform originalHole = transform.parent.gameObject.GetComponent<Tower>().hole;

        //继承连接关系
        GlobalLink.Inherit(newTower.GetComponent<Tower>(), transform.parent.gameObject.GetComponent<Tower>());

        // --------------------这个后面记得改成Tower，二级塔的基类--------------------
        newTower.GetComponent<Tower>().SetHole(originalHole); //设置对应的坑

        GlobalData.towers.Add(newTower.GetComponent<Tower>());

        //找到子物体，然后先禁用它
        Transform child = transform.Find("TwoOptionsCanva");
        child.gameObject.SetActive(false);
        //销毁前重置绘制
        MouseClickTower mouseClickTower = GetComponent<MouseClickTower>();
        if (mouseClickTower.tower != null)
        {
            mouseClickTower.tower.EraseAttackArea();
        }
        //然后在禁用本体
        if (GlobalData.towers.Contains(transform.parent.gameObject.GetComponent<Tower>()))
        {
            GlobalData.towers.Remove(transform.parent.gameObject.GetComponent<Tower>());
        }
        transform.parent.gameObject.SetActive(false);

        //GlobalData.towers.Remove(transform.parent.gameObject.GetComponent<Tower>());
        //销毁
        transform.parent.gameObject.GetComponent<Tower>().TowerDie();
        //Destroy(transform.parent.gameObject); //销毁
    }

    //选择分支1
    public void Branch1Select()
    {
        PlaceTower(branch_1);
    }

    //选择分支2
    public void Branch2Select()
    {
        PlaceTower(branch_2);
    }
}
