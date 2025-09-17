using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitManager : MonoBehaviour
{
    //限制管理器，用于选择当前关卡塔的级数限制与技能使用限制

    [Header("塔最多升级到的级数")]
    public GlobalLimit.Level maxLevel = GlobalLimit.Level.Lv0;

    [Header("五中元素塔放置限制（勾选上锁）")]
    [Header("金")]
    public bool JinLock = false;
    [Header("木")]
    public bool MuLock = false;
    [Header("水")]
    public bool ShuiLock = false;
    [Header("火")]
    public bool HuoLock = false;
    [Header("土")]
    public bool TuLock = false;

    //[Header("技能放置限制")]

    private void Awake()
    {
        //全局限制级赋值
        GlobalLimit.level = GlobalLimit.convert(maxLevel);

        //全局元素塔限制赋值
        GlobalLimit.towerElement[GlobalLimit.Element.JIN] = JinLock;
        GlobalLimit.towerElement[GlobalLimit.Element.MU] = MuLock;
        GlobalLimit.towerElement[GlobalLimit.Element.SHUI] = ShuiLock;
        GlobalLimit.towerElement[GlobalLimit.Element.HUO] = HuoLock;
        GlobalLimit.towerElement[GlobalLimit.Element.TU] = TuLock;


    }

}
