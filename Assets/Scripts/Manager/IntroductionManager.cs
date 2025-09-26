using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroductionManager : MonoBehaviour
{

    [Header("五座基础塔的介绍")]
    [Header("金")]
    public string _JinTower = "";
    [Header("木")]
    public string _MuTower = "";
    [Header("水")]
    public string _ShuiTower = "";
    [Header("火")]
    public string _HuoTower = "";
    [Header("土")]
    public string _TuTower = "";
    [Header("五座基础塔Lv1的介绍")]
    [Header("金")]
    public string _JinTower_Lv1 = "";
    [Header("木")]
    public string _MuTower_Lv1 = "";
    [Header("水")]
    public string _ShuiTower_Lv1 = "";
    [Header("火")]
    public string _HuoTower_Lv1 = "";
    [Header("土")]
    public string _TuTower_Lv1 = "";
    [Header("五座基础塔Lv2的介绍")]
    [Header("金")]
    public string _JinTower_Lv2 = "";
    [Header("木")]
    public string _MuTower_Lv2 = "";
    [Header("水")]
    public string _ShuiTower_Lv2 = "";
    [Header("火")]
    public string _HuoTower_Lv2 = "";
    [Header("土")]
    public string _TuTower_Lv2 = "";
    [Header("十座二级塔的售价")]
    [Header("钻")]
    public string _ZuanTower = "";
    [Header("炉")]
    public string _LuTower = "";
    [Header("棘")]
    public string _JiTower = "";
    [Header("树")]
    public string _ShuTower = "";
    [Header("浪")]
    public string _LangTower = "";
    [Header("冰")]
    public string _BingTower = "";
    [Header("炮")]
    public string _PaoTower = "";
    [Header("熔")]
    public string _RongTower = "";
    [Header("墙")]
    public string _QiangTower = "";
    [Header("沼")]
    public string _ZhaoTower = "";

    //售价
    Dictionary<GameObject, string> intros = new Dictionary<GameObject, string>();

    void Start()
    {
        SetIntro();
        GlobalIntroduction.towerIntroduction = intros;
    }

    //设置所有塔的介绍
    void SetIntro()
    {
        foreach (GameObject towerPref in GlobalData.globalTowerPrefabs)
        {
            if (towerPref.name.Contains("Jin") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _JinTower;
            }
            if (towerPref.name.Contains("Mu") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _MuTower;
            }
            if (towerPref.name.Contains("Shui") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _ShuiTower;
            }
            if (towerPref.name.Contains("Huo") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _HuoTower;
            }
            if (towerPref.name.Contains("Tu") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _TuTower;
            }
            if (towerPref.name.Contains("Jin") && towerPref.name.Contains("Lv1"))
            {
                intros[towerPref] = _JinTower_Lv1;
            }
            if (towerPref.name.Contains("Mu") && towerPref.name.Contains("Lv1"))
            {
                intros[towerPref] = _MuTower_Lv1;
            }
            if (towerPref.name.Contains("Shui") && towerPref.name.Contains("Lv1"))
            {
                intros[towerPref] = _ShuiTower_Lv1;
            }
            if (towerPref.name.Contains("Huo") && towerPref.name.Contains("Lv1"))
            {
                intros[towerPref] = _HuoTower_Lv1;
            }
            if (towerPref.name.Contains("Tu") && towerPref.name.Contains("Lv1"))
            {
                intros[towerPref] = _TuTower_Lv1;
            }
            if (towerPref.name.Contains("Jin") && towerPref.name.Contains("Lv2"))
            {
                intros[towerPref] = _JinTower_Lv2;
            }
            if (towerPref.name.Contains("Mu") && towerPref.name.Contains("Lv2"))
            {
                intros[towerPref] = _MuTower_Lv2;
            }
            if (towerPref.name.Contains("Shui") && towerPref.name.Contains("Lv2"))
            {
                intros[towerPref] = _ShuiTower_Lv2;
            }
            if (towerPref.name.Contains("Huo") && towerPref.name.Contains("Lv2"))
            {
                intros[towerPref] = _HuoTower_Lv2;
            }
            if (towerPref.name.Contains("Tu") && towerPref.name.Contains("Lv2"))
            {
                intros[towerPref] = _TuTower_Lv2;
            }
            if (towerPref.name.Contains("Zuan") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _ZuanTower;
            }
            if (towerPref.name.Contains("Lu") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _LuTower;
            }
            if (towerPref.name.Contains("Ji") && !towerPref.name.Contains("Lv") && !towerPref.name.Contains("n"))
            {
                intros[towerPref] = _JiTower;
            }
            if (towerPref.name.Contains("Shu") && !towerPref.name.Contains("Lv") && !towerPref.name.Contains("i"))
            {
                intros[towerPref] = _ShuTower;
            }
            if (towerPref.name.Contains("Lang") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _LangTower;
            }
            if (towerPref.name.Contains("Bing") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _BingTower;
            }
            if (towerPref.name.Contains("Pao") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _PaoTower;
            }
            if (towerPref.name.Contains("Rong") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _RongTower;
            }
            if (towerPref.name.Contains("Qiang") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _QiangTower;
            }
            if (towerPref.name.Contains("Zhao") && !towerPref.name.Contains("Lv"))
            {
                intros[towerPref] = _ZhaoTower;
            }
        }
    }




}
