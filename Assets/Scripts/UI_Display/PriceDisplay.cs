using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//终于tm找见反复调用的问题了————价格窗口选择了可遮盖（即遮挡射线检测）
//破防了

//记住两个用于UI的事件接受接口
public class PriceDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject tower = null;

    //显示售价与不显示
    public void DisplayPrice()
    {
        GlobalData.priceWindow.GetComponent<FollowMouse>().StartFollow(GlobalElementPowerFunction.towerSale[tower]);

    }
    public void NoDisplayPrice()
    {
        GlobalData.priceWindow.GetComponent<FollowMouse>().StopFollow();
    }

    // 鼠标进入时调用
    public void OnPointerEnter(PointerEventData eventData)
    {
        DisplayPrice();
    }

    // 鼠标离开时调用
    public void OnPointerExit(PointerEventData eventData)
    {
        NoDisplayPrice();
    }
}
