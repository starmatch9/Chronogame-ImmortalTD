using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalElementPowerFunction
{
    //ȫ��Ψһ��¼��������
    public static int count = 0;

    //ȫ��ֻ��һ��ElementPowerCount��������UI��ʾ��Ԫ��������
    public static ElementPowerCount countDisplay;

    //�������ۼ��������ֵ����ʽһһ��Ӧ
    public static Dictionary<GameObject, int> towerSale = new Dictionary<GameObject, int>();

    public static void AddCount(int n)
    {
        //��������
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

    //�������о�̬����
    public static void ResetAllData()
    {
        //ȫ��Ψһ��¼��������
        count = 0;

        //ȫ��ֻ��һ��ElementPowerCount��������UI��ʾ��Ԫ��������
        countDisplay = null;

        //�������ۼ��������ֵ����ʽһһ��Ӧ
        towerSale = new Dictionary<GameObject, int>();
    }
}
