using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShuTower : Tower
{
    //  树塔

    //暂定单纯生成元素力

    //树塔每次生成元素力的数量
    [Header("每次生成元素力数量")]
    [Range(1, 1000)]public int elementAmount = 100;

    [Header("元素力图标游戏对象")]
    public GameObject elementPower = null;

    //Start函数中获取元素力管理器（元素力数据交由全局管理）

    public override void TowerAction()
    {
        //元素力增加的协程动画
        StartCoroutine(AddElementPower());

        //生成元素力
        GlobalElementPowerFunction.AddCount(elementAmount);
    }

    public IEnumerator AddElementPower()
    {
        SpriteRenderer renderer = elementPower.GetComponent<SpriteRenderer>();
        Vector3 originalposition = elementPower.transform.position;

        //设置动画时间为0.3秒
        float duration = 1f;

        float timer = 0f;

        elementPower.SetActive(true);
        while (timer < duration) {

            if (elementPower != null)
            {
                //颜色改变
                float alpha = Mathf.Lerp(1f, 0f, timer / duration);
                Color color = renderer.color;
                color.a = alpha;
                renderer.color = color;

                //向上移动
                float distance = Mathf.Lerp(0f, 0.5f, timer / duration);
                float newY = originalposition.y + distance;
                elementPower.transform.position = new Vector3(originalposition.x, newY, originalposition.z);
            }
            timer += Time.deltaTime;
            yield return null;
        }
        elementPower.SetActive(false);
        elementPower.transform.position = originalposition;
    }
}
