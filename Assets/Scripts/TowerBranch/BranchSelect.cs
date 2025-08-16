using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchSelect : MonoBehaviour
{
    [Header("两个分支")]
    public GameObject branch_1;
    public GameObject branch_2;

    //与TowerSelect类似
    void PlaceTower(GameObject tower)
    {
        //初始化游戏对象
        GameObject newTower = Instantiate(tower, transform.parent.position, Quaternion.identity);
        Transform originalHole = transform.parent.gameObject.GetComponent<TowerInitial>().hole;
        newTower.GetComponent<TowerInitial>().SetHole(originalHole); //设置对应的坑

        //找到子物体，然后先禁用它
        Transform child = transform.Find("TwoOptionsCanva");
        child.gameObject.SetActive(false);
        //然后在禁用本体
        transform.parent.gameObject.SetActive(false);
        Destroy(transform.parent.gameObject); //销毁
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
