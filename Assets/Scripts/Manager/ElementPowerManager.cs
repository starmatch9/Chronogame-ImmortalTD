using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ElementPowerManager : MonoBehaviour
{
    //后续需要用来和“波次管理器”配合管理元素力数量

    //这里需要配置每个塔放置需要的货币数量

    [Header("初始金额")]
    [Range(0, 99999)]public int initialElementPower = 0;

    [Header("五座基础塔的售价")]
    [Header("金")]
    [Range(0, 99999)] public int _JinTower = 0;
    [Header("木")]
    [Range(0, 99999)] public int _MuTower = 0;
    [Header("水")]
    [Range(0, 99999)] public int _ShuiTower = 0;
    [Header("火")]
    [Range(0, 99999)] public int _HuoTower = 0;
    [Header("土")]
    [Range(0, 99999)] public int _TuTower = 0;

    [Header("五座基础塔Lv1的售价")]
    [Header("金")]
    [Range(0, 99999)] public int _JinTower_Lv1 = 0;
    [Header("木")]
    [Range(0, 99999)] public int _MuTower_Lv1 = 0;
    [Header("水")]
    [Range(0, 99999)] public int _ShuiTower_Lv1 = 0;
    [Header("火")]
    [Range(0, 99999)] public int _HuoTower_Lv1 = 0;
    [Header("土")]
    [Range(0, 99999)] public int _TuTower_Lv1 = 0;

    [Header("五座基础塔Lv2的售价")]
    [Header("金")]
    [Range(0, 99999)] public int _JinTower_Lv2 = 0;
    [Header("木")]
    [Range(0, 99999)] public int _MuTower_Lv2 = 0;
    [Header("水")]
    [Range(0, 99999)] public int _ShuiTower_Lv2 = 0;
    [Header("火")]
    [Range(0, 99999)] public int _HuoTower_Lv2 = 0;
    [Header("土")]
    [Range(0, 99999)] public int _TuTower_Lv2 = 0;

    [Header("十座二级塔的售价")]
    [Header("钻")]
    [Range(0, 99999)] public int _ZuanTower = 0;
    [Header("炉")]
    [Range(0, 99999)] public int _LuTower = 0;
    [Header("棘")]
    [Range(0, 99999)] public int _JiTower = 0;
    [Header("树")]
    [Range(0, 99999)] public int _ShuTower = 0;
    [Header("浪")]
    [Range(0, 99999)] public int _LangTower = 0;
    [Header("冰")]
    [Range(0, 99999)] public int _BingTower = 0;
    [Header("炮")]
    [Range(0, 99999)] public int _PaoTower = 0;
    [Header("熔")]
    [Range(0, 99999)] public int _RongTower = 0;
    [Header("墙")]
    [Range(0, 99999)] public int _QiangTower = 0;
    [Header("沼")]
    [Range(0, 99999)] public int _ZhaoTower = 0;

    //售价
    Dictionary<GameObject, int> prices = new Dictionary<GameObject, int>();

    void Start()
    {
        GlobalElementPowerFunction.AddCount(initialElementPower);
        SetPrice();
        GlobalElementPowerFunction.towerSale = prices;
    }

    //设置所有塔的价格
    void SetPrice()
    {
        foreach (GameObject towerPref in GlobalData.globalTowerPrefabs)
        {
            if (towerPref.name.Contains("Jin") && !towerPref.name.Contains("Lv"))
            {
                prices[towerPref] = _JinTower;
            }
            if (towerPref.name.Contains("Mu") && !towerPref.name.Contains("Lv"))
            {
                prices[towerPref] = _MuTower;
            }
            if (towerPref.name.Contains("Shui") && !towerPref.name.Contains("Lv"))
            {
                prices[towerPref] = _ShuiTower;
            }
            if (towerPref.name.Contains("Huo") && !towerPref.name.Contains("Lv"))
            {
                prices[towerPref] = _HuoTower;
            }
            if (towerPref.name.Contains("Tu") && !towerPref.name.Contains("Lv"))
            {
                prices[towerPref] = _TuTower;
            }
            if (towerPref.name.Contains("Jin") && towerPref.name.Contains("Lv1"))
            {
                prices[towerPref] = _JinTower_Lv1;
            }
            if (towerPref.name.Contains("Mu") && towerPref.name.Contains("Lv1"))
            {
                prices[towerPref] = _MuTower_Lv1;
            }
            if (towerPref.name.Contains("Shui") && towerPref.name.Contains("Lv1"))
            {
                prices[towerPref] = _ShuiTower_Lv1;
            }
            if (towerPref.name.Contains("Huo") && towerPref.name.Contains("Lv1"))
            {
                prices[towerPref] = _HuoTower_Lv1;
            }
            if (towerPref.name.Contains("Tu") && towerPref.name.Contains("Lv1"))
            {
                prices[towerPref] = _TuTower_Lv1;
            }
            if (towerPref.name.Contains("Jin") && towerPref.name.Contains("Lv2"))
            {
                prices[towerPref] = _JinTower_Lv2;
            }
            if (towerPref.name.Contains("Mu") && towerPref.name.Contains("Lv2"))
            {
                prices[towerPref] = _MuTower_Lv2;
            }
            if (towerPref.name.Contains("Shui") && towerPref.name.Contains("Lv2"))
            {
                prices[towerPref] = _ShuiTower_Lv2;
            }
            if (towerPref.name.Contains("Huo") && towerPref.name.Contains("Lv2"))
            {
                prices[towerPref] = _HuoTower_Lv2;
            }
            if (towerPref.name.Contains("Tu") && towerPref.name.Contains("Lv2"))
            {
                prices[towerPref] = _TuTower_Lv2;
            }
            if (towerPref.name.Contains("Zuan"))
            {
                prices[towerPref] = _ZuanTower;
            }
            if (towerPref.name.Contains("Lu"))
            {
                prices[towerPref] = _LuTower;
            }
            if (towerPref.name.Contains("Ji"))
            {
                prices[towerPref] = _JiTower;
            }
            if (towerPref.name.Contains("Shu"))
            {
                prices[towerPref] = _ShuTower;
            }
            if (towerPref.name.Contains("Lang"))
            {
                prices[towerPref] = _LangTower;
            }
            if (towerPref.name.Contains("Bing"))
            {
                prices[towerPref] = _BingTower;
            }
            if (towerPref.name.Contains("Pao"))
            {
                prices[towerPref] = _PaoTower;
            }
            if (towerPref.name.Contains("Rong"))
            {
                prices[towerPref] = _RongTower;
            }
            if (towerPref.name.Contains("Qiang"))
            {
                prices[towerPref] = _QiangTower;
            }
            if (towerPref.name.Contains("Zhao"))
            {
                prices[towerPref] = _ZhaoTower;
            }
        }
    }
}
