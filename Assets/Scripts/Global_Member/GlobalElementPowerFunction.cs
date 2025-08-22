using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalElementPowerFunction
{
    //全局唯一记录货币数量
    public static int count = 0;

    //全局只有一个ElementPowerCount用来更新UI显示的元素力数量
    public static ElementPowerCount countDisplay;

    //这里让售价与塔以字典的形式一一对应
    public static Dictionary<GameObject, int> towerSale = new Dictionary<GameObject, int>();

    public static void AddCount(int n)
    {
        //设置上限
        if (count + n > 99999999)
        {
            count = 99999999;
        }
        else
        {
            count += n;
        }
        countDisplay.UpdateDisplay(count);
    }
    public static void MinusCount(int n)
    {
        if (count - n < 0)
        {
            return;
        } else
        {
            count -= n;
        }
        countDisplay.UpdateDisplay(count);
    }

    public static bool CanMinus(int n)
    {
        if (count - n < 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    //重置所有静态变量
    public static void ResetAllData()
    {
        //全局唯一记录货币数量
        count = 0;

        //全局只有一个ElementPowerCount用来更新UI显示的元素力数量
        countDisplay = null;

        //这里让售价与塔以字典的形式一一对应
        towerSale = new Dictionary<GameObject, int>();
    }
}
