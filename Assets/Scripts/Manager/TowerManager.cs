using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    [Header("Tower预制件全列表")]
    [Header("金")]
    public GameObject JinTowerPref;
    [Header("木")]
    public GameObject MuTowerPref;
    [Header("水")]
    public GameObject ShuiTowerPref;
    [Header("火")]
    public GameObject HuoTowerPref;
    [Header("土")]
    public GameObject TuTowerPref;
    [Header("钻")]
    public GameObject ZuanTowerPref;
    [Header("炉")]
    public GameObject LuTowerPref;
    [Header("棘")]
    public GameObject JiTowerPref;
    [Header("树")]
    public GameObject ShuTowerPref;
    [Header("浪")]
    public GameObject LangTowerPref;
    [Header("冰")]
    public GameObject BingTowerPref;
    [Header("炮")]
    public GameObject PaoTowerPref;
    [Header("熔")]
    public GameObject RongTowerPref;
    [Header("墙")]
    public GameObject QiangTowerPref;
    [Header("沼")]
    public GameObject ZhaoTowerPref;

    List<GameObject> towers = new List<GameObject>();

    //这种全局都要用的东西，初始化的越早越好
    // Start is called before the first frame update
    void Awake()
    {
        //将所有预制件添加到列表
        towers.Add(JinTowerPref);
        towers.Add(MuTowerPref);
        towers.Add (ShuiTowerPref);
        towers.Add(HuoTowerPref);
        towers.Add(TuTowerPref);
        towers.Add(ZuanTowerPref);
        towers.Add(LuTowerPref);
        towers.Add(JiTowerPref);
        towers.Add(ShuTowerPref);
        towers.Add(LangTowerPref);
        towers.Add(BingTowerPref);
        towers.Add(PaoTowerPref);
        towers.Add(RongTowerPref);
        towers.Add(QiangTowerPref);
        towers.Add(ZhaoTowerPref);

        //存入全局当中
        GlobalData.globalTowerPrefabs = towers;
    }
}
