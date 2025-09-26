using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class GlobalElementPowerFunction
{
    public static MonoBehaviour mono;

    //全局唯一记录货币数量
    public static int count = 0;

    //全局只有一个ElementPowerCount用来更新UI显示的元素力数量
    public static ElementPowerCount countDisplay;

    //这里让售价与塔以字典的形式一一对应
    public static Dictionary<GameObject, int> towerSale = new Dictionary<GameObject, int>();

    //动画用元素图标
    public static GameObject elementPower;

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

    public static IEnumerator AddElementPowerAnim()
    {
        GlobalMusic.PlayOnce(GlobalMusic._Money);

        Image renderer = elementPower.GetComponent<Image>();
        RectTransform rectTransform = elementPower.GetComponent<RectTransform>();
        Vector3 originalPosition = rectTransform.position;

        //设置动画时间为0.6秒
        //设置动画时间
        float duration = 1f;
        float timer = 0f;

        elementPower.SetActive(true);

        //确保初始状态
        if (renderer != null)
        {
            Color color = renderer.color;
            color.a = 1f;
            renderer.color = color;
        }

        while (timer < duration)
        {
            if (elementPower != null && renderer != null)
            {
                //颜色
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                //向上移动
                float distance = Mathf.Lerp(0f, 50f, timer / duration);
                Vector3 newPosition = originalPosition + Vector3.up * distance;
                rectTransform.position = newPosition;
            }

            timer += Time.deltaTime;
            yield return null;
        }

        //重置状态
        if (elementPower != null)
        {
            elementPower.SetActive(false);
            rectTransform.position = originalPosition;

            //重置透明度
            if (renderer != null)
            {
                Color color = renderer.color;
                color.a = 1f;
                renderer.color = color;
            }
        }

    }
}
