using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalLimit
{
    //一共有四个等级
    public enum Level
    {
        Lv0, Lv1, Lv2, Lv3
    }

    public enum Element
    {
        JIN, MU, SHUI, HUO, TU
    }

    public static int convert(Level l)
    {
        if (l == Level.Lv0) return 0;
        if (l == Level.Lv1) return 1;
        if (l == Level.Lv2) return 2;
        if (l == Level.Lv3) return 3;

        return 0;
    }

    /*当前限制级*/
    public static int level = 0;

    /*当前元素放置限制*/
    public static Dictionary<Element, bool> towerElement = new Dictionary<Element, bool>
    {
        //true代表已经上锁
        [Element.JIN] = false,
        [Element.MU] = false,
        [Element.SHUI] = false,
        [Element.HUO] = false,
        [Element.TU] = false
    };

    /*当前技能放置限制*/


}
